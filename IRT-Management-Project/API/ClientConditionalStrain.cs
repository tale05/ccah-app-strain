using DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ClientConditionalStrain
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetConditionalStrainUri();
        public ClientConditionalStrain()
        {
            _httpClient = ApiConfig.Client;
        }
        public async Task<List<ApiConditionalStrainDTO>> GetAllConditionalStrainAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<ApiConditionalStrainDTO> objs = JsonSerializer.Deserialize<List<ApiConditionalStrainDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return objs;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
    }
}
