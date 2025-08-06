using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProjectBLL
    {
        ClientProject projectClient;
        ClientPartner partnerClient;
        ClientEmployee employeeClient;
        public ProjectBLL()
        {
            this.projectClient = new ClientProject();
            this.partnerClient = new ClientPartner();
            this.employeeClient = new ClientEmployee();
        }
        public async Task<List<ProjectCustom1DTO>> LoadDataProject()
        {
            try
            {
                var projectTask = projectClient.GetAllProjectAsync();
                var partnerTask = partnerClient.GetAllPartnerAsync();
                var employeeTask = employeeClient.GetAllEmployeeAsync();

                await Task.WhenAll(projectTask, partnerTask, employeeTask);

                var project = await projectTask;
                var partner = await partnerTask;
                var employee = await employeeTask;

                var query = from pr in project
                            join pa in partner on pr.idPartner equals pa.idPartner
                            join em in employee on pr.idEmployee equals em.IdEmployee
                            select new ProjectCustom1DTO
                            {
                                idProject = pr.idProject,
                                fullName = em.FullName,
                                nameCompany = pa.nameCompany,
                                projectName = pr.projectName,
                                results = pr.results,
                                startDateProject = DateTime.Parse(pr.startDateProject).ToString("dd/MM/yyyy"),
                                endDateProject = DateTime.Parse(pr.endDateProject).ToString("dd/MM/yyyy"),
                                contractNo = pr.contractNo,
                                description = pr.description,
                                status = pr.status,
                            };
                return query.ToList();
            }
            catch (Exception)
            {
                return new List<ProjectCustom1DTO>();
            }
        }

    }
}
