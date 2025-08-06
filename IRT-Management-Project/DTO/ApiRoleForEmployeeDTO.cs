using Newtonsoft.Json;

namespace DTO
{
    public class ApiRoleForEmployeeDTO
    {
        [JsonProperty("idRole")]
        public int IdRole { get; set; }

        [JsonProperty("roleName")]
        public string RoleName { get; set; }

        [JsonProperty("roleDescription")]
        public string RoleDescription { get; set; }
    }
}
