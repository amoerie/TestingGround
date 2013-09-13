using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using TestingGround.Core.Domain.Fitness.Repositories;
using TestingGround.Default.Database;
using TestingGround.Default.Persistence.Fitness.Repositories;

namespace TestingGround.Web
{
    public static class AutoFacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<TestingContext>().As<TestingContext>().As<DbContext>().InstancePerHttpRequest();
            builder.RegisterType<GymMemberRepository>().As<IGymMemberRepository>().InstancePerHttpRequest();
            var container = builder.Build();
            container.ActivateGlimpse();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            
        }
    }
}