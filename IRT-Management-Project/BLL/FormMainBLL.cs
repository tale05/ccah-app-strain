using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class FormMainBLL
    {
        private ClientInventory inventories;
        private ClientOrders orders;
        private ClientEmployee employees;
        private ClientStrain strains;

        private Task<List<ApiInventoryDTO>> inventoryTask;
        private Task<List<ApiStrainDTO>> strainTask;
        private Task<List<ApiEmployeeDTO>> employeeTask;
        private Task<List<ApiOrdersDTO>> orderTask;

        public FormMainBLL()
        {
            inventories = new ClientInventory();
            strains = new ClientStrain();
            employees = new ClientEmployee();
            orders = new ClientOrders();

            // Khởi tạo việc tải dữ liệu không đồng bộ
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            inventoryTask = inventories.GetAllInventoryAsync();
            strainTask = strains.GetAllStrainsAsync();
            employeeTask = employees.GetAllEmployeeAsync();
            orderTask = orders.GetAllOrdersAsync();

            // Chờ tất cả các nhiệm vụ hoàn thành để tránh lỗi tiềm ẩn
            await Task.WhenAll(inventoryTask, strainTask, employeeTask, orderTask).ConfigureAwait(false);
        }

        private async Task EnsureDataLoadedAsync()
        {
            await Task.WhenAll(inventoryTask, strainTask, employeeTask, orderTask).ConfigureAwait(false);
        }

        public async Task<List<QuantityStrainDTO>> GetListStrainNumber()
        {
            try
            {
                await EnsureDataLoadedAsync().ConfigureAwait(false);
                var inventory = await inventoryTask;
                var strain = await strainTask;

                var query = (from iv in inventory
                             join st in strain on iv.idStrain equals st.idStrain
                             where st.strainNumber != null
                             select new QuantityStrainDTO
                             {
                                 strainNumber = st.strainNumber,
                                 quantity = iv.quantity,
                             }).ToList();
                return query;
            }
            catch (Exception)
            {
                return new List<QuantityStrainDTO>();
            }
        }

        public async Task<int> CountStrainApprovaled()
        {
            try
            {
                await EnsureDataLoadedAsync().ConfigureAwait(false);
                var contentWorks = await strainTask;
                var count = contentWorks.Count(cw => cw.strainNumber != null);
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> CountStrainNotApprovaled()
        {
            try
            {
                await EnsureDataLoadedAsync().ConfigureAwait(false);
                var contentWorks = await strainTask;
                var count = contentWorks.Count(cw => cw.strainNumber == null);
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> CountEmployee()
        {
            try
            {
                await EnsureDataLoadedAsync().ConfigureAwait(false);
                var lstEmployee = await employeeTask;
                var count = lstEmployee.Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> CountOrdersWaiting()
        {
            try
            {
                await EnsureDataLoadedAsync().ConfigureAwait(false);
                var query = await orderTask;
                var count = query.Count(cw => cw.status == "Đang chờ xử lý");
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
