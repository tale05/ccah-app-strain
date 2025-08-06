using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormDetailOneStrainBLL
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

        public FormDetailOneStrainBLL()
        {
            clientGenus = new ClientGenus();
            clientClass = new ClientClass();
            clientStrain = new ClientStrain();
            clientPhylum = new ClientPhylum();
            clientSpecies = new ClientSpecies();
            clientEmployee = new ClientEmployee();
            clientIdentifyStrain = new ClientIdentifyStrain();
            clientIsolatorStrain = new ClientIsolatorStrain();
            clientConditionalStrain = new ClientConditionalStrain();
            clientStrainApprovalHistory = new ClientStrainApprovalHistory();
        }

        public async Task<StrainCustomWithIdDetailDTO> LoadFullProperties(int strainNumberInput)
        {
            if (strainNumberInput == 0)
            {
                throw new ArgumentException("Strain number truyền vào đang rỗng", nameof(strainNumberInput));
            }

            try
            {
                var strainTask = clientStrain.GetAllStrainsAsync();
                var speciesTask = clientSpecies.GetAllSpeciesAsync();
                var genusTask = clientGenus.GetAllGenusAsync();
                var classTask = clientClass.GetAllClassesAsync();
                var phylumTask = clientPhylum.GetAllPhylumsAsync();
                var conditionTask = clientConditionalStrain.GetAllConditionalStrainAsync();
                var strainApprovalsTask = clientStrainApprovalHistory.GetAllStrainApprovalHistoriesAsync();

                await Task.WhenAll(strainTask, speciesTask, genusTask, classTask,
                    phylumTask, conditionTask, strainApprovalsTask);

                var strains = await strainTask;
                var species = await speciesTask;
                var genus = await genusTask;
                var classes = await classTask;
                var phylum = await phylumTask;
                var condition = await conditionTask;
                var strainHistory = await strainApprovalsTask;

                if (strains == null || species == null || genus == null || classes == null ||
                    phylum == null || condition == null || strainHistory == null)
                {
                    throw new InvalidOperationException("One or more data sources returned null");
                }

                var query = (from st in strains
                             join sp in species on st.idSpecies equals sp.idSpecies
                             join ge in genus on sp.idGenus equals ge.idGenus
                             join cs in classes on ge.idClass equals cs.idClass
                             join ph in phylum on cs.idPhylum equals ph.idPhylum
                             join co in condition on st.idCondition equals co.idCondition
                             join sh in strainHistory on st.idStrain equals sh.idStrain
                             where st.idStrain == strainNumberInput
                             select new StrainCustomWithIdDetailDTO
                             {
                                 idStrain = st.idStrain,
                                 StrainNumber = st.strainNumber?.Trim() ?? "",
                                 nameSpecies = sp.nameSpecies?.Trim() ?? "",
                                 nameGenus = ge.nameGenus?.Trim() ?? "",
                                 nameClass = cs.nameClass?.Trim() ?? "",
                                 namePhylum = ph.namePhylum?.Trim() ?? "",
                                 mediumCondition = co.medium?.Trim() ?? "",
                                 temperatureCondition = co.temperature?.Trim() ?? "",
                                 lightIntensityCondition = co.lightIntensity?.Trim() ?? "",
                                 durationCondition = co.duration?.Trim() ?? "",
                                 ScientificName = st.scientificName?.Trim() ?? "",
                                 SynonymStrain = st.synonymStrain?.Trim() ?? "",
                                 FormerName = st.formerName?.Trim() ?? "",
                                 CommonName = st.commonName?.Trim() ?? "",
                                 CellSize = st.cellSize?.Trim() ?? "",
                                 Organization = st.organization?.Trim() ?? "",
                                 Characteristics = st.characteristics?.Trim() ?? "",
                                 CollectionSite = st.collectionSite?.Trim() ?? "",
                                 Continent = st.continent?.Trim() ?? "",
                                 Country = st.country?.Trim() ?? "",
                                 IsolationSource = st.isolationSource?.Trim() ?? "",
                                 ToxinProducer = st.toxinProducer?.Trim() ?? "",
                                 StateOfStrain = st.stateOfStrain?.Trim() ?? "",
                                 AgitationResistance = st.agitationResistance?.Trim() ?? "",
                                 Remarks = st.remarks?.Trim() ?? "",
                                 GeneInformation = st.geneInformation?.Trim() ?? "",
                                 Publications = st.publications?.Trim() ?? "",
                                 RecommendedForTeaching = st.recommendedForTeaching?.Trim() ?? "",
                                 Status = sh.status?.Trim() ?? "",
                                 ImageStrain = st.imageStrain?.ToArray(),
                             }).FirstOrDefault();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }


        public async Task<string> GetNameAndYearIdentify(int idStrain)
        {
            try
            {
                return (from id in await clientIdentifyStrain.GetAllIdentifyStrainAsync()
                        join em in await clientEmployee.GetAllEmployeeAsync() on id.iD_Employee equals em.IdEmployee
                        where id.iD_Strain == idStrain
                        select $"{em.FullName} - {id.year_of_Identify}"
                        ).FirstOrDefault().ToString().Trim() ?? "";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

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
    }
}
