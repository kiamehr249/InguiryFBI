using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FbiInquiry.Web.Models;
using Microsoft.Extensions.Configuration;
using FbiInquiry.CivilRegistry.Service;
using FbiInquiry.ProxyService;
using Microsoft.AspNetCore.Authorization;

namespace FbiInquiry.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //string phrase = "{name: \"kiamehr\", age: \"32\"}";
            //string code = StringCipher.Encrypt(phrase, "kiamehr");
            //ViewBag.EnCrypte = code;
            //ViewBag.DeCrypte = StringCipher.Decrypt(code, "kiamehr");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
