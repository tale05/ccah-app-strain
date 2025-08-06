using System;
using Newtonsoft.Json;

namespace DTO
{
    public class ApiProjectContentDTO
    {
        [JsonProperty("idProjectContent")]
        public int idProjectContent { get; set; }

        [JsonProperty("idProject")]
        public string idProject { get; set; }

        [JsonProperty("nameContent")]
        public string nameContent { get; set; }

        [JsonProperty("results")]
        public string results { get; set; }

        [JsonProperty("startDate")]
        public string startDate { get; set; }

        [JsonProperty("endDate")]
        public string endDate { get; set; }

        [JsonProperty("contractNo")]
        public string contractNo { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("priority")]
        public string priority { get; set; }

        [JsonProperty("title")]
        public int title { get; set; }
    }
}
