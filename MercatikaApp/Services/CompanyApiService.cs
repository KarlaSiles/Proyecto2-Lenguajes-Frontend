using MercatikaApp.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MercatikaApp.Services
{
    public class CompanyApiService
    {
        private readonly HttpClient _httpClient;

        public CompanyApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.GetApiBaseUrl())
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Company> GetCompanyAsync()
        {
            var response = await _httpClient.GetAsync("api/company");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Company>();
        }

        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/company/{company.Idsetup}", company);
            return response.IsSuccessStatusCode;
        }
    }
}
