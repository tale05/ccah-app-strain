using Newtonsoft.Json;

namespace DTO
{
    public class ApiIsolatorStrainDTO
    {
        [JsonProperty("iD_Employee")]
        public string iD_Employee { get; set; }

        [JsonProperty("iD_Strain")]
        public int iD_Strain { get; set; }

        [JsonProperty("yearOfIsolator")]
        public int yearOfIsolator { get; set; }
    }
}
