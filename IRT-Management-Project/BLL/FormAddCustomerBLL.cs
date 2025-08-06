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
    public class FormAddCustomerBLL
    {
        private readonly HttpClient _httpClient;
        private ClientCustomer customer;
        public FormAddCustomerBLL()
        {
            _httpClient = new HttpClient();
            customer = new ClientCustomer();
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

        public async Task<List<CustomerCustomDTO>> LoadData()
        {
            try
            {
                var customers = await customer.GetAllCustomerAsync();

                var combinedList = from a in customers
                                   select new CustomerCustomDTO
                                   {
                                       IdCustomer = a.idCustomer,
                                       FirstName = a.firstName,
                                       LastName = a.LastName,
                                       FullName = a.fullName,
                                       DateOfBirth = a.dateOfBirth,
                                       Gender = a.gender,
                                       Email = a.email,
                                       PhoneNumber = a.phoneNumber,
                                       Address = BuildAddress(a.address, a.nameWard, a.nameDistrict, a.nameProvince),
                                       nameWard = a.nameWard ?? "",
                                       nameDistrict = a.nameDistrict ?? "",
                                       nameProvince = a.nameProvince ?? "",
                                       Username = a.username ?? "",
                                       Status = a.status ?? "",
                                   };

                return combinedList.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<CustomerCustomDTO>();
            }
        }

        public async Task<string> AddData(string json)
        {
            try
            {
                return await customer.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> DeleteData(string id)
        {
            try
            {
                return await customer.Delete(id);
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return string.Empty;
            }
        }

        public async Task<string> DeleteDataAccount(string id)
        {
            try
            {
                return await customer.Delete(id);
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return string.Empty;
            }
        }



        public async Task<string> Update(string idCustomer, string customerJson)
        {
            try
            {
                return await customer.Update(idCustomer, customerJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<byte[]> GetImageIdCustomer(string idCustomer)
        {
            try
            {
                return (from em in await customer.GetAllCustomerAsync() where em.idCustomer.Equals(idCustomer) select em.image).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<CustomerCustomDTO>> SearchData(string search)
        {
            try
            {
                var customers = await customer.GetAllCustomerAsync();

                var combinedList = from a in customers
                                   select new CustomerCustomDTO
                                   {
                                       IdCustomer = a.idCustomer,
                                       FirstName = a.firstName,
                                       LastName = a.LastName,
                                       FullName = a.fullName,
                                       DateOfBirth = a.dateOfBirth,
                                       Gender = a.gender,
                                       Email = a.email,
                                       PhoneNumber = a.phoneNumber,
                                       Address = BuildAddress(a.address, a.nameWard, a.nameDistrict, a.nameProvince),
                                       nameWard = a.nameWard ?? "",
                                       nameDistrict = a.nameDistrict ?? "",
                                       nameProvince = a.nameProvince ?? "",
                                       Username = a.username ?? "",
                                       Status = a.status ?? "",
                                   };

                var query = combinedList.ToList();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.ToLower();
                    query = query.Where(a =>
                        a.IdCustomer.ToLower().Contains(search) ||
                        a.FullName.ToLower().Contains(search)
                    ).ToList();
                }

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<CustomerCustomDTO>();
            }
        }

    }
}
