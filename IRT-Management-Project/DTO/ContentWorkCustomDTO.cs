using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ContentWorkCustomDTO
    {
        public int idContentWork { get; set; }
        public string nameEmployee { get; set; }
        public string nameContent { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string contractNo { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
    }

    public class DetailContentWorkCustomDTO
    {
        public int idContentWork { get; set; }
        public string nameEmployee { get; set; }
        public string nameContent { get; set; }
        public string result { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string ennDateActual { get; set; }
        public string contractNo { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
    }

    public class DetailTableContentWorkDTO
    {
        public int idContentWork { get; set; }
        public string nameAndIdEmployee { get; set; }
        public string nameContent { get; set; }
        public string result { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string ennDateActual { get; set; }
        public string contractNo { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public string notification { get; set; }
        public int title { get; set; }
        public int subTitle { get; set; }
        public string histories { get; set; }
    }
}
