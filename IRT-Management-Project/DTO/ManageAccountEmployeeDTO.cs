using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ManageAccountEmployeeDTO
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeUsername { get; set; }
        public string StatusAccount { get; set; }
        public int IdRole { get; set; }
        public string NameRole { get; set; }
    }
}
