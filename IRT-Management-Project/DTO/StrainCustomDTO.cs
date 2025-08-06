using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StrainCustomDTO
    {
        public string strainNumber { get; set; }
        public string nameSpecies { get; set; }
        public string nameGenus { get; set; }
        public string nameClass { get; set; }
        public string namePhylum { get; set; }
        public byte[] imageStrain { get; set; }
    }

    public class StrainCustomWithIdStrainDTO
    {
        public int idStrain { get; set; }
        public string strainNumber { get; set; }
        public string nameSpecies { get; set; }
        public string nameGenus { get; set; }
        public string nameClass { get; set; }
        public string namePhylum { get; set; }
        public byte[] imageStrain { get; set; }
    }

    public class ListStrainDTO
    {
        public int idStrain { get; set; }
        public string strainNumber { get; set; }
    }
}
