using System;
using Newtonsoft.Json;

namespace DTO
{
    public class ApiInventoryDTO
    {
        [JsonProperty("inventoryId")]
        public int inventoryId { get; set; }

        [JsonProperty("idStrain")]
        public int idStrain { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }

        [JsonProperty("price")]
        public decimal price { get; set; }

        [JsonProperty("entryDate")]
        public string entryDate { get; set; }

        [JsonProperty("histories")]
        public string histories { get; set; }
    }
}
