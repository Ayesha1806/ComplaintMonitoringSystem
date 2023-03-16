namespace MVC.Helper
{
    public class ApiUrls
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7152/");
            return client;
        }
    }
}
