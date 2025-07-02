using MercatikaApp.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;

namespace MercatikaApp.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7086/api/products";
        private const string CategoriesUrl = "https://localhost:7086/api/Products/categories";

        public ProductService()
        {
            _httpClient = new HttpClient();
        }


        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllProductsAsync();

                var url = $"{BaseUrl}?searchTerm={Uri.EscapeDataString(searchTerm)}";
                var response = await _httpClient.GetFromJsonAsync<List<Product>>(url);
                return response ?? new List<Product>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar productos.", ex);
            }
        }


        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Product>>(BaseUrl);
                return response ?? new List<Product>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener todos los productos.", ex);
            }
        }


        public async Task<Product?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Product>($"{BaseUrl}/{id}");
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> CreateProductAsync(Product product)
        {

            var json = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });


            


            var response = await _httpClient.PostAsJsonAsync(BaseUrl, product);


            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Error al insertar producto:\n" + error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return response.IsSuccessStatusCode;
        }




        public async Task<bool> UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{product.ProductId}", product);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> CreateProductDetailAsync(int productId, ProductDetail detail)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{productId}/details", detail);
            return response.IsSuccessStatusCode;
        }


        public async Task<ProductDetail?> GetProductDetailAsync(int detailId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ProductDetail>($"{BaseUrl}/details/{detailId}");
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Category>>(CategoriesUrl);
                return response ?? new List<Category>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener las categorías.", ex);
            }
        }


        public async Task<bool> UpdateProductDetailAsync(ProductDetail detail)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/details/{detail.ProductDetailId}", detail);
            return response.IsSuccessStatusCode;
        }

    }
}