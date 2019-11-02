using Product.Domain.Abstract;
using System.Linq;
using System.Web.Mvc;

namespace StockApp_Lesson_Four.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        /// TODO
        /// </summary publicKey = "Token">
        private readonly IProductRepository repository;

        public AdminController(IProductRepository repoParam)
        {
            repository = repoParam;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ActionResult Edit(int ID)
        {
            Product.Domain.Product product = repository.Products.FirstOrDefault(p => p.ID == ID);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                TempData["RouteProductKey"] = product.ProductName;
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product.Domain.Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format($"{product.ProductName} has been saved");
                return View("Index", repository.Products.ToList());
            }
            else
            {
                return View(repository.Products.ToList());
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product.Domain.Product());
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            Product.Domain.Product deleteProduct = repository.DeleteProduct(ID);
            if (deleteProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deleteProduct.ProductName);
            }
            return View("Congrat");
        }

        public ActionResult Congrat()
        {
            return View();
        }
    }
}