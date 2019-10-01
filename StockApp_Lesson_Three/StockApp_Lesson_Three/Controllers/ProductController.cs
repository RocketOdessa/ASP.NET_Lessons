using Stock.Domain;
using Stock.Domain.Concrete;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace StockApp_Lesson_Three.Controllers
{
    public class ProductController : Controller
    {
        EFProductContext context = new EFProductContext();

        // GET: Product
        public ActionResult Index()
        {
            return View(context.Products);
        }

        public JsonResult JsonInfo()
        {
            return Json(context.Products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderByPrice(string sortOrder)
        {
            ViewBag.UANSortParam = sortOrder == "PriceUAN" ? "UAN_desc" : "PriceUAN";
            ViewBag.USDSortParam = sortOrder == "PriceUSD" ? "USD_desc" : "PriceUSD";

            var products = from prod in context.Products select prod;

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
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (product.ID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ID);
                if (dbEntry != null)
                {
                    dbEntry.Description = product.Description;
                    dbEntry.Guarantee = product.Guarantee;
                    dbEntry.PartNumber = product.PartNumber;
                    dbEntry.PriceUAN = product.PriceUAN;
                    dbEntry.PriceUSD = product.PriceUSD;
                    dbEntry.ProductCode = product.ProductCode;
                    dbEntry.ProductName = product.ProductName;
                    dbEntry.WholesalePrice = product.WholesalePrice;
                }
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product dbEntry = context.Products.FirstOrDefault(prodID => prodID.ID == id);

            if (dbEntry == null)
            {
                return HttpNotFound();
            }
            return View(dbEntry);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product dbEntry = context.Products.FirstOrDefault(prodID => prodID.ID == id);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
            }
            else
            {
                HttpNotFound();
            }
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}