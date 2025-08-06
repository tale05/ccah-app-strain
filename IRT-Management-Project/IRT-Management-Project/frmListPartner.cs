using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace IRT_Management_Project
{
    public partial class frmListPartner : Form
    {
        FormListPartnerBLL pbll;
        public frmListPartner()
        {
            InitializeComponent();
            pbll = new FormListPartnerBLL();
            LoadDataGridView();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
        private async void LoadDataGridView()
        {
            List<PartnerDTO> data = await pbll.GetDataFullProperties();
            InsertDataAndDesignTable(data);
        }
        private void InsertDataAndDesignTable(List<PartnerDTO> lstInput)
        {
            tblPartner.DataSource = null;
            tblPartner.Rows.Clear();
            tblPartner.Columns.Clear();
            tblPartner.AutoGenerateColumns = false;

            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idPartner",
                HeaderText = "Mã",
                DataPropertyName = "idPartner"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameCompany",
                HeaderText = "Tên Công Ty",
                DataPropertyName = "nameCompany"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "addressCompany",
                HeaderText = "Địa Chỉ Công Ty",
                DataPropertyName = "addressCompany"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "namePartner",
                HeaderText = "Tên Đối Tác",
                DataPropertyName = "namePartner"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "position",
                HeaderText = "Chức Vụ",
                DataPropertyName = "position"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "phoneNumber",
                HeaderText = "Số Điện Thoại",
                DataPropertyName = "phoneNumber"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "bankNumber",
                HeaderText = "Tài Khoản Ngân Hàng",
                DataPropertyName = "bankNumber"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "bankName",
                HeaderText = "Tên Ngân Hàng",
                DataPropertyName = "bankName"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "qhnsNumber",
                HeaderText = "Mã quan hệ ngân sách",
                DataPropertyName = "qhnsNumber"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameWard",
                HeaderText = "Col1",
                DataPropertyName = "nameWard"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameDistrict",
                HeaderText = "Col2",
                DataPropertyName = "nameDistrict"
            });
            tblPartner.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameProvince",
                HeaderText = "Col3",
                DataPropertyName = "nameProvince"
            });

            tblPartner.DataSource = lstInput;

            tblPartner.Columns["nameWard"].Visible = false;
            tblPartner.Columns["nameDistrict"].Visible = false;
            tblPartner.Columns["nameProvince"].Visible = false;

            tblPartner.Columns["idPartner"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblPartner.Columns["phoneNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblPartner.Columns["bankNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblPartner.Columns["bankName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblPartner.Columns["qhnsNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tblPartner.Columns["nameCompany"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tblPartner.Columns["addressCompany"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void frmListPartner_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
            Hide();
        }

        private void tblPartner_Click(object sender, EventArgs e)
        {
            if (tblPartner.CurrentRow != null)
            {
                flowLayoutPanel2.Visible = true;
                int i = tblPartner.CurrentRow.Index;
                lblTencty.Text = tblPartner.Rows[i].Cells["nameCompany"].Value.ToString();
                lblDiachi.Text = tblPartner.Rows[i].Cells["addressCompany"].Value.ToString();
                lblTendoitac.Text = tblPartner.Rows[i].Cells["namePartner"].Value.ToString();
                lblChucvu.Text = tblPartner.Rows[i].Cells["position"].Value.ToString();
                lblSdt.Text = tblPartner.Rows[i].Cells["phoneNumber"].Value.ToString();
                lblNganhang.Text = $"{tblPartner.Rows[i].Cells["bankNumber"].Value} - " +
                    $"{tblPartner.Rows[i].Cells["bankName"].Value}";
                lblMqhns.Text = tblPartner.Rows[i].Cells["qhnsNumber"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn dòng nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }

        private void btnThemthongtin_Click(object sender, EventArgs e)
        {
            Dispose();
            frmAddPartner frm = new frmAddPartner();
            frm.Show();
        }

        private async void linkExportExcel_Click(object sender, EventArgs e)
        {
            List<DTO.Excel.PartnerExcelDTO> list = await pbll.GetDataExcel();
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

        public DataTable ConvertToDataTable(List<DTO.Excel.PartnerExcelDTO> list)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("Mã đối tác", typeof(int));
            dataTable.Columns.Add("Tên công ty", typeof(string));
            dataTable.Columns.Add("Địa chỉ công ty", typeof(string));
            dataTable.Columns.Add("Tên đối tác", typeof(string));
            dataTable.Columns.Add("Chức vụ", typeof(string));
            dataTable.Columns.Add("Số điện thoại", typeof(string));
            dataTable.Columns.Add("Số tài khoản", typeof(string));
            dataTable.Columns.Add("Tên ngân hàng", typeof(string));
            dataTable.Columns.Add("Số QHNS", typeof(string));

            foreach (var item in list)
            {
                DataRow row = dataTable.NewRow();
                row["Mã đối tác"] = item.idPartner;
                row["Tên công ty"] = item.nameCompany;
                row["Địa chỉ công ty"] = item.addressCompany;
                row["Tên đối tác"] = item.namePartner;
                row["Chức vụ"] = item.position;
                row["Số điện thoại"] = item.phoneNumber;
                row["Số tài khoản"] = item.bankNumber;
                row["Tên ngân hàng"] = item.bankName;
                row["Số QHNS"] = item.qhnsNumber;

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
