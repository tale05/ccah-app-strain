using System;
using Newtonsoft.Json;

namespace DTO
{
    public class ApiProjectDTO
    {
        [JsonProperty("idProject")]
        public string idProject { get; set; }

        [JsonProperty("idEmployee")]
        public string idEmployee { get; set; }

        [JsonProperty("idPartner")]
        public int idPartner { get; set; }

        [JsonProperty("projectName")]
        public string projectName { get; set; }

        [JsonProperty("results")]
        public string results { get; set; }

        [JsonProperty("startDateProject")]
        public string startDateProject { get; set; }

        [JsonProperty("endDateProject")]
        public string endDateProject { get; set; }

        [JsonProperty("contractNo")]
        public string contractNo { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("fileProject")]
        public byte[] fileProject { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("fileName")]
        public string fileName { get; set; }
    }
}
