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
                name: "Sort",
                url: "{controller}/{action}/{sortOrder}",
                new { controller = "Product", action = "OrderByPrice", sortOrder = UrlParameter.Optional });

            routes.MapRoute(
    name: "Search",
    url: "{searchString}",
    new { searchString = UrlParameter.Optional });

            routes.MapRoute(
            name: "SearchWithPage",
            url: "{searchString}/{page}",
            new { searchString = (string)null },
            new { page = @"\+d" });

            routes.MapRoute(
                  name: "EditAction",
                  url: "{controller}/{action}/{id}",
                  new { controller = "Admin", action = "Edit", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "MainPage", id = UrlParameter.Optional }
                );
        }
    }
}
