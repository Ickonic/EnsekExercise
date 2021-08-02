using Autofac;
using Autofac.Integration.WebApi;
using EnsekDAL;
using EnsekService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace EnsekAPI
{
    public class IocConfig
    {
        public static void Config()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Live Data
            builder.RegisterType<SqlCustomerData>().As<ICustomerData>();
            builder.RegisterType<SqlMeterReadingData>().As<IMeterReadingData>();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}