using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using TestingGround.Core.Domain.Fitness.Models;
using TestingGround.Core.Domain.Internal.Bases;
using TestingGround.Core.Domain.Security;
using TestingGround.Core.Infrastructure.Attributes;
using TestingGround.Core.Infrastructure.PropertyIntercepting;

namespace TestingGround.Default.Database
{
    [UsedImplicitly]
    public class TestingContext : DbContext
    {
        private static readonly IDictionary<Type, IEnumerable<PropertyInfo>> CollectionPropertiesPerType = new Dictionary<Type, IEnumerable<PropertyInfo>>();

        public TestingContext()
            : this("Name=TestingConnection")
        {

        }

        public TestingContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            RegisterCollectionPropertyFilter();
        }

        private void RegisterCollectionPropertyFilter()
        {
            (this as IObjectContextAdapter).ObjectContext.ObjectMaterialized +=
                delegate(Object sender, ObjectMaterializedEventArgs e)
                {
                    if (e.Entity is Entity)
                    {
                        var entityType = e.Entity.GetType();
                        IEnumerable<PropertyInfo> collectionProperties;
                        if (!CollectionPropertiesPerType.TryGetValue(entityType, out collectionProperties))
                        {
                            CollectionPropertiesPerType[entityType] = (collectionProperties = entityType.GetProperties()
                               .Where(p => p.PropertyType.IsGenericType && typeof(ICollection<>) == p.PropertyType.GetGenericTypeDefinition()));
                        }
                        foreach (var collectionProperty in collectionProperties)
                        {
                            var collectionType = typeof(FilteredCollection<>).MakeGenericType(collectionProperty.PropertyType.GetGenericArguments());
                            DbCollectionEntry dbCollectionEntry = Entry(e.Entity).Collection(collectionProperty.Name);
                            dbCollectionEntry.CurrentValue = Activator.CreateInstance(collectionType, new[] { dbCollectionEntry.CurrentValue, dbCollectionEntry });
                        }
                    }
                };
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GymMember>().Map(mapping => mapping.MapInheritedProperties());
            modelBuilder.Entity<GymMember>().Property(g => g.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Workout>().Map(mapping => mapping.MapInheritedProperties());
            modelBuilder.Entity<Workout>().Property(w => w.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<WorkoutExercise>().Map(mapping => mapping.MapInheritedProperties());
            modelBuilder.Entity<WorkoutExercise>().Property(w => w.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Exercise>().Map(mapping => mapping.MapInheritedProperties());
            modelBuilder.Entity<Exercise>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserProfile>().Map(mapping => mapping.MapInheritedProperties());
            modelBuilder.Entity<UserProfile>().Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.Deleted = true;
                    entry.State = EntityState.Modified;
                }
            }
            return base.SaveChanges();
        }

        public DbSet<GymMember> GymMembers { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}