using DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ClientBill
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetBillUri();

        public ClientBill()
        {
            _httpClient = ApiConfig.Client;
        }

        private async Task<T> HandleRequestAsync<T>(Func<Task<HttpResponseMessage>> httpRequest)
        {
            try
            {
                HttpResponseMessage response = await httpRequest();
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred with the request: {e.Message}");
                return default;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"An unexpected error occurred: {e.Message}");
                return default;
            }
        }

        private async Task<string> HandleStringRequestAsync(Func<Task<HttpResponseMessage>> httpRequest)
        {
            try
            {
                HttpResponseMessage response = await httpRequest();
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

        public Task<List<ApiBillDTO>> GetAllBillsAsync()
        {
            return HandleRequestAsync<List<ApiBillDTO>>(() => _httpClient.GetAsync(_baseUri));
        }

        public Task<string> Post(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return HandleStringRequestAsync(() => _httpClient.PostAsync(_baseUri, content));
        }

        public Task<string> Delete(int idBill)
        {
            return HandleStringRequestAsync(() => _httpClient.DeleteAsync($"{_baseUri}/{idBill}"));
        }

        public Task<string> Update(int idBill, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return HandleStringRequestAsync(() => _httpClient.PutAsync($"{_baseUri}/{idBill}", content));
        }

        public Task<string> PatchStatus(string idBill, string status)
        {
            var content = new StringContent($"\"{status}\"", Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUri}/{idBill}/statusPay")
            {
                Content = content
            };
            return HandleStringRequestAsync(() => _httpClient.SendAsync(request));
        }
    }
}
