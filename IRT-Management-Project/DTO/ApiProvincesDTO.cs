using Newtonsoft.Json;

namespace DTO
{
    public class ApiProvincesDTO
    {
        [JsonProperty("idProvinces")]
        public int idProvinces { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }
    }

    public class DistrictDTO
    {
        [JsonProperty("idDistricts")]
        public int idDistricts { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("idProvinces")]
        public int idProvinces { get; set; }
    }

    public class WardDTO
    {
        [JsonProperty("idWards")]
        public int idWards { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("idDistricts")]
        public int idDistricts { get; set; }
    }
}
