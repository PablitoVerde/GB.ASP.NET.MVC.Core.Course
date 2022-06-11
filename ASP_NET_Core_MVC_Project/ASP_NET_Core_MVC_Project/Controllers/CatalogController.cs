using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_Core_MVC_Project.Models;
using ASP_NET_Core_MVC_Project.Interfaces;

namespace ASP_NET_Core_MVC_Project.Controllers
{
    public class CatalogController : Controller
    {
        private static Catalog _catalog = new Catalog();
        private readonly IEmailSender _emailSender;

        public CatalogController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
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
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            await _emailSender.SendEmailAsync(product);

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
