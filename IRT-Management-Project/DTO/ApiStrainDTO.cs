using Newtonsoft.Json;
using System;

namespace DTO
{
    public class ApiStrainDTO
    {
        [JsonProperty("idStrain")]
        public int idStrain { get; set; }

        [JsonProperty("strainNumber")]
        public string strainNumber { get; set; }

        [JsonProperty("idSpecies")]
        public int idSpecies { get; set; }

        [JsonProperty("idCondition")]
        public int idCondition { get; set; }

        [JsonProperty("imageStrain")]
        public byte[] imageStrain { get; set; }

        [JsonProperty("scientificName")]
        public string scientificName { get; set; }

        [JsonProperty("synonymStrain")]
        public string synonymStrain { get; set; }

        [JsonProperty("formerName")]
        public string formerName { get; set; }

        [JsonProperty("commonName")]
        public string commonName { get; set; }

        [JsonProperty("cellSize")]
        public string cellSize { get; set; }

        [JsonProperty("organization")]
        public string organization { get; set; }

        [JsonProperty("characteristics")]
        public string characteristics { get; set; }

        [JsonProperty("collectionSite")]
        public string collectionSite { get; set; }

        [JsonProperty("continent")]
        public string continent { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("isolationSource")]
        public string isolationSource { get; set; }

        [JsonProperty("toxinProducer")]
        public string toxinProducer { get; set; }

        [JsonProperty("stateOfStrain")]
        public string stateOfStrain { get; set; }

        [JsonProperty("agitationResistance")]
        public string agitationResistance { get; set; }

        [JsonProperty("remarks")]
        public string remarks { get; set; }

        [JsonProperty("geneInformation")]
        public string geneInformation { get; set; }

        [JsonProperty("publications")]
        public string publications { get; set; }

        [JsonProperty("recommendedForTeaching")]
        public string recommendedForTeaching { get; set; }

        [JsonProperty("dateAdd")]
        public string dateAdd { get; set; }
    }
}
