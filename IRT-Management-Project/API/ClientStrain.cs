using DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ClientStrain
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri_NoPaging = ApiConfig.GetStrainUriNoPaging();
        private readonly string _baseUri_Paging = ApiConfig.GetStrainUri();

        public ClientStrain()
        {
            _httpClient = ApiConfig.Client;
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

        private async Task<string> HandleStringRequestAsync(Func<Task<HttpResponseMessage>> request)
        {
            try
            {
                HttpResponseMessage response = await request();
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"An unexpected error occurred: {e.Message}");
                return null;
            }
        }

        public Task<List<ApiStrainDTO>> GetAllStrainsAsync()
        {
            return HandleRequestAsync<List<ApiStrainDTO>>(() => _httpClient.GetAsync(_baseUri_NoPaging));
        }

        public Task<List<ApiStrainDTO>> GetAllStrainsAsyncNormal()
        {
            return HandleRequestAsync<List<ApiStrainDTO>>(() => _httpClient.GetAsync(_baseUri_NoPaging));
        }

        public Task<string> Post(string strainJson)
        {
            var content = new StringContent(strainJson, Encoding.UTF8, "application/json");
            return HandleStringRequestAsync(() => _httpClient.PostAsync(_baseUri_Paging, content));
        }

        public Task<string> Update(int id, string strainJson)
        {
            var content = new StringContent(strainJson, Encoding.UTF8, "application/json");
            return HandleStringRequestAsync(() => _httpClient.PutAsync($"{_baseUri_Paging}/{id}", content));
        }

        public Task<string> PatchStrainNumber(int id, string strainNumber)
        {
            var content = new StringContent($"\"{strainNumber}\"", Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUri_Paging}/{id}/StrainNumber")
            {
                Content = content
            };

            return HandleStringRequestAsync(() => _httpClient.SendAsync(request));
        }

        public Task<string> PatchImageStrain(int idStrain, byte[] img)
        {
            string jsonPayload = img != null
                ? $"{{ \"imageStrain\": \"{Convert.ToBase64String(img)}\" }}"
                : "{ \"imageStrain\": null }";

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUri_Paging}/{idStrain}/imageStrain")
            {
                Content = content
            };

            return HandleStringRequestAsync(() => _httpClient.SendAsync(request));
        }
    }
}
