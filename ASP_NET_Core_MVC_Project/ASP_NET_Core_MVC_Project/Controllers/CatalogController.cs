using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_Core_MVC_Project.Models;

namespace ASP_NET_Core_MVC_Project.Controllers
{
    public class CatalogController : Controller
    {
        private static Catalog _catalog = new Catalog();

        public CatalogController()
        {

        }

        [HttpGet]
        public IActionResult Products()
        {
            if (_catalog.CountProducts() == 0)
            {
                return NotFound();
            }
            else
            {
                return View(_catalog);
            }
        }

        [HttpPost]
        public IActionResult AddProduct([FromForm] Product product)
        {
            _catalog.AddProduct(product);
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
    }
}
