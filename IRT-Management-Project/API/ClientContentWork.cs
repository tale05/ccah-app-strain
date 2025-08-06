using DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ClientContentWork
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetContentWorkUri();
        private readonly string _baseUriProjectContent = ApiConfig.GetProjectContentUri();

        public ClientContentWork()
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

        private async Task<string> SendDataAsync(string uri, HttpMethod method, HttpContent content = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, uri) { Content = content };
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public Task<List<ApiContentWorkDTO>> GetAllContentWorkAsync() => GetDataAsync<ApiContentWorkDTO>(_baseUri);

        public Task<string> Post(string contentWorkJson)
        {
            var content = new StringContent(contentWorkJson, Encoding.UTF8, "application/json");
            return SendDataAsync(_baseUri, HttpMethod.Post, content);
        }

        public Task<string> Delete(int idContentWork) => SendDataAsync($"{_baseUri}/{idContentWork}", HttpMethod.Delete);

        public Task<string> Update(int idContentWork, string contentWorkJson)
        {
            var content = new StringContent(contentWorkJson, Encoding.UTF8, "application/json");
            return SendDataAsync($"{_baseUri}/{idContentWork}", HttpMethod.Put, content);
        }

        public Task<string> PatchFileNameSave(int idContentWork, byte[] fileSaved, string fileName)
        {
            var updateFileDto = new TaskProgressEmployeeUploadFileDTO { FileSave = fileSaved, FileName = fileName };
            var patchJson = JsonSerializer.Serialize(updateFileDto);
            var content = new StringContent(patchJson, Encoding.UTF8, "application/json");
            return SendDataAsync($"{_baseUri}/{idContentWork}/file", new HttpMethod("PATCH"), content);
        }

        public Task<string> PatchStatusContentWork(int idContentWork, string result)
        {
            var updateFileDto = new TaskProgressEmployeeUpdateStatusDTO { results = result, endDateActual = DateTime.Now.ToString("yyyy-MM-dd") };
            var patchJson = JsonSerializer.Serialize(updateFileDto);
            var content = new StringContent(patchJson, Encoding.UTF8, "application/json");
            return SendDataAsync($"{_baseUri}/{idContentWork}/progressEmployee", new HttpMethod("PATCH"), content);
        }

        public Task<string> PatchNotification(int idContentWork) => SendDataAsync($"{_baseUri}/{idContentWork}/notificationNull", new HttpMethod("PATCH"));

        //------------------------------------------------------------------------------------------------------------------------
        // Cập nhật trạng thái cho Dự án lớn
        public Task<string> PatchStatusProject(string idProject, string status)
        {
            var content = new StringContent($"\"{status}\"", Encoding.UTF8, "application/json");
            return SendDataAsync($"{_baseUriProjectContent}/{idProject}/statusProject", new HttpMethod("PATCH"), content);
        }
        // Cập nhật trạng thái cho Nội dung cho Dự án lớn
        public Task<string> PatchStatusProjectContent(int idProjectContent, string status)
        {
            var content = new StringContent($"\"{status}\"", Encoding.UTF8, "application/json");
            return SendDataAsync($"{_baseUriProjectContent}/{idProjectContent}/statusProjectContent", new HttpMethod("PATCH"), content);
        }
        
    }
}
