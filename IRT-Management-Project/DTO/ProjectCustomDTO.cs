using System;
using System.Diagnostics.Contracts;

namespace DTO
{
    public class ProjectCustomDTO
    {
        public string idProject { get; set; }
        public string fullName { get; set; }
        public string nameCompany { get; set; }
        public string projectName { get; set;}
        public string results { get; set;}
        public string startDateProject { get; set; }
        public string endDateProject { get; set; }
        public string contractNo { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string fileName { get; set; }
    }

    public class ProjectCustom1DTO
    {
        public string idProject { get; set; }
        public string fullName { get; set; }
        public string nameCompany { get; set; }
        public string projectName { get; set; }
        public string results { get; set; }
        public string startDateProject { get; set; }
        public string endDateProject { get; set; }
        public string contractNo { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}
