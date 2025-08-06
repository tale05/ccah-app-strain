using Newtonsoft.Json;
namespace DTO
{
    public class ApiConditionalStrainDTO
    {
        [JsonProperty("idCondition")]
        public int idCondition { get; set; }

        [JsonProperty("medium")]
        public string medium { get; set; }

        [JsonProperty("temperature")]
        public string temperature { get; set; }

        [JsonProperty("lightIntensity")]
        public string lightIntensity { get; set; }

        [JsonProperty("duration")]
        public string duration { get; set; }
    }
}
