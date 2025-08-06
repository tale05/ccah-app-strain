using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProjectContentBLL
    {
        ClientProjectContent projectContentClient;
        ClientProject projectClient;
        public ProjectContentBLL()
        {
            projectContentClient = new ClientProjectContent();
            projectClient = new ClientProject();
        }
        public async Task<List<ProjectContentCustomDTO>> LoadDataProjectContent(string idProject)
        {
            try
            {
                var projectTask = projectClient.GetAllProjectAsync();
                var projectContentTask = projectContentClient.GetAllProjectContentAsync();

                await Task.WhenAll(projectTask, projectContentTask);

                var project = await projectTask;
                var projectContent = await projectContentTask;

                var query = from pr in project
                            join pc in projectContent on pr.idProject equals pc.idProject
                            where pc.idProject.Equals(idProject)
                            select new ProjectContentCustomDTO
                            {
                                idProjectContent = pc.idProjectContent,
                                idNameProject = pr.idProject + "/" + pr.projectName,
                                nameContent = pc.nameContent,
                                results = pc.results,
                                startDate = DateTime.Parse(pc.startDate).ToString("dd/MM/yyyy"),
                                endDate = DateTime.Parse(pc.endDate).ToString("dd/MM/yyyy"),
                                contractNo = pc.contractNo,
                                status = pc.status,
                                priority = pc.priority,
                            };
                return query.ToList();
            }
            catch (Exception)
            {
                return new List<ProjectContentCustomDTO>();
            }
        }
    }
}
