using MercatikaApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MercatikaApp.Services
{
    public class ClientApiService
    {
        private readonly HttpClient _httpClient;

        public ClientApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.GetApiBaseUrl())
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Client>> GetAllClients()
        {
            var response = await _httpClient.GetAsync("api/client");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Client>>();
        }

        public async Task<int> CreateClient(Client client)
        {
            var response = await _httpClient.PostAsJsonAsync("api/client", client);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateClient(Client client)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/client/{client.ClientId}", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClient(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/client/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Client> GetClientById(int id)
        {
            var response = await _httpClient.GetAsync($"api/client/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Client>();
        }

        public async Task<List<Client>> GetClientsByCompanyName(string companyName)
        {
            var response = await _httpClient.GetAsync($"api/client/company/{companyName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Client>>();
        }

        public async Task<List<Client>> GetClientsByName(string name)
        {
            var response = await _httpClient.GetAsync($"api/client/contractname/{name}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Client>>();
        }

        public async Task<List<Client>> GetClientsByLastname(string lastname)
        {
            var response = await _httpClient.GetAsync($"api/client/contractlastname/{lastname}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Client>>();
        }
    }
}
