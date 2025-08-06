using API;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace BLL
{
    public class FormTaskProgressEmployeeBLL
    {
		ClientContentWork clientContentWork;
		public FormTaskProgressEmployeeBLL()
		{
			clientContentWork = new ClientContentWork();
		}
        public async Task<List<TaskProgressEmployeeDTO>> GetData(string idEmployee)
        {
			try
			{
				return (from cw in await clientContentWork.GetAllContentWorkAsync()
					   where cw.idEmployee.Equals(idEmployee)
					   select new TaskProgressEmployeeDTO
					   {
						   idContentWork = cw.idContentWork,
                           idProjectContent = cw.idProjectContent,
						   nameContent = cw.nameContent,
						   results = cw.results ?? "",
						   startDate = string.IsNullOrEmpty(cw.startDate) ? "" : DateTime.Parse(cw.startDate).ToShortDateString(),
						   endDate = string.IsNullOrEmpty(cw.endDate) ? "" : DateTime.Parse(cw.endDate).ToShortDateString(),
						   contractNo = cw.contractNo ?? "",
						   priority = cw.priority,
						   status = cw.status,
						   notificattion = cw.notificattion ?? "",
                       }).ToList();
			}
			catch (Exception)
			{
				return new List<TaskProgressEmployeeDTO>();
			}
        }
        public async Task<List<TaskProgressEmployeeDTO>> FillDataBy(string idEmployee, string status)
        {
            try
            {
                return (from cw in await clientContentWork.GetAllContentWorkAsync()
                        where cw.idEmployee.Equals(idEmployee) && cw.status == status
                        select new TaskProgressEmployeeDTO
                        {
                            idContentWork = cw.idContentWork,
                            nameContent = cw.nameContent,
                            results = cw.results ?? "",
                            startDate = string.IsNullOrEmpty(cw.startDate) ? "" : DateTime.Parse(cw.startDate).ToShortDateString(),
                            endDate = string.IsNullOrEmpty(cw.endDate) ? "" : DateTime.Parse(cw.endDate).ToShortDateString(),
                            contractNo = cw.contractNo ?? "",
                            priority = cw.priority,
                            status = cw.status,
                            notificattion = cw.notificattion ?? "",
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<TaskProgressEmployeeDTO>();
            }
        }
        public async Task<string> ReturnIfHaveNotification(int idContentWork)
        {
            var allContentWorks = await clientContentWork.GetAllContentWorkAsync();
            var notiValue = (from cw in allContentWorks
                             where cw.idContentWork == idContentWork
                             select cw.notificattion).FirstOrDefault();

            return notiValue;
        }
        public async Task<int> CountTotalContentWorks(string idEmployee)
        {
            try
            {
                var allContentWorks = await clientContentWork.GetAllContentWorkAsync();
                var employeeContentWorks = allContentWorks.Where(cw => cw.idEmployee == idEmployee);
                int totalCount = employeeContentWorks.Count();
                return totalCount;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> CountTotalTaskSuccess(string idEmployee)
        {
            try
            {
                var allContentWorks = await clientContentWork.GetAllContentWorkAsync();
                var employeeContentWorks = allContentWorks.Where(cw => cw.idEmployee == idEmployee 
                                                                    && cw.status.Equals("Đã hoàn thành"));
                int totalCount = employeeContentWorks.Count();
                return totalCount;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> CountTotalTaskNotSuccess(string idEmployee)
        {
            try
            {
                var allContentWorks = await clientContentWork.GetAllContentWorkAsync();
                var employeeContentWorks = allContentWorks.Where(cw => cw.idEmployee == idEmployee
                                                                    && cw.status.Equals("Chưa hoàn thành"));
                int totalCount = employeeContentWorks.Count();
                return totalCount;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> CountTotalTasksWithNotification(string idEmployee)
        {
            try
            {
                var allContentWorks = await clientContentWork.GetAllContentWorkAsync();
                var employeeTasksWithNotification = allContentWorks.Where(cw => cw.idEmployee == idEmployee 
                                                                             && !string.IsNullOrEmpty(cw.notificattion));
                int totalCount = employeeTasksWithNotification.Count();

                return totalCount;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<bool> CheckNotExitFile(int idContentWork)
        {
            try
            {
                var contentWorks = await clientContentWork.GetAllContentWorkAsync();
                return contentWorks.Any(cw => cw.idContentWork == idContentWork && string.IsNullOrEmpty(cw.fileName));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<string> UpdateFileProperties(int idContentWork, byte[] fileSave, string fileName)
        {
            try
            {
                return await clientContentWork.PatchFileNameSave(idContentWork, fileSave, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> UpdateStatusContentWorkResultAndEndDateActual(int idContentWork, string result)
        {
            try
            {
                return await clientContentWork.PatchStatusContentWork(idContentWork, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> UpdateNotificationNull(int idContentWork)
        {
            try
            {
                return await clientContentWork.PatchNotification(idContentWork);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetFileNameById(int idContentWork)
        {
            try
            {
                var query = (from cw in await clientContentWork.GetAllContentWorkAsync()
                             where cw.idContentWork == idContentWork
                             select cw.fileName).FirstOrDefault();
                return query ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<byte[]> GetFileSavedById(int idContentWork)
        {
            try
            {
                var query = (from cw in await clientContentWork.GetAllContentWorkAsync()
                             where cw.idContentWork == idContentWork
                             select cw.fileSaved).FirstOrDefault();
                return query ?? null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
