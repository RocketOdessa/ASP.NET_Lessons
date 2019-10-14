using Product.Domain.Abstract;
using System.Linq;
using System.Web.Mvc;

namespace StockApp_Lesson_Four.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        // GET: Product
        public ProductController(IProductRepository repoParam)
        {
            repository = repoParam;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public JsonResult JsonInfo()
        {
            return Json(repository.Products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderByPrice(string sortOrder)
        {
            ViewBag.UANSortParam = sortOrder == "PriceUAN" ? "UAN_desc" : "PriceUAN";
            ViewBag.USDSortParam = sortOrder == "PriceUSD" ? "USD_desc" : "PriceUSD";

            var products = from prod in repository.Products select prod;

            switch (sortOrder)
            {

                case "UAN_desc":
                    products = products.OrderByDescending(p => p.PriceUAN);
                    break;
                case "USD_desc":
                    products = products.OrderByDescending(p => p.PriceUSD);
                    break;
                default:
                    products = products.OrderBy(p => p.PriceUAN);
                    break;
            }

            return View(products.ToList());
        }
    }
}