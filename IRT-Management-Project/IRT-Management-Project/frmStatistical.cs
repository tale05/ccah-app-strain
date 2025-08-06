using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using BLL;
using DTO;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Windows.Documents;
using System.IO;

namespace IRT_Management_Project
{
    public partial class frmStatistical : Form
    {
        private FormStatisticalBLL sblll;
        private float[] monthlyTotalPrices = new float[12];
        private float pay1, pay2, pay3;
        private List<string> lstStrainNumber = new List<string>();
        private List<int> lstQuantity = new List<int>();
        private int condition = 0;
        private string nameTopStrain = string.Empty, quantityTopStrain = string.Empty;

        public frmStatistical()
        {
            InitializeComponent();
            sblll = new FormStatisticalBLL();
        }

        private async void frmStatistical_Load(object sender, EventArgs e)
        {
            try
            {
                lblTopCustomer.Text = $"Khách hàng thân thiết: {await sblll.GetTopCustomer()}";
                lblTongdoanhthu.Text = $"Tổng doanh thu: {(await sblll.SumTotalBill()).ToString("N2")} đ";
                lblTonghoadon.Text = $"Tổng hóa đơn: {await sblll.TotalBill()}";
                lblNameUser.Text = frmLogin.fullNameEmployee;

                LoadDataTableBill();
                LoadComboBoxYears(cboNam, 10);

                //-----------------------------------------------
                // Biểu đồ thống kê theo tháng

                Task[] tasks = new Task[12];
                for (int month = 1; month <= 12; month++)
                {
                    int index = month - 1;
                    tasks[index] = UpdateMonthTotalPriceAsync(month, index);
                }
                await Task.WhenAll(tasks);
                ColumnChart1();

                //-----------------------------------------------
                // Biểu đồ thống kê số lượng strain theo strainNumber

                lstStrainNumber = await sblll.GetListStrainNumber();
                foreach (string strain in lstStrainNumber)
                {
                    int count = await sblll.GetQuantitySoldByStrainNumber(strain);
                    lstQuantity.Add(count);
                }
                ColumnChart2();

                //-----------------------------------------------
                // Biểu đồ tròn
                pay1 = await sblll.CountPaymentMethodBill("cod");
                pay2 = await sblll.CountPaymentMethodBill("vnpay");
                pay3 = await sblll.CountPaymentMethodBill("zalopay");
                PieChart1();

                //-----------------------------------------------
                var topSoldStrain = await sblll.GetTopSoldStrain();
                lblTopStrain.Text = $"Chủng được bán nhiều nhất là: {topSoldStrain.StrainNumber}";
                nameTopStrain = topSoldStrain.StrainNumber;
                UpdateSolidGauge(topSoldStrain.StrainNumber, topSoldStrain.Quantity);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private async Task UpdateMonthTotalPriceAsync(int month, int index)
        {
            try
            {
                monthlyTotalPrices[index] = await sblll.GetTotalPriceInMonth(month);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu tháng {month}: {ex.Message}");
            }
        }

        private void ColumnChart1()
        {
            try
            {
                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();

                var months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                                        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

                SeriesCollection seriesCollection = new SeriesCollection();

                ColumnSeries columnSeries = new ColumnSeries
                {
                    Values = new ChartValues<float>(monthlyTotalPrices),
                    Title = "Tổng tiền bán hàng"
                };

                var gradientStopCollection = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Colors.MediumSeaGreen, 1),
                    new GradientStop(System.Windows.Media.Colors.LightGreen, 0)
                };

                var fillBrush = new LinearGradientBrush(gradientStopCollection);
                columnSeries.Fill = fillBrush;

                seriesCollection.Add(columnSeries);

                LineSeries lineSeries = new LineSeries
                {
                    Values = new ChartValues<float>(monthlyTotalPrices),
                    Title = "Biến động",
                    PointGeometry = null,
                    StrokeThickness = 2,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    Stroke = System.Windows.Media.Brushes.Red
                };

                seriesCollection.Add(lineSeries);

                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Tháng",
                    Labels = months,
                    Separator = new Separator
                    {
                        Step = 1,
                        IsEnabled = false
                    }
                });

                cartesianChart1.AxisY.Add(new Axis
                {
                    LabelFormatter = value => value.ToString("N2"),
                    MinValue = 0
                });

                cartesianChart1.Series = seriesCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void ColumnChart2()
        {
            try
            {
                cartesianChart2.Series.Clear();
                cartesianChart2.AxisX.Clear();
                cartesianChart2.AxisY.Clear();

                SeriesCollection seriesCollection = new SeriesCollection();

                ColumnSeries columnSeries = new ColumnSeries
                {
                    Values = new ChartValues<int>(lstQuantity),
                    Title = "Số lượng bán ra"
                };

                var gradientStopCollection = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Colors.MediumSeaGreen, 1),
                    new GradientStop(System.Windows.Media.Colors.LightGreen, 0)
                };

                var fillBrush = new LinearGradientBrush(gradientStopCollection);
                columnSeries.Fill = fillBrush;

                seriesCollection.Add(columnSeries);

                cartesianChart2.AxisX.Add(new Axis
                {
                    Title = "Id strain",
                    Labels = lstStrainNumber,
                    Separator = new Separator
                    {
                        Step = 1,
                        IsEnabled = false
                    }
                });

                cartesianChart2.AxisY.Add(new Axis
                {
                    LabelFormatter = value => value.ToString("N0"),
                    MinValue = 0
                });

                cartesianChart2.Series = seriesCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void PieChart1()
        {
            pieChart2.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Thanh toán COD",
                    Values = new ChartValues<double> {pay1},
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation),
                },
                new PieSeries
                {
                    Title = "Thanh toán VNPAY",
                    Values = new ChartValues<double> {pay2},
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation),
                },
                new PieSeries
                {
                    Title = "Thanh toán ZALOPAY",
                    Values = new ChartValues<double> {pay3},
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation),
                },
            };
            pieChart2.LegendLocation = LegendLocation.Bottom;
        }

        private void UpdateSolidGauge(string strainNumber, int quantity)
        {
            try
            {
                solidGauge1.From = 0;
                solidGauge1.To = quantity * 1.2;
                solidGauge1.Value = quantity;
                solidGauge1.LabelFormatter = value => value == quantity ? $"{strainNumber}: {quantity}" : value.ToString("N0");

                solidGauge1.Base.Foreground = System.Windows.Media.Brushes.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private async void LoadDataTableBill()
        {
            tblBill.DataSource = null;
            tblBill.Rows.Clear();
            tblBill.Columns.Clear();
            tblBill.DataSource = await sblll.GetListBillPaied();
            DesignTableBill();
        }

        private async void LoadDataTableBillByConditionStatistical(DateTime date, int condition, int month, int year, int quarter)
        {
            tblBill.DataSource = null;
            tblBill.Rows.Clear();
            tblBill.Columns.Clear();
            tblBill.DataSource = await sblll.GetListBillPaiedByConditionStatistical(date, condition, month, year, quarter);
            DesignTableBill();
        }

        private void DesignTableBill()
        {
            tblBill.Columns[0].Name = "idBill";
            tblBill.Columns[0].HeaderText = "Mã hóa đơn";
            tblBill.Columns[1].Name = "customer";
            tblBill.Columns[1].HeaderText = "Khách hàng";
            tblBill.Columns[2].Name = "employee";
            tblBill.Columns[2].HeaderText = "Nhân viên hoàn tất đơn";
            tblBill.Columns[3].Name = "dateBill";
            tblBill.Columns[3].HeaderText = "Ngày lập hóa đơn";
            tblBill.Columns[4].Name = "typeBill";
            tblBill.Columns[4].HeaderText = "Loại hóa đơn";
            tblBill.Columns[5].Name = "status";
            tblBill.Columns[5].HeaderText = "Trạng thái";
            tblBill.Columns[6].Name = "totalPrice";
            tblBill.Columns[6].HeaderText = "Tổng tiền";

            tblBill.Columns["dateBill"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tblBill.Columns["status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tblBill.Columns["totalPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private async Task LoadDataTableBillDetail(string idBill)
        {
            tblBillDetail.DataSource = null;
            tblBillDetail.Rows.Clear();
            tblBillDetail.Columns.Clear();
            tblBillDetail.DataSource = await sblll.GetListBillDetailPaied(idBill);
            DesignTableBillDetail();
        }

        private void DesignTableBillDetail()
        {
            tblBillDetail.Columns[0].Name = "idBill";
            tblBillDetail.Columns[0].HeaderText = "Mã hóa đơn";
            tblBillDetail.Columns[1].Name = "strainNumber";
            tblBillDetail.Columns[1].HeaderText = "Tên chủng";
            tblBillDetail.Columns[2].Name = "quantity";
            tblBillDetail.Columns[2].HeaderText = "Số lượng";
        }

        private async void StatisticByDay(DateTime selectedDate, int condition, int month, int year, int quarter)
        {
            var rs = await sblll.GetEnhancedStatisticsByDate(selectedDate, condition, month, year, quarter);

            if (rs != null)
            {
                lblTonghdNgay.Text = $"{rs.TotalBills} đơn";
                lblTongdoanhthuNgay.Text = $"{rs.TotalRevenue.ToString("N0")}đ";
                lblTongstraindabanNgay.Text = $"{rs.TotalStrainSold} chủng";
                lblStrainnhieunhatNgay.Text = $"{rs.TopSoldStrainNumber} - Số lượng đã bán: {rs.TopSoldStrainQuantity}";
            }

            List<StatisticsStrainDTO> lst = await sblll.GetListStatisticsStrain(selectedDate, condition, month, year, quarter);
            tblHienstrainNgay.DataSource = null;
            tblHienstrainNgay.Rows.Clear();
            tblHienstrainNgay.Columns.Clear();
            tblHienstrainNgay.DataSource = lst;
            tblHienstrainNgay.Columns[0].HeaderText = "Mã chủng";
            tblHienstrainNgay.Columns[1].HeaderText = "Số lượng";
        }

        private void LoadComboBoxYears(ComboBox cboNam, int numberOfYears)
        {
            cboNam.Items.Clear();
            cboNam.Items.Add("Chọn");

            int currentYear = DateTime.Now.Year;

            for (int i = 0; i < numberOfYears; i++)
            {
                cboNam.Items.Add((currentYear - i).ToString());
            }
            cboNam.SelectedIndex = 0;
        }

        //---------------------------------------Event----------------------------------------

        private void frmStatistical_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
        }

        private async void tblBill_Click(object sender, EventArgs e)
        {
            int i = tblBill.CurrentRow.Index;
            await LoadDataTableBillDetail(tblBill.Rows[i].Cells["idBill"].Value.ToString());
            panel13.Visible = true;
        }

        private void ResetField()
        {
            lblTonghdNgay.Text = string.Empty;
            lblTongdoanhthuNgay.Text = string.Empty;
            lblTongstraindabanNgay.Text= string.Empty;
            tblHienstrainNgay.DataSource = null;
            lblStrainnhieunhatNgay.Text = string.Empty;
        }

        private async void btnThongKeNgay_Click(object sender, EventArgs e)
        {
            condition = 1;
            lblTieudeThongke.Text = "Chọn 1 ngày để thống kê:";
            pnlThongke.Visible = true;
            dateTimePicker1.Visible = true;
            cboThang.Visible = false;
            cboQuy.Visible = false;
            cboNam.Visible = false;
            panel13.Visible = false;
            LoadDataTableBill();
            await LoadDataTableBillDetail(string.Empty);

            dateTimePicker1.Value = DateTime.Now;
            ResetField();
        }

        private async void btnThongKeTuan_Click(object sender, EventArgs e)
        {
            condition = 2;
            lblTieudeThongke.Text = "Chọn 1 ngày và sẽ thống kê ngày trong tuần:";
            pnlThongke.Visible = true;
            dateTimePicker1.Visible = true;
            cboThang.Visible = false;
            cboQuy.Visible = false;
            cboNam.Visible = false;
            panel13.Visible = false;
            LoadDataTableBill();
            await LoadDataTableBillDetail(string.Empty);

            dateTimePicker1.Value = DateTime.Now;
            ResetField();
        }

        private async void btnThongKeThang_Click(object sender, EventArgs e)
        {
            condition = 3;
            lblTieudeThongke.Text = "Chọn 1 tháng:";
            pnlThongke.Visible = true;
            dateTimePicker1.Visible = false;
            cboThang.Visible = true;
            cboQuy.Visible = false;
            cboNam.Visible = false;
            panel13.Visible = false;
            LoadDataTableBill();
            await LoadDataTableBillDetail(string.Empty);

            cboThang.SelectedIndex = 0;
            ResetField();
        }

        private async void btnThongKeQuy_Click(object sender, EventArgs e)
        {
            condition = 4;
            lblTieudeThongke.Text = "Chọn 1 quý:";
            pnlThongke.Visible = true;
            dateTimePicker1.Visible = false;
            cboThang.Visible = false;
            cboQuy.Visible = true;
            cboNam.Visible = false;
            panel13.Visible = false;
            LoadDataTableBill();
            await LoadDataTableBillDetail(string.Empty);

            cboQuy.SelectedIndex = 0;
            ResetField();
        }

        private async void btnThongKeNam_Click(object sender, EventArgs e)
        {
            condition = 5;
            lblTieudeThongke.Text = "Chọn 1 năm:";
            pnlThongke.Visible = true;
            dateTimePicker1.Visible = false;
            cboThang.Visible = false;
            cboQuy.Visible = false;
            cboNam.Visible = true;
            panel13.Visible = false;
            LoadDataTableBill();
            await LoadDataTableBillDetail(string.Empty);

            cboNam.SelectedIndex = 0;
            ResetField();
        }

        private void btnXemThongKe_Click(object sender, EventArgs e)
        {
            if (condition == 1)
            {
                DateTime selectedDate = dateTimePicker1.Value;
                StatisticByDay(selectedDate, 1, 0, 0, 0);
                LoadDataTableBillByConditionStatistical(selectedDate, 1, 0, 0, 0);
            }
            else if (condition == 2)
            {
                DateTime selectedDate = dateTimePicker1.Value;
                StatisticByDay(selectedDate, 2, 0, 0, 0);
                LoadDataTableBillByConditionStatistical(selectedDate, 2, 0, 0, 0);
            }
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThang.SelectedIndex == 0)
            {
                StatisticByDay(DateTime.Now, 3, 0, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 0, 0, 0);
            }
            else if (cboThang.SelectedIndex == 1)
            {
                StatisticByDay(DateTime.Now, 3, 1, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 1, 0, 0);
            }
            else if (cboThang.SelectedIndex == 2)
            {
                StatisticByDay(DateTime.Now, 3, 2, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 2, 0, 0);
            }
            else if (cboThang.SelectedIndex == 3)
            {
                StatisticByDay(DateTime.Now, 3, 3, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 3, 0, 0);
            }
            else if (cboThang.SelectedIndex == 4)
            {
                StatisticByDay(DateTime.Now, 3, 4, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 4, 0, 0);
            }
            else if (cboThang.SelectedIndex == 5)
            {
                StatisticByDay(DateTime.Now, 3, 5, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 5, 0, 0);
            }
            else if (cboThang.SelectedIndex == 6)
            {
                StatisticByDay(DateTime.Now, 3, 6, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 6, 0, 0);
            }
            else if (cboThang.SelectedIndex == 7)
            {
                StatisticByDay(DateTime.Now, 3, 7, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 7, 0, 0);
            }
            else if (cboThang.SelectedIndex == 8)
            {
                StatisticByDay(DateTime.Now, 3, 8, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 8, 0, 0);
            }
            else if (cboThang.SelectedIndex == 9)
            {
                StatisticByDay(DateTime.Now, 3, 9, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 9, 0, 0);
            }
            else if (cboThang.SelectedIndex == 10)
            {
                StatisticByDay(DateTime.Now, 3, 10, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 10, 0, 0);
            }
            else if (cboThang.SelectedIndex == 11)
            {
                StatisticByDay(DateTime.Now, 3, 11, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 11, 0, 0);
            }
            else if (cboThang.SelectedIndex == 12)
            {
                StatisticByDay(DateTime.Now, 3, 12, 0, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 3, 12, 0, 0);
            }
        }

        private void cboQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQuy.SelectedIndex == 0)
            {

            }
            else if (cboQuy.SelectedIndex == 1)
            {
                StatisticByDay(DateTime.Now, 4, 0, DateTime.Now.Year, 1);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 4, 0, DateTime.Now.Year, 1);
            }
            else if (cboQuy.SelectedIndex == 2)
            {
                StatisticByDay(DateTime.Now, 4, 0, DateTime.Now.Year, 2);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 4, 0, DateTime.Now.Year, 2);
            }
            else if (cboQuy.SelectedIndex == 3)
            {
                StatisticByDay(DateTime.Now, 4, 0, DateTime.Now.Year, 3);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 4, 0, DateTime.Now.Year, 3);
            }
            else if (cboQuy.SelectedIndex == 4)
            {
                StatisticByDay(DateTime.Now, 4, 0, DateTime.Now.Year, 4);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 4, 0, DateTime.Now.Year, 4);
            }
        }

        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            int yearValue = 0;
            if (cboNam.SelectedIndex != 0)
            {
                yearValue = int.Parse(cboNam.Text);
                StatisticByDay(DateTime.Now, 5, 0, yearValue, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 5, 0, yearValue, 0);
            }
            else
            {
                StatisticByDay(DateTime.Now, 5, 0, yearValue, 0);
                LoadDataTableBillByConditionStatistical(DateTime.Now, 5, 0, yearValue, 0);
            }
        }

        private async void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                var tasks = new Task[]
                {
                    sblll.GetListBillPaied(),
                    sblll.GetTotalProduct(),
                    sblll.GetTopCustomer(),
                    sblll.SumTotalBill(),
                    sblll.TotalBill(),
                    sblll.GetTopSoldStrain()
                };

                await Task.WhenAll(tasks);

                var lst = ((Task<List<BillCustomDTO>>)tasks[0]).Result;
                var totalProduct = ((Task<string>)tasks[1]).Result;
                var customer = ((Task<string>)tasks[2]).Result;
                var tongdoanhthu = ((Task<decimal>)tasks[3]).Result.ToString("N2");
                var tonghoadon = ((Task<int>)tasks[4]).Result.ToString();
                var topSoldStrain = ((Task<(string StrainNumber, int Quantity)>)tasks[5]).Result;

                var topStrainNumberValue = topSoldStrain.StrainNumber;
                var topQuantityValue = topSoldStrain.Quantity.ToString();


                if (condition == 1)
                {
                    string thongkeNgay = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                    string tongHdNgay = lblTonghdNgay.Text.Trim();
                    string doanhthuNgay = lblTongdoanhthuNgay.Text.Trim();
                    string tongSpNgay = lblTongstraindabanNgay.Text.Trim();
                    string topStrainNgay = lblStrainnhieunhatNgay.Text.Trim();
                    List<StatisticsStrainDTO> lst1 = new List<StatisticsStrainDTO>();
                    if (tblHienstrainNgay == null)
                    {
                        lst1 = null;
                    }
                    else
                    {
                        foreach (DataGridViewRow row in tblHienstrainNgay.Rows)
                        {
                            if (row.IsNewRow)
                            {
                                continue;
                            }

                            string strainNumber = row.Cells[0].Value?.ToString();
                            int strainQuantity = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : 0;

                            var dto = new StatisticsStrainDTO
                            {
                                strainNumber = strainNumber,
                                strainQuantity = strainQuantity
                            };

                            lst1.Add(dto);
                        }
                    }
                    SaveFilePdfInvoice(1, lst,
                    tongdoanhthu, tonghoadon, totalProduct, topStrainNumberValue, topQuantityValue, customer,
                    thongkeNgay, tongHdNgay, doanhthuNgay, tongSpNgay, topStrainNgay, lst1,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null);
                }
                else if (condition == 2)
                {
                    string thongkeTuan = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                    string tongHdTuan = lblTonghdNgay.Text.Trim();
                    string doanhthuTuan = lblTongdoanhthuNgay.Text.Trim();
                    string tongSpTuan = lblTongstraindabanNgay.Text.Trim();
                    string topStrainTuan = lblStrainnhieunhatNgay.Text.Trim();
                    List<StatisticsStrainDTO> lst2 = new List<StatisticsStrainDTO>();
                    if (tblHienstrainNgay == null)
                    {
                        lst2 = null;
                    }
                    else
                    {
                        foreach (DataGridViewRow row in tblHienstrainNgay.Rows)
                        {
                            if (row.IsNewRow)
                            {
                                continue;
                            }

                            string strainNumber = row.Cells[0].Value?.ToString();
                            int strainQuantity = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : 0;

                            var dto = new StatisticsStrainDTO
                            {
                                strainNumber = strainNumber,
                                strainQuantity = strainQuantity
                            };

                            lst2.Add(dto);
                        }
                    }
                    SaveFilePdfInvoice(2, lst,
                    tongdoanhthu, tonghoadon, totalProduct, topStrainNumberValue, topQuantityValue, customer,
                    "", "", "", "", "", null,
                    thongkeTuan, tongHdTuan, doanhthuTuan, tongSpTuan, topStrainTuan, lst2,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null);
                }
                else if (condition == 3)
                {
                    string thongkeThang = cboThang.Text;
                    string tongHdThang = lblTonghdNgay.Text.Trim();
                    string doanhthuThang = lblTongdoanhthuNgay.Text.Trim();
                    string tongSpThang = lblTongstraindabanNgay.Text.Trim();
                    string topStrainThang = lblStrainnhieunhatNgay.Text.Trim();
                    List<StatisticsStrainDTO> lst3 = new List<StatisticsStrainDTO>();
                    if (tblHienstrainNgay == null)
                    {
                        lst3 = null;
                    }
                    else
                    {
                        foreach (DataGridViewRow row in tblHienstrainNgay.Rows)
                        {
                            if (row.IsNewRow)
                            {
                                continue;
                            }

                            string strainNumber = row.Cells[0].Value?.ToString();
                            int strainQuantity = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : 0;

                            var dto = new StatisticsStrainDTO
                            {
                                strainNumber = strainNumber,
                                strainQuantity = strainQuantity
                            };

                            lst3.Add(dto);
                        }
                    }
                    SaveFilePdfInvoice(3, lst,
                    tongdoanhthu, tonghoadon, totalProduct, topStrainNumberValue, topQuantityValue, customer,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null,
                    thongkeThang, tongHdThang, doanhthuThang, tongSpThang, topStrainThang, lst3,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null);
                }   
                else if (condition == 4)
                {
                    string thongkeQuy = cboQuy.Text;
                    string tongHdQuy = lblTonghdNgay.Text.Trim();
                    string doanhthuQuy = lblTongdoanhthuNgay.Text.Trim();
                    string tongSpQuy = lblTongstraindabanNgay.Text.Trim();
                    string topStrainQuy = lblStrainnhieunhatNgay.Text.Trim();
                    List<StatisticsStrainDTO> lst4 = new List<StatisticsStrainDTO>();
                    if (tblHienstrainNgay == null)
                    {
                        lst4 = null;
                    }
                    else
                    {
                        foreach (DataGridViewRow row in tblHienstrainNgay.Rows)
                        {
                            if (row.IsNewRow)
                            {
                                continue;
                            }

                            string strainNumber = row.Cells[0].Value?.ToString();
                            int strainQuantity = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : 0;

                            var dto = new StatisticsStrainDTO
                            {
                                strainNumber = strainNumber,
                                strainQuantity = strainQuantity
                            };

                            lst4.Add(dto);
                        }
                    }
                    SaveFilePdfInvoice(4, lst,
                    tongdoanhthu, tonghoadon, totalProduct, topStrainNumberValue, topQuantityValue, customer,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null,
                    thongkeQuy, tongHdQuy, doanhthuQuy, tongSpQuy, topStrainQuy, lst4,
                    "", "", "", "", "", null);
                }   
                else if (condition == 5)
                {
                    string thongkeNam = cboNam.Text;
                    string tongHdNam = lblTonghdNgay.Text.Trim();
                    string doanhthuNam = lblTongdoanhthuNgay.Text.Trim();
                    string tongSpNam = lblTongstraindabanNgay.Text.Trim();
                    string topStrainNam = lblStrainnhieunhatNgay.Text.Trim();
                    List<StatisticsStrainDTO> lst5 = new List<StatisticsStrainDTO>();
                    if (tblHienstrainNgay == null)
                    {
                        lst5 = null;
                    }
                    else
                    {
                        foreach (DataGridViewRow row in tblHienstrainNgay.Rows)
                        {
                            if (row.IsNewRow)
                            {
                                continue;
                            }

                            string strainNumber = row.Cells[0].Value?.ToString();
                            int strainQuantity = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : 0;

                            var dto = new StatisticsStrainDTO
                            {
                                strainNumber = strainNumber,
                                strainQuantity = strainQuantity
                            };

                            lst5.Add(dto);
                        }
                    }
                    SaveFilePdfInvoice(5, lst,
                    tongdoanhthu, tonghoadon, totalProduct, topStrainNumberValue, topQuantityValue, customer,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null,
                    "", "", "", "", "", null,
                    thongkeNam, tongHdNam, doanhthuNam, tongSpNam, topStrainNam, lst5);
                }    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveFilePdfInvoice(int condition, List<BillCustomDTO> lst,
            string doanhthu, string tonghd, string tongslsp, string spmuanhieunhat, string sltopsp, string khachhang,
            string thongkeNgay, string tongHdNgay, string doanhthuNgay, string tongSpNgay, string topStrainNgay, List<StatisticsStrainDTO> lst1,
            string thongkeTuan, string tongHdTuan, string doanhthuTuan, string tongSpTuan, string topStrainTuan, List<StatisticsStrainDTO> lst2,
            string thongkeThang, string tongHdThang, string doanhthuThang, string tongSpThang, string topStrainThang, List<StatisticsStrainDTO> lst3,
            string thongkeQuy, string tongHdQuy, string doanhthuQuy, string tongSpQuy, string topStrainQuy, List<StatisticsStrainDTO> lst4,
            string thongkeNam, string tongHdNam, string doanhthuNam, string tongSpNam, string topStrainNam, List<StatisticsStrainDTO> lst5)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF Files|*.pdf";
                    saveFileDialog.Title = "Save PDF File";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        PdfDocument document = new PdfDocument();
                        document.Info.Title = "Invoice";

                        PdfPage page = document.AddPage();
                        XGraphics gfx = XGraphics.FromPdfPage(page);

                        XFont titleFont = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
                        XFont headerFont = new XFont("Times New Roman", 14, XFontStyleEx.Bold);
                        XFont bodyFont = new XFont("Times New Roman", 12);

                        int marginTop = 40;
                        int marginLeft = 40;
                        int marginRight = 40;
                        int yPoint = marginTop;

                        void DrawCenteredText(string text, XFont font, int yOffset)
                        {
                            gfx.DrawString(text, font, XBrushes.Black, new XRect(0, yOffset, page.Width, 40), XStringFormats.TopCenter);
                        }

                        void DrawLeftAlignedText(string text, XFont font, int yOffset)
                        {
                            gfx.DrawString(text, font, XBrushes.Black, new XRect(marginLeft, yOffset, page.Width - marginRight, 20), XStringFormats.TopLeft);
                        }

                        DrawCenteredText("BẢNG THỐNG KÊ", titleFont, yPoint);
                        yPoint += 40;

                        DrawLeftAlignedText("VIỆN NGHIÊN CỨU ỨNG DỤNG VÀ CHUYỂN GIAO CÔNG NGHỆ IRT", headerFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText("140 Đ. Lê Trọng Tấn, Tây Thạnh, Tân Phú, Thành phố Hồ Chí Minh", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText("SĐT: 028 6270 6275", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText("Email: info@hufi.edu.vn", bodyFont, yPoint);
                        yPoint += 40;

                        DrawLeftAlignedText("THỐNG KÊ TỔNG QUÁT:", headerFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText($"Ngày hiện tại: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText("Doanh thu:", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText($"    1. Tổng doanh thu hiện tại: {doanhthu}", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText($"    2. Tổng hóa đơn đã bán: {tonghd}", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText($"    3. Tổng số lượng sản phẩm đã bán: {tongslsp}", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText("Sản phẩm:", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText($"    1. Sản phẩm được mua nhiều nhất: {spmuanhieunhat}", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText($"    2. Số lượng: {sltopsp}", bodyFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText($"    3. Khách hàng thân thiết: {khachhang}", bodyFont, yPoint);
                        yPoint += 40;

                        DrawLeftAlignedText("THỐNG KÊ CHI TIẾT:", headerFont, yPoint);
                        yPoint += 20;
                        DrawLeftAlignedText("Danh sách hóa đơn:", bodyFont, yPoint);
                        yPoint += 20;

                        gfx.DrawLine(XPens.Black, marginLeft, yPoint, page.Width - marginRight, yPoint);
                        DrawLeftAlignedText("STT", headerFont, yPoint);
                        gfx.DrawString("Mã hóa đơn", headerFont, XBrushes.Black, new XRect(80, yPoint, 90, 20), XStringFormats.TopLeft);
                        gfx.DrawString("Ngày lập", headerFont, XBrushes.Black, new XRect(170, yPoint, 90, 20), XStringFormats.TopLeft);
                        gfx.DrawString("Loại hóa đơn", headerFont, XBrushes.Black, new XRect(260, yPoint, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString("Tổng tiền", headerFont, XBrushes.Black, new XRect(360, yPoint, 100, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawLine(XPens.Black, marginLeft, yPoint, page.Width - marginRight, yPoint);

                        int indexNumber = 1;
                        foreach (BillCustomDTO item in lst)
                        {
                            if (yPoint > page.Height - marginTop - 60)
                            {
                                page = document.AddPage();
                                gfx = XGraphics.FromPdfPage(page);
                                yPoint = marginTop;

                                gfx.DrawLine(XPens.Black, marginLeft, yPoint, page.Width - marginRight, yPoint);
                                DrawLeftAlignedText("STT", headerFont, yPoint);
                                gfx.DrawString("Mã hóa đơn", headerFont, XBrushes.Black, new XRect(80, yPoint, 90, 20), XStringFormats.TopLeft);
                                gfx.DrawString("Ngày lập", headerFont, XBrushes.Black, new XRect(170, yPoint, 90, 20), XStringFormats.TopLeft);
                                gfx.DrawString("Loại hóa đơn", headerFont, XBrushes.Black, new XRect(260, yPoint, 100, 20), XStringFormats.TopLeft);
                                gfx.DrawString("Tổng tiền", headerFont, XBrushes.Black, new XRect(360, yPoint, 100, 20), XStringFormats.TopLeft);
                                yPoint += 20;
                                gfx.DrawLine(XPens.Black, marginLeft, yPoint, page.Width - marginRight, yPoint);
                            }

                            DrawLeftAlignedText(indexNumber.ToString(), bodyFont, yPoint);
                            gfx.DrawString(item.idBill, bodyFont, XBrushes.Black, new XRect(80, yPoint, 90, 20), XStringFormats.TopLeft);
                            gfx.DrawString(item.dateBill, bodyFont, XBrushes.Black, new XRect(170, yPoint, 90, 20), XStringFormats.TopLeft);
                            gfx.DrawString(item.typeBill, bodyFont, XBrushes.Black, new XRect(260, yPoint, 100, 20), XStringFormats.TopLeft);
                            gfx.DrawString(item.totalPrice, bodyFont, XBrushes.Black, new XRect(360, yPoint, 100, 20), XStringFormats.TopLeft);
                            yPoint += 20;

                            gfx.DrawLine(XPens.Gray, marginLeft, yPoint, page.Width - marginRight, yPoint);

                            indexNumber++;
                        }

                        DrawLeftAlignedText("", bodyFont, yPoint);
                        yPoint += 20;
                        if (condition >= 1 && condition <= 4)
                        {
                            var statisticsData = new[]
                            {
                                (title: "Danh sách hóa đơn thống kê theo ngày:", thongkeNgay, tongHdNgay, doanhthuNgay, tongSpNgay, topStrainNgay, lst1),
                                (title: "Danh sách hóa đơn thống kê theo tuần:", thongkeTuan, tongHdTuan, doanhthuTuan, tongSpTuan, topStrainTuan, lst2),
                                (title: "Danh sách hóa đơn thống kê theo tháng:", thongkeThang, tongHdThang, doanhthuThang, tongSpThang, topStrainThang, lst3),
                                (title: "Danh sách hóa đơn thống kê theo quý:", thongkeQuy, tongHdQuy, doanhthuQuy, tongSpQuy, topStrainQuy, lst4),
                                (title: "Danh sách hóa đơn thống kê theo năm:", thongkeNam, tongHdNam, doanhthuNam, tongSpNam, topStrainNam, lst5)
                            };

                            var data = statisticsData[condition - 1];

                            DrawLeftAlignedText(data.title, bodyFont, yPoint);
                            yPoint += 20;

                            DrawLeftAlignedText($"Thời gian thống kê: {data.Item2}", bodyFont, yPoint);
                            yPoint += 20;
                            DrawLeftAlignedText($"Tổng số hóa đơn: {data.Item3}", bodyFont, yPoint);
                            yPoint += 20;
                            DrawLeftAlignedText($"Tổng doanh thu: {data.Item4}", bodyFont, yPoint);
                            yPoint += 20;
                            DrawLeftAlignedText($"Tổng sản phẩm đã bán: {data.Item5}", bodyFont, yPoint);
                            yPoint += 20;
                            DrawLeftAlignedText($"Sản phẩm bán chạy nhất: {data.Item6}", bodyFont, yPoint);
                            yPoint += 80;

                            gfx.DrawLine(XPens.Black, marginLeft, yPoint, page.Width - marginRight, yPoint);
                            DrawLeftAlignedText("STT", headerFont, yPoint);
                            gfx.DrawString("Mã sản phẩm", headerFont, XBrushes.Black, new XRect(80, yPoint, 90, 20), XStringFormats.TopLeft);
                            gfx.DrawString("Số lượng bán", headerFont, XBrushes.Black, new XRect(260, yPoint, 100, 20), XStringFormats.TopLeft);
                            yPoint += 20;
                            gfx.DrawLine(XPens.Black, marginLeft, yPoint, page.Width - marginRight, yPoint);

                            int itemIndex = 1;
                            foreach (var item in data.Item7)
                            {
                                if (yPoint > page.Height - marginTop - 60)
                                {
                                    page = document.AddPage();
                                    gfx = XGraphics.FromPdfPage(page);
                                    yPoint = marginTop;

                                    gfx.DrawLine(XPens.Black, marginLeft, yPoint, page.Width - marginRight, yPoint);
                                    DrawLeftAlignedText("STT", headerFont, yPoint);
                                    gfx.DrawString("Mã sản phẩm", headerFont, XBrushes.Black, new XRect(80, yPoint, 90, 20), XStringFormats.TopLeft);
                                    gfx.DrawString("Số lượng bán", headerFont, XBrushes.Black, new XRect(260, yPoint, 100, 20), XStringFormats.TopLeft);
                                    yPoint += 20;
                                    gfx.DrawLine(XPens.Black, marginLeft, yPoint, page.Width - marginRight, yPoint);
                                }

                                DrawLeftAlignedText(itemIndex.ToString(), bodyFont, yPoint);
                                gfx.DrawString(item.strainNumber, bodyFont, XBrushes.Black, new XRect(80, yPoint, 90, 20), XStringFormats.TopLeft);
                                gfx.DrawString(item.strainQuantity.ToString(), bodyFont, XBrushes.Black, new XRect(260, yPoint, 100, 20), XStringFormats.TopLeft);
                                yPoint += 20;

                                gfx.DrawLine(XPens.Gray, marginLeft, yPoint, page.Width - marginRight, yPoint);

                                itemIndex++;
                            }
                        }

                        document.Save(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    }
}
