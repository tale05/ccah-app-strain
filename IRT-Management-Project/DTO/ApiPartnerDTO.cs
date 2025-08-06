using System;
using Newtonsoft.Json;

namespace DTO
{
    public class ApiPartnerDTO
    {
        [JsonProperty("idPartner")]
        public int idPartner { get; set; }

        [JsonProperty("nameCompany")]
        public string nameCompany { get; set; }

        [JsonProperty("addressCompany")]
        public string addressCompany { get; set; }

        [JsonProperty("namePartner")]
        public string namePartner { get; set; }

        [JsonProperty("position")]
        public string position { get; set; }

        [JsonProperty("phoneNumber")]
        public string phoneNumber { get; set; }

        [JsonProperty("bankNumber")]
        public string bankNumber { get; set; }

        [JsonProperty("bankName")]
        public string bankName { get; set; }

        [JsonProperty("qhnsNumber")]
        public string qhnsNumber { get; set; }

        [JsonProperty("nameWard")]
        public string nameWard { get; set; }

        [JsonProperty("nameDistrict")]
        public string nameDistrict { get; set; }

        [JsonProperty("nameProvince")]
        public string nameProvince { get; set; }
    }
}