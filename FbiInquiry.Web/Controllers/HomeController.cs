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
using FbiInquiry.DataModel;

namespace FbiInquiry.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IPersonService iPersonServ { get; set; }
        private IConfiguration Config { get; }
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration)
        {
            Config = Configuration;
            var ufw = ObjectFactory.GetInstance<ICivilRegUnitOfWork>(new CivilRegContext(Configuration.GetConnectionString("CivilRegistery")));
            iPersonServ = new PersonService(ufw);
            //ICivilRegUnitOfWork uow = new CivilRegContext(Configuration.GetConnectionString("CivilRegistery"));
            //iPersonServ = new PersonService(uow);

            _logger = logger;
        }

        public IActionResult Index()
        {
            var person = iPersonServ.Find(x => true);
            ViewBag.person = person.NationalNumber;
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
