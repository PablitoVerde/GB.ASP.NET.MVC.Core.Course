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
        private readonly IEmailSender _emailSender;
        private readonly IConfigurationRoot _config;
        private readonly IOptions<SmtpCredentials> _smtpCredentials;

        public CatalogController(IEmailSender emailSender, IConfigurationRoot config, IOptions<SmtpCredentials> options)
        {
            _emailSender = emailSender;
            _config = config;
            _smtpCredentials = options;
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
            _emailSender.SendEmail(product, _config, _smtpCredentials);

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
