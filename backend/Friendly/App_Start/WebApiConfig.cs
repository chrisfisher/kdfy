using System;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Friendly.Services;
using Friendly.Context;
using Friendly.Infrastructure;

namespace Friendly
{
    public static class WebApiConfig
    {
        private const string RedisConnectionString = "kdfy.redis.cache.windows.net:6380,password=8WazEjTx3/Nq/qfMIs0IS2bUjWqrvqyQd4Xu5qBKSeo=,ssl=True,abortConnect=False";

        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "FriendlyApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var xmlFormatter = config.Formatters.XmlFormatter;
            config.Formatters.Remove(xmlFormatter);
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            RegisterTypes(builder);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<LocationService>().As<ILocationService>();
            builder.RegisterType<LocationReviewService>().As<ILocationReviewService>();
            builder.RegisterType<LocationBoundsService>().As<ILocationBoundsService>();
            builder.RegisterType<FriendlyContext>().AsSelf().InstancePerRequest();
            builder.Register(c => new Cache(RedisConnectionString, new TimeSpan(1, 0, 0))).As<ICache>();
        }
    }
}
