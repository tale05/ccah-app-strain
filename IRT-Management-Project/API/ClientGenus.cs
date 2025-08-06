using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DTO;

namespace API
{
    public class ClientGenus
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public ClientGenus()
        {
            _httpClient = ApiConfig.Client;
            _baseUri = ApiConfig.GetGenusUri();
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

        public Task<List<ApiGenusDTO>> GetAllGenusAsync()
        {
            return HandleRequestAsync<List<ApiGenusDTO>>(() => _httpClient.GetAsync(_baseUri));
        }
    }
}
