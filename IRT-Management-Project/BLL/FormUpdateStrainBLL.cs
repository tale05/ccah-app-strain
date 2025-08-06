using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormUpdateStrainBLL
    {
        ClientStrain clientStrain;
        ClientConditionalStrain clientConditionalStrain;
        ClientSpecies clientSpecies;
        public FormUpdateStrainBLL()
        {
            clientStrain = new ClientStrain();
            clientSpecies = new ClientSpecies();
            clientConditionalStrain = new ClientConditionalStrain();
        }
        public async Task<List<ApiStrainDTO>> GetData()
        {
            try
            {
                var query = (from s 
                             in await clientStrain.GetAllStrainsAsync()
                             where s.strainNumber != null 
                             select s).ToList();
                return query;
            }
            catch (Exception)
            {
                return new List<ApiStrainDTO>();
            }
        }
        public async Task<byte[]> GetDataImageById(int id)
        {
            try
            {
                var query = (from s
                             in await clientStrain.GetAllStrainsAsync()
                             where s.strainNumber != null && s.idStrain == id
                             select s.imageStrain).FirstOrDefault();
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<string>> getListSpecies()
        {
            try
            {
                var query = from s in await clientSpecies.GetAllSpeciesAsync() select s.nameSpecies.Trim();
                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }
        public async Task<List<string>> getListIdConditionalStrain()
        {
            try
            {
                var query = from s in await clientConditionalStrain.GetAllConditionalStrainAsync() select s.idCondition.ToString().Trim();
                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }
        public async Task<string> GetNameSpeciesById(int idSpecies)
        {
            try
            {
                var species = (from sp
                              in await clientSpecies.GetAllSpeciesAsync()
                               where sp.idSpecies.Equals(idSpecies)
                               select sp.nameSpecies).FirstOrDefault();
                return species;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return string.Empty;
            }
        }
        public async Task<ApiConditionalStrainDTO> GetConditionByName(int idCondition)
        {
            try
            {
                var condition = await clientConditionalStrain.GetAllConditionalStrainAsync();

                var query = (from co in condition
                             where co.idCondition == idCondition
                             select new ApiConditionalStrainDTO
                             {
                                 idCondition = co.idCondition,
                                 medium = co.medium,
                                 temperature = co.temperature,
                                 lightIntensity = co.lightIntensity,
                                 duration = co.duration,
                             }).FirstOrDefault();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> UpdateStrain(int id, string json)
        {
            try
            {
                return await clientStrain.Update(id, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return string.Empty;
            }
        }
        public async Task<int> GetIdSpeciesByName(string name)
        {
            try
            {
                var species = (from sp
                              in await clientSpecies.GetAllSpeciesAsync()
                               where sp.nameSpecies.Equals(name)
                               select sp.idSpecies).FirstOrDefault();
                return species;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return 0;
            }
        }
        public async Task<ApiConditionalStrainDTO> ViewDetailConditionalStrain(int idCondition)
        {
            try
            {
                var condition = await clientConditionalStrain.GetAllConditionalStrainAsync();

                var query = (from co in condition
                             where co.idCondition == idCondition
                             select new ApiConditionalStrainDTO
                             {
                                 idCondition = co.idCondition,
                                 medium = co.medium,
                                 temperature = co.temperature,
                                 lightIntensity = co.lightIntensity,
                                 duration = co.duration,
                             }).FirstOrDefault();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
