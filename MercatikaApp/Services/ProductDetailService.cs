using MercatikaApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MercatikaApp.Services
{
    public class ProductDetailService
    {
        private readonly HttpClient _httpClient;

        public ProductDetailService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.GetApiBaseUrl())
            };
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            var url = string.IsNullOrWhiteSpace(searchTerm) ? "api/products" : $"api/products?searchTerm={searchTerm}";
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(url);
            return products ?? new List<Product>();
        }

        public async Task<bool> CreateProductDetailAsync(int productId, ProductDetail detail)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/products/{productId}/details", detail);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductDetailAsync(int productId, ProductDetail detail)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{productId}/details", detail);
            return response.IsSuccessStatusCode;
        }
    }
}
