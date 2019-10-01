using Controllers_And_Models.Models;
using External_Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Controllers_And_Models.Controllers
{
    public class ProductController : Controller
    {
        static List<Product> productList = new List<Product>()
        {
            new Product
            {
                ID = 1,
                PName = "Banan",
                Description = "So sweet fruit and fresh",
                Cost = 5.758M
            },
            new Product
            {
                ID = 2,
                PName = "Tomato",
                Description = "is an herbaceous annual in the family Solanaceae grown for its edible fruit",
                Cost = 7.660M
            },
            new Product
            {
                ID = 3,
                PName = "Cucumber",
                Description = "There are three main varieties of cucumber: slicing, pickling, and seedless.",
                Cost = 6.459M
            },
            new Product
            {
                ID = 4,
                PName = "Bike",
                Description = "Is a small, human powered land vehicle with a seat, two wheels, two pedals, " +
                "and a metal chain connected to cogs on the pedals and rear wheel.",
                Cost = 75.758M
            },
            new Product
            {
                ID = 5,
                PName = "C# in a Nutshell",
                Description = "Since its debut in 2000, C# has become a language of unusual flexibility and breadth, but its continual growth means there's always more to learn. " +
                "Organized around concepts and use cases, this updated edition provides intermediate and advanced programmers with a concise map of C# and .NET knowledge.",
                Cost = 75.990M
            },
            new Product
            {
                ID = 6,
                PName = "Beer",
                Description = "Just perfect for this price",
                Cost = 9.990M
            }
        };
        public int PageSize = 3;
        // GET: Product
        public ViewResult List(int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = productList.OrderBy(p => p.ID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = productList.Count()
                }
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product p = productList.Find(i => i.ID == id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product m = productList.Find(i => i.ID == id);
            if (m == null)
            {
                return HttpNotFound();
            }
            productList.Remove(m);
            return RedirectToAction("List");
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product products)
        {
            productList.Add(new Product()
            {
                ID = products.ID,
                PName = products.PName,
                Description = products.Description,
                Cost = products.Cost
            });

            return RedirectToAction("List");
        }

        public JsonResult Info()
        {
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
    }
}