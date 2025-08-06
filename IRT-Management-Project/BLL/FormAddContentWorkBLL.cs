using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormAddContentWorkBLL
    {
        ClientProjectContent clientProjectContent;
        ClientContentWork clientContentWork;
        ClientProject clientProject;
        ClientEmployee clientEmployee;
        public FormAddContentWorkBLL()
        {
            clientProjectContent = new ClientProjectContent();
            clientContentWork = new ClientContentWork();
            clientEmployee = new ClientEmployee();
            clientProject = new ClientProject();
        }
        public async Task<List<string>> GetListEmployee()
        {
            try
            {
                var employees = await clientEmployee.GetAllEmployeeAsync();

                var sortedEmployees = employees
                    .OrderBy(em => int.Parse(em.IdEmployee.Substring(2)))
                    .Select(em => em.IdEmployee + " - " + em.FullName)
                    .ToList();

                return sortedEmployees;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        public async Task<List<DetailTableContentWorkDTO>> GetListContentWork(int idProjectContent)
        {
            try
            {
                var employeeTask = clientEmployee.GetAllEmployeeAsync();
                var projectTask = clientProject.GetAllProjectAsync();
                var projectContentTask = clientProjectContent.GetAllProjectContentAsync();
                var contentWorkTask = clientContentWork.GetAllContentWorkAsync();

                await Task.WhenAll(employeeTask, projectTask, projectContentTask, contentWorkTask);

                var employees = await employeeTask;
                var projects = await projectTask;
                var projectContents = await projectContentTask;
                var contentWorks = await contentWorkTask;

                var list = (from pc in projectContents
                            join cw in contentWorks on pc.idProjectContent equals cw.idProjectContent
                            join em in employees on cw.idEmployee equals em.IdEmployee
                            where cw.idProjectContent == idProjectContent
                            select new DetailTableContentWorkDTO
                            {
                                idContentWork = cw.idContentWork,
                                nameAndIdEmployee = em.IdEmployee + " - " + em.FullName,
                                nameContent =  $"Công việc {cw.title}.{cw.subTitle}: {cw.nameContent}",
                                result = cw.results,
                                startDate = DateTime.Parse(cw.startDate).ToString("dd/MM/yyyy"),
                                endDate = DateTime.Parse(cw.endDate).ToString("dd/MM/yyyy"),
                                ennDateActual = string.IsNullOrEmpty(cw.endDateActual) ? "" : DateTime.Parse(cw.endDateActual).ToString("dd/MM/yyyy"),
                                contractNo = cw.contractNo ?? "",
                                priority = cw.priority,
                                status = cw.status,
                                notification = string.IsNullOrEmpty(cw.notificattion) ? "" : cw.notificattion,
                                title = cw.title,
                                subTitle = cw.subTitle,
                                histories = cw.histories ?? "",
                            }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetListContentWork: " + ex.Message);
                return new List<DetailTableContentWorkDTO>();
            }
        }
        public async Task<List<DetailTableContentWorkDTO>> SearchData(int idProjectContent, string search)
        {
            try
            {
                var employeeTask = clientEmployee.GetAllEmployeeAsync();
                var projectTask = clientProject.GetAllProjectAsync();
                var projectContentTask = clientProjectContent.GetAllProjectContentAsync();
                var contentWorkTask = clientContentWork.GetAllContentWorkAsync();

                await Task.WhenAll(employeeTask, projectTask, projectContentTask, contentWorkTask);

                var employees = await employeeTask;
                var projects = await projectTask;
                var projectContents = await projectContentTask;
                var contentWorks = await contentWorkTask;

                var list = (from pc in projectContents
                            join cw in contentWorks on pc.idProjectContent equals cw.idProjectContent
                            join em in employees on cw.idEmployee equals em.IdEmployee
                            where cw.idProjectContent == idProjectContent 
                            && (cw.nameContent.Contains(search) || em.IdEmployee.Contains(search))
                            select new DetailTableContentWorkDTO
                            {
                                idContentWork = cw.idContentWork,
                                nameAndIdEmployee = em.IdEmployee + " - " + em.FullName,
                                nameContent = $"Công việc {cw.title}.{cw.subTitle}: {cw.nameContent}",
                                result = cw.results,
                                startDate = DateTime.Parse(cw.startDate).ToString("dd/MM/yyyy"),
                                endDate = DateTime.Parse(cw.endDate).ToString("dd/MM/yyyy"),
                                ennDateActual = string.IsNullOrEmpty(cw.endDateActual) ? "" : DateTime.Parse(cw.endDateActual).ToString("dd/MM/yyyy"),
                                contractNo = cw.contractNo,
                                priority = cw.priority,
                                status = cw.status,
                                notification = string.IsNullOrEmpty(cw.notificattion) ? "" : cw.notificattion,
                                title = cw.title,
                                subTitle = cw.subTitle,
                                histories = cw.histories,
                            }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetListContentWork: " + ex.Message);
                return new List<DetailTableContentWorkDTO>();
            }
        }
        public async Task<List<DetailTableContentWorkDTO>> FillDataByStatus(int idProjectContent, string status)
        {
            try
            {
                var employeeTask = clientEmployee.GetAllEmployeeAsync();
                var projectTask = clientProject.GetAllProjectAsync();
                var projectContentTask = clientProjectContent.GetAllProjectContentAsync();
                var contentWorkTask = clientContentWork.GetAllContentWorkAsync();

                await Task.WhenAll(employeeTask, projectTask, projectContentTask, contentWorkTask);

                var employees = await employeeTask;
                var projects = await projectTask;
                var projectContents = await projectContentTask;
                var contentWorks = await contentWorkTask;

                var list = (from pc in projectContents
                            join cw in contentWorks on pc.idProjectContent equals cw.idProjectContent
                            join em in employees on cw.idEmployee equals em.IdEmployee
                            where cw.idProjectContent == idProjectContent
                            && cw.status.Contains(status)
                            select new DetailTableContentWorkDTO
                            {
                                idContentWork = cw.idContentWork,
                                nameAndIdEmployee = em.IdEmployee + " - " + em.FullName,
                                nameContent = $"Công việc {cw.title}.{cw.subTitle}: {cw.nameContent}",
                                result = cw.results,
                                startDate = DateTime.Parse(cw.startDate).ToString("dd/MM/yyyy"),
                                endDate = DateTime.Parse(cw.endDate).ToString("dd/MM/yyyy"),
                                ennDateActual = string.IsNullOrEmpty(cw.endDateActual) ? "" : DateTime.Parse(cw.endDateActual).ToString("dd/MM/yyyy"),
                                contractNo = cw.contractNo,
                                priority = cw.priority,
                                status = cw.status,
                                notification = string.IsNullOrEmpty(cw.notificattion) ? "" : cw.notificattion,
                                title = cw.title,
                                subTitle = cw.subTitle,
                                histories = cw.histories,
                            }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetListContentWork: " + ex.Message);
                return new List<DetailTableContentWorkDTO>();
            }
        }
        public async Task<string> Post(string json)
        {
            try
            {
                return await clientContentWork.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> Delete(int idContentWork)
        {
            try
            {
                return await clientContentWork.Delete(idContentWork);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> Update(int idContentWork, string json)
        {
            try
            {
                return await clientContentWork.Update(idContentWork, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> CheckStatus(int idProjectContent)
        {
            try
            {
                var contentWorks = await clientContentWork.GetAllContentWorkAsync();
                var exists = contentWorks.Any(cw => cw.idProjectContent == idProjectContent && cw.status.Equals("Chưa hoàn thành"));

                return exists;
            }
            catch (Exception)
            {
                return true;
            }
        }
        public async Task<string> GetLastTitleByIdProjectContent(int idProjectContent)
        {
            try
            {
                var allContentWork = await clientContentWork.GetAllContentWorkAsync();
                var lastTitle = (from cw in allContentWork
                                 where cw.idProjectContent == idProjectContent
                                 orderby cw.subTitle descending
                                 select cw.subTitle).FirstOrDefault();

                return lastTitle.ToString() ?? "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public async Task<List<DTO.Excel.ContentWorkExcelDTO>> DataToExportExcel(int idProjectContent)
        {
            try
            {
                var employeeTask = clientEmployee.GetAllEmployeeAsync();
                var projectTask = clientProject.GetAllProjectAsync();
                var projectContentTask = clientProjectContent.GetAllProjectContentAsync();
                var contentWorkTask = clientContentWork.GetAllContentWorkAsync();

                await Task.WhenAll(employeeTask, projectTask, projectContentTask, contentWorkTask);

                var employees = await employeeTask;
                var projects = await projectTask;
                var projectContents = await projectContentTask;
                var contentWorks = await contentWorkTask;

                var list = (from cw in contentWorks
                            join em in employees on cw.idEmployee equals em.IdEmployee
                            where cw.idProjectContent == idProjectContent
                            select new DTO.Excel.ContentWorkExcelDTO
                            {
                                idContentWork = cw.idContentWork,
                                idEmployee = em.IdEmployee,
                                nameEmployee = em.FullName,
                                nameContent = $"Công việc {cw.title}.{cw.subTitle}: {cw.nameContent}",
                                result = cw.results,
                                startDate = DateTime.Parse(cw.startDate).ToString("dd/MM/yyyy"),
                                endDate = DateTime.Parse(cw.endDate).ToString("dd/MM/yyyy"),
                                ennDateActual = string.IsNullOrEmpty(cw.endDateActual) ? "" : DateTime.Parse(cw.endDateActual).ToString("dd/MM/yyyy"),
                                contractNo = cw.contractNo,
                                priority = cw.priority,
                                status = cw.status,
                            }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetListContentWork: " + ex.Message);
                return new List<DTO.Excel.ContentWorkExcelDTO>();
            }
        }
        public async Task<string> GetHistoryByIdContentWork(int idContentWork)
        {
            try
            {
                var allContentWork = await clientContentWork.GetAllContentWorkAsync();
                var lastTitle = (from cw in allContentWork
                                 where cw.idContentWork == idContentWork
                                 select cw.histories).FirstOrDefault();

                return lastTitle.ToString() ?? "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public async Task<string> GetIdProjectByIdProjectContent(int idProjectContent)
        {
            try
            {
                var query = await clientProjectContent.GetAllProjectContentAsync();
                var result = (from pc 
                              in query 
                              where pc.idProjectContent == idProjectContent
                              select pc.idProject).FirstOrDefault();
                return result.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<bool> CheckAllStatusContentWork(int idProjectContent)
        {
            try
            {
                var contentWorks = await clientContentWork.GetAllContentWorkAsync();
                var exists = contentWorks.Any(cw => cw.idProjectContent == idProjectContent && cw.status.Equals("Chưa hoàn thành"));

                return exists;
            }
            catch (Exception)
            {
                return true;
            }
        }
        public async Task<bool> CheckAllStatusProjectContent(string idProject)
        {
            try
            {
                var contentWorks = await clientProjectContent.GetAllProjectContentAsync();
                var exists = contentWorks.Any(pc => pc.idProject == idProject && pc.status.Equals("Chưa hoàn thành"));

                return exists;
            }
            catch (Exception)
            {
                return true;
            }
        }
        public async Task<string> UpdateStatusProject(string idProject, string status)
        {
            try
            {
                return await clientContentWork.PatchStatusProject(idProject, status);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> UpdateStatusProjectContent(int idProjectContent, string status)
        {
            try
            {
                return await clientContentWork.PatchStatusProjectContent(idProjectContent, status);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
