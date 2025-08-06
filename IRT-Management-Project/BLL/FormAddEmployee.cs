using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormAddEmployee
    {
        private readonly HttpClient _httpClient;
        private ClientEmployee employee;
        private ClientRoleForEmployee roleForEmployee;
        public FormAddEmployee()
        {
            _httpClient = new HttpClient();
            employee = new ClientEmployee();
            roleForEmployee = new ClientRoleForEmployee();
        }

        private string BuildAddress(string address, string ward, string district, string province)
        {
            var components = new List<string>
            {
                address ?? "",
                !string.IsNullOrEmpty(ward) ? ward : "",
                !string.IsNullOrEmpty(district) ? district : "",
                !string.IsNullOrEmpty(province) ? province : ""
            };
            return string.Join(", ", components.Where(c => !string.IsNullOrEmpty(c)));
        }

        public async Task<List<EmployeeCustomDTO2>> LoadData()
        {
            try
            {
                var employees = await employee.GetAllEmployeeAsync();
                var roles = await roleForEmployee.GetAllRoleForEmployeeAsync();

                var combinedList = from a in employees
                                   join r in roles on a.IdRole equals r.IdRole
                                   select new EmployeeCustomDTO2
                                   {
                                       IdEmployee = a.IdEmployee ?? "",
                                       NameRole = r.RoleName ?? "",
                                       FirstName = a.FirstName ?? "",
                                       LastName = a.LastName ?? "",
                                       FullName = a.FullName ?? "",
                                       IdCard = a.IdCard ?? "",
                                       DateOfBirth = a.DateOfBirth,
                                       Gender = a.Gender ?? "",
                                       Email = a.Email ?? "",
                                       PhoneNumber = a.PhoneNumber ?? "",
                                       Degree = a.Degree ?? "",
                                       Address = BuildAddress(a.Address, a.nameWard, a.nameDistrict, a.nameProvince),
                                       JoinDate = a.JoinDate,
                                       nameWard = a.nameWard ?? "",
                                       nameDistrict = a.nameDistrict ?? "",
                                       nameProvince = a.nameProvince ?? "",
                                       Username = a.Username ?? "",
                                       Status = a.Status ?? "",
                                   };

                return combinedList.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<EmployeeCustomDTO2>();
            }
        }
        public async Task<List<string>> GetListNameRole()
        {
            try
            {
                var roles = await roleForEmployee.GetAllRoleForEmployeeAsync();
                var lst = from a in roles select a.RoleName;
                return lst.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }
        public async Task<string> AddData(string json)
        {
            try
            {
                return await employee.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> DeleteData(string id)
        {
            try
            {
                return await employee.Delete(id);
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteDataAccount(string id)
        {
            try
            {
                return await employee.Delete(id);
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return false;
            }
        }

        public async Task<int> GetIdRoleByName(string roleName)
        {
            try
            {
                var roles = await roleForEmployee.GetAllRoleForEmployeeAsync();
                int idRole = roles.FirstOrDefault(a => a.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase))?.IdRole ?? 0;
                return idRole;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return 0;
            }
        }

        public async Task<string> Update(string idEmployee, string employeeJson)
        {
            try
            {
                return await employee.Update(idEmployee, employeeJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<byte[]> GetImageIdEmployee(string idEmployee)
        {
            try
            {
                return (from em in await employee.GetAllEmployeeAsync() where em.IdEmployee.Equals(idEmployee) select em.ImageEmployee).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<EmployeeCustomDTO2>> SearchData(string search)
        {
            try
            {
                var employees = await employee.GetAllEmployeeAsync();
                var roles = await roleForEmployee.GetAllRoleForEmployeeAsync();

                var combinedList = from a in employees
                                   join r in roles on a.IdRole equals r.IdRole
                                   select new EmployeeCustomDTO2
                                   {
                                       IdEmployee = a.IdEmployee ?? "",
                                       NameRole = r.RoleName ?? "",
                                       FirstName = a.FirstName ?? "",
                                       LastName = a.LastName ?? "",
                                       FullName = a.FullName ?? "",
                                       IdCard = a.IdCard ?? "",
                                       DateOfBirth = a.DateOfBirth,
                                       Gender = a.Gender ?? "",
                                       Email = a.Email ?? "",
                                       PhoneNumber = a.PhoneNumber ?? "",
                                       Degree = a.Degree ?? "",
                                       Address = BuildAddress(a.Address, a.nameWard, a.nameDistrict, a.nameProvince),
                                       JoinDate = a.JoinDate,
                                       nameWard = a.nameWard ?? "",
                                       nameDistrict = a.nameDistrict ?? "",
                                       nameProvince = a.nameProvince ?? "",
                                       Username = a.Username ?? "",
                                       Status = a.Status ?? "",
                                   };

                var query = combinedList.ToList();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.ToLower();
                    query = query.Where(a =>
                        a.IdEmployee.ToLower().Contains(search) ||
                        a.FullName.ToLower().Contains(search) ||
                        a.NameRole.ToLower().Contains(search)
                    ).ToList();
                }

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<EmployeeCustomDTO2>();
            }
        }

    }
}
