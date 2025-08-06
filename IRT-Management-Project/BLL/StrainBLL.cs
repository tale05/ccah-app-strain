using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API;
using DTO;

namespace BLL
{
    public class StrainBLL
    {
        ClientStrain clientStrain;
        ClientSpecies clientSpecies;
        ClientGenus clientGenus;
        ClientClass clientClass;
        ClientPhylum clientPhylum;
        ClientConditionalStrain clientConditionalStrain;
        ClientStrainApprovalHistory clientStrainApprovalHistory;
        public StrainBLL()
        {
            clientStrain = new ClientStrain();
            clientSpecies = new ClientSpecies();
            clientGenus = new ClientGenus();
            clientClass = new ClientClass();
            clientPhylum = new ClientPhylum();
            clientConditionalStrain = new ClientConditionalStrain();
            clientStrainApprovalHistory = new ClientStrainApprovalHistory();
        }
        public async Task<List<StrainCustomExcelDTO>> GetDataToExportExcel()
        {
            try
            {
                var strainTask = clientStrain.GetAllStrainsAsync();
                var speciesTask = clientSpecies.GetAllSpeciesAsync();
                var conditionTask = clientConditionalStrain.GetAllConditionalStrainAsync();
                var strainApprovalTask = clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();

                await Task.WhenAll(strainTask, speciesTask, conditionTask, strainApprovalTask);

                var strain = await strainTask;
                var species = await speciesTask;
                var condition = await conditionTask;
                var strainApproval = await strainApprovalTask;

                var query = (from s in strain
                             join sp in species on s.idSpecies equals sp.idSpecies
                             join co in condition on s.idCondition equals co.idCondition
                             join sa in strainApproval on s.idStrain equals sa.idStrain
                             select new StrainCustomExcelDTO
                             {
                                 idStrain = s.idStrain,
                                 strainNumber = s.strainNumber,
                                 nameSpecies = sp.nameSpecies,
                                 scientificName = s.scientificName,
                                 synonymStrain = s.synonymStrain,
                                 formerName = s.formerName,
                                 commonName = s.commonName,
                                 cellSize = s.cellSize,
                                 organization = s.organization,
                                 medium = co.medium,
                                 temperature = co.temperature,
                                 lightIntensity = co.lightIntensity,
                                 duration = co.duration,
                                 characteristics = s.characteristics,
                                 collectionSite = s.collectionSite,
                                 continent = s.continent,
                                 country = s.country,
                                 isolationSource = s.isolationSource,
                                 toxinProducer = s.toxinProducer,
                                 stateOfStrain = s.stateOfStrain,
                                 agitationResistance = s.agitationResistance,
                                 remarks = s.remarks,
                                 geneInformation = s.geneInformation,
                                 publications = s.publications,
                                 recommendedForTeaching = s.recommendedForTeaching,
                                 dateAdd = s.dateAdd,
                                 dateApproval = sa.dateApproval,
                                 status = sa.status,
                             }).ToList();
                return query;

            }
            catch (Exception)
            {
                return new List<StrainCustomExcelDTO>();
            }
        }
        public async Task<StrainCustomDetailDTO> LoadFullProperties(string strainNumberInput)
        {
            try
            {
                var strainTask = clientStrain.GetAllStrainsAsync();
                var speciesTask = clientSpecies.GetAllSpeciesAsync();
                var genusTask = clientGenus.GetAllGenusAsync();
                var classTask = clientClass.GetAllClassesAsync();
                var phylumTask = clientPhylum.GetAllPhylumsAsync();
                var conditionTask = clientConditionalStrain.GetAllConditionalStrainAsync();
                var strainApprovals = clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();

                await Task.WhenAll(strainTask, speciesTask, genusTask, classTask, phylumTask, conditionTask, strainApprovals);

                var strains = await strainTask;
                var species = await speciesTask;
                var genus = await genusTask;
                var classes = await classTask;
                var phylum = await phylumTask;
                var condition = await conditionTask;
                var strainHistory = await strainApprovals;

                var query = (from st in strains
                             join sp in species on st.idSpecies equals sp.idSpecies
                             join ge in genus on sp.idGenus equals ge.idGenus
                             join cs in classes on ge.idClass equals cs.idClass
                             join ph in phylum on cs.idPhylum equals ph.idPhylum
                             join co in condition on st.idCondition equals co.idCondition
                             join sh in strainHistory on st.idStrain equals sh.idStrain
                             where st.strainNumber.Equals(strainNumberInput) && sh.status.Equals("Đã được duyệt")
                             select new StrainCustomDetailDTO
                             {
                                 StrainNumber = st.strainNumber.Trim(),
                                 nameSpecies = sp.nameSpecies.Trim(),
                                 nameGenus = ge.nameGenus.Trim(),
                                 nameClass = cs.nameClass.Trim(),
                                 namePhylum = ph.namePhylum.Trim(),
                                 mediumCondition = co.medium.Trim(),
                                 temperatureCondition = co.temperature.Trim(),
                                 lightIntensityCondition = co.lightIntensity.Trim(),
                                 durationCondition = co.duration.Trim(),
                                 ScientificName = st.scientificName.Trim(),
                                 SynonymStrain = st.synonymStrain.Trim(),
                                 FormerName = st.formerName.Trim(),
                                 CommonName = st.commonName.Trim(),
                                 CellSize = st.cellSize.Trim(),
                                 Organization = st.organization.Trim(),
                                 Characteristics = st.characteristics.Trim(),
                                 CollectionSite = st.collectionSite.Trim(),
                                 Continent = st.continent.Trim(),
                                 Country = st.country.Trim(),
                                 IsolationSource = st.isolationSource.Trim(),
                                 ToxinProducer = st.toxinProducer.Trim(),
                                 StateOfStrain = st.stateOfStrain.Trim(),
                                 AgitationResistance = st.agitationResistance.Trim(),
                                 Remarks = st.remarks.Trim(),
                                 GeneInformation = st.geneInformation.Trim(),
                                 Publications = st.publications.Trim(),
                                 RecommendedForTeaching = st.recommendedForTeaching.Trim(),
                                 Status = sh.status.Trim(),
                                 ImageStrain = st.imageStrain?.ToArray()
                             }).FirstOrDefault();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<StrainCustomWithIdStrainDTO>> LoadData()
        {
            try
            {
                var strainTask = clientStrain.GetAllStrainsAsync();
                var speciesTask = clientSpecies.GetAllSpeciesAsync();
                var genusTask = clientGenus.GetAllGenusAsync();
                var classTask = clientClass.GetAllClassesAsync();
                var phylumTask = clientPhylum.GetAllPhylumsAsync();
                var conditionTask = clientConditionalStrain.GetAllConditionalStrainAsync();
                var strainHistoryTask = clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();

                await Task.WhenAll(strainTask, speciesTask, genusTask, classTask, phylumTask, conditionTask, strainHistoryTask);

                var strains = await strainTask;
                var species = await speciesTask;
                var genus = await genusTask;
                var classes = await classTask;
                var phylum = await phylumTask;
                var condition = await conditionTask;
                var strainHistory = await strainHistoryTask;

                var query = from s in strains
                            join sp in species on s.idSpecies equals sp.idSpecies
                            join ge in genus on sp.idGenus equals ge.idGenus
                            join cs in classes on ge.idClass equals cs.idClass
                            join ph in phylum on cs.idPhylum equals ph.idPhylum
                            join co in condition on s.idCondition equals co.idCondition
                            join sh in strainHistory on s.idStrain equals sh.idStrain
                            where sh.status.Equals("Đã được duyệt")
                            select new StrainCustomWithIdStrainDTO
                            {
                                idStrain = s.idStrain,
                                strainNumber = s.strainNumber.Trim(),
                                nameSpecies = sp.nameSpecies.Trim(),
                                nameGenus = ge.nameGenus.Trim(),
                                nameClass = cs.nameClass.Trim(),
                                namePhylum = ph.namePhylum.Trim(),
                                imageStrain = s.imageStrain?.ToArray(),
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<StrainCustomWithIdStrainDTO>();
            }
        }
        public async Task<List<StrainCustomWithIdStrainDTO>> SearchData(string strainNumber)
        {
            try
            {
                var strainTask = clientStrain.GetAllStrainsAsyncNormal();
                var speciesTask = clientSpecies.GetAllSpeciesAsync();
                var genusTask = clientGenus.GetAllGenusAsync();
                var classTask = clientClass.GetAllClassesAsync();
                var phylumTask = clientPhylum.GetAllPhylumsAsync();

                await Task.WhenAll(strainTask, speciesTask, genusTask, classTask, phylumTask);

                var strains = await strainTask;
                var species = await speciesTask;
                var genus = await genusTask;
                var classes = await classTask;
                var phylum = await phylumTask;

                var query = from s in strains
                            join sp in species on s.idSpecies equals sp.idSpecies
                            join ge in genus on sp.idGenus equals ge.idGenus
                            join cs in classes on ge.idClass equals cs.idClass
                            join ph in phylum on cs.idPhylum equals ph.idPhylum
                            where (s.strainNumber != null && s.strainNumber.Equals($"CCAH-{strainNumber}"))
                            select new StrainCustomWithIdStrainDTO
                            {
                                idStrain = s.idStrain,
                                strainNumber = s.strainNumber.Trim() ?? "",
                                nameSpecies = sp.nameSpecies.Trim() ?? "",
                                nameGenus = ge.nameGenus.Trim() ?? "",
                                nameClass = cs.nameClass.Trim() ?? "",
                                namePhylum = ph.namePhylum.Trim() ?? "",
                                imageStrain = s.imageStrain?.ToArray(),
                            };

                return query.ToList();
            }
            catch (Exception)
            {
                return new List<StrainCustomWithIdStrainDTO>();
            }
        }
        public async Task<List<StrainCustomWithIdStrainDTO>> FillDataByPhylum(string namePhylum)
        {
            try
            {
                var strainTask = clientStrain.GetAllStrainsAsync();
                var speciesTask = clientSpecies.GetAllSpeciesAsync();
                var genusTask = clientGenus.GetAllGenusAsync();
                var classTask = clientClass.GetAllClassesAsync();
                var phylumTask = clientPhylum.GetAllPhylumsAsync();
                var strainHistoryTask = clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();

                await Task.WhenAll(strainTask, speciesTask, genusTask, classTask, phylumTask, strainHistoryTask);

                var strains = await strainTask;
                var species = await speciesTask;
                var genus = await genusTask;
                var classes = await classTask;
                var phylum = await phylumTask;
                var strainHistory = await strainHistoryTask;

                var query = from s in strains
                            join sp in species on s.idSpecies equals sp.idSpecies
                            join ge in genus on sp.idGenus equals ge.idGenus
                            join cs in classes on ge.idClass equals cs.idClass
                            join ph in phylum on cs.idPhylum equals ph.idPhylum
                            join sh in strainHistory on s.idStrain equals sh.idStrain
                            where ph.namePhylum.Equals(namePhylum.Trim()) && s.strainNumber != null
                            select new StrainCustomWithIdStrainDTO
                            {
                                idStrain = s.idStrain,
                                strainNumber = s.strainNumber.Trim(),
                                nameSpecies = sp.nameSpecies.Trim(),
                                nameGenus = ge.nameGenus.Trim(),
                                nameClass = cs.nameClass.Trim(),
                                namePhylum = ph.namePhylum.Trim(),
                                imageStrain = s.imageStrain?.ToArray()
                            };

                return query.ToList();
            }
            catch (Exception)
            {
                return new List<StrainCustomWithIdStrainDTO>();
            }
        }
        public async Task<List<StrainCustomWithIdStrainDTO>> FillDataBySpecies(string nameSpecies)
        {
            try
            {
                var strainTask = clientStrain.GetAllStrainsAsync();
                var speciesTask = clientSpecies.GetAllSpeciesAsync();
                var genusTask = clientGenus.GetAllGenusAsync();
                var classTask = clientClass.GetAllClassesAsync();
                var phylumTask = clientPhylum.GetAllPhylumsAsync();
                var strainHistoryTask = clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();

                await Task.WhenAll(strainTask, speciesTask, genusTask, classTask, phylumTask, strainHistoryTask);

                var strains = await strainTask;
                var species = await speciesTask;
                var genus = await genusTask;
                var classes = await classTask;
                var phylum = await phylumTask;
                var strainHistory = await strainHistoryTask;

                var query = from s in strains
                            join sp in species on s.idSpecies equals sp.idSpecies
                            join ge in genus on sp.idGenus equals ge.idGenus
                            join cs in classes on ge.idClass equals cs.idClass
                            join ph in phylum on cs.idPhylum equals ph.idPhylum
                            join sh in strainHistory on s.idStrain equals sh.idStrain
                            where sp.nameSpecies.Equals(nameSpecies.Trim()) && s.strainNumber != null
                            select new StrainCustomWithIdStrainDTO
                            {
                                idStrain = s.idStrain,
                                strainNumber = s.strainNumber.Trim(),
                                nameSpecies = sp.nameSpecies.Trim(),
                                nameGenus = ge.nameGenus.Trim(),
                                nameClass = cs.nameClass.Trim(),
                                namePhylum = ph.namePhylum.Trim(),
                                imageStrain = s.imageStrain?.ToArray()
                            };

                return query.ToList();
            }
            catch (Exception)
            {
                return new List<StrainCustomWithIdStrainDTO>();
            }
        }
        public async Task<List<StrainCustomWithIdStrainDTO>> FillDataByClass(string nameClass)
        {
            try
            {
                var strainTask = clientStrain.GetAllStrainsAsync();
                var speciesTask = clientSpecies.GetAllSpeciesAsync();
                var genusTask = clientGenus.GetAllGenusAsync();
                var classTask = clientClass.GetAllClassesAsync();
                var phylumTask = clientPhylum.GetAllPhylumsAsync();
                var strainHistoryTask = clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();

                await Task.WhenAll(strainTask, speciesTask, genusTask, classTask, phylumTask, strainHistoryTask);

                var strains = await strainTask;
                var species = await speciesTask;
                var genus = await genusTask;
                var classes = await classTask;
                var phylum = await phylumTask;
                var strainHistory = await strainHistoryTask;

                var query = from s in strains
                            join sp in species on s.idSpecies equals sp.idSpecies
                            join ge in genus on sp.idGenus equals ge.idGenus
                            join cs in classes on ge.idClass equals cs.idClass
                            join ph in phylum on cs.idPhylum equals ph.idPhylum
                            join sh in strainHistory on s.idStrain equals sh.idStrain
                            where cs.nameClass.Equals(nameClass.Trim()) && s.strainNumber != null
                            select new StrainCustomWithIdStrainDTO
                            {
                                idStrain = s.idStrain,
                                strainNumber = s.strainNumber.Trim(),
                                nameSpecies = sp.nameSpecies.Trim(),
                                nameGenus = ge.nameGenus.Trim(),
                                nameClass = cs.nameClass.Trim(),
                                namePhylum = ph.namePhylum.Trim(),
                                imageStrain = s.imageStrain?.ToArray()
                            };

                return query.ToList();
            }
            catch (Exception)
            {
                return new List<StrainCustomWithIdStrainDTO>();
            }
        }
        public async Task<List<StrainCustomWithIdStrainDTO>> FillDataByGenus(string nameGenus)
        {
            try
            {
                var strainTask = clientStrain.GetAllStrainsAsync();
                var speciesTask = clientSpecies.GetAllSpeciesAsync();
                var genusTask = clientGenus.GetAllGenusAsync();
                var classTask = clientClass.GetAllClassesAsync();
                var phylumTask = clientPhylum.GetAllPhylumsAsync();
                var strainHistoryTask = clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();

                await Task.WhenAll(strainTask, speciesTask, genusTask, classTask, phylumTask, strainHistoryTask);

                var strains = await strainTask;
                var species = await speciesTask;
                var genus = await genusTask;
                var classes = await classTask;
                var phylum = await phylumTask;
                var strainHistory = await strainHistoryTask;

                var query = from s in strains
                            join sp in species on s.idSpecies equals sp.idSpecies
                            join ge in genus on sp.idGenus equals ge.idGenus
                            join cs in classes on ge.idClass equals cs.idClass
                            join ph in phylum on cs.idPhylum equals ph.idPhylum
                            join sh in strainHistory on s.idStrain equals sh.idStrain
                            where ge.nameGenus.Equals(nameGenus.Trim()) && s.strainNumber != null
                            select new StrainCustomWithIdStrainDTO
                            {
                                idStrain = s.idStrain,
                                strainNumber = s.strainNumber.Trim(),
                                nameSpecies = sp.nameSpecies.Trim(),
                                nameGenus = ge.nameGenus.Trim(),
                                nameClass = cs.nameClass.Trim(),
                                namePhylum = ph.namePhylum.Trim(),
                                imageStrain = s.imageStrain?.ToArray()
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to search strains", ex);
            }
        }
        public async Task<List<string>> getListPhylum()
        {
            try
            {
                var query = from s in await clientPhylum.GetAllPhylumsAsync() select s.namePhylum.Trim();
                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }
        public async Task<List<string>> getListClassByPhylum(string phylum)
        {
            try
            {
                var query = from cls in await clientClass.GetAllClassesAsync()
                            join phy in await clientPhylum.GetAllPhylumsAsync()
                            on cls.idPhylum equals phy.idPhylum
                            where phy.namePhylum.Trim() == phylum.Trim()
                            select cls.nameClass.Trim();
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get classes by phylum", ex);
            }
        }
        public async Task<List<string>> getListGenusByClass(string className)
        {
            try
            {
                var query = from ge in await clientGenus.GetAllGenusAsync()
                            join cl in await clientClass.GetAllClassesAsync()
                            on ge.idClass equals cl.idClass
                            where cl.nameClass.Trim() == className.Trim()
                            select ge.nameGenus.Trim();
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get genus by class", ex);
            }
        }
        public async Task<List<string>> getListSpeciesByGenus(string genus)
        {
            try
            {
                var query = from sp in await clientSpecies.GetAllSpeciesAsync()
                            join ge in await clientGenus.GetAllGenusAsync()
                            on sp.idGenus equals ge.idGenus
                            where ge.nameGenus.Trim() == genus.Trim()
                            select sp.nameSpecies.Trim();
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get species by genus", ex);
            }
        }
        public async Task<int> CountPhylums()
        {
            var count = await clientPhylum.GetAllPhylumsAsync();
            var query = count.GroupBy(p => p.namePhylum)
                                           .Select(group => group.First())
                                           .Count();

            return query;
        }
        public async Task<int> CountClass()
        {
            var count = await clientClass.GetAllClassesAsync();
            var query = count.GroupBy(p => p.nameClass)
                                           .Select(group => group.First())
                                           .Count();

            return query;
        }
        public async Task<int> CountGenus()
        {
            var count = await clientGenus.GetAllGenusAsync();
            var query = count.GroupBy(p => p.nameGenus)
                                           .Select(group => group.First())
                                           .Count();

            return query;
        }
        public async Task<int> CountSpecies()
        {
            var count = await clientSpecies.GetAllSpeciesAsync();
            var query = count.GroupBy(p => p.nameSpecies)
                                           .Select(group => group.First())
                                           .Count();

            return query;
        }
        public async Task<int> CountStrain()
        {
            var count = await clientStrain.GetAllStrainsAsync();
            var query = count.Count();

            return query;
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
        public async Task<string> UpdateStrainNumber(int id, string strainNumber)
        {
            try
            {
                return await clientStrain.PatchStrainNumber(id, strainNumber);
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
    }
}
