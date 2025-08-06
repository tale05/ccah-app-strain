using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ManageEmployeeAccountsBLL
    {
        ClientEmployee clientEmployee;
        ClientRoleForEmployee clientRoleForEmployee;
        public ManageEmployeeAccountsBLL()
        {
            clientEmployee = new ClientEmployee();
            clientRoleForEmployee = new ClientRoleForEmployee();
        }
        public async Task<List<ManageAccountEmployeeDTO>> GetData()
        {
            try
            {
                var employeeTask = clientEmployee.GetAllEmployeeAsync();
                var roleTask = clientRoleForEmployee.GetAllRoleForEmployeeAsync();

                await Task.WhenAll(employeeTask, roleTask);

                var employees = await employeeTask;
                var roles = await roleTask;

                return (from em in employees
                        join ro in roles on em.IdRole equals ro.IdRole
                        select new ManageAccountEmployeeDTO
                        {
                            EmployeeId = em.IdEmployee,
                            EmployeeName = em.FullName,
                            EmployeeUsername = em.Username,
                            StatusAccount = em.Status,
                            IdRole = em.IdRole,
                            NameRole = ro.RoleName,
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<ManageAccountEmployeeDTO>();
            }
        }
        public async Task<List<ManageAccountEmployeeDTO>> SearchData(string str)
        {
            try
            {
                try
                {
                    var employeeTask = clientEmployee.GetAllEmployeeAsync();
                    var roleTask = clientRoleForEmployee.GetAllRoleForEmployeeAsync();

                    await Task.WhenAll(employeeTask, roleTask);

                    var employees = await employeeTask;
                    var roles = await roleTask;

                    return (from em in employees
                            join ro in roles on em.IdRole equals ro.IdRole
                            where em.IdEmployee == str
                            select new ManageAccountEmployeeDTO
                            {
                                EmployeeId = em.IdEmployee,
                                EmployeeName = em.FullName,
                                EmployeeUsername = em.Username,
                                StatusAccount = em.Status,
                                IdRole = em.IdRole,
                                NameRole = ro.RoleName,
                            }).ToList();
                }
                catch (Exception)
                {
                    return new List<ManageAccountEmployeeDTO>();
                }
            }
            catch (Exception)
            {
                return new List<ManageAccountEmployeeDTO>();
            }
        }
        public async Task<List<string>> GetRoleName()
        {
            try
            {
                return (from ro in await clientRoleForEmployee.GetAllRoleForEmployeeAsync()
                        select ro.RoleName).ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        public async Task<string> LockAccount(string idEmployee)
        {
            try
            {
                return await clientEmployee.LockAccount(idEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> OpenAccount(string idEmployee)
        {
            try
            {
                return await clientEmployee.OpenAccount(idEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> ChangeRole(string idEmployee, int idRole)
        {
            try
            {
                return await clientEmployee.ChangeRole(idEmployee, idRole);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<int> GetIdRoleByName(string name)
        {
            try
            {
                var roles = await clientRoleForEmployee.GetAllRoleForEmployeeAsync();
                return roles.FirstOrDefault(ro => ro.RoleName == name)?.IdRole ?? 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<string> ChangePassword(string id, string pass)
        {
            try
            {
                return await clientEmployee.PatchPasswordEmployee(id, pass);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
