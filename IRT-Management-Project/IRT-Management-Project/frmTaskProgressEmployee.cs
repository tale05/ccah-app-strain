using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmTaskProgressEmployee : Form
    {
        private FormTaskProgressEmployeeBLL bll;
        private FormAddContentWorkBLL cwbll;
        public static string noti = string.Empty, fileNameValue = string.Empty, idEmployeeValue = string.Empty;
        private static int idContentWork = 0, idProjectContent = 0;
        private static byte[] fileSavedValue = null;
        public frmTaskProgressEmployee()
        {
            InitializeComponent();
            bll = new FormTaskProgressEmployeeBLL();
            cwbll = new FormAddContentWorkBLL();
            lblNameUser.Text = frmLogin.fullNameEmployee;
            idEmployeeValue = frmLogin.idEmployee;
        }

        private async void frmTaskProgressEmployee_Load(object sender, EventArgs e)
        {
            await LoadData(frmLogin.idEmployee);
            DisplayLabel(frmLogin.idEmployee);
        }

        private async void DisplayLabel(string idEmployee)
        {
            int count1 = await bll.CountTotalContentWorks(idEmployee);
            lblTongcv.Text = $"Tổng công việc: {count1}";
            int count2 = await bll.CountTotalTaskSuccess(idEmployee);
            lblCvdahoanthanh.Text = $"Đã hoàn thành: {count2}";
            int count3 = await bll.CountTotalTaskNotSuccess(idEmployee);
            lblCvchuahoanthanh.Text = $"Chưa hoàn thành: {count3}";
            int count4 = await bll.CountTotalTasksWithNotification(idEmployee);
            lblThongbao.Text = $"Thông báo: {count4}";
        }

        private async Task LoadData(string idEmployee)
        {
            List<TaskProgressEmployeeDTO> data = new List<TaskProgressEmployeeDTO>();
            data = await bll.GetData(idEmployee);
            BindDataToDataGridView(data);
        }

        private async Task FillDataBy(string idEmployee, string status)
        {
            List<TaskProgressEmployeeDTO> data = new List<TaskProgressEmployeeDTO>();
            data = await bll.FillDataBy(idEmployee, status);
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
                Name = "idProjectContent",
                HeaderText = "idProjectContent",
                DataPropertyName = "idProjectContent"
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

            for (int i = 0; i < tblTaskEmployee.Rows.Count; i++)
            {
                var row = tblTaskEmployee.Rows[i];
                int idContentWork = (int)row.Cells["idContentWork"].Value;
                string hasNotification = await bll.ReturnIfHaveNotification(idContentWork);

                DataGridViewCell notificationCell;
                if ( string.IsNullOrEmpty(hasNotification) || hasNotification.Equals("Nhân viên đã xem yêu cầu"))
                {
                    notificationCell = new DataGridViewTextBoxCell
                    {
                        Value = "Không có thông báo nào"
                    };
                }
                else
                {
                    notificationCell = new DataGridViewLinkCell
                    {
                        Value = "Có thông báo đang chờ"
                    };
                }
                row.Cells["notification"] = notificationCell;
            }

            tblTaskEmployee.Columns["noti"].Visible = false;
            tblTaskEmployee.Columns["idProjectContent"].Visible = false;

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
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                else if (status == "Chưa hoàn thành")
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LavenderBlush;
                }
            }
        }

        private void frmTaskProgressEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
        }

        private void tblTaskEmployee_Click(object sender, EventArgs e)
        {
            if (tblTaskEmployee.CurrentRow != null)
            {
                int i = tblTaskEmployee.CurrentRow.Index;
                idProjectContent = int.Parse(tblTaskEmployee.Rows[i].Cells["idProjectContent"].Value.ToString());
                idContentWork = Convert.ToInt32(tblTaskEmployee.Rows[i].Cells["idContentWork"].Value);
                lblCongviec.Text = tblTaskEmployee.Rows[i].Cells["nameContent"].Value.ToString();
                if (tblTaskEmployee.Rows[i].Cells["results"].Value.ToString().Equals("Chưa có kết quả"))
                    txtResult.Text = string.Empty;
                else
                    txtResult.Text = tblTaskEmployee.Rows[i].Cells["results"].Value.ToString();

                lblThoihan.Text = $"{tblTaskEmployee.Rows[i].Cells["startDate"].Value}-{tblTaskEmployee.Rows[i].Cells["endDate"].Value}";
                lblSohopdong.Text = tblTaskEmployee.Rows[i].Cells["contractNo"].Value.ToString();
                lblDouutien.Text = tblTaskEmployee.Rows[i].Cells["priority"].Value.ToString();
                lblTrangthai.Text = tblTaskEmployee.Rows[i].Cells["status"].Value.ToString();

                noti = tblTaskEmployee.Rows[i].Cells["noti"].Value != null ? tblTaskEmployee.Rows[i].Cells["noti"].Value.ToString() : string.Empty;
                if (string.IsNullOrEmpty(noti) || noti.Equals("Nhân viên đã xem yêu cầu"))
                {
                    panel15.Visible = false;
                    txtNotification.Text = string.Empty;
                }
                else
                {
                    panel15.Visible = true;
                    txtNotification.Text = noti;
                }    

                btnFile.Enabled = true;
                panelChiTiet.Visible = true;

                if (tblTaskEmployee.Rows[i].Cells["status"].Value.ToString().Equals("Đã hoàn thành"))
                {
                    btnCapnhathoanthanh.Visible = false;
                    panel7.Visible = false;
                    txtResult.ReadOnly = true;
                }    
                else
                {
                    btnCapnhathoanthanh.Visible= true;
                    panel7.Visible = true;
                    txtResult.ReadOnly = false;
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn dòng nào!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private async void Reset()
        {
            tblTaskEmployee.DataSource = null;
            tblTaskEmployee.Rows.Clear();
            tblTaskEmployee.Columns.Clear();
            await LoadData(frmLogin.idEmployee);
            lblCongviec.Text = "";
            lblThoihan.Text = "";
            lblSohopdong.Text = "";
            lblDouutien.Text = "";
            lblTrangthai.Text = "";
            btnFile.Enabled = false;
            //btnThongbao.Enabled = false;
        }

        private async void btnFile_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bool rs = await bll.CheckNotExitFile(idContentWork);
                if (!rs)
                {
                    btnAddfile.Visible = false;
                    btnShowfile.Visible = true;
                    btnDeletefile.Visible = true;
                }
                else
                {
                    btnAddfile.Visible = true;
                    btnShowfile.Visible = false;
                    btnDeletefile.Visible = false;
                }
                menuFile.Show((Control)sender, e.Location);
            }
        }

        private async void btnShowfile_Click(object sender, EventArgs e)
        {
            byte[] fileSaved = await bll.GetFileSavedById(idContentWork);
            string fileName = await bll.GetFileNameById(idContentWork);

            if (fileSaved != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = fileName;
                    saveFileDialog.Filter = "All Files (*.*)|*.*";
                    saveFileDialog.Title = "Lưu tệp";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            File.WriteAllBytes(saveFileDialog.FileName, fileSaved);
                            MessageBox.Show("Tệp đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không thể lưu tệp. Chi tiết lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có tệp nào được lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnDeletefile_Click(object sender, EventArgs e)
        {
            string rs = await bll.UpdateFileProperties(idContentWork, null, string.Empty);
            if (rs != null)
            {
                MessageBox.Show("Xóa file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadData(frmLogin.idEmployee);
            }
            else
            {
                MessageBox.Show("Xóa file không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCapnhathoanthanh_Click(object sender, EventArgs e)
        {
            if (idContentWork != 0)
            {
                if (!string.IsNullOrEmpty(txtResult.Text))
                {
                    string rs = await bll.UpdateStatusContentWorkResultAndEndDateActual(idContentWork, txtResult.Text);
                    if (rs != null)
                    {
                        string idProject = await cwbll.GetIdProjectByIdProjectContent(idProjectContent);

                        bool rsProjectContent = await cwbll.CheckAllStatusContentWork(idProjectContent);
                        if (!rsProjectContent)
                            await cwbll.UpdateStatusProjectContent(idProjectContent, "Đã hoàn thành");
                        else
                            await cwbll.UpdateStatusProjectContent(idProjectContent, "Chưa hoàn thành");

                        bool rsProject = await cwbll.CheckAllStatusProjectContent(idProject);
                        if (!rsProject)
                            await cwbll.UpdateStatusProject(idProject, "Đã hoàn thành");
                        else
                            await cwbll.UpdateStatusProject(idProject, "Chưa hoàn thành");

                        MessageBox.Show("Đã thay đổi công việc sang 'Đã hoàn thành'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadData(frmLogin.idEmployee);
                        DisplayLabel(frmLogin.idEmployee);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật tiến độ công việc không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Bạn chưa nhập dữ liệu kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Bạn chưa chọn công việc nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private async void btnThongbao_Click(object sender, EventArgs e)
        {
            string rs = await bll.UpdateNotificationNull(idContentWork);
            if (rs != null)
            {
                MessageBox.Show("Đánh dấu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadData(frmLogin.idEmployee);
                //txtNotification.Text = string.Empty;
            }
            else
                MessageBox.Show("Đánh dấu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void lblTongcv_Click(object sender, EventArgs e)
        {
            await LoadData(frmLogin.idEmployee);
        }

        private async void lblCvdahoanthanh_Click(object sender, EventArgs e)
        {
            await FillDataBy(frmLogin.idEmployee, "Đã hoàn thành");
        }

        private async void txtNotification_TextChanged(object sender, EventArgs e)
        {
            await bll.UpdateNotificationNull(idContentWork);
        }

        private async void lblCvchuahoanthanh_Click(object sender, EventArgs e)
        {
            await FillDataBy(frmLogin.idEmployee, "Chưa hoàn thành");
        }

        private async void btnAddfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn tệp";
            ofd.Filter = "All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileSavedValue = File.ReadAllBytes(ofd.FileName);
                fileNameValue = Path.GetFileName(ofd.FileName);


            }
            string result = await bll.UpdateFileProperties(idContentWork, fileSavedValue, fileNameValue);

            if (result != null)
            {
                MessageBox.Show("Thêm file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadData(frmLogin.idEmployee);
            }
            else
                MessageBox.Show("Thêm file thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
