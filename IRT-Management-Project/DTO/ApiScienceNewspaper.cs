using Newtonsoft.Json;

namespace DTO
{
    public class ApiScienceNewspaper
    {
        [JsonProperty("idNewspaper")]
        public int idNewspaper   { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("content")]
        public string content { get; set; }

        [JsonProperty("postDate")]
        public string postDate { get; set; }

        [JsonProperty("image")]
        public byte[] image { get; set; }

        [JsonProperty("idEmployee")]
        public string idEmployee { get; set; }

        [JsonProperty("content2")]
        public string content2 { get; set; }
    }
}