using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class ContentWorkBLL
    {
        private readonly ClientContentWork _client1;
        private readonly ClientEmployee _client2;

        public ContentWorkBLL()
        {
            _client1 = new ClientContentWork();
            _client2 = new ClientEmployee();
        }

        public async Task<List<ContentWorkCustomDTO>> LoadDataContentWork(int idProjectContent)
        {
            try
            {
                var contentWorkTask = _client1.GetAllContentWorkAsync();
                var employeeTask = _client2.GetAllEmployeeAsync();

                await Task.WhenAll(contentWorkTask, employeeTask);

                var contentWork = await contentWorkTask;
                var employee = await employeeTask;

                var query = from cw in contentWork
                            join em in employee on cw.idEmployee equals em.IdEmployee
                            where cw.idProjectContent == idProjectContent
                            select new ContentWorkCustomDTO
                            {
                                idContentWork = cw.idContentWork,
                                nameEmployee = em.FullName,
                                nameContent = cw.nameContent,
                                startDate = DateTime.Parse(cw.startDate).ToString("dd/MM/yyyy"),
                                endDate = DateTime.Parse(cw.endDate).ToString("dd/MM/yyyy"),
                                contractNo = cw.contractNo,
                                status = cw.status,
                                priority = cw.priority,
                            };

                return query.ToList();
            }
            catch (Exception)
            {
                return new List<ContentWorkCustomDTO>();
            }
        }

        public async Task<DetailContentWorkCustomDTO> GetDetailContentWork(int idContentWork)
        {
            try
            {
                var contentWorkTask = _client1.GetAllContentWorkAsync();
                var employeeTask = _client2.GetAllEmployeeAsync();

                await Task.WhenAll(contentWorkTask, employeeTask);

                var contentWork = await contentWorkTask;
                var employee = await employeeTask;

                var query = from cw in contentWork
                            join em in employee on cw.idEmployee equals em.IdEmployee
                            where cw.idContentWork == idContentWork
                            select new DetailContentWorkCustomDTO
                            {
                                idContentWork = cw.idContentWork,
                                nameEmployee = em.FullName,
                                nameContent = cw.nameContent,
                                result = cw.results,
                                startDate = cw.startDate,
                                endDate = cw.endDate,
                                ennDateActual = cw.endDateActual,
                                contractNo = cw.contractNo,
                                status = cw.status,
                                priority = cw.priority,
                            };

                return query.FirstOrDefault();
            }
            catch (Exception)
            {
                return new DetailContentWorkCustomDTO();
            }
        }

        private async Task<int> CountStatus(int idProjectContent, string status, Func<ApiContentWorkDTO, bool> extraCondition = null)
        {
            try
            {
                var contentWorks = await _client1.GetAllContentWorkAsync();
                return contentWorks.Count(cw => cw.status.Equals(status) &&
                                                 cw.idProjectContent == idProjectContent &&
                                                 (extraCondition == null || extraCondition(cw)));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Task<int> CountStatusDaHoanThannh(int idProjectContent) => CountStatus(idProjectContent, "Đã hoàn thành");

        public Task<int> CountStatusChuaHoanThannh(int idProjectContent) => CountStatus(idProjectContent, "Chưa hoàn thành");

        public Task<int> CountCompletedTasksOnTime(int idProjectContent)
        {
            return CountStatus(idProjectContent, "Đã hoàn thành", cw =>
                cw.endDateActual == null || DateTime.Parse(cw.endDateActual) <= DateTime.Parse(cw.endDate));
        }

        public Task<int> CountCompletedTasksDelayed(int idProjectContent)
        {
            return CountStatus(idProjectContent, "Đã hoàn thành", cw =>
                DateTime.Parse(cw.endDateActual) > DateTime.Parse(cw.endDate));
        }

        public Task<int> CountUnfinishedTasksWithinDeadline(int idProjectContent)
        {
            var currentDate = DateTime.Now.Date;
            return CountStatus(idProjectContent, "Chưa hoàn thành", cw =>
                DateTime.Parse(cw.endDate) >= currentDate);
        }

        public Task<int> CountUnfinishedTasksOverdue(int idProjectContent)
        {
            var currentDate = DateTime.Now.Date;
            return CountStatus(idProjectContent, "Chưa hoàn thành", cw =>
                DateTime.Parse(cw.endDate) < currentDate);
        }

        public async Task<List<string>> GetListIdEmployee(int idProjectContent)
        {
            try
            {
                var contentWork = await _client1.GetAllContentWorkAsync();
                return contentWork.Where(cw => cw.idProjectContent == idProjectContent)
                                  .Select(cw => cw.idEmployee)
                                  .Distinct()
                                  .ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public Task<int> CountTasksByEmployee(string employeeId, int idProjectContent, string status)
        {
            return CountStatus(idProjectContent, status, cw => cw.idEmployee == employeeId);
        }

        public Task<int> CountUnfinishedTasksByEmployee(string employeeId, int idProjectContent)
        {
            return CountTasksByEmployee(employeeId, idProjectContent, "Chưa hoàn thành");
        }

        public Task<int> CountCompletedTasksByEmployee(string employeeId, int idProjectContent)
        {
            return CountTasksByEmployee(employeeId, idProjectContent, "Đã hoàn thành");
        }

        public async Task<string> GetFileNameById(int idContentWork)
        {
            try
            {
                var query = (await _client1.GetAllContentWorkAsync()).FirstOrDefault(cw => cw.idContentWork == idContentWork)?.fileName;
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
                var query = (await _client1.GetAllContentWorkAsync()).FirstOrDefault(cw => cw.idContentWork == idContentWork)?.fileSaved;
                return query ?? null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
