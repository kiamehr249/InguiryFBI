using Microsoft.AspNetCore.Mvc;
using System;

namespace FbiInquiry.Web.Areas.FbiToken.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetTokenController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromForm] string Key)
        {
            if (string.IsNullOrWhiteSpace(Key))
            {
                return Ok(new
                {
                    title = "دریافت توکن",
                    status = 1,
                    message = "کلید را صحیح ارسال کنید",
                    token = ""
                });
            }

            var result = FbiTokenTools.GetToken(Key);
            if (string.IsNullOrWhiteSpace(result))
            {
                return Ok(new
                {
                    title = "دریافت توکن",
                    status = 2,
                    message = "دریافت ناموفق دوباره تلاش کنید",
                    token = result
                });
            }

            return Ok(new
            {
                title = "دریافت توکن",
                status = 0,
                message = "دریافت موفق",
                token = result
            });
        }
    }
}