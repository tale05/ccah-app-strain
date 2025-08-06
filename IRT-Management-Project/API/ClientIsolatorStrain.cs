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
    public class ClientIsolatorStrain
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetIsolatorStrainUri();
        public ClientIsolatorStrain()
        {
            _httpClient = ApiConfig.Client;
        }
        public async Task<List<ApiIsolatorStrainDTO>> GetAllIsolatorStrainAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<ApiIsolatorStrainDTO> objs = JsonSerializer.Deserialize<List<ApiIsolatorStrainDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return objs;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
        public async Task<string> Post(string isolatorStrainJson)
        {
            try
            {
                HttpContent content = new StringContent(isolatorStrainJson, Encoding.UTF8, "application/json");
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
    }
}
