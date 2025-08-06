using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StrainCustomWithIdDetailDTO
    {
        public int idStrain { get; set; }
        public string StrainNumber { get; set; }
        public string namePhylum { get; set; }
        public string nameClass { get; set; }
        public string nameGenus { get; set; }
        public string nameSpecies { get; set; }
        public string mediumCondition { get; set; }
        public string temperatureCondition { get; set; }
        public string lightIntensityCondition { get; set; }
        public string durationCondition { get; set; }
        public byte[] ImageStrain { get; set; }
        public string ScientificName { get; set; }
        public string SynonymStrain { get; set; }
        public string FormerName { get; set; }
        public string CommonName { get; set; }
        public string CellSize { get; set; }
        public string Organization { get; set; }
        public string Characteristics { get; set; }
        public string CollectionSite { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string IsolationSource { get; set; }
        public string ToxinProducer { get; set; }
        public string StateOfStrain { get; set; }
        public string AgitationResistance { get; set; }
        public string Remarks { get; set; }
        public string GeneInformation { get; set; }
        public string Publications { get; set; }
        public string RecommendedForTeaching { get; set; }
        public string Status { get; set; }
        public string NameYearIdentify { get; set; }
    }
}
