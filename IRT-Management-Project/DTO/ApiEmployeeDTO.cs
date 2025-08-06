using System;
using Newtonsoft.Json;

namespace DTO
{
    public class ApiEmployeeDTO
    {
        [JsonProperty("idEmployee")]
        public string IdEmployee { get; set; }

        [JsonProperty("idRole")]
        public int IdRole { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("idCard")]
        public string IdCard { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("degree")]
        public string Degree { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("joinDate")]
        public DateTime JoinDate { get; set; }

        [JsonProperty("imageEmployee")]
        public byte[] ImageEmployee { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("nameWard")]
        public string nameWard { get; set; }

        [JsonProperty("nameDistrict")]
        public string nameDistrict { get; set; }

        [JsonProperty("nameProvince")]
        public string nameProvince { get; set; }


        public ApiEmployeeDTO() { }

        public ApiEmployeeDTO(int idRole, string firstName, string lastName, string fullName,
            string idCard, DateTime dateOfBirth, string gender, string email, string phoneNumber, string degree,
            string address, DateTime joinDate, byte[] imageEmployee, string username, string password, string status,
            string nameWard, string nameDistrict, string nameProvince)
        {
            IdRole = idRole;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            IdCard = idCard;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Degree = degree;
            Address = address;
            JoinDate = joinDate;
            ImageEmployee = imageEmployee;
            Username = username;
            Password = password;
            Status = status;
            nameWard = nameWard;
            nameDistrict = nameDistrict;
            nameProvince = nameProvince;
        }
    }
}
