using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DTO;
using System.Text.Json;

namespace API
{
    public class ClientProvinces
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetProvinceUri();
        private readonly string _baseUri1 = ApiConfig.GetDistrictUri();
        private readonly string _baseUri2 = ApiConfig.GetWardUri();

        public ClientProvinces()
        {
            _httpClient = ApiConfig.Client;
        }

        private async Task<List<T>> GetDataAsync<T>(string uri)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public Task<List<ApiProvincesDTO>> GetAllProvincesAsync() => GetDataAsync<ApiProvincesDTO>(_baseUri);
        public Task<List<DistrictDTO>> GetAllDistrictAsync() => GetDataAsync<DistrictDTO>(_baseUri1);
        public Task<List<WardDTO>> GetAllWardAsync() => GetDataAsync<WardDTO>(_baseUri2);
    }
}
