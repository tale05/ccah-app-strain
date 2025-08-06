using DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ClientInventory
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public ClientInventory()
        {
            _httpClient = ApiConfig.Client;
            _baseUri = ApiConfig.GetInventoryUri();
        }

        private async Task<T> HandleRequestAsync<T>(Func<Task<HttpResponseMessage>> request)
        {
            try
            {
                HttpResponseMessage response = await request();
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return default;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"An unexpected error occurred: {e.Message}");
                return default;
            }
        }

        public Task<List<ApiInventoryDTO>> GetAllInventoryAsync()
        {
            return HandleRequestAsync<List<ApiInventoryDTO>>(() => _httpClient.GetAsync(_baseUri));
        }

        public async Task<string> Post(string inventoryJson)
        {
            try
            {
                HttpContent content = new StringContent(inventoryJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_baseUri, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred with the request: {e.Message}");
                return null;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"An unexpected error occurred: {e.Message}");
                return null;
            }
        }

        public async Task<string> Update(int idInventory, string json)
        {
            try
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_baseUri}/{idInventory}", content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"An unexpected error occurred: {e.Message}");
                return null;
            }
        }
    }
}
