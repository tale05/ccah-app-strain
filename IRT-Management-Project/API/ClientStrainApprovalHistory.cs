using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DTO;

namespace API
{
    public class ClientStrainApprovalHistory
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetStrainApprovalHistoryUri();

        public ClientStrainApprovalHistory()
        {
            _httpClient = ApiConfig.Client;
        }

        public async Task<List<ApiStrainApprovalHistoryDTO>> GetAllStrainApprovalHistoriesAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<ApiStrainApprovalHistoryDTO> objs = JsonSerializer.Deserialize<List<ApiStrainApprovalHistoryDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return objs;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public async Task<ApiStrainApprovalHistoryDTO> GetStrainApprovalHistoryByIdAsync(int id)
        {
            try
            {
                string requestUri = $"{_baseUri}/{id}";
                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ApiStrainApprovalHistoryDTO strainApprovalHistory = JsonSerializer.Deserialize<ApiStrainApprovalHistoryDTO>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return strainApprovalHistory;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public async Task<string> Post(string strainApprovalHistoryJson)
        {
            try
            {
                HttpContent content = new StringContent(strainApprovalHistoryJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_baseUri, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public async Task<string> Update(int id, string strainApprovalHistoryJson)
        {
            try
            {
                HttpContent content = new StringContent(strainApprovalHistoryJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_baseUri}/{id}", content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUri}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return false;
            }
        }
    }
}
