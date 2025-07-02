using MercatikaApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Windows;

namespace MercatikaApp.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.GetApiBaseUrl())
            };
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            var url = string.IsNullOrWhiteSpace(searchTerm) ? "api/products" : $"api/products?searchTerm={Uri.EscapeDataString(searchTerm)}";
            var response = await _httpClient.GetFromJsonAsync<List<Product>>(url);
            return response ?? new List<Product>();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
            return response ?? new List<Product>();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", product);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Error al insertar producto:\n" + error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{product.ProductId}", product);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Category>>("api/products/categories");
            return response ?? new List<Category>();
        }

        public async Task<ProductDetail?> GetProductDetailAsync(int detailId)
        {
            return await _httpClient.GetFromJsonAsync<ProductDetail>($"api/products/details/{detailId}");
        }

        public async Task<bool> CreateProductDetailAsync(int productId, ProductDetail detail)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/products/{productId}/details", detail);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductDetailAsync(ProductDetail detail)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/details/{detail.ProductDetailId}", detail);
            return response.IsSuccessStatusCode;
        }
    }
}
