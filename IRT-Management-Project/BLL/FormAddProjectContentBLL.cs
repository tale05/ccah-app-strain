using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormAddProjectContentBLL
    {
        ClientProject clientProject;
        ClientProjectContent clientProjectContent;
        public FormAddProjectContentBLL()
        {
            clientProject = new ClientProject();
            clientProjectContent = new ClientProjectContent();
        }
        public async Task<List<ProjectCustomIdAndNameDTO>> GetListProjectById(string projectId)
        {
            try
            {
                var list = (from p in await clientProject.GetAllProjectAsync() 
                            where p.idProject.Equals(projectId)
                            select new ProjectCustomIdAndNameDTO
                            {
                                idProject = p.idProject,
                                projectName = p.projectName,
                            }).ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<ProjectCustomIdAndNameDTO>();
            }
        }
        public async Task<List<ProjectContentWithTitleDTO>> GetListProjectContentByIdProject(string projectId)
        {
            try
            {
                var list = (from p in await clientProjectContent.GetAllProjectContentAsync()
                            where p.idProject.Equals(projectId)
                            select new ProjectContentWithTitleDTO
                            {
                                idProjectContent = p.idProjectContent,
                                idNameProject = p.idProject,
                                nameContent = $"Nội dung {p.title}: " + p.nameContent,
                                results = p.results,
                                startDate = p.startDate,
                                endDate = p.endDate,
                                contractNo = p.contractNo,
                                priority = p.priority,
                                status = p.status,
                                title = p.title,
                            }).ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<ProjectContentWithTitleDTO>();
            }
        }
        public async Task<List<ProjectContentWithTitleDTO>> SearchData(string projectId, string search)
        {
            try
            {
                var list = (from p in await clientProjectContent.GetAllProjectContentAsync()
                            where p.idProject.Equals(projectId) && p.nameContent.Contains(search)
                            select new ProjectContentWithTitleDTO
                            {
                                idProjectContent = p.idProjectContent,
                                idNameProject = p.idProject,
                                nameContent = $"Nội dung {p.title}: " + p.nameContent,
                                results = p.results,
                                startDate = p.startDate,
                                endDate = p.endDate,
                                contractNo = p.contractNo,
                                priority = p.priority,
                                status = p.status,
                                title = p.title,
                            }).ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<ProjectContentWithTitleDTO>();
            }
        }
        public async Task<List<ProjectContentWithTitleDTO>> FillByStatusSuccess(string projectId, string status)
        {
            try
            {
                var list = (from p in await clientProjectContent.GetAllProjectContentAsync()
                            where p.idProject.Equals(projectId) && p.status.Equals(status)
                            select new ProjectContentWithTitleDTO
                            {
                                idProjectContent = p.idProjectContent,
                                idNameProject = p.idProject,
                                nameContent = $"Nội dung {p.title}: " + p.nameContent,
                                results = p.results,
                                startDate = p.startDate,
                                endDate = p.endDate,
                                contractNo = p.contractNo,
                                priority = p.priority,
                                status = p.status,
                                title = p.title,
                            }).ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<ProjectContentWithTitleDTO>();
            }
        }
        public async Task<List<ProjectContentWithTitleDTO>> FillByStatusNotSuccess(string projectId, string status)
        {
            try
            {
                var list = (from p in await clientProjectContent.GetAllProjectContentAsync()
                            where p.idProject.Equals(projectId) && p.status.Equals(status)
                            select new ProjectContentWithTitleDTO
                            {
                                idProjectContent = p.idProjectContent,
                                idNameProject = p.idProject,
                                nameContent = $"Nội dung {p.title}: " + p.nameContent,
                                results = p.results,
                                startDate = p.startDate,
                                endDate = p.endDate,
                                contractNo = p.contractNo,
                                priority = p.priority,
                                status = p.status,
                                title = p.title,
                            }).ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<ProjectContentWithTitleDTO>();
            }
        }
        public async Task<string> Post(string json)
        {
            try
            {
                return await clientProjectContent.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> Delete(string idProject)
        {
            try
            {
                return await clientProjectContent.Delete(idProject);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> Update(string idProject, string json)
        {
            try
            {
                return await clientProjectContent.Update(idProject, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetLastTitleByIdProject(string idProject)
        {
            try
            {
                var allProjectContent = await clientProjectContent.GetAllProjectContentAsync();
                var lastTitle = (from pc in allProjectContent
                                 where pc.idProject == idProject
                                 orderby pc.idProjectContent descending
                                 select pc.title).FirstOrDefault();

                return lastTitle.ToString() ?? "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public async Task<bool> CheckStatusProjectContent(string idProject)
        {
            try
            {
                var contentWorks = await clientProjectContent.GetAllProjectContentAsync();
                var exists = contentWorks.Any(cw => cw.idProject == idProject && cw.status.Equals("Chưa hoàn thành"));

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
                return await clientProjectContent.PatchStatusProject(idProject, status);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<DTO.Excel.ProjectContentExcelDTO>> DataToExportExcel(string projectId)
        {
            try
            {
                var list = (from pc in await clientProjectContent.GetAllProjectContentAsync()
                            join p in await clientProject.GetAllProjectAsync()
                            on pc.idProject equals p.idProject
                            where pc.idProject.Equals(projectId)
                            select new DTO.Excel.ProjectContentExcelDTO
                            {
                                idProject = pc.idProject,
                                nameProject = p.projectName,
                                nameContent = $"Nội dung {pc.title}: " + pc.nameContent,
                                result = pc.results,
                                startDate = pc.startDate,
                                endDate = pc.endDate,
                                contractNo = pc.contractNo,
                                priority = pc.priority,
                                status = pc.status,
                            }).ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<DTO.Excel.ProjectContentExcelDTO>();
            }
        }
    }
}
