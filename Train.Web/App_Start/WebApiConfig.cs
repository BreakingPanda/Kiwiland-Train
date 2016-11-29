using System.Web.Http;

namespace Train.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // only JSON formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Kiwiland",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
