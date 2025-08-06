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
    public class ClientProjectContent
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetProjectContentUri();
        public ClientProjectContent()
        {
            _httpClient = ApiConfig.Client;
        }
        public async Task<List<ApiProjectContentDTO>> GetAllProjectContentAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<ApiProjectContentDTO> objs = JsonSerializer.Deserialize<List<ApiProjectContentDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return objs;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
        public async Task<string> Post(string projectContentJson)
        {
            try
            {
                HttpContent content = new StringContent(projectContentJson, Encoding.UTF8, "application/json");
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
        public async Task<string> Delete(string idProject)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUri}/{idProject}");
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
        public async Task<string> Update(string id, string projectJson)
        {
            try
            {
                HttpContent content = new StringContent(projectJson, Encoding.UTF8, "application/json");
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
        public async Task<string> PatchStatusProject(string idProject, string status)
        {
            try
            {
                HttpContent content = new StringContent($"\"{status}\"", Encoding.UTF8, "application/json");
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUri}/{idProject}/status")
                {
                    Content = content
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);
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
