using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ClientOrders
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetOrderUri();
        public ClientOrders()
        {
            _httpClient = ApiConfig.Client;
        }
        public async Task<List<ApiOrdersDTO>> GetAllOrdersAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<ApiOrdersDTO> objs = JsonSerializer.Deserialize<List<ApiOrdersDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return objs;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
        public async Task<string> Post(string contentWorkJson)
        {
            try
            {
                HttpContent content = new StringContent(contentWorkJson, Encoding.UTF8, "application/json");
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
        public async Task<string> Delete(int idOrders)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUri}/{idOrders}");
                response.EnsureSuccessStatusCode();
                return "Deleted";
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
        public async Task<string> Update(int idOrders, string json)
        {
            try
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_baseUri}/{idOrders}", content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
    }
}
