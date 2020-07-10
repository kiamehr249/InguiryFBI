using System.Net.Http;
using System.Threading.Tasks;
using FbiInquiry.ProxyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FbiInquiry.Web.Areas.InquiryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipCodeController : ControllerBase
    {
        private IConfiguration Config { get; }
        public ZipCodeController(IConfiguration Configuration)
        {
            Config = Configuration;
        }


        [HttpPost()]
        public async Task<IActionResult> Post([FromForm] ZipCodeRequest input)
        {
            var proxyConfig = new ProxyConfig
            {
                Method = HttpMethod.Post,
                ContentType = "application/json",
                Authorization = "Bearer dsaUhdsCASxsd",
                Url = Config.GetValue<string>("SourceProxies:ZipCode")
            };

            var apiInfo = new ApiInfo()
            {
                ClientUsername = input.ClientUsername,
                RefrenceCode = input.RefrenceCode,
                ApiId = 1
            };

            var proxy = new ProxyFactory<ZipCodeResponse>(proxyConfig, apiInfo, Config);
            var dataItems = new ZipCodeRequest()
            {
                ClientUsername = "",
                RefrenceCode = "",
                ZipCode = input.ZipCode
            };
            var result = await proxy.PostRequestAsync(dataItems);
            return Ok(result);
        }
    }
}