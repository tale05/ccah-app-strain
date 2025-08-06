using Newtonsoft.Json;

namespace DTO
{
    public class ApiBillDetailDTO
    {
        [JsonProperty("idBillDetail")]
        public int idBillDetail { get; set; }

        [JsonProperty("idBill")]
        public string idBill { get; set; }

        [JsonProperty("idStrain")]
        public int idStrain { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }
    }
}
