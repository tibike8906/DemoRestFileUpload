using System.Web.Http;

namespace DemoRestFileUpload
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{fileName}",
                defaults: new { fileName = RouteParameter.Optional }
            );
            
        }
    }
}
