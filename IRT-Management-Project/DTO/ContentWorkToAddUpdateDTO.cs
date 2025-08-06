using Newtonsoft.Json;

namespace DTO
{
    public class ContentWorkToAddUpdateDTO
    {
        [JsonProperty("idContentWork")]
        public int idContentWork { get; set; }

        [JsonProperty("idProjectContent")]
        public int idProjectContent { get; set; }

        [JsonProperty("idEmployee")]
        public string idEmployee { get; set; }

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

        [JsonProperty("endDateActual")]
        public string endDateActual { get; set; }

        [JsonProperty("notificattion")]
        public string notificattion { get; set; }

        [JsonProperty("title")]
        public int title { get; set; }

        [JsonProperty("subTitle")]
        public int subTitle { get; set; }

        [JsonProperty("histories")]
        public string histories { get; set; }
    }
}
