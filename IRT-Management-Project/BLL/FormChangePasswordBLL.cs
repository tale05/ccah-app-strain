using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormChangePasswordBLL
    {
        ClientEmployee empl;
        public FormChangePasswordBLL()
        {
            empl = new ClientEmployee();
        }
        public async Task<string> GetPasswordCurrent(string idEmployee)
        {
            try
            {
                var query = await empl.GetAllEmployeeAsync();
                var employee = query.FirstOrDefault(em => em.IdEmployee.Equals(idEmployee));
                return employee.Password;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<string> ChangePassword(string id, string pass)
        {
            try
            {
                return await empl.PatchPasswordEmployee(id, pass);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
