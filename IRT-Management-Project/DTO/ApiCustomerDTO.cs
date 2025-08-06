using Newtonsoft.Json;

namespace DTO
{
    public class ApiCustomerDTO
    {
        [JsonProperty("idCustomer")]
        public string idCustomer { get; set; }

        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("fullName")]
        public string fullName { get; set; }

        [JsonProperty("dateOfBirth")]
        public string dateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string gender { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("phoneNumber")]
        public string phoneNumber { get; set; }

        [JsonProperty("address")]
        public string address { get; set; }

        [JsonProperty("image")]
        public byte[] image { get; set; }

        [JsonProperty("nameWard")]
        public string nameWard { get; set; }

        [JsonProperty("nameDistrict")]
        public string nameDistrict { get; set; }

        [JsonProperty("nameProvince")]
        public string nameProvince { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }
    }
}
