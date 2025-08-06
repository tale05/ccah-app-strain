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
    public class ClientClass
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;
        private readonly ClientPhylum _clientPhylum;

        public ClientClass()
        {
            _httpClient = ApiConfig.Client;
            _baseUri = ApiConfig.GetClassUri();
            _clientPhylum = new ClientPhylum();
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

        public Task<List<ApiClassesDTO>> GetAllClassesAsync()
        {
            return HandleRequestAsync<List<ApiClassesDTO>>(() => _httpClient.GetAsync(_baseUri));
        }

        public async Task<List<ClassPhylumDTO>> GetClassesWithPhylumAsync()
        {
            var phylums = await _clientPhylum.GetAllPhylumsAsync();
            var classes = await GetAllClassesAsync();

            var result = from cl in classes
                         join ph in phylums on cl.idPhylum equals ph.idPhylum
                         select new ClassPhylumDTO
                         {
                             idClass = cl.idClass,
                             nameClass = cl.nameClass,
                             namePhylum = ph.namePhylum
                         };

            return result.ToList();
        }
    }
}
