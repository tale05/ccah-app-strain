using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class LoginBLL
    {
        ClientEmployee clientEmployee;
        ClientRoleForEmployee clientRoleForEmployee;
        private List<ApiEmployeeDTO> employees;
        private Dictionary<string, ApiEmployeeDTO> employeeDictionary;
        private Dictionary<int, string> roleDictionary;
        private Task loadDataTask;

        public LoginBLL()
        {
            clientEmployee = new ClientEmployee();
            clientRoleForEmployee = new ClientRoleForEmployee();
            loadDataTask = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            employees = await clientEmployee.GetAllEmployeeAsync().ConfigureAwait(false);
            employeeDictionary = employees.ToDictionary(emp => emp.Username, emp => emp);

            var roles = await clientRoleForEmployee.GetAllRoleForEmployeeAsync().ConfigureAwait(false);
            roleDictionary = roles.ToDictionary(role => role.IdRole, role => role.RoleName);
        }

        private async Task EnsureDataLoadedAsync()
        {
            if (loadDataTask != null)
            {
                await loadDataTask.ConfigureAwait(false);
                loadDataTask = null;
            }
        }

        public async Task<int?> GetRoleIdByUserNameAsync(string username)
        {
            await EnsureDataLoadedAsync().ConfigureAwait(false);

            if (employeeDictionary.TryGetValue(username, out var employee))
            {
                return employee.IdRole;
            }
            return 0;
        }

        public async Task<string> GetUserNameAsync(string username)
        {
            await EnsureDataLoadedAsync().ConfigureAwait(false);

            if (employeeDictionary.TryGetValue(username, out var employee))
            {
                return employee.Username ?? string.Empty;
            }
            return string.Empty;
        }

        public async Task<string> GetPasswordAsync(string username)
        {
            await EnsureDataLoadedAsync().ConfigureAwait(false);

            if (employeeDictionary.TryGetValue(username, out var employee))
            {
                return employee.Password ?? string.Empty;
            }
            return string.Empty;
        }

        public async Task<string> GetActivityAsync(string username)
        {
            await EnsureDataLoadedAsync().ConfigureAwait(false);

            if (employeeDictionary.TryGetValue(username, out var employee))
            {
                return employee.Status?.Trim() ?? string.Empty;
            }
            return string.Empty;
        }

        public async Task<string> GetFullNameEmployeeByUsernameAsync(string username)
        {
            await EnsureDataLoadedAsync().ConfigureAwait(false);

            if (employeeDictionary.TryGetValue(username, out var employee))
            {
                return employee.FullName ?? "Không tìm thấy tên nhân viên";
            }
            return "Không tìm thấy tên nhân viên";
        }

        public async Task<string> GetIdEmployeeByUsernameAsync(string username)
        {
            await EnsureDataLoadedAsync().ConfigureAwait(false);

            if (employeeDictionary.TryGetValue(username, out var employee))
            {
                return employee.IdEmployee ?? "Không tìm thấy ID nhân viên";
            }
            return "Không tìm thấy ID nhân viên";
        }

        public async Task<string> GetPositionByUsernameAsync(string username)
        {
            await EnsureDataLoadedAsync().ConfigureAwait(false);

            if (employeeDictionary.TryGetValue(username, out var employee))
            {
                if (roleDictionary.TryGetValue(employee.IdRole, out var roleName))
                {
                    return roleName ?? "Không tìm thấy chức vụ nhân viên";
                }
            }
            return "Không tìm thấy chức vụ nhân viên";
        }

        public async Task<byte[]> GetEmployeeImageAsync(string username)
        {
            await EnsureDataLoadedAsync().ConfigureAwait(false);

            if (employeeDictionary.TryGetValue(username, out var employee))
            {
                return employee.ImageEmployee?.ToArray();
            }
            return null;
        }
    }
}
