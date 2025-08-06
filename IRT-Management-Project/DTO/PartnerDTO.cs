using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PartnerDTO
    {
        public int idPartner { get; set; }
        public string nameCompany { get; set; }
        public string addressCompany { get; set; }
        public string namePartner { get; set; }
        public string position { get; set; }
        public string phoneNumber { get; set; }
        public string bankNumber { get; set; }
        public string bankName { get; set; }
        public string qhnsNumber { get; set; }
        public string nameWard { get; set; }
        public string nameDistrict { get; set; }
        public string nameProvince { get; set; }
    }
}
