using MercatikaApp.Models;
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
        private const string BaseUrl = "https://localhost:7086/api/products";

        public ProductDetailService()
        {
            _httpClient = new HttpClient();
        }

        
        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            string url = string.IsNullOrWhiteSpace(searchTerm)
                ? $"{BaseUrl}?searchTerm="
                : $"{BaseUrl}?searchTerm={searchTerm}";

            var products = await _httpClient.GetFromJsonAsync<List<Product>>(url);
            return products ?? new List<Product>();
        }

        
        public async Task<bool> CreateProductDetailAsync(int productId, ProductDetail detail)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{productId}/details", detail);
            return response.IsSuccessStatusCode;
        }

       
        public async Task<bool> UpdateProductDetailAsync(int productId, ProductDetail detail)
        {
            
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{productId}/details", detail);
            return response.IsSuccessStatusCode;
        }
    }
}

