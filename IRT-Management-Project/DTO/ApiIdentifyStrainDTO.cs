using Newtonsoft.Json;

namespace DTO
{
    public class ApiIdentifyStrainDTO
    {
        [JsonProperty("iD_Employee")]
        public string iD_Employee {  get; set; }

        [JsonProperty("iD_Strain")]
        public int iD_Strain { get; set; }

        [JsonProperty("year_of_Identify")]
        public int year_of_Identify { get; set; }
    }
}
