using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmAddProjectContent : Form
    {
        private FormAddProjectContentBLL pcbll;
        public static int title = 0;
        public static string idProject = string.Empty, idProjectContent = string.Empty, nameContent = string.Empty;
        public frmAddProjectContent()
        {
            InitializeComponent();
            pcbll = new FormAddProjectContentBLL();
            idProject = frmAddProject.idProjectChoose;
            LoadTableProject();
            LoadTableProjectContent();
            txtStartDate.Value = DateTime.Now;
            txtEndDate.Value = DateTime.Now;
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
        public async void LoadTableProject()
        {
            tblProject.Rows.Clear();
            tblProject.Columns.Clear();
            tblProject.DataSource = await pcbll.GetListProjectById(idProject);
            tblProject.Columns[0].HeaderText = "Mã dự án";
            tblProject.Columns[1].HeaderText = "Tên dự án";
        }
        public async void LoadTableProjectContent()
        {
            bool rs = await pcbll.CheckStatusProjectContent(idProject);
            if (!rs)
                await pcbll.UpdateStatusProject(idProject, "Đã hoàn thành");
            else
                await pcbll.UpdateStatusProject(idProject, "Chưa hoàn thành");
            tblProjectContent.DataSource = null;
            tblProjectContent.Rows.Clear();
            tblProjectContent.Columns.Clear();
            tblProjectContent.DataSource = await pcbll.GetListProjectContentByIdProject(idProject);
            DesignDataGridView();
        }
        private async void SearchData(string search)
        {
            tblProjectContent.DataSource = null;
            tblProjectContent.Rows.Clear();
            tblProjectContent.Columns.Clear();
            tblProjectContent.DataSource = await pcbll.SearchData(idProject, search);
            DesignDataGridView();
        }
        private async void FillByStatusSuccess(string status)
        {
            tblProjectContent.DataSource = null;
            tblProjectContent.Rows.Clear();
            tblProjectContent.Columns.Clear();
            tblProjectContent.DataSource = await pcbll.FillByStatusSuccess(idProject, status);
            DesignDataGridView();
        }
        private async void FillByStatusNotSuccess(string status)
        {
            tblProjectContent.DataSource = null;
            tblProjectContent.Rows.Clear();
            tblProjectContent.Columns.Clear();
            tblProjectContent.DataSource = await pcbll.FillByStatusNotSuccess(idProject, status);
            DesignDataGridView();
        }
        private void DesignDataGridView()
        {
            tblProjectContent.Columns[0].HeaderText = "Mã";
            tblProjectContent.Columns[1].HeaderText = "Dự án";
            tblProjectContent.Columns[2].HeaderText = "Công việc";
            tblProjectContent.Columns[3].HeaderText = "Kết quả yêu cầu";
            tblProjectContent.Columns[4].HeaderText = "Ngày bắt đầu";
            tblProjectContent.Columns[5].HeaderText = "Ngày kết thúc";
            tblProjectContent.Columns[6].HeaderText = "Số hợp đồng";
            tblProjectContent.Columns[7].HeaderText = "Độ ưu tiên";
            tblProjectContent.Columns[8].HeaderText = "Trạng thái";
            tblProjectContent.Columns[9].HeaderText = "Title";
            tblProjectContent.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProjectContent.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProjectContent.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProjectContent.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProjectContent.Columns[0].Visible = false;
            tblProjectContent.Columns[4].Visible = false;
            tblProjectContent.Columns[5].Visible = false;
            tblProjectContent.Columns[6].Visible = false;
            tblProjectContent.Columns[9].Visible = false;
            tblProjectContent.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tblProjectContent.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
        private void tblProjectContent_Click(object sender, EventArgs e)
        {
            if (tblProjectContent.CurrentRow != null)
            {
                int i = tblProjectContent.CurrentRow.Index;
                if (i >= 0 && i < tblProjectContent.Rows.Count)
                {
                    idProjectContent = tblProjectContent.Rows[i].Cells[0].Value.ToString();
                    string input = tblProjectContent.Rows[i].Cells[2].Value.ToString().Trim();
                    nameContent = input;
                    int index = input.IndexOf(':');
                    if (index != -1 && index + 1 < input.Length)
                    {
                        string result = input.Substring(index + 1).Trim();
                        txtNameContent.Text = result;
                    }
                    txtResult.Text = tblProjectContent.Rows[i].Cells[3].Value.ToString();
                    txtStartDate.Value = DateTime.Parse(tblProjectContent.Rows[i].Cells[4].Value.ToString());
                    txtEndDate.Value = DateTime.Parse(tblProjectContent.Rows[i].Cells[5].Value.ToString());
                    txtContractNo.Text = tblProjectContent.Rows[i].Cells[6].Value.ToString();
                    cboPriority.Text = tblProjectContent.Rows[i].Cells[7].Value.ToString();
                    cboStatus.Text = tblProjectContent.Rows[i].Cells[8].Value.ToString();
                    title = int.Parse(tblProjectContent.Rows[i].Cells[9].Value.ToString());
                    guna2GradientButton1.Text = "Thêm phần nội dung công việc: " + title;
                }
            }
            else
            {
                MessageBox.Show("Không có hàng nào được chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmAddProject.idProjectChoose = string.Empty;
            frmAddProject frm = new frmAddProject();
            frm.Show();
            Hide();
        }

        private void frmAddProjectContent_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmAddProject.idProjectChoose = string.Empty;
            frmAddProject frm = new frmAddProject();
            frm.Show();
            Hide();
        }

        private void Reset()
        {
            cboFillBy.SelectedIndex = 0;
            nameContent = string.Empty;
            idProjectContent = string.Empty;
            txtNameContent.Text = string.Empty;
            txtResult.Text = string.Empty;
            txtStartDate.Value = DateTime.Now;
            txtEndDate.Value = DateTime.Now;
            txtContractNo.Text = string.Empty;
            cboPriority.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            title = 0;
            txtSearch.Text = string.Empty;
            LoadTableProjectContent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            DateTime startDate = txtStartDate.Value;
            DateTime endDate = txtEndDate.Value;

            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm dữ liệu mới không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (startDate < endDate)
                {
                    int lastTitle = await GenerateTitleAuto();
                    var newProjectContent = new ApiProjectContentDTO()
                    {
                        idProject = idProject,
                        nameContent = txtNameContent.Text,
                        results = "Chưa có kết quả",
                        startDate = startDate.ToString("yyyy-MM-dd"),
                        endDate = endDate.ToString("yyyy-MM-dd"),
                        contractNo = txtContractNo.Text,
                        status = cboStatus.Text,
                        priority = cboPriority.Text,
                        title = lastTitle,
                    };
                    string projectContentJson = JsonSerializer.Serialize(newProjectContent);
                    string result = await pcbll.Post(projectContentJson);
                    if (result != null)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTableProjectContent();
                        bool rs = await pcbll.CheckStatusProjectContent(idProject);
                        if (!rs)
                            await pcbll.UpdateStatusProject(idProject, "Đã hoàn thành");
                        else
                            await pcbll.UpdateStatusProject(idProject, "Chưa hoàn thành");
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Thêm dữ liệu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (!idProjectContent.Equals(string.Empty))
                {
                    DateTime startDate = txtStartDate.Value;
                    DateTime endDate = txtEndDate.Value;

                    if (startDate < endDate)
                    {
                        string rsValue = string.Empty;
                        if (string.IsNullOrEmpty(txtResult.Text))
                            rsValue = "Chưa có kết quả";
                        else rsValue = txtResult.Text;
                        var newProjectContent = new ApiProjectContentDTO()
                        {
                            idProject = idProject,
                            nameContent = txtNameContent.Text,
                            results = rsValue,
                            startDate = startDate.ToString("yyyy-MM-dd"),
                            endDate = endDate.ToString("yyyy-MM-dd"),
                            contractNo = txtContractNo.Text,
                            status = cboStatus.Text,
                            priority = cboPriority.Text,
                            title = title,
                        };
                        string projectContentJson = JsonSerializer.Serialize(newProjectContent);
                        string result = await pcbll.Update(idProjectContent, projectContentJson);
                        if (result != null)
                        {
                            MessageBox.Show("Cập nhật dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTableProjectContent();
                            bool rs = await pcbll.CheckStatusProjectContent(idProject);
                            if (!rs)
                                await pcbll.UpdateStatusProject(idProject, "Đã hoàn thành");
                            else
                                await pcbll.UpdateStatusProject(idProject, "Chưa hoàn thành");
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật dữ liệu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show("Phải chọn 1 dữ liệu để cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (!idProjectContent.Equals(string.Empty))
            {
                frmAddContentWork frm = new frmAddContentWork();
                frm.Show();
                Hide();
            }
            else
                MessageBox.Show("Bạn cần chọn 1 công việc để thêm nội dung cho công việc đó", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadTableProjectContent();
            }
            else
            {
                SearchData(txtSearch.Text);
            }    
        }

        private void cboFillBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboFillBy.SelectedIndex;
            switch (index)
            {
                case 0:
                    LoadTableProjectContent();
                    break;
                case 1:
                    FillByStatusSuccess(cboFillBy.Text.Trim());
                    break;
                case 2:
                    FillByStatusNotSuccess(cboFillBy.Text.Trim());
                    break;
                default:
                    break;
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (!idProjectContent.Equals(string.Empty))
                {
                    string result = await pcbll.Delete(idProjectContent);

                    if (result == "Deleted")
                    {
                        MessageBox.Show("Xóa dữ liệu thành công", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTableProjectContent();
                        Reset();
                        idProjectContent = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu đang được sử dụng. Không thể xóa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Phải chọn 1 dữ liệu để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task<int> GenerateTitleAuto()
        {
            string lastTitleStr = await pcbll.GetLastTitleByIdProject(idProject);

            if (int.TryParse(lastTitleStr, out int lastTitle) && lastTitle != 0)
            {
                return lastTitle + 1;
            }
            else
            {
                return 1;
            }
        }

        private async void linkExportExcel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<DTO.Excel.ProjectContentExcelDTO> list = await pcbll.DataToExportExcel(idProject);
            System.Data.DataTable strainData = ConvertToDataTable(list);

            if (strainData.Rows.Count > 0)
            {
                SaveDataToExcel(strainData);
            }
            else
            {
                MessageBox.Show("No data found to export.");
            }
        }

        public DataTable ConvertToDataTable(List<DTO.Excel.ProjectContentExcelDTO> list)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("idProject", typeof(string));
            dataTable.Columns.Add("nameProject", typeof(string));
            dataTable.Columns.Add("nameContent", typeof(string));
            dataTable.Columns.Add("result", typeof(string));
            dataTable.Columns.Add("startDate", typeof(string));
            dataTable.Columns.Add("endDate", typeof(string));
            dataTable.Columns.Add("contractNo", typeof(string));
            dataTable.Columns.Add("priority", typeof(string));
            dataTable.Columns.Add("status", typeof(string));

            foreach (var item in list)
            {
                DataRow row = dataTable.NewRow();
                row["idProject"] = item.idProject;
                row["nameProject"] = item.nameProject;
                row["nameContent"] = item.nameContent;
                row["result"] = item.result;
                row["startDate"] = item.startDate;
                row["endDate"] = item.endDate;
                row["contractNo"] = item.contractNo;
                row["priority"] = item.priority;
                row["status"] = item.status;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private void SaveDataToExcel(System.Data.DataTable dataTable)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            if (excelApp == null)
            {
                MessageBox.Show("Excel is not properly installed.");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;
            worksheet.Name = "Danh sách strain";

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j];
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Save an Excel File",
                FileName = "Book1.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    workbook.SaveAs(filePath);
                    MessageBox.Show("Đã lưu file thành công. đường dẫn: " + filePath, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workbook.Close(0);
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                }
            }
        }
    }
}
