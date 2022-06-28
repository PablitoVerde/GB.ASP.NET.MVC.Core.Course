using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_Core_MVC_Project.Models;
using ASP_NET_Core_MVC_Project.Interfaces;
using Microsoft.Extensions.Options;

namespace ASP_NET_Core_MVC_Project.Controllers
{
    public class CatalogController : Controller
    {
        private static Catalog _catalog = new Catalog();
        private readonly IEmailSender _emailsender;
        private CancellationToken _cancelationToken;

        public CatalogController(IEmailSender emailSender)
        {
            _emailsender = emailSender;
        }

        [HttpGet]
        public IActionResult Products()
        {
            return View(_catalog);
        }

        [HttpPost]
        public IActionResult AddProduct([FromForm] Product product, CancellationToken cancellationToken)
        {
            _catalog.AddProduct(product, cancellationToken);
            SendEmailNewProduct(product, cancellationToken);
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        private async Task SendEmailNewProduct(Product product, CancellationToken cancellationToken)
        {
            await _emailsender.SendEmail(product, cancellationToken);        
        }
    }
}
