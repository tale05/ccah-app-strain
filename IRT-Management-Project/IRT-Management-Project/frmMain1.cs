using BLL;
using DTO;
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
using System.Web.Security;
using System.Windows.Media;
using System.Windows.Markup;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

namespace IRT_Management_Project
{
    public partial class frmMain1 : Form
    {
        public static string userNameFormMain, passwordFormMain;
        public static bool isOpenForm;
        private FormMainBLL fmbll;
        private FormTaskProgressEmployeeBLL bll;
        //private int numberOfStrainNotApproval, totalEmployee, totalOrderWaiting;
        public frmMain1()
        {
            InitializeComponent();
            btnXemTaiKhoan.Visible = false;
            fmbll = new FormMainBLL();
            bll = new FormTaskProgressEmployeeBLL();
            treeView1.ExpandAll();
            isOpenForm = true;
        }

        private async void frmMain1_Load(object sender, EventArgs e)
        {
            DisplayByRole(treeView1.Nodes, frmLogin.role);
            //numberOfStrainNotApproval = await fmbll.CountStrainNotApprovaled();
            //totalOrderWaiting = await fmbll.CountOrdersWaiting();
            //totalEmployee = await fmbll.CountEmployee();
            if (frmLogin.role == 1)
            {
                panel_Admin.Visible = true;
                ColumnChart();
                //PieChart();
                btnDuyetStrain.Text = "Strain đang chờ duyệt";
                lblChaoMung.Text = "Xin chào, " + frmLogin.fullNameEmployee.Substring(frmLogin.fullNameEmployee.LastIndexOf(' ') + 1);
                btnNhanvien.Text = "Nhân viên";
                btnDonHang.Text = $"Đơn hàng đang xử lý";
                btnSaoLuuDuLieu.Visible = true;
                btnRestoreDB.Visible = true;
            }
            else
            {
                panel_Admin.Visible = true;
                ColumnChart();
                //PieChart();
                btnDuyetStrain.Text = "Strain đang chờ duyệt";
                lblChaoMung.Text = "Xin chào, " + frmLogin.fullNameEmployee.Substring(frmLogin.fullNameEmployee.LastIndexOf(' ') + 1);
                btnNhanvien.Text = "Nhân viên";
                btnDonHang.Text = $"Đơn hàng đang xử lý";
                btnNhanvien.Visible = false;
                guna2GradientButton4.Visible = false;
                btnDuyetStrain.Visible = false;
                btnSaoLuuDuLieu.Visible = false;
                btnRestoreDB.Visible = false;
            }
            await LoadData(frmLogin.idEmployee);
        }

        private async Task LoadData(string idEmployee)
        {
            List<TaskProgressEmployeeDTO> data = new List<TaskProgressEmployeeDTO>();
            data = await bll.GetData(idEmployee);
            BindDataToDataGridView(data);
        }

        private async void BindDataToDataGridView(List<TaskProgressEmployeeDTO> data)
        {
            tblTaskEmployee.DataSource = null;
            tblTaskEmployee.Rows.Clear();
            tblTaskEmployee.Columns.Clear();
            tblTaskEmployee.AutoGenerateColumns = false;

            tblTaskEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idContentWork",
                HeaderText = "Mã",
                DataPropertyName = "idContentWork"
            });
            tblTaskEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameContent",
                HeaderText = "Tên Công Việc",
                DataPropertyName = "nameContent"
            });
            tblTaskEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "results",
                HeaderText = "Kết Quả",
                DataPropertyName = "results"
            });
            tblTaskEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "startDate",
                HeaderText = "Ngày Bắt Đầu",
                DataPropertyName = "startDate"
            });
            tblTaskEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "endDate",
                HeaderText = "Ngày Kết Thúc",
                DataPropertyName = "endDate"
            });
            tblTaskEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "contractNo",
                HeaderText = "Số Hợp Đồng",
                DataPropertyName = "contractNo"
            });
            tblTaskEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "priority",
                HeaderText = "Ưu Tiên",
                DataPropertyName = "priority"
            });
            tblTaskEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "status",
                HeaderText = "Trạng Thái",
                DataPropertyName = "status"
            });
            tblTaskEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "noti",
                HeaderText = "Thông báo",
                DataPropertyName = "notificattion"
            });

            var notificationColumn = new DataGridViewColumn
            {
                Name = "notification",
                HeaderText = "Xem Thông Báo",
                CellTemplate = new DataGridViewTextBoxCell()
            };
            tblTaskEmployee.Columns.Add(notificationColumn);

            tblTaskEmployee.DataSource = data;

            tblTaskEmployee.Columns["noti"].Visible = false;
            tblTaskEmployee.Columns["notification"].Visible = false;

            tblTaskEmployee.Columns["idContentWork"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblTaskEmployee.Columns["contractNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblTaskEmployee.Columns["priority"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblTaskEmployee.Columns["notification"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblTaskEmployee.Columns["startDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblTaskEmployee.Columns["endDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblTaskEmployee.Columns["status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tblTaskEmployee.Columns["nameContent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tblTaskEmployee.Columns["results"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            tblTaskEmployee.RowPrePaint -= tblContentWork_RowPrePaint;
            tblTaskEmployee.RowPrePaint += tblContentWork_RowPrePaint;
        }

        private void tblContentWork_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var dgv = sender as DataGridView;

            if (dgv.Rows[e.RowIndex].Cells["status"].Value != null)
            {
                string status = dgv.Rows[e.RowIndex].Cells["status"].Value.ToString();

                if (status == "Đã hoàn thành")
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
                else if (status == "Chưa hoàn thành")
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LavenderBlush;
                }
            }
        }

        private void DisplayByRole(TreeNodeCollection nodes, int role)
        {
            List<TreeNode> nodesToRemove = new List<TreeNode>();

            foreach (TreeNode node in nodes)
            {
                if (node.Tag != null)
                {
                    if (role == 1)
                    {
                        DisplayByRole(node.Nodes, role);
                    }
                    else if (role == 2 || role == 3)
                    {
                        if (node.Tag.ToString() == "tagDuyetSP"
                            || node.Tag.ToString() == "tagAllDuAn"
                            || node.Tag.ToString() == "tagThemDuAn"
                            || node.Tag.ToString() == "tagQLNhanVien"
                            || node.Tag.ToString() == "tagQLDonHang"
                            || node.Tag.ToString() == "tagQLDoiTac"
                            || node.Tag.ToString() == "tagQLAccKhachHang"
                            || node.Tag.ToString() == "tagQLAccNhanVien")
                        {
                            nodesToRemove.Add(node);
                        }
                    }
                }

                if (node.Nodes.Count > 0)
                {
                    DisplayByRole(node.Nodes, role);
                }
            }
            foreach (TreeNode nodeToRemove in nodesToRemove)
            {
                nodeToRemove.Remove();
            }
            //---------------------------------------------------------------
            lblNameUser.Text = frmLogin.fullNameEmployee;
            //---------------------------------------------------------------

        }

        //private async void PieChart()
        //{
        //    var color1 = new GradientStopCollection
        //    {
        //        new GradientStop(System.Windows.Media.Colors.Blue, 0),
        //        new GradientStop(System.Windows.Media.Colors.DeepSkyBlue, 1)
        //    };
        //    var gradiant1 = new LinearGradientBrush(color1);

        //    var color2 = new GradientStopCollection
        //    {
        //        new GradientStop(System.Windows.Media.Colors.LightCoral, 0),
        //        new GradientStop(System.Windows.Media.Colors.Crimson, 1)
        //    };
        //    var gradiant2 = new LinearGradientBrush(color2);

        //    pieChart1.Series = new SeriesCollection
        //    {
        //        new PieSeries
        //        {
        //            Title = "Strain đã được duyệt",
        //            Values = new ChartValues<double> {await fmbll.CountStrainApprovaled()},
        //            DataLabels = true,
        //            LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation),
        //            Fill = gradiant1
        //        },
        //        new PieSeries
        //        {
        //            Title = "Strain đang chờ duyệt",
        //            Values = new ChartValues<double> {numberOfStrainNotApproval},
        //            DataLabels = true,
        //            LabelPoint = chartPoint => string.Format("{0:P}", chartPoint.Participation),
        //            Fill = gradiant2
        //        },
        //    };
        //    pieChart1.LegendLocation = LegendLocation.Bottom;

        //}

        private async void ColumnChart()
        {
            try
            {
                List<QuantityStrainDTO> strainQuantities = await fmbll.GetListStrainNumber();
                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();
                SeriesCollection seriesCollection = new SeriesCollection();
                ColumnSeries columnSeries = new ColumnSeries
                {
                    Values = new ChartValues<int>(),
                };

                foreach (var strainQuantity in strainQuantities)
                {
                    columnSeries.Values.Add(strainQuantity.quantity);
                    columnSeries.Title = "Số lượng ->";
                }

                var gradientStopCollection = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Colors.MediumSeaGreen, 1),
                    new GradientStop(System.Windows.Media.Colors.LightGreen, 0)
                };

                var fillBrush = new LinearGradientBrush(gradientStopCollection);

                columnSeries.Fill = fillBrush;

                seriesCollection.Add(columnSeries);

                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Strain Number",
                    Labels = strainQuantities.Select(sq => sq.strainNumber).ToList()
                });

                cartesianChart1.AxisY.Add(new Axis
                {
                });

                cartesianChart1.Series = seriesCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }


        private void lblNameUser_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point menuPoint = new Point(83, 39);
                Point screenPoint = lblNameUser.Parent.PointToScreen(menuPoint);
                menuForAvatar.Show(screenPoint);
            }
        }

        public void EmbedChildForm()
        {
            frmListEmployee childForm = new frmListEmployee();
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            container.Controls.Clear();
            container.Controls.Add(childForm);
            childForm.Show();
        }


        private void menuForAvatar_MouseLeave(object sender, EventArgs e)
        {
            menuForAvatar.Hide();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                switch (e.Node.Tag.ToString())
                {
                    case "tagNhanVien":
                        frmEmployeeList frmListEmployee = new frmEmployeeList();
                        frmListEmployee.Show();
                        Hide();
                        break;

                    case "tagKhachHang":
                        frmAddCustomer frmAddCustomer = new frmAddCustomer();
                        frmAddCustomer.Show();
                        Hide();
                        break;
                    case "tagXemSanPham":
                        frmListStrain frmListStrain = new frmListStrain();
                        frmListStrain.Show();
                        Hide();
                        break;
                    case "tagThemSP":
                        frmAddOneStrain frmAddStrain = new frmAddOneStrain();
                        frmAddStrain.Show();
                        Hide();
                        break;
                    case "tagDuyetSP":
                        frmApprovalStrain frmApprovalStrain = new frmApprovalStrain();
                        frmApprovalStrain.Show();
                        Hide();
                        break;
                    case "tagAllDuAn":
                        frmListProject frmListProject = new frmListProject();
                        frmListProject.Show();
                        Hide();
                        break;
                    case "tagUpdateStrain":
                        frmUpdateStrain frmUpdateStrain = new frmUpdateStrain();
                        frmUpdateStrain.Show();
                        Hide();
                        break;
                    case "tagThemDuAn":
                        frmAddProject frmAddProject = new frmAddProject();
                        frmAddProject.Show();
                        Hide();
                        break;
                    case "tagTiendo":
                        frmTaskProgressEmployee frmTaskProgressEmployee = new frmTaskProgressEmployee();
                        frmTaskProgressEmployee.Show();
                        Hide();
                        break;
                    case "tagListPartner":
                        frmListPartner frmListPartner = new frmListPartner();
                        frmListPartner.Show();
                        Hide();
                        break;
                    case "tagAddPartner":
                        frmAddPartner frmAddPartner = new frmAddPartner();
                        frmAddPartner.Show();
                        Hide();
                        break;
                    case "tagDSStrainNV":
                        frmApprovalStrainViewEmployee frmApprovalStrainViewEmployee = new frmApprovalStrainViewEmployee();
                        frmApprovalStrainViewEmployee.Show();
                        Hide();
                        break;
                    case "tagQLKhoHang":
                        frmAddInventory frmAddInventory = new frmAddInventory();
                        frmAddInventory.Show();
                        Hide();
                        break;
                    case "tagThongKe":
                        frmStatistical frmStatistical = new frmStatistical();
                        frmStatistical.Show();
                        Hide();
                        break;
                    case "tagBaiBao":
                        frmAddScienceNewspaper frmAddScienceNewspaper = new frmAddScienceNewspaper();
                        frmAddScienceNewspaper.Show();
                        Hide();
                        break;
                    case "tagQLAccKhachHang":
                        frmManageCustomerAccounts frmManageCustomerAccounts = new frmManageCustomerAccounts();
                        frmManageCustomerAccounts.ShowDialog();
                        break;
                    case "tagQLAccNhanVien":
                        frmManageEmployeeAccounts frmManageEmployeeAccounts = new frmManageEmployeeAccounts();
                        frmManageEmployeeAccounts.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword();
            frm.ShowDialog();
        }

        private void btnDuyetStrain_Click(object sender, EventArgs e)
        {
            frmApprovalStrain frm = new frmApprovalStrain();
            frm.Show();
            Visible = false;
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            frmApprovalOrdersCustomer frm = new frmApprovalOrdersCustomer();
            frm.Show();
            Hide();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            frmAddCustomer frm = new frmAddCustomer();
            frm.Show();
            Hide();
        }

        private void btnNhanvien_Click(object sender, EventArgs e)
        {
            frmEmployeeList frm = new frmEmployeeList();
            frm.Show();
            Hide();
        }

        private void frmMain1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isOpenForm = false;
            frmLogin frm = new frmLogin();
            frm.Show();
            Hide();
        }

        private async void btnSaoLuuDuLieu_Click(object sender, EventArgs e)
        {
            await BackupDatabase();
        }

        private async Task BackupDatabase()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7168/api/backup/backup");

                if (response.IsSuccessStatusCode)
                {
                    byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                    string backupFolderPath = @"C:\BackupFolder";

                    // Kiểm tra và tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(backupFolderPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(backupFolderPath);
                            MessageBox.Show($"Đã tạo thư mục {backupFolderPath} thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Không thể tạo thư mục {backupFolderPath}: {ex.Message}");
                            return;
                        }
                    }

                    string filePath = Path.Combine(backupFolderPath, $"backup_{DateTime.Now:yyyyMMddHHmmss}.bak");
                    File.WriteAllBytes(filePath, fileBytes);
                    MessageBox.Show($"Backup thành công! File đã được lưu tại {filePath}");
                }
                else
                {
                    MessageBox.Show("Backup thất bại!");
                }
            }
        }

        private async void btnRestoreDB_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string backupFilePath = openFileDialog.FileName;

                    try
                    {
                        using (var client = new HttpClient())
                        {
                            HttpResponseMessage response = await client.PostAsync($"https://localhost:7168/api/Backup/restore/{backupFilePath}", null);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Phục hồi dữ liệu thành công. Hệ thống sẽ tự đóng lại. Vui lòng khởi động lại ứng dụng.");
                                Application.Exit();
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi: {response.ReasonPhrase}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}");
                    }
                }
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            isOpenForm = false;
            Hide();
            frmLogin frm = new frmLogin();
            frm.Show();
        }
    }
}
