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
    public partial class frmAddContentWork : Form
    {
        private FormAddContentWorkBLL cwbll;
        private static string idProjectContent = string.Empty,
            idProject = string.Empty,
            nameContent = string.Empty,
            nameProject = string.Empty;
        private static int idContentWork = 0, titleValue = 0, subTitle = 0;
        public frmAddContentWork()
        {
            InitializeComponent();
            txtStartDate.Value = DateTime.Now;
            txtEndDate.Value = DateTime.Now;
            idProjectContent = frmAddProjectContent.idProjectContent;
            nameContent = frmAddProjectContent.nameContent;
            idProject = frmAddProject.idProjectChoose;
            nameProject = frmAddProject.nameProject;
            cwbll = new FormAddContentWorkBLL();
            lblThuocduan.Text = $"{idProject} - {nameProject}";
            lblThuocnoidungcongviec.Text = nameContent;
            LoadComboBoxEmployee();
            LoadDataGridView();
            titleValue = frmAddProjectContent.title;
            DisplayHint();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }

        private async void DisplayHint()
        {
            string title = titleValue.ToString();
            int subTitle = await GenerateSubTitleAuto();
            label7.Text = $"Bạn đang thêm Công việc {title}.{subTitle}";
        }

        private void frmAddContentWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            frmAddProjectContent.idProjectContent = string.Empty;
            frmAddProjectContent.nameContent = string.Empty;
            frmAddProjectContent frm = new frmAddProjectContent();
            frm.Show();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmAddProjectContent.idProjectContent = string.Empty;
            frmAddProjectContent.nameContent = string.Empty;
            Hide();
            frmAddProjectContent frm = new frmAddProjectContent();
            frm.Show();
        }
        private void DesignTable(List<DetailTableContentWorkDTO> contentWorkList)
        {
            tblContentWork.DataSource = null;
            tblContentWork.Rows.Clear();
            tblContentWork.Columns.Clear();

            tblContentWork.AutoGenerateColumns = false;
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idContentWork",
                HeaderText = "Mã",
                DataPropertyName = "idContentWork"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameAndIdEmployee",
                HeaderText = "Nhân viên",
                DataPropertyName = "nameAndIdEmployee"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameContent",
                HeaderText = "Nội dung công việc",
                DataPropertyName = "nameContent"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "result",
                HeaderText = "Kết quả yêu cầu",
                DataPropertyName = "result"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "startDate",
                HeaderText = "Ngày bắt đầu",
                DataPropertyName = "startDate"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "endDate",
                HeaderText = "Ngày kết thúc",
                DataPropertyName = "endDate"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ennDateActual",
                HeaderText = "Ngày kết thúc thực tế",
                DataPropertyName = "ennDateActual"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "contractNo",
                HeaderText = "Số hợp đồng",
                DataPropertyName = "contractNo"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "priority",
                HeaderText = "Ưu tiên",
                DataPropertyName = "priority"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "status",
                HeaderText = "Trạng thái",
                DataPropertyName = "status"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "notification",
                HeaderText = "Thông báo",
                DataPropertyName = "notification"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "title",
                HeaderText = "Tiêu đề",
                DataPropertyName = "title"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "subTitle",
                HeaderText = "Tiêu đề phụ",
                DataPropertyName = "subTitle"
            });
            tblContentWork.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "histories",
                HeaderText = "Lịch sử",
                DataPropertyName = "histories"
            });

            tblContentWork.DataSource = contentWorkList;

            tblContentWork.Columns["idContentWork"].Visible = false;
            tblContentWork.Columns["ennDateActual"].Visible = false;
            tblContentWork.Columns["contractNo"].Visible = false;
            tblContentWork.Columns["notification"].Visible = false;
            tblContentWork.Columns["title"].Visible = false;
            tblContentWork.Columns["subTitle"].Visible = false;
            tblContentWork.Columns["histories"].Visible = false;

            tblContentWork.Columns["nameAndIdEmployee"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblContentWork.Columns["startDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblContentWork.Columns["endDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblContentWork.Columns["priority"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblContentWork.Columns["status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tblContentWork.Columns["nameContent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tblContentWork.Columns["result"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            tblContentWork.RowPrePaint -= tblContentWork_RowPrePaint;
            tblContentWork.RowPrePaint += tblContentWork_RowPrePaint;
        }
        private async void LoadDataGridView()
        {
            try
            {
                tblContentWork.DataSource = null;
                tblContentWork.Rows.Clear();
                tblContentWork.Columns.Clear();

                int parsedIdProjectContent;
                if (int.TryParse(idProjectContent.Trim(), out parsedIdProjectContent))
                {
                    var contentWorkList = await cwbll.GetListContentWork(parsedIdProjectContent);

                    if (contentWorkList.Any())
                    {
                        DesignTable(contentWorkList);
                    }
                }
                else
                {
                    MessageBox.Show("idProjectContent không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void SearchData(string search)
        {
            try
            {
                tblContentWork.DataSource = null;
                tblContentWork.Rows.Clear();
                tblContentWork.Columns.Clear();

                int parsedIdProjectContent;
                if (int.TryParse(idProjectContent.Trim(), out parsedIdProjectContent))
                {
                    var contentWorkList = await cwbll.SearchData(parsedIdProjectContent, search);

                    if (contentWorkList.Any())
                    {
                        DesignTable(contentWorkList);
                    }
                }
                else
                {
                    MessageBox.Show("idProjectContent không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void FillByStatus(string status)
        {
            try
            {
                tblContentWork.DataSource = null;
                tblContentWork.Rows.Clear();
                tblContentWork.Columns.Clear();

                int parsedIdProjectContent;
                if (int.TryParse(idProjectContent.Trim(), out parsedIdProjectContent))
                {
                    var contentWorkList = await cwbll.FillDataByStatus(parsedIdProjectContent, status);

                    if (contentWorkList.Any())
                    {
                        DesignTable(contentWorkList);
                    }
                }
                else
                {
                    MessageBox.Show("idProjectContent không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void Reset()
        {
            LoadDataGridView();
            txtNameContent.Text = string.Empty;
            txtResult.Text = string.Empty;
            txtContractNo.Text = string.Empty;
            txtEnddatactual.Text = string.Empty;
            txtNotification.Text = string.Empty;
            txtStartDate.Value = DateTime.Now;
            txtEndDate.Value = DateTime.Now;
            cboEmployee.SelectedIndex = 0;
            cboPriority.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            txtHistory.Text = string.Empty;
        }

        private async Task<int> GenerateSubTitleAuto()
        {
            string lastSubTitleStr = await cwbll.GetLastTitleByIdProjectContent(int.Parse(idProjectContent));

            if (int.TryParse(lastSubTitleStr, out int lastTitle) && lastTitle != 0)
            {
                return lastTitle + 1;
            }
            else
            {
                return 1;
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (!idContentWork.Equals(string.Empty))
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DateTime startDate = txtStartDate.Value;
                    DateTime endDate = txtEndDate.Value;

                    if (startDate < endDate)
                    {
                        string idEmployee = cboEmployee.Text.Trim().Substring(0, 5).Trim();
                        string historyValue = await cwbll.GetHistoryByIdContentWork(idContentWork) + "\n";
                        string rsValue = string.Empty;
                        if (string.IsNullOrEmpty(txtResult.Text))
                            rsValue = "Chưa có kết quả";
                        else rsValue = txtResult.Text;
                        var newContentWork = new ContentWorkToAddUpdateDTO
                        {
                            idProjectContent = int.Parse(idProjectContent),
                            idEmployee = idEmployee,
                            nameContent = txtNameContent.Text,
                            results = rsValue,
                            startDate = txtStartDate.Value.ToString("yyyy-MM-dd"),
                            endDate = txtEndDate.Value.ToString("yyyy-MM-dd"),
                            contractNo = txtContractNo.Text,
                            status = cboStatus.Text,
                            priority = cboPriority.Text,
                            notificattion = txtNotification.Text,
                            title = titleValue,
                            subTitle = subTitle,
                            histories = $"{historyValue}{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}, {cboEmployee.Text.Trim()}, công việc: {txtNameContent.Text}",
                        };
                        string json = JsonSerializer.Serialize(newContentWork);
                        string result = await cwbll.Update(idContentWork, json);
                        if (result != null)
                        {
                            MessageBox.Show("Cập nhật dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string idProject = await cwbll.GetIdProjectByIdProjectContent(int.Parse(idProjectContent));

                            bool rsProjectContent = await cwbll.CheckAllStatusContentWork(int.Parse(idProjectContent));
                            if (!rsProjectContent)
                                await cwbll.UpdateStatusProjectContent(int.Parse(idProjectContent), "Đã hoàn thành");
                            else
                                await cwbll.UpdateStatusProjectContent(int.Parse(idProjectContent), "Chưa hoàn thành");

                            bool rsProject = await cwbll.CheckAllStatusProjectContent(idProject);
                            if (!rsProject)
                                await cwbll.UpdateStatusProject(idProject, "Đã hoàn thành");
                            else
                                await cwbll.UpdateStatusProject(idProject, "Chưa hoàn thành");
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
            }
            else
            {
                MessageBox.Show("Phải chọn 1 dữ liệu để cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (!idProjectContent.Equals(string.Empty))
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    string result = await cwbll.Delete(idContentWork);

                    if (result != null)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridView();
                        DisplayHint();
                        bool rs = await cwbll.CheckStatus(int.Parse(idProjectContent));
                        if (!rs)
                            await cwbll.UpdateStatusProjectContent(int.Parse(idProjectContent), "Đã hoàn thành");
                        else
                            await cwbll.UpdateStatusProjectContent(int.Parse(idProjectContent), "Chưa hoàn thành");
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu đang được sử dụng. Không thể xóa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Phải chọn 1 dữ liệu để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void cboFillBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboFillBy.SelectedIndex;
            switch (index)
            {
                case 0:
                    LoadDataGridView();
                    break;
                case 1:
                    FillByStatus(cboFillBy.Text.Trim());
                    break;
                case 2:
                    FillByStatus(cboFillBy.Text.Trim());
                    break;
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                DateTime startDate = txtStartDate.Value;
                DateTime endDate = txtEndDate.Value;

                if (startDate < endDate)
                {
                    int lastSubTitle = await GenerateSubTitleAuto();
                    string idEmployee = cboEmployee.Text.Trim().Substring(0, 5).Trim();
                    var newContentWork = new ContentWorkToAddUpdateDTO
                    {
                        idProjectContent = int.Parse(idProjectContent),
                        idEmployee = idEmployee,
                        nameContent = txtNameContent.Text,
                        results = "Chưa có kết quả",
                        startDate = txtStartDate.Value.ToString("yyyy-MM-dd"),
                        endDate = txtEndDate.Value.ToString("yyyy-MM-dd"),
                        contractNo = txtContractNo.Text,
                        status = cboStatus.Text,
                        priority = cboPriority.Text,
                        notificattion = txtNotification.Text,
                        title = titleValue,
                        subTitle = lastSubTitle,
                        histories = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}, {cboEmployee.Text.Trim()}, công việc: {txtNameContent.Text}",
                    };
                    string json = JsonSerializer.Serialize(newContentWork);
                    string result = await cwbll.Post(json);
                    if (result != null)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string idProject = await cwbll.GetIdProjectByIdProjectContent(int.Parse(idProjectContent));

                        bool rsProjectContent = await cwbll.CheckAllStatusContentWork(int.Parse(idProjectContent));
                        if (!rsProjectContent)
                            await cwbll.UpdateStatusProjectContent(int.Parse(idProjectContent), "Đã hoàn thành");
                        else
                            await cwbll.UpdateStatusProjectContent(int.Parse(idProjectContent), "Chưa hoàn thành");

                        bool rsProject = await cwbll.CheckAllStatusProjectContent(idProject);
                        if (!rsProject)
                            await cwbll.UpdateStatusProject(idProject, "Đã hoàn thành");
                        else
                            await cwbll.UpdateStatusProject(idProject, "Chưa hoàn thành");

                        Reset();
                        DisplayHint();
                    }
                    else
                    {
                        MessageBox.Show("Thêm dữ liệu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void tblContentWork_Click(object sender, EventArgs e)
        {
            if (tblContentWork.CurrentRow != null)
            {
                int i = tblContentWork.CurrentRow.Index;
                if (i >= 0 && i < tblContentWork.Rows.Count)
                {
                    idContentWork = int.Parse(tblContentWork.Rows[i].Cells["idContentWork"].Value.ToString());
                    cboEmployee.Text = tblContentWork.Rows[i].Cells["nameAndIdEmployee"].Value.ToString();

                    string input = tblContentWork.Rows[i].Cells["nameContent"].Value.ToString();
                    nameContent = input;
                    int index = input.IndexOf(':');
                    if (index != -1 && index + 1 < input.Length)
                    {
                        string result = input.Substring(index + 1).Trim();
                        txtNameContent.Text = result;
                    }
                    txtResult.Text = tblContentWork.Rows[i].Cells["result"].Value.ToString();
                    txtStartDate.Text = tblContentWork.Rows[i].Cells["startDate"].Value.ToString();
                    txtEndDate.Text = tblContentWork.Rows[i].Cells["endDate"].Value.ToString();
                    txtEnddatactual.Text = tblContentWork.Rows[i].Cells["ennDateActual"].Value?.ToString() ?? "";
                    txtContractNo.Text = tblContentWork.Rows[i].Cells["contractNo"].Value.ToString();
                    cboPriority.Text = tblContentWork.Rows[i].Cells["priority"].Value.ToString();
                    cboStatus.Text = tblContentWork.Rows[i].Cells["status"].Value.ToString();

                    txtHistory.Text = tblContentWork.Rows[i].Cells["histories"].Value.ToString();
                    subTitle = int.Parse(tblContentWork.Rows[i].Cells["subTitle"].Value.ToString());

                    if (string.IsNullOrEmpty(tblContentWork.Rows[i].Cells["notification"].Value.ToString()))
                    {
                        txtNotification.Text = string.Empty;
                    }
                    else if (tblContentWork.Rows[i].Cells["notification"].Value.ToString().Equals("Nhân viên đã xem yêu cầu"))
                    {
                        txtNotification.Text = string.Empty;
                        txtNotification.PlaceholderText = "Nhân viên đã xem yêu cầu";
                    }
                    else
                    {
                        txtNotification.Text = tblContentWork.Rows[i].Cells["notification"].Value.ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có hàng nào được chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void LoadComboBoxEmployee()
        {
            cboEmployee.Items.Clear();
            cboEmployee.DataSource = await cwbll.GetListEmployee();
        }

        private async void linkExportExcel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<DTO.Excel.ContentWorkExcelDTO> list = await cwbll.DataToExportExcel(int.Parse(idProjectContent));
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

        public DataTable ConvertToDataTable(List<DTO.Excel.ContentWorkExcelDTO> list)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("idContentWork", typeof(int));
            dataTable.Columns.Add("idEmployee", typeof(string));
            dataTable.Columns.Add("nameEmployee", typeof(string));
            dataTable.Columns.Add("nameContent", typeof(string));
            dataTable.Columns.Add("result", typeof(string));
            dataTable.Columns.Add("startDate", typeof(string));
            dataTable.Columns.Add("endDate", typeof(string));
            dataTable.Columns.Add("ennDateActual", typeof(string));
            dataTable.Columns.Add("contractNo", typeof(string));
            dataTable.Columns.Add("priority", typeof(string));
            dataTable.Columns.Add("status", typeof(string));

            foreach (var item in list)
            {
                DataRow row = dataTable.NewRow();
                row["idContentWork"] = item.idContentWork;
                row["idEmployee"] = item.idEmployee;
                row["nameEmployee"] = item.nameEmployee;
                row["nameContent"] = item.nameContent;
                row["result"] = item.result;
                row["startDate"] = item.startDate;
                row["endDate"] = item.endDate;
                row["ennDateActual"] = item.ennDateActual;
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
