using DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ClientPhylum
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public ClientPhylum()
        {
            _httpClient = ApiConfig.Client;
            _baseUri = ApiConfig.GetPhylumUri();
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

        private async Task<bool> HandleBooleanRequestAsync(Func<Task<HttpResponseMessage>> request)
        {
            try
            {
                HttpResponseMessage response = await request();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return false;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"An unexpected error occurred: {e.Message}");
                return false;
            }
        }

        public Task<List<ApiPhylumDTO>> GetAllPhylumsAsync()
        {
            return HandleRequestAsync<List<ApiPhylumDTO>>(() => _httpClient.GetAsync(_baseUri));
        }

        public Task<ApiPhylumDTO> GetPhylumByIdAsync(int id)
        {
            string requestUri = $"{_baseUri}/{id}";
            return HandleRequestAsync<ApiPhylumDTO>(() => _httpClient.GetAsync(requestUri));
        }

        public Task<string> AddPhylumAsync(string phylumJson)
        {
            var content = new StringContent(phylumJson, Encoding.UTF8, "application/json");
            return HandleRequestAsync<string>(() => _httpClient.PostAsync(_baseUri, content));
        }

        public Task<string> UpdatePhylumAsync(int id, string phylumJson)
        {
            var content = new StringContent(phylumJson, Encoding.UTF8, "application/json");
            return HandleRequestAsync<string>(() => _httpClient.PutAsync($"{_baseUri}/{id}", content));
        }

        public Task<bool> DeletePhylumAsync(int id)
        {
            return HandleBooleanRequestAsync(() => _httpClient.DeleteAsync($"{_baseUri}/{id}"));
        }
    }
}
