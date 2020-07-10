using Microsoft.AspNetCore.Mvc;
using System;

namespace FbiInquiry.Web.Areas.FbiToken.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenVerificationController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromForm] string Key, [FromForm] string Token, [FromForm] DateTime CreateDate)
        {
            if (string.IsNullOrWhiteSpace(Key) || string.IsNullOrWhiteSpace(Token))
            {
                return Ok(new
                {
                    title = "استعلام توکن",
                    status = 1,
                    message = "خطا در مقادیر ورودی",
                    isValid = 0
                });
            }
            var result = FbiTokenTools.TokenIsValid(Key, Token);
            return Ok(new { 
                title = "استعلام توکن",
                status = 0,
                message = result ? "مقدار صحیح" : "مقدار نادرست",
                isValid = result ? 1 : 0
            });
        }
    }
}