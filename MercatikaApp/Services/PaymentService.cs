using MercatikaApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;

namespace MercatikaApp.Services
{
    public class PaymentApiService
    {
        private readonly HttpClient _httpClient;

        public PaymentApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.GetApiBaseUrl())
            };
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            var payments = await _httpClient.GetFromJsonAsync<List<Payment>>("api/payments");
            return payments ?? new List<Payment>();
        }

        public async Task<bool> ConfirmPaymentAsync(int paymentId, string estado, string cardNumber, int paymentMethodId)
        {
            var obj = new
            {
                PaymentId = paymentId,
                Estado = estado,
                CreditCardNum = cardNumber,
                PaymentMethodId = paymentMethodId
            };

            var jsonDebug = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            
            var response = await _httpClient.PutAsJsonAsync($"api/payments/{paymentId}", obj);
            var responseText = await response.Content.ReadAsStringAsync();
            
            return response.IsSuccessStatusCode;
        }
    }
}
