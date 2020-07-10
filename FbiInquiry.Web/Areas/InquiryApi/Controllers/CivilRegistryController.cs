using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FbiInquiry.CivilRegistry.Service;
using FbiInquiry.ProxyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FbiInquiry.Web.Areas.InquiryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CivilRegistryController : ControllerBase
    {
        public IPersonService iPersonServ { get; set; }
        private IConfiguration Config { get; }
        public CivilRegistryController(IConfiguration Configuration)
        {
            Config = Configuration;
            ICivilRegUnitOfWork uow = new CivilRegContext(Configuration.GetConnectionString("CivilRegistery"));
            iPersonServ = new PersonService(uow);
        }


        [HttpPost()]
        public async Task<IActionResult> Post([FromForm] CivilRegistryRequest input)
        {
            var ReadyItem = await iPersonServ.FindAsync(x => x.NationalNumber == input.NATIONAL_NUMBER && x.LastUpdate > DateTime.Now.AddDays(-1));
            if (ReadyItem != null)
            {
                var answer = new ResponseModel<CivilRegistryResponse>()
                {
                    Title = "دریافت موفق",
                    Status = 0,
                    Message = "دریافت موفق",
                    DataSet = JsonConvert.DeserializeObject<CivilRegistryResponse>(ReadyItem.JsonResult)
                };
                return Ok(answer);
            }


            var proxyConfig = new ProxyConfig
            {
                Method = HttpMethod.Post,
                Authorization = "Bearer dsaUhdsCASxsd",
                Url = Config.GetValue<string>("SourceProxies:CivilRegistry")
            };

            var apiInfo = new ApiInfo()
            {
                ClientUsername = input.ClientUsername,
                RefrenceCode = input.RefrenceCode,
                ApiId = 1
            };

            var proxy = new ProxyFactory<CivilRegistryResponse>(proxyConfig, apiInfo, Config);

            var dataItems = new  Dictionary<string, string>();
            dataItems.Add("NATIONAL_NUMBER", input.NATIONAL_NUMBER);
            var result = await proxy.PostXformRequestAsync(dataItems);
            if(result.Status == 0)
            {
                if (!String.IsNullOrWhiteSpace(result.DataSet.NATIONAL_NUMBER))
                {
                    var parameters = new object[] { input.NATIONAL_NUMBER, result.DataSet.DEATHDATE, JsonConvert.SerializeObject(result.DataSet), DateTime.Now };
                    iPersonServ.ExecSqlCommand("SP_CivilRegisteryInguiry @p0, @p1, @p2, @p3", parameters);
                }
                
            }
            
            return Ok(result);
        }
    }
}