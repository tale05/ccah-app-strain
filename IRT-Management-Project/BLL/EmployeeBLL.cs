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
    public class EmployeeBLL
    {
        private readonly HttpClient _httpClient;
        private ClientEmployee employee;
        private ClientRoleForEmployee roleForEmployee;
        public EmployeeBLL()
        {
            employee = new ClientEmployee();
            roleForEmployee = new ClientRoleForEmployee();
            _httpClient = new HttpClient();
        }
        public async Task<List<EmployeeCustomDTO1>> LoadData()
        {
            try
            {
                var employees = await employee.GetAllEmployeeAsync();
                var roles = await roleForEmployee.GetAllRoleForEmployeeAsync();

                var combinedList = from a in employees
                                   join r in roles on a.IdRole equals r.IdRole
                                   select new EmployeeCustomDTO1
                                   {
                                       IdEmployee = a.IdEmployee,
                                       NameRole = r.RoleName,
                                       FullName = a.FullName,
                                       IdCard = a.IdCard,
                                       DateOfBirth = DateTime.Parse(a.DateOfBirth.ToString("dd/MM/yyyy")),
                                       Gender = a.Gender,
                                       Email = a.Email,
                                       PhoneNumber = a.PhoneNumber,
                                       Degree = a.Degree,
                                       Address = a.Address,
                                       JoinDate = DateTime.Parse(a.JoinDate.ToString("dd/MM/yyyy")),
                                   };

                return combinedList.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<EmployeeCustomDTO1>();
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

    }
}
