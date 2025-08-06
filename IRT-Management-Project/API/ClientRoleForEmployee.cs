namespace API
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using DTO;
    public class ClientRoleForEmployee
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetRoleForEmployeeUri();
        public ClientRoleForEmployee()
        {
            _httpClient = ApiConfig.Client;
        }
        public async Task<List<ApiRoleForEmployeeDTO>> GetAllRoleForEmployeeAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<ApiRoleForEmployeeDTO> objs = JsonSerializer.Deserialize<List<ApiRoleForEmployeeDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
