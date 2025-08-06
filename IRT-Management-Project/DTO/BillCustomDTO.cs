using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BillCustomDTO
    {
        public string idBill {  get; set; }
        public string customer {  get; set; }
        public string employee { get; set; }
        public string dateBill { get; set; }
        public string typeBill { get; set; }
        public string status { get; set; }
        public string totalPrice { get; set; }
    }
}