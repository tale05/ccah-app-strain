using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProjectContentCustomDTO
    {
        public int idProjectContent { get; set; }
        public string idNameProject { get; set; }
        public string nameContent { get; set; }
        public string results { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string contractNo { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
    }

    public class ProjectContentWithTitleDTO
    {
        public int idProjectContent { get; set; }
        public string idNameProject { get; set; }
        public string nameContent { get; set; }
        public string results { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string contractNo { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public int title {  get; set; }
    }
}
