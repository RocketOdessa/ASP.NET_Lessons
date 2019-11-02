using System.Web.Mvc;
using System.Web.Routing;

namespace StockApp_Lesson_Four
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
                  name: "EditAction",
                  url: "{controller}/{action}/{id}",
                  new { controller = "Admin", action = "Edit", id = UrlParameter.Optional }
            );
        }
    }
}
