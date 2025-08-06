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
    public class ClientOrdersDetail
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetOrderDetailUri();
        public ClientOrdersDetail()
        {
            _httpClient = ApiConfig.Client;
        }
        public async Task<List<ApiOrderDetailDTO>> GetAllOrdersDetailAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<ApiOrderDetailDTO> objs = JsonSerializer.Deserialize<List<ApiOrderDetailDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return objs;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
        public async Task<List<ApiOrderDetailDTO>> GetOrdersDetailByIdOrdersAsync(int idOrders)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUri}/GetAllByIdOrder/{idOrders}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<ApiOrderDetailDTO> objs = JsonSerializer.Deserialize<List<ApiOrderDetailDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return objs;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
        public async Task<string> Post(string json)
        {
            try
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
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
        public async Task<string> Delete(int idOrdersDetail)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUri}/{idOrdersDetail}");
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
        public async Task<string> Update(int idOrdersDetail, string json)
        {
            try
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_baseUri}/{idOrdersDetail}", content);
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
