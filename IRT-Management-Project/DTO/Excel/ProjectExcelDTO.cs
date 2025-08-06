using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Excel
{
    public class ProjectExcelDTO
    {
        public string idProject { get; set; }
        public string idAndNameEmployee { get; set; }
        public string nameCompany { get; set; }
        public string nameProject { get; set; }
        public string result { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string contractNo { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}
