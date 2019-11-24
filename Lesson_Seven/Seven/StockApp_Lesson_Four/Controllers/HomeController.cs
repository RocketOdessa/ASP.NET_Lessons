using System.Web.Mvc;

namespace StockApp_Lesson_Four.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult MainPage()
        {
            return View();
        }
    }
}