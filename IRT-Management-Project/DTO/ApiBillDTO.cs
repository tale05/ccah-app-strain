using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ApiBillDTO
    {
        [JsonProperty("idBill")]
        public string idBill { get; set; }

        [JsonProperty("idOrder")]
        public int idOrder { get; set; }

        [JsonProperty("idCustomer")]
        public string idCustomer { get; set; }

        [JsonProperty("idEmployee")]
        public string idEmployee { get; set; }

        [JsonProperty("billDate")]
        public string billDate { get; set; }

        [JsonProperty("statusOfBill")]
        public string statusOfBill { get; set; }

        [JsonProperty("typeOfBill")]
        public string typeOfBill { get; set; }

        [JsonProperty("totalPrice")]
        public float totalPrice { get; set; }
    }
}
