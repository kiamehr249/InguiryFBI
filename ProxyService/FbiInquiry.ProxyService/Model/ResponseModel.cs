namespace FbiInquiry.ProxyService
{
    public class ResponseModel<T> : ResponseBaseModel
    {
        public T DataSet { get; set; }
    }
}
