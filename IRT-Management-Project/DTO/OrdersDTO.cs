using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrdersDTO
    {
        public int idOrder { get; set; }
        public string idCustomer { get; set; }
        public string nameCustomer { get; set; }
        public string idEmployee { get; set; }
        public string nameEmployee { get; set; }
        public string dateOrder { get; set; }
        public float totalPrice { get; set; }
        public string status { get; set; }
        public string deliveryAddress { get; set; }
        public string note { get; set; }
        public string paymentMethod { get; set; }
        public string statusOrder { get; set; }
    }
}
