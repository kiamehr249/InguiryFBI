using System.Net.Http;

namespace FbiInquiry.ProxyService
{
    public class ProxyConfig
    {
        public HttpMethod Method { get; set; }
        public string ContentType { get; set; }
        public string Authorization { get; set; }
        public string Url { get; set; }
    }
}
