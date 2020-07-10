using System;

namespace FbiInquiry.Api.DataAccess
{
    public class ApiUsage
    {
        public int Id { get; set; }
        public string ClientUsername { get; set; }
        public string RefrenceCode { get; set; }
        public int StatusCode { get; set; }
        public DateTime UseDateTime { get; set; }
        public int ApiId { get; set; }
        public virtual SourceApi SourceApi { get; set; }
    }
}
