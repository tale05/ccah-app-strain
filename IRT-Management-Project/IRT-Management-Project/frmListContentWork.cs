using BLL;
using DTO;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveCharts.Wpf;
using LiveCharts;
using System.Linq;
using System;
using System.Drawing;

namespace IRT_Management_Project
{
    public partial class frmListContentWork : Form
    {
        private ContentWorkBLL cwbll;
        public static int idContentWork;
        public frmListContentWork()
        {
            InitializeComponent();
            cwbll = new ContentWorkBLL();
            LoadDataGridView();
            InitializePieChart();
            InitializeColumnChart();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
        private async void InitializeColumnChart()
        {
            try
            {
                List<string> lstNhanVien = await cwbll.GetListIdEmployee(frmListProjectContent.idProjectContent);

                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();

                SeriesCollection seriesCollection1 = new SeriesCollection();
                List<string> employeeNames = new List<string>();
                List<double> completedTasks = new List<double>();
                List<double> unfinishedTasks = new List<double>();

                foreach (string employeeId in lstNhanVien)
                {
                    int completedCount = await cwbll.CountCompletedTasksByEmployee(employeeId, frmListProjectContent.idProjectContent);
                    int unfinishedCount = await cwbll.CountUnfinishedTasksByEmployee(employeeId, frmListProjectContent.idProjectContent);

                    employeeNames.Add(employeeId);
                    completedTasks.Add(completedCount);
                    unfinishedTasks.Add(unfinishedCount);
                }

                ColumnSeries completedTasksSeries = new ColumnSeries
                {
                    Title = "Công việc đã hoàn thành",
                    Values = new ChartValues<double>(completedTasks)
                };

                ColumnSeries unfinishedTasksSeries = new ColumnSeries
                {
                    Title = "Công việc chưa hoàn thành",
                    Values = new ChartValues<double>(unfinishedTasks)
                };

                seriesCollection1.Add(completedTasksSeries);
                seriesCollection1.Add(unfinishedTasksSeries);

                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Employee ID",
                    Labels = employeeNames
                });

                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Number of Tasks"
                });

                cartesianChart1.Series = seriesCollection1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }


        private async void InitializePieChart()
        {
            pieChart1.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Đã hoàn thành",
                    Values = new ChartValues<double> { await cwbll.CountStatusDaHoanThannh(frmListProjectContent.idProjectContent) },
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation),
                },
                new PieSeries
                {
                    Title = "Chưa hoàn thành",
                    Values = new ChartValues<double> { await cwbll.CountStatusChuaHoanThannh(frmListProjectContent.idProjectContent) },
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation), // Định dạng hiển thị phần trăm
                },
            };
            pieChart1.LegendLocation = LegendLocation.Bottom;

            //-----------------------------------------------------------------------------

            pieChart2.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Hoàn thành đúng hạn",
                    Values = new ChartValues<double> { await cwbll.CountCompletedTasksOnTime(frmListProjectContent.idProjectContent) },
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation),
                },
                new PieSeries
                {
                    Title = "Hoàn thành trễ hạn",
                    Values = new ChartValues<double> { await cwbll.CountCompletedTasksDelayed(frmListProjectContent.idProjectContent) },
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation), // Định dạng hiển thị phần trăm
                },
                new PieSeries
                {
                    Title = "Công việc chưa hoàn thành còn thời hạn",
                    Values = new ChartValues<double> { await cwbll.CountUnfinishedTasksWithinDeadline(frmListProjectContent.idProjectContent) },
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation),
                },
                new PieSeries
                {
                    Title = "Công việc chưa hoàn thành hết thời hạn",
                    Values = new ChartValues<double> { await cwbll.CountUnfinishedTasksOverdue(frmListProjectContent.idProjectContent) },
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation),
                },
            };
            pieChart2.LegendLocation = LegendLocation.Bottom;
        }
        public async void LoadDataGridView()
        {
            tblContentWork.DataSource = null;
            tblContentWork.Rows.Clear();
            tblContentWork.Columns.Clear();

            List<ContentWorkCustomDTO> lst = await cwbll.LoadDataContentWork(frmListProjectContent.idProjectContent);
            tblContentWork.DataSource = lst;

            tblContentWork.AutoGenerateColumns = false;
            tblContentWork.Columns[0].HeaderText = "STT";
            tblContentWork.Columns[1].HeaderText = "Nhân viên phụ trách";
            tblContentWork.Columns[2].HeaderText = "Nội dung công việc";
            tblContentWork.Columns[3].HeaderText = "Ngày bắt đầu";
            tblContentWork.Columns[4].HeaderText = "Ngày kết thúc";
            tblContentWork.Columns[5].HeaderText = "Số hợp đồng";
            tblContentWork.Columns[6].HeaderText = "Trạng thái";
            tblContentWork.Columns[7].HeaderText = "Độ ưu tiên";

            tblContentWork.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tblContentWork.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            tblContentWork.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblContentWork.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblContentWork.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblContentWork.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblContentWork.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            if (!tblContentWork.Columns.Contains("Thao tác"))
            {
                DataGridViewLinkColumn actionColumn = new DataGridViewLinkColumn();
                actionColumn.Name = "ThaoTac";
                actionColumn.HeaderText = "Thao tác";
                actionColumn.Text = "Xem chi tiết";
                actionColumn.UseColumnTextForLinkValue = true;
                tblContentWork.Columns.Add(actionColumn);
            }

            tblContentWork.CellContentClick -= tblContentWork_CellContentClick;
            tblContentWork.CellContentClick += tblContentWork_CellContentClick;

            tblContentWork.RowPrePaint -= tblContentWork_RowPrePaint;
            tblContentWork.RowPrePaint += tblContentWork_RowPrePaint;
        }
        private void tblContentWork_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var dgv = sender as DataGridView;

            if (dgv.Rows[e.RowIndex].Cells[6].Value != null)
            {
                string status = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();

                if (status == "Đã hoàn thành")
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                else if (status == "Chưa hoàn thành")
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LavenderBlush;
                }
            }
        }

        private void tblContentWork_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == tblContentWork.Columns["ThaoTac"].Index && e.RowIndex >= 0)
            {
                var rowData = (ContentWorkCustomDTO)tblContentWork.Rows[e.RowIndex].DataBoundItem;
                idContentWork = rowData.idContentWork;
                frmDetailContentWork frm = new frmDetailContentWork();
                frm.Show();
                Hide();
            }
        }
        private void frmListContentWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmListProjectContent frm = new frmListProjectContent();
            frm.Show();
            Hide();
        }
    }
}
