using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using TestingGround.Core.Domain.Fitness.Models;
using TestingGround.Default.Database;

namespace TestingGround.Tests.Data
{
    public class TestingInitializer : DropCreateDatabaseAlways<TestingContext>
    {
        protected override void Seed(TestingContext context)
        {
            Log.Write("Initializing test data");
            var pushups = new Exercise { Name = "Pushups" };
            var crunches = new Exercise { Name = "Crunches" };
            var situps = new Exercise { Name = "Situps" };
            var alex = new GymMember
            {
                Name = "Alex",
                Workouts = new List<Workout>
                {
                    new Workout
                    {
                        Date = DateTime.Today.AddDays(-7*5),
                        WorkoutExercises = new List<WorkoutExercise>
                        {
                            new WorkoutExercise(pushups,500,50),
                            new WorkoutExercise(situps, 300, 75),
                            new WorkoutExercise(crunches, 120, 15)
                        }
                    },
                    new Workout
                    {
                        Date = DateTime.Today.AddDays(-7*4),
                        WorkoutExercises = new List<WorkoutExercise>
                        {
                            new WorkoutExercise(pushups,500,50) { Deleted = true },
                            new WorkoutExercise(situps, 300, 75) { Deleted = true },
                            new WorkoutExercise(crunches, 120, 15) { Deleted = true }
                        }
                    },
                    new Workout { Deleted = true, Date = DateTime.Today.AddDays(-7*3) }
                }
            };

            var oldGymMember = new GymMember
            {
                Name = "Old gym member",
                Deleted = true
            };
            Log.Write("Adding gym members to context");
            context.GymMembers.Add(alex);
            context.GymMembers.Add(oldGymMember);
            Log.Write("Calling context.SaveChanges()");
            context.SaveChanges();
            Log.Write("Test data succesfully written to database");
        }
    }
}
