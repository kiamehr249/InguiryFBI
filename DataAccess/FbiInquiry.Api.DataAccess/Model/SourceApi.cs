using System.Collections.Generic;

namespace FbiInquiry.Api.DataAccess
{
    public class SourceApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public virtual ICollection<ApiUsage> ApiUsages { get; set; }
    }
}
