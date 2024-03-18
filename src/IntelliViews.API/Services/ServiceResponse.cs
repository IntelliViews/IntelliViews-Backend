namespace IntelliViews.API.Services
{
    public class ServiceResponse<T> where T : class
    {
        public bool Status { get; set; } = true;
        public T? Data { get; set; }
    
    }
}
