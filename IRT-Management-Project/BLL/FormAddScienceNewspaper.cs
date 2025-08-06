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
    public class FormAddScienceNewspaper
    {
        private readonly HttpClient _httpClient;
        private ClientScienceNewspaper news;
        private ClientEmployee employee;

        public FormAddScienceNewspaper()
        {
            _httpClient = new HttpClient();
            news = new ClientScienceNewspaper();
            employee = new ClientEmployee();
        }
        public async Task<List<string>> getListEmployee()
        {
            try
            {
                var query = from s in await employee.GetAllEmployeeAsync() select s.FullName.Trim();
                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task<List<string>> GetListNameEmployee()
        {
            try
            {
                var roles = await employee.GetAllEmployeeAsync();
                var lst = from a in roles select a.FullName;
                return lst.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task<string> GetIdByNameEmployee(string Name)
        {
            try
            {
                var employees = await employee.GetAllEmployeeAsync();
                string id = (from em in employees where em.FullName.Equals(Name) select em.IdEmployee).FirstOrDefault();
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ScienceNewspaperCustomDTO>> LoadData()
        {
            try
            {
                var newss = await news.GetAllNewspaperAsync();
                var employees = await employee.GetAllEmployeeAsync();
                var combinedList = from a in newss
                                   join em in employees on a.idEmployee equals em.IdEmployee
                                   select new ScienceNewspaperCustomDTO
                                   {
                                       IdNewspaper = a.idNewspaper,
                                       Title = a.title,
                                       Content = a.content,
                                       Postdate = a.postDate,
                                       IdEmployee = em.FullName,
                                       Content2 = a.content2,

                                   };

                return combinedList.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<ScienceNewspaperCustomDTO>();
            }
        }

        public async Task<string> AddData(string json)
        {
            try
            {
                return await news.Post(json);
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
                return await news.Delete(id);
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return false;
            }
        }




        public async Task<string> Update(string id, string Json)
        {
            try
            {
                return await news.Update(id, Json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<byte[]> GetImageIdNewspaper(int id)
        {
            try
            {
                return (from em in await news.GetAllNewspaperAsync() where em.idNewspaper == id select em.image).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ScienceNewspaperCustomDTO>> SearchData(string search)
        {
            try
            {
                var newss = await news.GetAllNewspaperAsync();

                var combinedList = from a in newss
                                   select new ScienceNewspaperCustomDTO
                                   {
                                       IdNewspaper = a.idNewspaper,
                                       Title = a.title,
                                       Content = a.content,
                                       Postdate = a.postDate,
                                       IdEmployee = a.idEmployee,
                                       Content2 = a.content2,

                                   };

                var query = combinedList.ToList();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.ToLower();
                    query = query.Where(a =>
                        a.Title.ToLower().Contains(search) ||
                        a.IdEmployee.ToLower().Contains(search)
                    ).ToList();
                }

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<ScienceNewspaperCustomDTO>();
            }
        }

    }
}
