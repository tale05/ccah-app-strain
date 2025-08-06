using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API;
using DTO;

namespace BLL
{
    public class FormAddProjectBLL
    {
        ClientProject clientProject;
        ClientEmployee clientEmployee;
        ClientPartner clientPartner;
        public FormAddProjectBLL()
        {
            clientProject = new ClientProject();
            clientEmployee = new ClientEmployee();
            clientPartner = new ClientPartner();
        }
        public async Task<List<ProjectCustomDTO>> GetFullProperties()
        {
            try
            {
                var projectTask = clientProject.GetAllProjectAsync();
                var partnerTask = clientPartner.GetAllPartnerAsync();
                var employeeTask = clientEmployee.GetAllEmployeeAsync();

                await Task.WhenAll(projectTask, partnerTask, employeeTask);

                var project = await projectTask;
                var partner = await partnerTask;
                var employee = await employeeTask;

                var query = from pr in project
                            join pa in partner on pr.idPartner equals pa.idPartner
                            join em in employee on pr.idEmployee equals em.IdEmployee
                            select new ProjectCustomDTO
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
                                fileName = pr.fileName,
                            };
                return query.ToList();
            }
            catch (Exception)
            {
                return new List<ProjectCustomDTO>();
            }
        }
        public async Task<List<ProjectCustomDTO>> SearchData(string search)
        {
            try
            {
                var projectTask = clientProject.GetAllProjectAsync();
                var partnerTask = clientPartner.GetAllPartnerAsync();
                var employeeTask = clientEmployee.GetAllEmployeeAsync();

                await Task.WhenAll(projectTask, partnerTask, employeeTask);

                var project = await projectTask;
                var partner = await partnerTask;
                var employee = await employeeTask;

                var query = from pr in project
                            join pa in partner on pr.idPartner equals pa.idPartner
                            join em in employee on pr.idEmployee equals em.IdEmployee
                            where pr.idProject.Contains(search) || pr.projectName.Contains(search)
                            select new ProjectCustomDTO
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
                                fileName = pr.fileName,
                            };
                return query.ToList();
            }
            catch (Exception)
            {
                return new List<ProjectCustomDTO>();
            }
        }
        public async Task<List<string>> GetListNameEmployee()
        {
            try
            {
                return (from em in await clientEmployee.GetAllEmployeeAsync() select em.FullName).ToList();

            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        public async Task<List<string>> GetListNameCompany()
        {
            try
            {
                return (from pa in await clientPartner.GetAllPartnerAsync() select pa.nameCompany).ToList();

            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        public async Task<string> Post(string json)
        {
            try
            {
                return await clientProject.Post(json);
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
                return await clientProject.Delete(idProject);
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
                return await clientProject.Update(idProject, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetIdEmployee(string name)
        {
            try
            {
                return (from em in await clientEmployee.GetAllEmployeeAsync() where em.FullName.Equals(name) select em.IdEmployee).FirstOrDefault().ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<int> GetIdPartner(string name)
        {
            try
            {
                return int.Parse((from pa in await clientPartner.GetAllPartnerAsync() where pa.nameCompany.Equals(name) select pa.idPartner).FirstOrDefault().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<byte[]> GetDataFileByIdProject(string idProject)
        {
            try
            {
                var query = (from p
                             in await clientProject.GetAllProjectAsync()
                             where p.idProject.Equals(idProject)
                             select p.fileProject).FirstOrDefault();
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<DTO.Excel.ProjectExcelDTO>> DataToExportExcel()
        {
            try
            {
                var projectTask = clientProject.GetAllProjectAsync();
                var partnerTask = clientPartner.GetAllPartnerAsync();
                var employeeTask = clientEmployee.GetAllEmployeeAsync();

                await Task.WhenAll(projectTask, partnerTask, employeeTask);

                var project = await projectTask;
                var partner = await partnerTask;
                var employee = await employeeTask;

                var query = from pr in project
                            join pa in partner on pr.idPartner equals pa.idPartner
                            join em in employee on pr.idEmployee equals em.IdEmployee
                            select new DTO.Excel.ProjectExcelDTO
                            {
                                idProject = pr.idProject,
                                idAndNameEmployee = $"{em.IdEmployee} - {em.FullName}",
                                nameCompany = pa.nameCompany,
                                nameProject = pr.projectName,
                                result = pr.results,
                                startDate = DateTime.Parse(pr.startDateProject).ToString("dd/MM/yyyy"),
                                endDate = DateTime.Parse(pr.endDateProject).ToString("dd/MM/yyyy"),
                                contractNo = pr.contractNo,
                                description = pr.description,
                                status = pr.status,
                            };
                return query.ToList();
            }
            catch (Exception)
            {
                return new List<DTO.Excel.ProjectExcelDTO>();
            }
        }
    }
}
