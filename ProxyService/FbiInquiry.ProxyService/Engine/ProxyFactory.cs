using FbiInquiry.Api.DataAccess;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FbiInquiry.ProxyService
{
    public class ProxyFactory<T>
    {
        private ProxyConfig configs;
        private ProxyDataService LogService;
        private ApiInfo ApiInfo;
        public ProxyFactory(ProxyConfig proxyConfig, ApiInfo apiInfo, IConfiguration config)
        {
            configs = proxyConfig;
            ApiInfo = apiInfo;
            LogService = new ProxyDataService(config);
        }

        public async Task<ResponseModel<T>> FormFilePostRequestAsync(Dictionary<string, string> parameters, List<FileModel> sendFiles)
        {
            HttpClient client = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            HttpContent DictionaryItems = new FormUrlEncodedContent(parameters);
            form.Add(DictionaryItems, "formpart");

            foreach (var file in sendFiles)
            {
                HttpContent content = new StringContent(file.Name);
                form.Add(content, "PicFile");
                var stream = file.FormFile.OpenReadStream();
                content = new StreamContent(stream);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "PicFile",
                    FileName = file.FormFile.FileName
                };
                form.Add(content);
            }
            
            var response = await client.PostAsync(configs.Url, form);
            await LogService.SetProxyAsyncLog(new ApiUsage
            {
                ClientUsername = ApiInfo.ClientUsername,
                RefrenceCode = ApiInfo.RefrenceCode,
                StatusCode = Convert.ToInt32(response.StatusCode),
                UseDateTime = DateTime.Now,
                ApiId = ApiInfo.ApiId
            });

            if (!response.IsSuccessStatusCode)
            {
                return new ResponseModel<T>
                {
                    Title = "خطای پذیرنده",
                    Status = Convert.ToInt32(response.StatusCode),
                    Message = "خطای پذیرنده"
                };
            }

            var result = response.Content.ReadAsStringAsync().Result;
            T ObjResult = JsonConvert.DeserializeObject<T>(result);
            return new ResponseModel<T>
            {
                Title = "success load",
                Status = 0,
                Message = "دریافت موفق",
                DataSet = ObjResult
            };
        }

        public async Task<ResponseModel<T>> PostRequestAsync(object dataModel)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(dataModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(configs.Url, data);

            await LogService.SetProxyAsyncLog(new ApiUsage
            {
                ClientUsername = ApiInfo.ClientUsername,
                RefrenceCode = ApiInfo.RefrenceCode,
                StatusCode = Convert.ToInt32(response.StatusCode),
                UseDateTime = DateTime.Now,
                ApiId = ApiInfo.ApiId
            });

            if (!response.IsSuccessStatusCode)
            {
                return new ResponseModel<T>
                {
                    Title = "خطای پذیرنده",
                    Status = Convert.ToInt32(response.StatusCode),
                    Message = "خطای پذیرنده"
                };
            }

            var result = response.Content.ReadAsStringAsync().Result;
            T ObjResult = JsonConvert.DeserializeObject<T>(result);
            return new ResponseModel<T>
            {
                Title = "نتیجه موفق",
                Status = 0,
                Message = "دریافت موفق",
                DataSet = ObjResult
            };
        }

        public async Task<ResponseModel<T>> PostXformRequestAsync(Dictionary<string, string> parameters)
        {
            HttpClient client = new HttpClient();

            string paramString = "";
            int counter = 0;
            foreach (var item in parameters)
            {
                counter++;
                if (counter == 1)
                {
                    paramString += item.Key + "=" + item.Value;
                }
                else
                {
                    paramString += "&" + item.Key + "=" + item.Value;
                }
            }

            var data = new StringContent(paramString, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync(configs.Url, data);

            await LogService.SetProxyAsyncLog(new ApiUsage
            {
                ClientUsername = ApiInfo.ClientUsername,
                RefrenceCode = ApiInfo.RefrenceCode,
                StatusCode = Convert.ToInt32(response.StatusCode),
                UseDateTime = DateTime.Now,
                ApiId = ApiInfo.ApiId
            });

            if (!response.IsSuccessStatusCode)
            {
                return new ResponseModel<T>
                {
                    Title = "خطای پذیرنده",
                    Status = Convert.ToInt32(response.StatusCode),
                    Message = "خطای پذیرنده"
                };
            }

            var result = response.Content.ReadAsStringAsync().Result;
            T ObjResult = JsonConvert.DeserializeObject<T>(result);
            return new ResponseModel<T>
            {
                Title = "نتیجه موفق",
                Status = 0,
                Message = "دریافت موفق",
                DataSet = ObjResult
            };
        }

        public async Task<ResponseModel<T>> GetRequestAsync(Dictionary<string, string> parameters)
        {
            string fullUrl = configs.Url + "?";
            int counter = 0;
            foreach (var item in parameters)
            {
                counter++;
                if (counter == 1)
                {
                    fullUrl += item.Key + "=" + item.Value;
                }
                else
                {
                    fullUrl += "&" + item.Key + "=" + item.Value;
                }
                
            }
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            T ObjResult = JsonConvert.DeserializeObject<T>(response);
            return new ResponseModel<T>
            {
                Title = "success load",
                Status = 0,
                Message = "دریافت موفق",
                DataSet = ObjResult
            };
        }

    }
}
