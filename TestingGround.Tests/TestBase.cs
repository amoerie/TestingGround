using System;
using System.Data.Entity;
using Autofac;
using NUnit.Framework;
using TestingGround.Core.Domain.Fitness.Repositories;
using TestingGround.Default.Database;
using TestingGround.Default.Persistence.Fitness.Repositories;
using TestingInitializer = TestingGround.Tests.Data.TestingInitializer;

namespace TestingGround.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        protected IGymMemberRepository GymMembers { get; private set; }
        protected TestingContext Context { get; private set; }
        private IContainer _container;
        private IDisposable _profilingStep; 

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Log.Write("Setting database initializer");
            Database.SetInitializer(new TestingInitializer());
            Log.Write("Initializing AutoFac ContainerBuilder");
            var builder = new ContainerBuilder();
            Log.Write("Registering AutoFac Services");
            builder.RegisterType<TestingContext>().As<TestingContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<GymMemberRepository>().As<IGymMemberRepository>().InstancePerLifetimeScope();
            _container = builder.Build();
            Log.Write("AutoFac Container constructed succesfully");
            _container.BeginLifetimeScope();
            Log.Write("Preparing repository");
            GymMembers = _container.Resolve<IGymMemberRepository>();
            Context = _container.Resolve<TestingContext>();
            Log.Write("Started profiler");
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            Log.Write("Disposing AutoFac Container");
            _container.Dispose();
            Log.Write("AutoFac Container Disposed");
        }
    }
}
