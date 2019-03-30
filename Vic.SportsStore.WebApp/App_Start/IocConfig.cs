using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace Vic.SportsStore.WebApp
{
    public class IocConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterControllers(AppDomain.CurrentDomain.GetAssemblies())
                .PropertiesAutowired();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}