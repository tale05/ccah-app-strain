using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormApprovalOrdersCustomerBLL
    {
        ClientStrain clientStrain;
        ClientEmployee clientEmployee;
        ClientCustomer clientCustomer;

        ClientOrders clientOrders;
        ClientOrdersDetail clientOrdersDetail;

        ClientBill clientBill;
        ClientBillDetail clientBillDetail;

        ClientInventory clientInventory;
        public FormApprovalOrdersCustomerBLL()
        {
            clientStrain = new ClientStrain();
            clientEmployee = new ClientEmployee();
            clientCustomer = new ClientCustomer();

            clientOrders = new ClientOrders();
            clientOrdersDetail = new ClientOrdersDetail();

            clientBill = new ClientBill();
            clientBillDetail = new ClientBillDetail();

            clientInventory = new ClientInventory();
        }
        public async Task<List<OrdersDTO>> GetData()
        {
            try
            {
                var ordersTask = clientOrders.GetAllOrdersAsync();
                var employeeTask = clientEmployee.GetAllEmployeeAsync();
                var customerTask = clientCustomer.GetAllCustomerAsync();

                await Task.WhenAll(ordersTask, employeeTask, customerTask);

                var orders = await ordersTask;
                var employees = await employeeTask;
                var customers = await customerTask;

                var query = (from od in orders
                             join em in employees on od.idEmployee equals em.IdEmployee into emGroup
                             from em in emGroup.DefaultIfEmpty()
                             join ct in customers on od.idCustomer equals ct.idCustomer
                             select new OrdersDTO
                             {
                                 idOrder = od.idOrder,
                                 idCustomer = od.idCustomer,
                                 nameCustomer = ct.fullName ?? "",
                                 idEmployee = od.idEmployee ?? "",
                                 nameEmployee = em?.FullName ?? "",
                                 dateOrder = DateTime.Parse(od.dateOrder).ToString("dd/MM/yyyy"),
                                 totalPrice = od.totalPrice,
                                 status = od.status,
                                 deliveryAddress = od.deliveryAddress,
                                 note = od.note ?? "",
                                 paymentMethod = od.paymentMethod,
                                 statusOrder = od.statusOrder,
                             }).ToList();
                return query;
            }
            catch (Exception)
            {
                return new List<OrdersDTO>();
            }
        }
        public async Task<List<OrdersDTO>> FillDataBy(string status)
        {
            try
            {
                var ordersTask = clientOrders.GetAllOrdersAsync();
                var employeeTask = clientEmployee.GetAllEmployeeAsync();
                var customerTask = clientCustomer.GetAllCustomerAsync();

                await Task.WhenAll(ordersTask, employeeTask, customerTask);

                var orders = await ordersTask;
                var employees = await employeeTask;
                var customers = await customerTask;

                var query = (from od in orders
                             join em in employees on od.idEmployee equals em.IdEmployee into emGroup
                             from em in emGroup.DefaultIfEmpty()
                             join ct in customers on od.idCustomer equals ct.idCustomer
                             where od.status == status.Trim()
                             select new OrdersDTO
                             {
                                 idOrder = od.idOrder,
                                 idCustomer = od.idCustomer,
                                 nameCustomer = ct.fullName ?? "",
                                 idEmployee = od.idEmployee ?? "",
                                 nameEmployee = em?.FullName ?? "",
                                 dateOrder = DateTime.Parse(od.dateOrder).ToString("dd/MM/yyyy"),
                                 totalPrice = od.totalPrice,
                                 status = od.status,
                                 deliveryAddress = od.deliveryAddress,
                                 note = od.note ?? "",
                                 paymentMethod = od.paymentMethod,
                                 statusOrder = od.statusOrder,
                             }).ToList();
                return query;
            }
            catch (Exception)
            {
                return new List<OrdersDTO>();
            }
        }
        public async Task<List<OrderDetailDTO>> GetOrderDetailByIdOrder(int idOrder)
        {
            try
            {
                var strainsTask = clientStrain.GetAllStrainsAsync();
                var ordersDetailTask = clientOrdersDetail.GetOrdersDetailByIdOrdersAsync(idOrder);

                await Task.WhenAll(ordersDetailTask, strainsTask);

                var strains = await strainsTask;
                var orderDetail = await ordersDetailTask;

                var query = (from od in orderDetail
                             join st in strains on od.idStrain equals st.idStrain
                             select new OrderDetailDTO
                             {
                                 idOrderDetail = od.idOrderDetail,
                                 idOrder = od.idOrder,
                                 idStrain = od.idStrain,
                                 nameStrain = st.strainNumber,
                                 quantity = od.quantity,
                                 price = od.price,
                             }).ToList();
                return query;
            }
            catch (Exception)
            {
                return new List<OrderDetailDTO>();
            }
        }
        public async Task<string> Update(int idOrder, string json)
        {
            try
            {
                return await clientOrders.Update(idOrder, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> PostBill(string json)
        {
            try
            {
                return await clientBill.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> PostBillDetail(string json)
        {
            try
            {
                return await clientBillDetail.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetLastBillId()
        {
            try
            {
                var bills = await clientBill.GetAllBillsAsync();
                if (bills == null || bills.Count == 0)
                {
                    return string.Empty;
                }
                return bills.Last().idBill;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<List<OrderDetailToAddBillDetailDTO>> GetListOrderDetailById(int idOrder)
        {
            try
            {
                return (from od 
                        in await clientOrdersDetail.GetAllOrdersDetailAsync()
                        where od.idOrder == idOrder
                        select new OrderDetailToAddBillDetailDTO
                        {
                            idStrain = od.idStrain,
                            quantity = od.quantity,
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<OrderDetailToAddBillDetailDTO>();
            }
        }
        public async Task<int> CountOrders()
        {
            try
            {
                var query = await clientOrders.GetAllOrdersAsync();
                var count = query.Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> CountOrders1()
        {
            try
            {
                var query = await clientOrders.GetAllOrdersAsync();
                var count = query.Count(cw => cw.status == "Đang chờ xử lý");
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> CountOrders2()
        {
            try
            {
                var query = await clientOrders.GetAllOrdersAsync();
                var count = query.Count(cw => cw.status == "Đang được xử lý");
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> CountOrders3()
        {
            try
            {
                var query = await clientOrders.GetAllOrdersAsync();
                var count = query.Count(cw => cw.status == "Đang vận chuyển");
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> CountOrders4()
        {
            try
            {
                var query = await clientOrders.GetAllOrdersAsync();
                var count = query.Count(cw => cw.status == "Đã hoàn thành");
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
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
        public async Task<CustomerCustomDTO> GetDataCustomerById(string id)
        {
            try
            {
                return (from cs
                        in await clientCustomer.GetAllCustomerAsync()
                        where cs.idCustomer == id
                        select new CustomerCustomDTO
                        {
                            IdCustomer = cs.idCustomer,
                            FullName = cs.fullName,
                            Email = cs.email,
                            PhoneNumber = cs.phoneNumber,
                            Address = BuildAddress(cs.address, cs.nameWard, cs.nameDistrict, cs.nameProvince),
                        }).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<BillToExportPdfDTO> GetDataBillById(string id)
        {
            try
            {
                return (from cs
                        in await clientBill.GetAllBillsAsync()
                        where cs.idBill == id
                        select new BillToExportPdfDTO
                        {
                            idBill = cs.idBill,
                            dateBill = cs.billDate,
                            status = cs.statusOfBill,
                            totalPrice = cs.totalPrice,
                        }).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<string> GetIdBillByIdOrder(int idOrder)
        {
            try
            {
                var query = (from bl in await clientBill.GetAllBillsAsync()
                             where bl.idOrder == idOrder
                             select bl.idBill).FirstOrDefault();
                return query;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<List<BillDetailToExportPdfDTO>> GetDataBillDetailByIdBill(string idBill)
        {
            try
            {
                var billDetailsTask = clientBillDetail.GetAllBillsDetailAsync();
                var strainsTask = clientStrain.GetAllStrainsAsync();
                var inventoryTask = clientInventory.GetAllInventoryAsync();

                await Task.WhenAll(billDetailsTask, strainsTask, inventoryTask);

                var billDetails = await billDetailsTask;
                var strains = await strainsTask;
                var inventories = await inventoryTask;

                var result = (from bd in billDetails
                              join st in strains on bd.idStrain equals st.idStrain
                              join it in inventories on st.idStrain equals it.idStrain
                              where bd.idBill == idBill
                              select new BillDetailToExportPdfDTO
                              {
                                  strainNumber = st.strainNumber,
                                  quantity = bd.quantity,
                                  price = it.price,
                              }).ToList();

                return result;
            }
            catch (Exception)
            {
                return new List<BillDetailToExportPdfDTO>();
            }
        }
        public async Task<string> UpdateStatusPay(string idBill, string status)
        {
            try
            {
                return await clientBill.PatchStatus(idBill, status);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
