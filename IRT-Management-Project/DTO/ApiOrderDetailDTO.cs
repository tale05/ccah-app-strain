using Newtonsoft.Json;

namespace DTO
{
    public class ApiOrderDetailDTO
    {
        [JsonProperty("idOrderDetail")]
        public int idOrderDetail { get; set; }

        [JsonProperty("idOrder")]
        public int idOrder { get; set; }

        [JsonProperty("idStrain")]
        public int idStrain { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }

        [JsonProperty("price")]
        public int price { get; set; }
    }
}
