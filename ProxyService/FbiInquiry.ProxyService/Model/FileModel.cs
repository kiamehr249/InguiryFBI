using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FbiInquiry.ProxyService
{
    public class FileModel
    {
        public string Name { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
