using MercatikaApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MercatikaApp.Services
{
    public class OrderApiService
    {
        private readonly HttpClient _httpClient;

        public OrderApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.GetApiBaseUrl())
            };
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Order>>("api/orders");
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _httpClient.GetFromJsonAsync<Order>($"api/orders/{orderId}");
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", order);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/orders/{order.OrderId}", order);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var response = await _httpClient.DeleteAsync($"api/orders/{orderId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _httpClient.GetFromJsonAsync<List<OrderDetail>>($"api/orderdetails/{orderId}");
        }

        public async Task<bool> CreateOrderDetailAsync(OrderDetail detail)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orderdetails", detail);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderDetailAsync(int orderId, int productDetailId)
        {
            var response = await _httpClient.DeleteAsync($"api/orderdetails/{orderId}/{productDetailId}");
            return response.IsSuccessStatusCode;
        }
    }
}
