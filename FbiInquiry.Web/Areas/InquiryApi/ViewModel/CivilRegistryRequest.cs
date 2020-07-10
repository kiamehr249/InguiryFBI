using FbiInquiry.ProxyService;

namespace FbiInquiry.Web.Areas.InquiryApi
{
    public class CivilRegistryRequest : InputModel
    {
        public string NATIONAL_NUMBER { get; set; }
        public string BIRTHDATE { get; set; }
    }
}
