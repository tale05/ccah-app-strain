using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ManageCustomerAccountsBLL
    {
        ClientCustomer clientCustomer;
        public ManageCustomerAccountsBLL()
        {
            clientCustomer = new ClientCustomer();
        }
        public async Task<List<ManageAccountCustomerDTO>> GetData()
        {
            try
            {
                return (from ac in await clientCustomer.GetAllCustomerAsync()
                        select new ManageAccountCustomerDTO
                        {
                            CustomerId = ac.idCustomer,
                            CustomerName = ac.fullName,
                            CustomerUsername = ac.username,
                            StatusAccount = ac.status,
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<ManageAccountCustomerDTO>();
            }
        }
        public async Task<List<ManageAccountCustomerDTO>> SearchData(string str)
        {
            try
            {
                return (from ac in await clientCustomer.GetAllCustomerAsync()
                        where ac.idCustomer == str
                        select new ManageAccountCustomerDTO
                        {
                            CustomerId = ac.idCustomer,
                            CustomerName = ac.fullName,
                            CustomerUsername = ac.username,
                            StatusAccount = ac.status,
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<ManageAccountCustomerDTO>();
            }
        }
        public async Task<string> LockAccount(string idCustomer)
        {
            try
            {
                return await clientCustomer.LockAccount(idCustomer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> OpenAccount(string idCustomer)
        {
            try
            {
                return await clientCustomer.OpenAccount(idCustomer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
