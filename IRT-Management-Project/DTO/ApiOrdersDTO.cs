using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ApiOrdersDTO
    {
        [JsonProperty("idOrder")]
        public int idOrder { get; set; }

        [JsonProperty("idCustomer")]
        public string idCustomer { get; set; }

        [JsonProperty("idEmployee")]
        public string idEmployee { get; set; }

        [JsonProperty("dateOrder")]
        public string dateOrder { get; set; }

        [JsonProperty("totalPrice")]
        public float totalPrice { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("deliveryAddress")]
        public string deliveryAddress { get; set; }

        [JsonProperty("note")]
        public string note { get; set; }

        [JsonProperty("paymentMethod")]
        public string paymentMethod { get; set; }

        [JsonProperty("statusOrder")]
        public string statusOrder { get; set; }
    }
}
