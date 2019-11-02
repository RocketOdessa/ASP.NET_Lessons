using Product.Domain.Abstract;
using StockApp_Lesson_Four.Models;
using System.Linq;
using System.Web.Mvc;

namespace StockApp_Lesson_Four.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int pageSize = 3;
        // GET: Product
        public ProductController(IProductRepository repoParam)
        {
            repository = repoParam;
        }

        public ViewResult Index(int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products.OrderBy(p => p.ID).Skip((page - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Products.Count()
                }
            };
            return View(model);
        }

        [Route("Json", Name = "Json")]
        public JsonResult JsonInfo()
        {
            return Json(repository.Products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderByPrice(string sortOrder, int page = 1)
        {
            ProductListViewModel model;

            ViewBag.UANSortParam = sortOrder == "UAN" ? "UAND" : "UAN";
            ViewBag.USDSortParam = sortOrder == "USD" ? "USDD" : "USD";


            //Take from memory
            var products = from prod in repository.Products select prod;
            switch (sortOrder)
            {
                case "UAND":
                    model = new ProductListViewModel
                    {
                        //Take from memory
                        Products = repository.Products.OrderByDescending(p => p.PriceUAN).Skip((page - 1) * pageSize).Take(pageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                            TotalItems = repository.Products.Count()
                        }
                    };
                    break;
                case "USDD":
                    model = new ProductListViewModel
                    {
                        //Take from memory
                        Products = repository.Products.OrderByDescending(p => p.PriceUSD).Skip((page - 1) * pageSize).Take(pageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                            TotalItems = repository.Products.Count()
                        }
                    };
                    break;
                default:
                    model = new ProductListViewModel
                    {
                        //Take from memory                
                        Products = repository.Products.OrderBy(p => p.PriceUAN).Skip((page - 1) * pageSize).Take(pageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                            TotalItems = repository.Products.Count()
                        }
                    };
                    break;
            }
            return View(model);
        }
    }
}