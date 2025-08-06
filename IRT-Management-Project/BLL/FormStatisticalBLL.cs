using API;
using DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public class FormStatisticalBLL
    {
        ClientBill clientBill;
        ClientBillDetail clientBillDetail;
        ClientStrain clientStrain;
        ClientOrders clientOrders;
        ClientCustomer clientCustomer;
        ClientEmployee clientEmployee;
        public FormStatisticalBLL()
        {
            clientBill = new ClientBill();
            clientBillDetail = new ClientBillDetail();
            clientStrain = new ClientStrain();
            clientOrders = new ClientOrders();
            clientCustomer = new ClientCustomer();
            clientEmployee = new ClientEmployee();
        }
        public async Task<float> GetTotalPriceInMonth(int month)
        {
            try
            {
                var bills = await clientBill.GetAllBillsAsync();

                var totalPriceInMonth = (from bl in bills
                                         where DateTime.Parse(bl.billDate).Month == month
                                         select bl.totalPrice).Sum();

                return totalPriceInMonth;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return 0;
            }
        }
        public async Task<int> GetQuantitySoldByStrainNumber(string strainNumber)
        {
            try
            {
                var billDetailsTask = clientBillDetail.GetAllBillsDetailAsync();
                var strainsTask = clientStrain.GetAllStrainsAsync();
                var billsTask = clientBill.GetAllBillsAsync();

                await Task.WhenAll(billDetailsTask, strainsTask, billsTask);

                var billDetails = await billDetailsTask;
                var bills = await billsTask;
                var strains = await strainsTask;

                var query = (from bd in billDetails
                             join bl in bills on bd.idBill equals bl.idBill
                             join st in strains on bd.idStrain equals st.idStrain
                             where bl.statusOfBill == "Đã thanh toán" && st.idStrain == int.Parse(strainNumber) && st.strainNumber != null
                             select bd.quantity).Sum();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return 0;
            }
        }
        public async Task<List<string>> GetListStrainNumber()
        {
            try
            {
                return (from st in await clientStrain.GetAllStrainsAsync() where st.strainNumber != null select st.idStrain.ToString()).ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        public async Task<(string StrainNumber, int Quantity)> GetTopSoldStrain()
        {
            try
            {
                var billDetailsTask = clientBillDetail.GetAllBillsDetailAsync();
                var strainsTask = clientStrain.GetAllStrainsAsync();
                var billsTask = clientBill.GetAllBillsAsync();

                await Task.WhenAll(billDetailsTask, strainsTask, billsTask);

                var billDetails = await billDetailsTask;
                var bills = await billsTask;
                var strains = await strainsTask;

                var topStrain = (from bd in billDetails
                                 join bl in bills on bd.idBill equals bl.idBill
                                 join st in strains on bd.idStrain equals st.idStrain
                                 where bl.statusOfBill == "Đã thanh toán" && st.strainNumber != null
                                 group bd by st.strainNumber into g
                                 orderby g.Sum(bd => bd.quantity) descending
                                 select new
                                 {
                                     StrainNumber = g.Key,
                                     TotalQuantity = g.Sum(bd => bd.quantity)
                                 }).FirstOrDefault();

                return (topStrain?.StrainNumber, topStrain?.TotalQuantity ?? 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return (null, 0);
            }
        }
        public async Task<int> CountPaymentMethodBill(string payment)
        {
            try
            {
                var orderTask = clientOrders.GetAllOrdersAsync();
                var billTask = clientBill.GetAllBillsAsync();

                await Task.WhenAll(orderTask, billTask);

                var orders = await orderTask;
                var bills = await billTask;

                return (from od in orders
                        join bl in bills on od.idOrder equals bl.idOrder
                        where od.paymentMethod == payment && bl.statusOfBill == "Đã thanh toán"
                        select bl.idBill).Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<string> GetTopCustomer()
        {
            try
            {
                var bills = await clientBill.GetAllBillsAsync();

                var topCustomer = bills.GroupBy(b => b.idCustomer)
                                       .OrderByDescending(g => g.Count())
                                       .Select(g => g.Key)
                                       .FirstOrDefault();

                if (topCustomer != null)
                {
                    var query = (from cs in await clientCustomer.GetAllCustomerAsync()
                                 where cs.idCustomer == topCustomer
                                 select cs.fullName).FirstOrDefault();
                    return query;
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return string.Empty;
            }
        }
        public async Task<decimal> SumTotalBill()
        {
            try
            {
                var bills = await clientBill.GetAllBillsAsync();

                if (bills == null || !bills.Any())
                {
                    Console.WriteLine("Không có hóa đơn nào được tìm thấy.");
                    return 0;
                }

                decimal sumTotal = (decimal)bills.Where(bl => bl.statusOfBill == "Đã thanh toán").Sum(bl => bl.totalPrice);

                return sumTotal;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return 0;
            }
        }
        public async Task<int> TotalBill()
        {
            try
            {
                var bills = await clientBill.GetAllBillsAsync();

                if (bills == null || !bills.Any())
                {
                    Console.WriteLine("Không có hóa đơn nào được tìm thấy.");
                    return 0;
                }

                int totalBills = bills.Count(bl => bl.statusOfBill == "Đã thanh toán");

                return totalBills;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return 0;
            }
        }
        public async Task<List<BillCustomDTO>> GetListBillPaied()
        {
            try
            {
                return (from bl in await clientBill.GetAllBillsAsync()
                        join cs in await clientCustomer.GetAllCustomerAsync() on bl.idCustomer equals cs.idCustomer
                        join em in await clientEmployee.GetAllEmployeeAsync() on bl.idEmployee equals em.IdEmployee
                        where bl.statusOfBill == "Đã thanh toán"
                        select new BillCustomDTO
                        {
                            idBill = bl.idBill,
                            customer = $"{cs.fullName} (Mã KH: {cs.idCustomer})",
                            employee = $"{em.FullName} - {em.IdEmployee}",
                            dateBill = DateTime.Parse(bl.billDate).ToShortDateString(),
                            typeBill = bl.typeOfBill,
                            status = bl.statusOfBill,
                            totalPrice = $"{((decimal)bl.totalPrice).ToString("N2")}đ",
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<BillCustomDTO>();
            }
        }
        public async Task<List<BillCustomDTO>> GetListBillPaiedByConditionStatistical(DateTime date, int condition, int month, int year, int quarter)
        {
            try
            {
                var allBillsTask = clientBill.GetAllBillsAsync();
                var allCustomersTask = clientCustomer.GetAllCustomerAsync();
                var allEmployeesTask = clientEmployee.GetAllEmployeeAsync();

                await Task.WhenAll(allBillsTask, allCustomersTask, allEmployeesTask);

                var allBills = await allBillsTask;
                var allCustomers = await allCustomersTask;
                var allEmployees = await allEmployeesTask;

                if (condition == 1)
                {
                    return (from bl in allBills
                            join cs in allCustomers on bl.idCustomer equals cs.idCustomer
                            join em in allEmployees on bl.idEmployee equals em.IdEmployee
                            where bl.statusOfBill == "Đã thanh toán" && DateTime.Parse(bl.billDate).Date.Equals(date.Date)
                            select new BillCustomDTO
                            {
                                idBill = bl.idBill,
                                customer = $"{cs.fullName} (Mã KH: {cs.idCustomer})",
                                employee = $"{em.FullName} - {em.IdEmployee}",
                                dateBill = DateTime.Parse(bl.billDate).ToShortDateString(),
                                typeBill = bl.typeOfBill,
                                status = bl.statusOfBill,
                                totalPrice = $"{((decimal)bl.totalPrice).ToString("N2")}đ",
                            }).ToList();
                }
                else if (condition == 2)
                {
                    DateTime startOfWeek = date.AddDays(-(int)date.DayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(7);

                    return (from bl in allBills
                            join cs in allCustomers on bl.idCustomer equals cs.idCustomer
                            join em in allEmployees on bl.idEmployee equals em.IdEmployee
                            where bl.statusOfBill == "Đã thanh toán"
                                  && DateTime.Parse(bl.billDate) >= startOfWeek
                                  && DateTime.Parse(bl.billDate) < endOfWeek
                            select new BillCustomDTO
                            {
                                idBill = bl.idBill,
                                customer = $"{cs.fullName} (Mã KH: {cs.idCustomer})",
                                employee = $"{em.FullName} - {em.IdEmployee}",
                                dateBill = DateTime.Parse(bl.billDate).ToShortDateString(),
                                typeBill = bl.typeOfBill,
                                status = bl.statusOfBill,
                                totalPrice = $"{((decimal)bl.totalPrice).ToString("N2")}đ",
                            }).ToList();
                }
                else if (condition == 3)
                {
                    return (from bl in allBills
                            join cs in allCustomers on bl.idCustomer equals cs.idCustomer
                            join em in allEmployees on bl.idEmployee equals em.IdEmployee
                            where bl.statusOfBill == "Đã thanh toán" && DateTime.Parse(bl.billDate).Month == month
                            select new BillCustomDTO
                            {
                                idBill = bl.idBill,
                                customer = $"{cs.fullName} (Mã KH: {cs.idCustomer})",
                                employee = $"{em.FullName} - {em.IdEmployee}",
                                dateBill = DateTime.Parse(bl.billDate).ToShortDateString(),
                                typeBill = bl.typeOfBill,
                                status = bl.statusOfBill,
                                totalPrice = $"{((decimal)bl.totalPrice).ToString("N2")}đ",
                            }).ToList();
                }
                else if (condition == 4)
                {
                    DateTime startOfQuarter = new DateTime(year, (quarter - 1) * 3 + 1, 1);
                    DateTime endOfQuarter = startOfQuarter.AddMonths(3);
                    return (from bl in allBills
                            join cs in allCustomers on bl.idCustomer equals cs.idCustomer
                            join em in allEmployees on bl.idEmployee equals em.IdEmployee
                            where bl.statusOfBill == "Đã thanh toán"
                                                        && DateTime.Parse(bl.billDate) >= startOfQuarter
                                                        && DateTime.Parse(bl.billDate) < endOfQuarter
                            select new BillCustomDTO
                            {
                                idBill = bl.idBill,
                                customer = $"{cs.fullName} (Mã KH: {cs.idCustomer})",
                                employee = $"{em.FullName} - {em.IdEmployee}",
                                dateBill = DateTime.Parse(bl.billDate).ToShortDateString(),
                                typeBill = bl.typeOfBill,
                                status = bl.statusOfBill,
                                totalPrice = $"{((decimal)bl.totalPrice).ToString("N2")}đ",
                            }).ToList();
                }
                else if (condition == 5)
                {
                    return (from bl in allBills
                            join cs in allCustomers on bl.idCustomer equals cs.idCustomer
                            join em in allEmployees on bl.idEmployee equals em.IdEmployee
                            where bl.statusOfBill == "Đã thanh toán" && DateTime.Parse(bl.billDate).Year == year
                            select new BillCustomDTO
                            {
                                idBill = bl.idBill,
                                customer = $"{cs.fullName} (Mã KH: {cs.idCustomer})",
                                employee = $"{em.FullName} - {em.IdEmployee}",
                                dateBill = DateTime.Parse(bl.billDate).ToShortDateString(),
                                typeBill = bl.typeOfBill,
                                status = bl.statusOfBill,
                                totalPrice = $"{((decimal)bl.totalPrice).ToString("N2")}đ",
                            }).ToList();
                }
                else
                {
                    return new List<BillCustomDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return new List<BillCustomDTO>();
            }
        }

        public async Task<List<BillDetailCustomDTO>> GetListBillDetailPaied(string idBill)
        {
            try
            {
                return (from bl in await clientBillDetail.GetAllBillsDetailAsync()
                        join st in await clientStrain.GetAllStrainsAsync() on bl.idStrain equals st.idStrain
                        where bl.idBill == idBill
                        select new BillDetailCustomDTO
                        {
                            idBill = bl.idBill,
                            strainNumber = st.strainNumber,
                            quantity = $"{bl.quantity} chủng",
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<BillDetailCustomDTO>();
            }
        }
        public async Task<StatisticsResultDTO> GetEnhancedStatisticsByDate(DateTime date, int condition, int month, int year, int quarter)
        {
            try
            {
                var billsTask = clientBill.GetAllBillsAsync();
                var billDetailsTask = clientBillDetail.GetAllBillsDetailAsync();
                var strainsTask = clientStrain.GetAllStrainsAsync();
                var ordersTask = clientOrders.GetAllOrdersAsync();

                await Task.WhenAll(billsTask, billDetailsTask, strainsTask, ordersTask);

                var bills = await billsTask;
                var billDetails = await billDetailsTask;
                var strains = await strainsTask;
                var orders = await ordersTask;


                if (condition == 1)
                {
                    var filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán" && DateTime.Parse(bl.billDate).Date == date.Date).ToList();
                    int totalBills = filteredBills.Count();

                    decimal totalRevenue = (decimal)filteredBills.Sum(bl => bl.totalPrice);

                    var billIds = filteredBills.Select(bl => bl.idBill).ToList();

                    var strainQuantities = (from bd in billDetails
                                            join billId in billIds on bd.idBill equals billId
                                            select new { bd.idStrain, bd.quantity }).ToList();

                    int totalStrainSold = strainQuantities.Sum(sq => sq.quantity);

                    var strainNumbers = (from sq in strainQuantities
                                         join st in strains on sq.idStrain equals st.idStrain
                                         select new { st.strainNumber, sq.quantity }).ToList();

                    var topSoldStrainGroup = strainNumbers.GroupBy(sn => sn.strainNumber)
                                                          .OrderByDescending(g => g.Sum(x => x.quantity))
                                                          .FirstOrDefault();

                    var topSoldStrainNumber = topSoldStrainGroup?.Key;
                    var topSoldStrainQuantity = topSoldStrainGroup?.Sum(x => x.quantity) ?? 0;

                    StatisticsResultDTO result = new StatisticsResultDTO
                    {
                        TotalBills = totalBills,
                        TotalRevenue = totalRevenue,
                        TotalStrainSold = totalStrainSold,
                        TopSoldStrainNumber = topSoldStrainNumber,
                        TopSoldStrainQuantity = topSoldStrainQuantity,
                    };

                    return result;
                }

                else if (condition == 2)
                {
                    DateTime startOfWeek = date.AddDays(-(int)date.DayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(7);

                    var filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán"
                                              && DateTime.Parse(bl.billDate) >= startOfWeek
                                              && DateTime.Parse(bl.billDate) < endOfWeek);

                    int totalBills = filteredBills.Count();

                    decimal totalRevenue = (decimal)filteredBills.Sum(bl => bl.totalPrice);

                    var billIds = filteredBills.Select(bl => bl.idBill).ToList();

                    var strainQuantities = (from bd in billDetails
                                            join billId in billIds on bd.idBill equals billId
                                            select new { bd.idStrain, bd.quantity }).ToList();

                    int totalStrainSold = strainQuantities.Sum(sq => sq.quantity);

                    var strainNumbers = (from sq in strainQuantities
                                         join st in strains on sq.idStrain equals st.idStrain
                                         select new { st.strainNumber, sq.quantity }).ToList();

                    var topSoldStrainGroup = strainNumbers.GroupBy(sn => sn.strainNumber)
                                                          .OrderByDescending(g => g.Sum(x => x.quantity))
                                                          .FirstOrDefault();

                    var topSoldStrainNumber = topSoldStrainGroup?.Key;
                    var topSoldStrainQuantity = topSoldStrainGroup?.Sum(x => x.quantity) ?? 0;

                    StatisticsResultDTO result = new StatisticsResultDTO
                    {
                        TotalBills = totalBills,
                        TotalRevenue = totalRevenue,
                        TotalStrainSold = totalStrainSold,
                        TopSoldStrainNumber = topSoldStrainNumber,
                        TopSoldStrainQuantity = topSoldStrainQuantity,
                    };

                    return result;
                }
                else if (condition == 3)
                {
                    var filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán" && DateTime.Parse(bl.billDate).Month == month).ToList();
                    int totalBills = filteredBills.Count();

                    decimal totalRevenue = (decimal)filteredBills.Sum(bl => bl.totalPrice);

                    var billIds = filteredBills.Select(bl => bl.idBill).ToList();

                    var strainQuantities = (from bd in billDetails
                                            join billId in billIds on bd.idBill equals billId
                                            select new { bd.idStrain, bd.quantity }).ToList();

                    int totalStrainSold = strainQuantities.Sum(sq => sq.quantity);

                    var strainNumbers = (from sq in strainQuantities
                                         join st in strains on sq.idStrain equals st.idStrain
                                         select new { st.strainNumber, sq.quantity }).ToList();

                    var topSoldStrainGroup = strainNumbers.GroupBy(sn => sn.strainNumber)
                                                          .OrderByDescending(g => g.Sum(x => x.quantity))
                                                          .FirstOrDefault();

                    var topSoldStrainNumber = topSoldStrainGroup?.Key;
                    var topSoldStrainQuantity = topSoldStrainGroup?.Sum(x => x.quantity) ?? 0;

                    StatisticsResultDTO result = new StatisticsResultDTO
                    {
                        TotalBills = totalBills,
                        TotalRevenue = totalRevenue,
                        TotalStrainSold = totalStrainSold,
                        TopSoldStrainNumber = topSoldStrainNumber,
                        TopSoldStrainQuantity = topSoldStrainQuantity,
                    };

                    return result;
                }
                else if (condition == 4)
                {
                    DateTime startOfQuarter = new DateTime(year, (quarter - 1) * 3 + 1, 1);
                    DateTime endOfQuarter = startOfQuarter.AddMonths(3);
                    var filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán"
                                                       && DateTime.Parse(bl.billDate) >= startOfQuarter
                                                       && DateTime.Parse(bl.billDate) < endOfQuarter);
                    int totalBills = filteredBills.Count();

                    decimal totalRevenue = (decimal)filteredBills.Sum(bl => bl.totalPrice);

                    var billIds = filteredBills.Select(bl => bl.idBill).ToList();

                    var strainQuantities = (from bd in billDetails
                                            join billId in billIds on bd.idBill equals billId
                                            select new { bd.idStrain, bd.quantity }).ToList();

                    int totalStrainSold = strainQuantities.Sum(sq => sq.quantity);

                    var strainNumbers = (from sq in strainQuantities
                                         join st in strains on sq.idStrain equals st.idStrain
                                         select new { st.strainNumber, sq.quantity }).ToList();

                    var topSoldStrainGroup = strainNumbers.GroupBy(sn => sn.strainNumber)
                                                          .OrderByDescending(g => g.Sum(x => x.quantity))
                                                          .FirstOrDefault();

                    var topSoldStrainNumber = topSoldStrainGroup?.Key;
                    var topSoldStrainQuantity = topSoldStrainGroup?.Sum(x => x.quantity) ?? 0;

                    StatisticsResultDTO result = new StatisticsResultDTO
                    {
                        TotalBills = totalBills,
                        TotalRevenue = totalRevenue,
                        TotalStrainSold = totalStrainSold,
                        TopSoldStrainNumber = topSoldStrainNumber,
                        TopSoldStrainQuantity = topSoldStrainQuantity,
                    };

                    return result;
                }
                else if (condition == 5)
                {
                    var filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán" && DateTime.Parse(bl.billDate).Year == year).ToList();
                    int totalBills = filteredBills.Count();

                    decimal totalRevenue = (decimal)filteredBills.Sum(bl => bl.totalPrice);

                    var billIds = filteredBills.Select(bl => bl.idBill).ToList();

                    var strainQuantities = (from bd in billDetails
                                            join billId in billIds on bd.idBill equals billId
                                            select new { bd.idStrain, bd.quantity }).ToList();

                    int totalStrainSold = strainQuantities.Sum(sq => sq.quantity);

                    var strainNumbers = (from sq in strainQuantities
                                         join st in strains on sq.idStrain equals st.idStrain
                                         select new { st.strainNumber, sq.quantity }).ToList();

                    var topSoldStrainGroup = strainNumbers.GroupBy(sn => sn.strainNumber)
                                                          .OrderByDescending(g => g.Sum(x => x.quantity))
                                                          .FirstOrDefault();

                    var topSoldStrainNumber = topSoldStrainGroup?.Key;
                    var topSoldStrainQuantity = topSoldStrainGroup?.Sum(x => x.quantity) ?? 0;

                    StatisticsResultDTO result = new StatisticsResultDTO
                    {
                        TotalBills = totalBills,
                        TotalRevenue = totalRevenue,
                        TotalStrainSold = totalStrainSold,
                        TopSoldStrainNumber = topSoldStrainNumber,
                        TopSoldStrainQuantity = topSoldStrainQuantity,
                    };

                    return result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return null;
            }
        }
        public async Task<List<StatisticsStrainDTO>> GetListStatisticsStrain(DateTime date, int condition, int month, int year, int quarter)
        {
            try
            {
                var bills = await clientBill.GetAllBillsAsync();
                var billDetails = await clientBillDetail.GetAllBillsDetailAsync();
                var strains = await clientStrain.GetAllStrainsAsync();

                IEnumerable<ApiBillDTO> filteredBills;
                if (condition == 1)
                {
                    filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán" && DateTime.Parse(bl.billDate).Date == date.Date);
                }
                else if (condition == 2)
                {
                    DateTime startOfWeek = date.AddDays(-(int)date.DayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(7);
                    filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán"
                                                    && DateTime.Parse(bl.billDate) >= startOfWeek
                                                    && DateTime.Parse(bl.billDate) < endOfWeek);
                }
                else if (condition == 3)
                {
                    filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán" && DateTime.Parse(bl.billDate).Month == month);
                }
                else if (condition == 4)
                {
                    DateTime startOfQuarter = new DateTime(year, (quarter - 1) * 3 + 1, 1);
                    DateTime endOfQuarter = startOfQuarter.AddMonths(3);
                    filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán"
                                                       && DateTime.Parse(bl.billDate) >= startOfQuarter
                                                       && DateTime.Parse(bl.billDate) < endOfQuarter);
                }
                else if (condition == 5)
                {
                    filteredBills = bills.Where(bl => bl.statusOfBill == "Đã thanh toán" && DateTime.Parse(bl.billDate).Year == year);
                }
                else
                {
                    return new List<StatisticsStrainDTO>();
                }

                var query = (from bl in filteredBills
                             join bd in billDetails on bl.idBill equals bd.idBill
                             join st in strains on bd.idStrain equals st.idStrain
                             group bd by new { st.strainNumber } into strainGroup
                             select new StatisticsStrainDTO
                             {
                                 strainNumber = strainGroup.Key.strainNumber,
                                 strainQuantity = strainGroup.Sum(bd => bd.quantity)
                             }).ToList();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return new List<StatisticsStrainDTO>();
            }
        }
        public async Task<string> GetTotalProduct()
        {
            try
            {
                var allBillDetails = await clientBillDetail.GetAllBillsDetailAsync();

                var totalQuantity = allBillDetails.Sum(bd => bd.quantity);

                return totalQuantity.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
}
