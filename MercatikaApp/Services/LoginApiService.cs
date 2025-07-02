using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MercatikaApp.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MercatikaApp.Services
{
    public class LoginApiService
    {
        private readonly HttpClient _http;
        private const string Endpoint = "https://localhost:7086/api/login";

        public LoginApiService()
        {
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var credentials = new Login { Username = username, Password = password };
            var json = JsonSerializer.Serialize(credentials);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _http.PostAsync(Endpoint, content);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}