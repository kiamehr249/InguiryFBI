using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FbiInquiry.Web.Areas.ApiFbi.Controllers
{
    [Route("OrderService")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IConfiguration Config { get; }
        public OrderController(IConfiguration Configuration)
        {
            Config = Configuration;
            //Configuration.GetConnectionString("CivilRegistery")
        }

        [HttpPost]
        public IActionResult Post([FromForm] string Key)
        {
            

            return Ok(new
            {
                title = "دریافت توکن",
                status = 0,
                message = "دریافت موفق",
                token = ""
            });
        }
    }
}