using MercatikaApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;

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
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Client>> GetAllClients()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/client");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Client>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clientes: {ex.Message}");
                return new List<Client>();
            }
        }

        public async Task<int> CreateClient(Client client)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/client", client);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error {response.StatusCode}: {errorContent}");
                }

                // Opción 1: Intenta leer como int directamente
                try
                {
                    return await response.Content.ReadFromJsonAsync<int>();
                }
                catch
                {
                    // Opción 2: Si falla, intenta leer como objeto y extraer el ID
                    var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
                    if (result != null && result.TryGetValue("clientId", out var idObj))
                    {
                        return Convert.ToInt32(idObj);
                    }
                    throw new Exception("Formato de respuesta inesperado del servidor");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateClient: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> UpdateClient(Client client)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/client/{client.ClientId}", client);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar cliente: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteClient(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/client/{id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar cliente: {ex.Message}");
                return false;
            }


        }

        public async Task<Client> GetClientById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/client/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Client>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Client>> GetClientsByCompanyName(string companyName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/client/company/{companyName}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Client>>();
            }
            catch
            {
                return new List<Client>();
            }
        }

        public async Task<List<Client>> GetClientsByName(string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/client/contractname/{name}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Client>>();
            }
            catch
            {
                return new List<Client>();
            }
        }

        public async Task<List<Client>> GetClientsByLastname(string lastname)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/client/contractlastname/{lastname}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Client>>();
            }
            catch
            {
                return new List<Client>();
            }
        }
    }
}
