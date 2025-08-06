using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormAddOneStrainBLL
    {
        ClientStrain clientStrain;
        ClientSpecies clientSpecies;
        ClientIdentifyStrain clientIdentifyStrain;
        ClientIsolatorStrain clientIsolatorStrain;
        ClientConditionalStrain clientConditionalStrain;
        ClientStrainApprovalHistory clientStrainApprovalHistory;
        public FormAddOneStrainBLL()
        {
            clientStrain = new ClientStrain();
            clientSpecies = new ClientSpecies();
            clientIdentifyStrain = new ClientIdentifyStrain();
            clientIsolatorStrain = new ClientIsolatorStrain();
            clientConditionalStrain = new ClientConditionalStrain();
            clientStrainApprovalHistory = new ClientStrainApprovalHistory();
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
                var query = from s 
                            in await clientConditionalStrain.GetAllConditionalStrainAsync() 
                            select $"{s.idCondition}, {s.medium ?? ""}, {s.temperature ?? ""}, {s.duration ?? ""}, {s.lightIntensity ?? ""}";
                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }
        public async Task<string> getListIdConditionalStrainByMedium(int id)
        {
            try
            {
                var query = from s
                            in await clientConditionalStrain.GetAllConditionalStrainAsync()
                            where s.idCondition == id
                            select $"{s.idCondition}, {s.medium ?? ""}, {s.temperature ?? ""}, {s.duration ?? ""}, {s.lightIntensity ?? ""}";
                return query.FirstOrDefault().ToString() ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return string.Empty;
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
                                 medium = co.medium ?? "",
                                 temperature = co.temperature ?? "",
                                 lightIntensity = co.lightIntensity ?? "",
                                 duration = co.duration ?? "",
                             }).FirstOrDefault();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
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
        public async Task<string> AddData(string json)
        {
            try
            {
                return await clientStrain.Post(json);
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
        public async Task<int> GetIdFromLastRecord()
        {
            try
            {
                var query = (from st
                             in await clientStrain.GetAllStrainsAsync()
                             orderby st.idStrain descending
                             select st.idStrain).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return 0;
            }
        }
        public async Task<string> AddDataStrainHistory(string json)
        {
            try
            {
                return await clientStrainApprovalHistory.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> AddDataIdentifyStrain(string json)
        {
            try
            {
                return await clientIdentifyStrain.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> AddDataIsolatorStrain(string json)
        {
            try
            {
                return await clientIsolatorStrain.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
