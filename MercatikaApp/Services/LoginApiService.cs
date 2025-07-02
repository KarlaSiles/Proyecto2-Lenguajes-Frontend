using MercatikaApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MercatikaApp.Services
{
    public class LoginApiService
    {
        private readonly HttpClient _http;

        public LoginApiService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.GetApiBaseUrl())
            };
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var credentials = new Login { Username = username, Password = password };
            var json = JsonSerializer.Serialize(credentials);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/login", content);
            return response.IsSuccessStatusCode;
        }
    }
}
