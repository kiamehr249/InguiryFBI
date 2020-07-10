using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FbiInquiry.Utilities;
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
        private ADOUtilities _db;
        public OrderController(IConfiguration Configuration)
        {
            Config = Configuration;
            _db = new ADOUtilities(Configuration.GetConnectionString("SupplierSystem"));
        }

        [Route("OrderID")]
        [HttpPost]
        public IActionResult Post([FromForm] string CurrentOrderID, [FromForm] string ServiceCode)
        {
            try
            {
                Guid OrderID;
                if (!Guid.TryParse(CurrentOrderID, out OrderID))
                    return Ok(new
                    {
                        title = "دریافت توکن جدید",
                        status = 0,
                        message = "مقدار CurrentOrderID نامعتبر است",
                        token = string.Empty
                    });

                Int64 Code;
                if (!Int64.TryParse(ServiceCode, out Code))
                    return Ok(new
                    {
                        title = "دریافت توکن جدید",
                        status = 0,
                        message = "مقدار ServiceCode نامعتبر است",
                        token = string.Empty
                    });


                DataSet ds = _db.GetDataSetExec("EXEC SPIRunningServicesGenerateOrderID_C @CurrentOrderID = '" + CurrentOrderID + "' , @ServiceCode = " + ServiceCode);

                if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        return Ok(new
                        {
                            title = "دریافت توکن جدید",
                            status = 1,
                            message = "دریافت موفق",
                            token = ds.Tables[0].Rows[0]["ServiceCodeOnOperatorSide"].ToString()
                        });
                    else
                        return Ok(new
                        {
                            title = "دریافت توکن جدید",
                            status = 0,
                            message = ds.Tables[0].Rows[0]["ServiceCodeOnOperatorSide"].ToString(),
                            token = string.Empty
                        });
                }
                else
                    return Ok(new
                    {
                        title = "دریافت توکن جدید",
                        status = 0,
                        message = "خطا در فراخوانی اطلاعات",
                        token = string.Empty
                    });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    title = "دریافت توکن جدید",
                    status = 0,
                    message = ex.Message,
                    token = string.Empty
                });
            }
        }
    }
}