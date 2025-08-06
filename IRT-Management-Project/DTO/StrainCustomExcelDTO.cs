using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StrainCustomExcelDTO
    {
        public int idStrain { get; set; }
        public string strainNumber { get; set; }
        public string nameSpecies { get; set; }
        public string scientificName { get; set; }
        public string synonymStrain { get; set; }
        public string formerName { get; set; }
        public string commonName { get; set; }
        public string cellSize { get; set; }
        public string organization { get; set; }
        public string medium { get; set; }
        public string temperature { get; set; }
        public string lightIntensity { get; set; }
        public string duration { get; set; }
        public string characteristics { get; set; }
        public string collectionSite { get; set; }
        public string continent { get; set; }
        public string country { get; set; }
        public string isolationSource { get; set; }
        public string toxinProducer { get; set; }
        public string stateOfStrain { get; set; }
        public string agitationResistance { get; set; }
        public string remarks { get; set; }
        public string geneInformation { get; set; }
        public string publications { get; set; }
        public string recommendedForTeaching { get; set; }
        public string dateAdd { get; set; }
        public string dateApproval { get; set; }
        public string status { get; set; }
    }
}
