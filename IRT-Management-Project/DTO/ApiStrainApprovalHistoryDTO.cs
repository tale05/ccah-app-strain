using Newtonsoft.Json;
using System;

namespace DTO
{
    public class ApiStrainApprovalHistoryDTO
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("idStrain")]
        public int idStrain { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("dateApproval")]
        public string dateApproval { get; set; }

        [JsonProperty("reason")]
        public object reason { get; set; }
    }
}
