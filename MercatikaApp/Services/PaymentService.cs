using MercatikaApp.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;

namespace MercatikaApp.Services
{
    public class PaymentApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7086/api/payments";

        public PaymentApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            var payments = await _httpClient.GetFromJsonAsync<List<Payment>>(BaseUrl);
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

            var jsonDebug = System.Text.Json.JsonSerializer.Serialize(obj, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            MessageBox.Show($"JSON que se enviará:\n{jsonDebug}", "Debug - JSON Enviado", MessageBoxButton.OK, MessageBoxImage.Information);

            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{paymentId}", obj);
            var responseText = await response.Content.ReadAsStringAsync();

            MessageBox.Show($"Respuesta del servidor:\n{responseText}", "Debug - Respuesta", MessageBoxButton.OK, MessageBoxImage.Information);

            return response.IsSuccessStatusCode;
        }
    }
}
