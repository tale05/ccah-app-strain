using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BillToExportPdfDTO
    {
        public string idBill { get; set; }
        public string dateBill { get; set; }
        public string status { get; set; }
        public float totalPrice { get; set; }
    }

    public class BillDetailToExportPdfDTO
    {
        public string strainNumber { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
    }
}
