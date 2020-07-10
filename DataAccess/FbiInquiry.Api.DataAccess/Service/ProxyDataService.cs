using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FbiInquiry.Api.DataAccess
{
    public class ProxyDataService
    {
        private ApiDbContext context;
        public ProxyDataService(IConfiguration Config)
        {
            context = new ApiDbContext(Config.GetValue<string>("ConnectionStrings:FBIConnection"));
        }

        public async Task<int> SetProxyAsyncLog(ApiUsage log)
        {
            context.ApiUsages.Add(log);
            int result = await context.SaveChangesAsync();
            return result;
        }
    }
}
