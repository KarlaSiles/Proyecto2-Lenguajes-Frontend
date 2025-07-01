using MercatikaApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace MercatikaApp.Services
{
    public class OrderApiService
    {
        private readonly HttpClient _httpClient;

        // URL fija directamente en el código
        private const string BaseUrl = "https://localhost:7086/";

        public OrderApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        // -------- ORDERS --------

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Order>>("api/Orders");
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _httpClient.GetFromJsonAsync<Order>($"api/Orders/{orderId}");
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            try
            {
                // Serializamos a JSON y lo imprimimos
                var json = System.Text.Json.JsonSerializer.Serialize(order, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true // para mejor legibilidad
                });

                Console.WriteLine("JSON que se enviará al backend:");
                MessageBox.Show(json);

                // Enviar el request como antes
                var response = await _httpClient.PostAsJsonAsync("api/Orders", order);

                Console.WriteLine($"Resultado del request: {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear orden: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Orders/{order.OrderId}", order);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var response = await _httpClient.DeleteAsync($"api/Orders/{orderId}");
            return response.IsSuccessStatusCode;
        }

        // -------- ORDER DETAILS --------

        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _httpClient.GetFromJsonAsync<List<OrderDetail>>($"api/OrderDetails/{orderId}");
        }

        public async Task<bool> CreateOrderDetailAsync(OrderDetail detail)
        {
            var response = await _httpClient.PostAsJsonAsync("api/OrderDetails", detail);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderDetailAsync(int orderId, int productDetailId)
        {
            var response = await _httpClient.DeleteAsync($"api/OrderDetails/{orderId}/{productDetailId}");
            return response.IsSuccessStatusCode;
        }
    }
}
