using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmAddProject : Form
    {
        private FormAddProjectBLL fapbll;
        private byte[] fileSaved;
        public static string idProjectChoose = string.Empty, fileNameProject = string.Empty,
            nameProject = string.Empty;
        public frmAddProject()
        {
            InitializeComponent();
            fapbll = new FormAddProjectBLL();
            LoadDataGridView();
            LoadComboboxEmployee();
            LoadComboboxNameCompany();
            txtDateTime.Value = DateTime.Now;
            txtDateTime1.Value = DateTime.Now;
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
        public async void LoadDataGridView()
        {
            tblProject.DataSource = null;
            tblProject.Rows.Clear();
            tblProject.Columns.Clear();

            List<ProjectCustomDTO> lst = await fapbll.GetFullProperties();
            tblProject.DataSource = lst;

            tblProject.AutoGenerateColumns = true;

            tblProject.Columns[0].HeaderText = "Mã";
            tblProject.Columns[1].HeaderText = "Trưởng dự án";
            tblProject.Columns[2].HeaderText = "Đối tác";
            tblProject.Columns[3].HeaderText = "Tên dự án";
            tblProject.Columns[4].HeaderText = "Kết quả yêu cầu";
            tblProject.Columns[5].HeaderText = "Ngày bắt đầu";
            tblProject.Columns[6].HeaderText = "Ngày kết thúc";
            tblProject.Columns[7].HeaderText = "Số hợp đồng";
            tblProject.Columns[8].HeaderText = "Mô tả";
            tblProject.Columns[9].HeaderText = "Trạng thái";
            tblProject.Columns[10].HeaderText = "File đã lưu";
            tblProject.Columns[10].Visible = false;

            tblProject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            tblProject.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tblProject.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tblProject.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            tblProject.CurrentCell = null;
        }

        public async void SearchData(string search)
        {
            tblProject.DataSource = null;
            tblProject.Rows.Clear();
            tblProject.Columns.Clear();

            List<ProjectCustomDTO> lst = await fapbll.SearchData(search);
            tblProject.DataSource = lst;

            tblProject.AutoGenerateColumns = true;

            tblProject.Columns[0].HeaderText = "Mã";
            tblProject.Columns[1].HeaderText = "Trưởng dự án";
            tblProject.Columns[2].HeaderText = "Đối tác";
            tblProject.Columns[3].HeaderText = "Tên dự án";
            tblProject.Columns[4].HeaderText = "Yêu cầu cần đạt";
            tblProject.Columns[5].HeaderText = "Ngày bắt đầu";
            tblProject.Columns[6].HeaderText = "Ngày kết thúc";
            tblProject.Columns[7].HeaderText = "Số hợp đồng";
            tblProject.Columns[8].HeaderText = "Mô tả";
            tblProject.Columns[9].HeaderText = "Trạng thái";
            tblProject.Columns[10].HeaderText = "File đã lưu";
            tblProject.Columns[10].Visible = false;

            tblProject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            tblProject.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProject.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tblProject.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tblProject.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            tblProject.CurrentCell = null;
        }

        public async void LoadComboboxEmployee()
        {
            cboEmployee.Items.Clear();
            cboEmployee.DataSource = await fapbll.GetListNameEmployee();
        }
        public async void LoadComboboxNameCompany()
        {
            cboPartner.Items.Clear();
            cboPartner.DataSource = await fapbll.GetListNameCompany();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn tệp";
            ofd.Filter = "All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileSaved = File.ReadAllBytes(ofd.FileName);
                fileNameProject = Path.GetFileName(ofd.FileName);
                txtFileName.Text = fileNameProject;

                MessageBox.Show("Tệp đã được chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void label13_Click(object sender, EventArgs e)
        {
            if (fileSaved != null)
            {
                string tempDirectory = Path.GetTempPath();
                string tempFilePath = Path.Combine(tempDirectory, txtFileName.Text);
                if (!Directory.Exists(tempDirectory))
                {
                    Directory.CreateDirectory(tempDirectory);
                }

                File.WriteAllBytes(tempFilePath, fileSaved);

                try
                {
                    System.Diagnostics.Process.Start(tempFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở tệp. Chi tiết lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Không có tệp nào được lưu.");
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa file đang lưu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                txtFileName.Text = "Không có file nào được lưu";
                fileSaved = null;
                fileNameProject = null;
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            DateTime startDate = txtDateTime.Value;
            DateTime endDate = txtDateTime1.Value;

            if (startDate < endDate)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm dữ liệu không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    string idEmployeeValue = await fapbll.GetIdEmployee(cboEmployee.Text.Trim());
                    int idPartner = await fapbll.GetIdPartner(cboPartner.Text.Trim());

                    var newProject = new ApiProjectDTO
                    {
                        idEmployee = idEmployeeValue,
                        idPartner = idPartner,
                        projectName = txtNameProject.Text,
                        results = "Chưa có kết quả",
                        startDateProject = txtDateTime.Value.ToString("yyyy-MM-dd"),
                        endDateProject = txtDateTime1.Value.ToString("yyyy-MM-dd"),
                        contractNo = txtNoNumber.Text,
                        description = txtDesc.Text,
                        fileProject = fileSaved,
                        status = "Chưa hoàn thành",
                        fileName = fileNameProject,
                    };

                    string projectJson = JsonSerializer.Serialize(newProject);
                    string result = await fapbll.Post(projectJson);

                    if (result != null)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridView();
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Thêm dữ liệu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Reset()
        {
            cboEmployee.SelectedIndex = 0;
            cboPartner.SelectedIndex = 0;
            txtNameProject.Text = string.Empty;
            txtResult.Text = string.Empty;
            txtNoNumber.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            fileSaved = null;
            fileNameProject = string.Empty;
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idProjectChoose))
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu nào để xóa", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa dự án này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                string result = await fapbll.Delete(idProjectChoose);

                if (result == "Deleted")
                {
                    MessageBox.Show("Xóa dữ liệu thành công", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    idProjectChoose = string.Empty;
                    Reset();
                }
                else
                {
                    MessageBox.Show("Dự án này đang có những danh sách công việc, không thể xóa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private async void tblProject_Click(object sender, EventArgs e)
        {
            if (tblProject.CurrentRow != null)
            {
                int i = tblProject.CurrentRow.Index;
                idProjectChoose = tblProject.Rows[i].Cells[0].Value.ToString();
                cboEmployee.Text = tblProject.Rows[i].Cells[1].Value.ToString();
                cboPartner.Text = tblProject.Rows[i].Cells[2].Value.ToString();
                txtNameProject.Text = tblProject.Rows[i].Cells[3].Value.ToString();
                nameProject = tblProject.Rows[i].Cells[3].Value.ToString();
                txtResult.Text = tblProject.Rows[i].Cells[4].Value.ToString();
                txtDateTime.Value = DateTime.Parse(tblProject.Rows[i].Cells[5].Value.ToString());
                txtDateTime1.Value = DateTime.Parse(tblProject.Rows[i].Cells[6].Value.ToString());
                txtNoNumber.Text = tblProject.Rows[i].Cells[7].Value.ToString();
                txtDesc.Text = tblProject.Rows[i].Cells[8].Value.ToString();
                cboHoanThanh.Text = tblProject.Rows[i].Cells[9].Value.ToString();
                fileSaved = await fapbll.GetDataFileByIdProject(idProjectChoose);
                if (tblProject.Rows[i].Cells[10].Value == null)
                {
                    txtFileName.Text = "Không có file nào được lưu";
                }
                else
                {
                    txtFileName.Text = tblProject.Rows[i].Cells[10].Value.ToString();
                }
                btnAddNewProjectContent.Text = "Thêm công việc cho dự án: " + idProjectChoose;
            }
            else
                MessageBox.Show("Bạn chưa chọn dòng nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmMain1 frmMain1 = new frmMain1();
            frmMain1.Show();
            Hide();
        }

        private void frmAddProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frmMain1 = new frmMain1();
            frmMain1.Show();
            Hide();
        }

        private void btnAddNewProjectContent_Click(object sender, EventArgs e)
        {
            if (!idProjectChoose.Equals(string.Empty))
            {
                frmAddProjectContent frm = new frmAddProjectContent();
                frm.Show();
                Hide();
            }
            else
                MessageBox.Show("Bạn cần chọn 1 dự án để tạo công việc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            idProjectChoose = string.Empty;
            nameProject = string.Empty;
            cboEmployee.SelectedIndex = 0;
            cboPartner.SelectedIndex = 0;
            txtNameProject.Text = string.Empty;
            txtResult.Text = string.Empty;
            txtDateTime.Value = DateTime.Now;
            txtDateTime1.Value = DateTime.Now;
            txtNoNumber.Text = string.Empty;
            txtDesc.Text = string.Empty;
            cboHoanThanh.SelectedIndex = 0;
            txtFileName.Text = string.Empty;
            fileSaved = null;
            LoadDataGridView();
            btnAddNewProjectContent.Text = "Thêm công việc cho dự án";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadDataGridView();
            }
            else
            {
                SearchData(txtSearch.Text);
            }    
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            DateTime startDate = txtDateTime.Value;
            DateTime endDate = txtDateTime1.Value;
            if (startDate < endDate)
            {
                if (!idProjectChoose.Equals(string.Empty))
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật dự án này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        string idEmployeeValue = await fapbll.GetIdEmployee(cboEmployee.Text.Trim());
                        int idPartner = await fapbll.GetIdPartner(cboPartner.Text.Trim());
                        string rsValue = string.Empty;
                        if (string.IsNullOrEmpty(txtResult.Text))
                            rsValue = "Chưa có kết quả";
                        else rsValue = txtResult.Text;
                        var newProject = new ApiProjectDTO
                        {
                            idEmployee = idEmployeeValue,
                            idPartner = idPartner,
                            projectName = txtNameProject.Text,
                            results = rsValue,
                            startDateProject = txtDateTime.Value.ToString("yyyy-MM-dd"),
                            endDateProject = txtDateTime1.Value.ToString("yyyy-MM-dd"),
                            contractNo = txtNoNumber.Text,
                            description = txtDesc.Text,
                            fileProject = fileSaved,
                            fileName = fileNameProject,
                            status = cboHoanThanh.Text,
                        };
                        string projectJson = JsonSerializer.Serialize(newProject);
                        string result = await fapbll.Update(idProjectChoose, projectJson);
                        if (result != null)
                        {
                            MessageBox.Show("Cập nhật dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataGridView();
                            Reset();
                        }
                        else
                            MessageBox.Show("Cập nhật dữ liệu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chọn 1 dữ liệu cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private async void linkExportExcel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<DTO.Excel.ProjectExcelDTO> list = await fapbll.DataToExportExcel();
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

        public DataTable ConvertToDataTable(List<DTO.Excel.ProjectExcelDTO> list)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("Mã dự án", typeof(string));
            dataTable.Columns.Add("Mã và Tên nhân viên", typeof(string));
            dataTable.Columns.Add("Tên công ty", typeof(string));
            dataTable.Columns.Add("Tên dự án", typeof(string));
            dataTable.Columns.Add("Kết quả", typeof(string));
            dataTable.Columns.Add("Ngày bắt đầu", typeof(string));
            dataTable.Columns.Add("Ngày kết thúc", typeof(string));
            dataTable.Columns.Add("Số hợp đồng", typeof(string));
            dataTable.Columns.Add("Mô tả", typeof(string));
            dataTable.Columns.Add("Trạng thái", typeof(string));

            foreach (var item in list)
            {
                DataRow row = dataTable.NewRow();
                row["Mã dự án"] = item.idProject;
                row["Mã và Tên nhân viên"] = item.idAndNameEmployee;
                row["Tên công ty"] = item.nameCompany;
                row["Tên dự án"] = item.nameProject;
                row["Kết quả"] = item.result;
                row["Ngày bắt đầu"] = item.startDate;
                row["Ngày kết thúc"] = item.endDate;
                row["Số hợp đồng"] = item.contractNo;
                row["Mô tả"] = item.description;
                row["Trạng thái"] = item.status;

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
