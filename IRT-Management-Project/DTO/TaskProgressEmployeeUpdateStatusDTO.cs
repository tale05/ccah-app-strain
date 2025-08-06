using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TaskProgressEmployeeUpdateStatusDTO
    {
        [JsonProperty("results")]
        public string results { get; set; }

        [JsonProperty("endDateActual")]
        public string endDateActual { get; set; }
    }
}
