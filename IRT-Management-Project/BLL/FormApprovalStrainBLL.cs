using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormApprovalStrainBLL
    {
        ClientStrain clientStrain;
        ClientSpecies clientSpecies;
        ClientGenus clientGenus;
        ClientClass clientClass;
        ClientPhylum clientPhylum;
        ClientConditionalStrain clientConditionalStrain;
        ClientStrainApprovalHistory clientStrainApprovalHistory;

        ClientIdentifyStrain clientIdentifyStrain;
        ClientIsolatorStrain clientIsolatorStrain;
        ClientEmployee clientEmployee;

        ClientInventory clientInventory;
        public FormApprovalStrainBLL()
        {
            clientStrain = new ClientStrain();
            clientSpecies = new ClientSpecies();
            clientGenus = new ClientGenus();
            clientClass = new ClientClass();
            clientPhylum = new ClientPhylum();
            clientConditionalStrain = new ClientConditionalStrain();
            clientStrainApprovalHistory = new ClientStrainApprovalHistory();

            clientIdentifyStrain = new ClientIdentifyStrain();
            clientIsolatorStrain = new ClientIsolatorStrain();
            clientEmployee = new ClientEmployee();

            clientInventory = new ClientInventory();
        }
        public async Task<List<ApiStrainApprovalHistoryDTO>> LoadData()
        {
            try
            {
                var allHistories = await clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();
                var query = from sh in allHistories
                            where sh.status.Equals("Đang chờ xét duyệt", StringComparison.OrdinalIgnoreCase)
                            select sh;
                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<ApiStrainApprovalHistoryDTO>();
            }
        }
        public async Task<List<ApiStrainApprovalHistoryDTO>> LoadDataBelongToEmployee(string idEmployee)
        {
            try
            {
                var allIdentifyStrainsTask = clientIsolatorStrain.GetAllIsolatorStrainAsync();
                var allHistoriesTask = clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();
                await Task.WhenAll(allIdentifyStrainsTask, allHistoriesTask);
                var allIdentifyStrains = await allIdentifyStrainsTask;
                var allHistories = await allHistoriesTask;

                var query = from sh in allHistories
                            join ids in allIdentifyStrains on sh.idStrain equals ids.iD_Strain
                            where sh.status.Equals("Đang chờ xét duyệt", StringComparison.OrdinalIgnoreCase)
                                    && ids.iD_Employee.Equals(idEmployee)
                            select sh;
                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<ApiStrainApprovalHistoryDTO>();
            }
        }
        public async Task<string> UpdateDataStrainHistory(int id, string json)
        {
            try
            {
                return await clientStrainApprovalHistory.Update(id, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<StrainCustomDetailDTO> LoadFullProperties(int idStrain)
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
                             where st.idStrain == idStrain
                             select new StrainCustomDetailDTO
                             {
                                 nameSpecies = CheckNullOrEmpty(sp.nameSpecies),
                                 nameGenus = CheckNullOrEmpty(ge.nameGenus),
                                 nameClass = CheckNullOrEmpty(cs.nameClass),
                                 namePhylum = CheckNullOrEmpty(ph.namePhylum),
                                 mediumCondition = CheckNullOrEmpty(co.medium),
                                 temperatureCondition = CheckNullOrEmpty(co.temperature),
                                 lightIntensityCondition = CheckNullOrEmpty(co.lightIntensity),
                                 durationCondition = CheckNullOrEmpty(co.duration),
                                 ScientificName = CheckNullOrEmpty(st.scientificName),
                                 SynonymStrain = CheckNullOrEmpty(st.synonymStrain),
                                 FormerName = CheckNullOrEmpty(st.formerName),
                                 CommonName = CheckNullOrEmpty(st.commonName),
                                 CellSize = CheckNullOrEmpty(st.cellSize),
                                 Organization = CheckNullOrEmpty(st.organization),
                                 Characteristics = CheckNullOrEmpty(st.characteristics),
                                 CollectionSite = CheckNullOrEmpty(st.collectionSite),
                                 Continent = CheckNullOrEmpty(st.continent),
                                 Country = CheckNullOrEmpty(st.country),
                                 IsolationSource = CheckNullOrEmpty(st.isolationSource),
                                 ToxinProducer = CheckNullOrEmpty(st.toxinProducer),
                                 StateOfStrain = CheckNullOrEmpty(st.stateOfStrain),
                                 AgitationResistance = CheckNullOrEmpty(st.agitationResistance),
                                 Remarks = CheckNullOrEmpty(st.remarks),
                                 GeneInformation = CheckNullOrEmpty(st.geneInformation),
                                 Publications = CheckNullOrEmpty(st.publications),
                                 RecommendedForTeaching = CheckNullOrEmpty(st.recommendedForTeaching),
                                 Status = CheckNullOrEmpty(sh.status),
                                 ImageStrain = st.imageStrain,
                                 DateAdd = DateTime.Parse(CheckNullOrEmpty(st.dateAdd)).ToString("dd/MM/yyyy"),
                                 IdConditionStrain = co.idCondition,
                             }).FirstOrDefault();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        private string CheckNullOrEmpty(string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
        }
        //public async Task<string> GetNameAndYearIdentify(int idStrain)
        //{
        //    try
        //    {
        //        return (from id in await clientIdentifyStrain.GetAllIdentifyStrainAsync()
        //                join em in await clientEmployee.GetAllEmployeeAsync() on id.iD_Employee equals em.IdEmployee
        //                where id.iD_Strain == idStrain
        //                select $"{em.FullName} - {id.year_of_Identify}"
        //                ).FirstOrDefault().ToString().Trim() ?? "";
        //    }
        //    catch (Exception)
        //    {
        //        return string.Empty;
        //    }
        //}
        public async Task<string> GetNameAndYearIsolator(int idStrain)
        {
            try
            {
                return (from id in await clientIsolatorStrain.GetAllIsolatorStrainAsync()
                        join em in await clientEmployee.GetAllEmployeeAsync() on id.iD_Employee equals em.IdEmployee
                        where id.iD_Strain == idStrain
                        select $"{em.FullName} - {id.yearOfIsolator}"
                        ).FirstOrDefault().ToString().Trim() ?? "";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<string> GetNextStrainNumber()
        {
            try
            {
                var strains = await clientStrain.GetAllStrainsAsync();
                var strainsHistory = await clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();
                var strainNumbers = (from st in strains
                                     join sh in strainsHistory on st.idStrain equals sh.idStrain
                                     where sh.status.Equals("Đã được duyệt")
                                     select st.strainNumber).ToList();

                if (strainNumbers.Count == 0)
                {
                    return "CCAH-001/1";
                }

                var strainPatterns = strainNumbers.Select(sn =>
                {
                    var parts = sn.Split('-');
                    var basePart = parts[0];
                    var numberPart = parts[1].Split('/');
                    var mainNumber = int.Parse(numberPart[0]);
                    var subNumber = int.Parse(numberPart[1]);
                    return (basePart, mainNumber, subNumber);
                }).ToList();

                var maxPattern = strainPatterns.OrderByDescending(p => p.mainNumber)
                                               .ThenByDescending(p => p.subNumber)
                                               .First();

                string nextStrainNumber;
                if (maxPattern.subNumber == 9)
                {
                    nextStrainNumber = $"{maxPattern.basePart}-{(maxPattern.mainNumber + 1).ToString("D3")}/1";
                }
                else
                {
                    nextStrainNumber = $"{maxPattern.basePart}-{maxPattern.mainNumber.ToString("D3")}/{(maxPattern.subNumber + 1)}";
                }

                return nextStrainNumber;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<ListStrainDTO>> GetListStrainNumber()
        {
            try
            {
                return (from st in await clientStrain.GetAllStrainsAsync()
                        join sh in await clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync() on st.idStrain equals sh.idStrain
                        where sh.status.Equals("Đã được duyệt")
                        select new ListStrainDTO
                        {
                            idStrain = st.idStrain,
                            strainNumber = st.strainNumber,
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<ListStrainDTO>();
            }
        }
        public async Task<string> AddDataInventory(string json)
        {
            try
            {
                return await clientInventory.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> UpdateImageStrain(int id, byte[] imgValue)
        {
            try
            {
                return await clientStrain.PatchImageStrain(id, imgValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
