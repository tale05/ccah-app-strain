using DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ClientBillDetail
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = ApiConfig.GetBillDetailUri();

        public ClientBillDetail()
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

        public Task<List<ApiBillDetailDTO>> GetAllBillsDetailAsync()
        {
            return HandleRequestAsync<List<ApiBillDetailDTO>>(() => _httpClient.GetAsync(_baseUri));
        }

        public Task<string> Post(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return HandleStringRequestAsync(() => _httpClient.PostAsync(_baseUri, content));
        }

        public Task<string> Delete(int idBillDetail)
        {
            return HandleStringRequestAsync(() => _httpClient.DeleteAsync($"{_baseUri}/{idBillDetail}"));
        }

        public Task<string> Update(int idBillDetail, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return HandleStringRequestAsync(() => _httpClient.PutAsync($"{_baseUri}/{idBillDetail}", content));
        }
    }
}
