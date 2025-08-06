using BLL;
using DTO;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IRT_Management_Project
{
    public partial class frmAddOneStrain : Form
    {
        private static byte[] fileImage = null;
        private FormAddOneStrainBLL osbll;
        public frmAddOneStrain()
        {
            InitializeComponent();
            osbll = new FormAddOneStrainBLL();
            LoadComboBoxSpecies();
            LoadComboBoxIdConditional();
            txtDinhdanh.Text = $"{frmLogin.idEmployee} - {frmLogin.fullNameEmployee}";
            txtPhanlap.Text = $"{frmLogin.idEmployee} - {frmLogin.fullNameEmployee}";
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }

        private async void LoadComboBoxSpecies()
        {
            cboSpecies.Items.Add("Chọn--");
            foreach (string item in await osbll.getListSpecies())
            {
                cboSpecies.Items.Add(item);
            }
            cboSpecies.SelectedIndex = 0;
        }

        private async void LoadComboBoxIdConditional()
        {
            cboConditionalStrain.Items.Add("Chọn--");
            foreach (string item in await osbll.getListIdConditionalStrain())
            {
                cboConditionalStrain.Items.Add(item);
            }
            cboConditionalStrain.SelectedIndex = 0;
        }

        private void btnChonHinh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn hình ảnh";
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileImage = null;
                fileImage = File.ReadAllBytes(ofd.FileName);
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void btnXoaHinh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fileImage = null;
            pictureBox1.Image = null;
        }

        private void ResetFields()
        {
            txtScientificName.Text = string.Empty;
            txtSynonym.Text = string.Empty;
            txtFormerName.Text = string.Empty;
            txtCommonName.Text = string.Empty;
            txtCellSize.Text = string.Empty;
            txtOrganization.Text = string.Empty;
            txtCharacterist.Text = string.Empty;
            txtCollectionSite.Text = string.Empty;
            txtContient.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtIsolationSource.Text = string.Empty;
            txtToxinProducer.Text = string.Empty;
            txtStateOfStrain.Text = string.Empty;
            txtAgitationResistance.Text = string.Empty;
            txtRemark.Text = string.Empty;
            txtGenInformation.Text = string.Empty;
            txtPublication.Text = string.Empty;
            cboRecommended.SelectedIndex = 0;
            cboSpecies.SelectedIndex = 0;
            cboConditionalStrain.SelectedIndex = 0;

            fileImage = null;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            frmMain1 frmMain = new frmMain1();
            frmMain.Show();
        }

        private void frmAddOneStrain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            frmMain1 frmMain = new frmMain1();
            frmMain.Show();
        }

        private List<string[]> allDataRows;
        private int currentDataRowIndex;

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string message = string.Empty;

            if (cboConditionalStrain.SelectedIndex == 0)
            {
                message += "Bạn chưa chọn điều kiện cho strain.\n";
            }
            if (cboSpecies.SelectedIndex == 0)
            {
                message += "Bạn chưa chọn loài cho strain.\n";
            }

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
            string strainNumberNull = null;
            int idSpeciesValue = await osbll.GetIdSpeciesByName(cboSpecies.Text.Trim());
            int idConditionValue = 0;
            string firstPart = cboConditionalStrain.Text.Split(',')[0];
            Match match = Regex.Match(firstPart, @"\d+");
            if (match.Success)
            {
                idConditionValue = int.Parse(match.Value);
            }

            var newStrain = new StrainToAddDTO
            {
                strainNumber = strainNumberNull,
                idSpecies = idSpeciesValue,
                idCondition = idConditionValue,
                imageStrain = fileImage,
                scientificName = txtScientificName.Text.Trim(),
                synonymStrain = txtSynonym.Text.Trim(),
                formerName = txtFormerName.Text.Trim(),
                commonName = txtCommonName.Text.Trim(),
                cellSize = txtCellSize.Text.Trim(),
                organization = txtOrganization.Text.Trim(),
                characteristics = txtCharacterist.Text.Trim(),
                collectionSite = txtCollectionSite.Text.Trim(),
                continent = txtContient.Text.Trim(),
                country = txtCountry.Text.Trim(),
                isolationSource = txtIsolationSource.Text.Trim(),
                toxinProducer = txtToxinProducer.Text.Trim(),
                stateOfStrain = txtStateOfStrain.Text.Trim(),
                agitationResistance = txtAgitationResistance.Text.Trim(),
                remarks = txtRemark.Text.Trim(),
                geneInformation = txtGenInformation.Text.Trim(),
                publications = txtPublication.Text.Trim(),
                recommendedForTeaching = cboRecommended.Text.Trim(),
                dateAdd = dateTimeNow
            };

            string strainJson = JsonSerializer.Serialize(newStrain);
            string result = await osbll.AddData(strainJson);
            if (result != null)
            {
                message += "Thêm thành công 1 strain mới.\n";
                int idLastRecord = await osbll.GetIdFromLastRecord();
                string newReason = null;
                string dateTime = null;

                var newStrainHistory = new
                {
                    idStrain = idLastRecord,
                    status = "Đang chờ xét duyệt",
                    dateApproval = dateTime,
                    reason = newReason
                };
                string strainHistoryJson = JsonSerializer.Serialize(newStrainHistory);
                string result1 = await osbll.AddDataStrainHistory(strainHistoryJson);
                if (result1 != null)
                    message += "Thêm lịch sử duyệt strain thành công.\n";
                else
                    message += "Thêm lịch sử duyệt strain thất bại.\n";

                var newIdentifyStrain = new
                {
                    iD_Employee = frmLogin.idEmployee,
                    iD_Strain = idLastRecord,
                    year_of_Identify = DateTime.Now.Year,
                };
                string identifyStrainJson = JsonSerializer.Serialize(newIdentifyStrain);
                string result2 = await osbll.AddDataIdentifyStrain(identifyStrainJson);
                if (result2 != null)
                    message += "Định danh strain thành công.\n";
                else
                    message += "Định danh strain thất bại.\n";

                var newIsolatorStrain = new
                {
                    iD_Employee = frmLogin.idEmployee,
                    iD_Strain = idLastRecord,
                    yearOfIsolator = DateTime.Now.Year,
                };
                string isolatorStrainJson = JsonSerializer.Serialize(newIsolatorStrain);
                string result3 = await osbll.AddDataIsolatorStrain(isolatorStrainJson);
                if (result3 != null)
                    message += "Phân lập strain thành công.\n";
                else
                    message += "Phân lập strain thất bại.\n";

                ResetFields();
            }
            else
            {
                message += "Thêm 1 strain mới thất bại.\n";
            }

            MessageBox.Show(message.Trim(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //private void LoadExcelData(string filePath)
        //{
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
        //    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;

        //    try
        //    {
        //        allDataRows = new List<string[]>();
        //        currentDataRowIndex = 0;

        //        for (int row = 2; row <= xlRange.Rows.Count; row++)
        //        {
        //            string[] rowData = new string[xlRange.Columns.Count];

        //            for (int col = 1; col <= xlRange.Columns.Count; col++)
        //            {
        //                rowData[col - 1] = xlRange.Cells[row, col].Value2?.ToString() ?? "";
        //            }

        //            allDataRows.Add(rowData);
        //        }

        //        DisplayCurrentDataRow();
        //        btnNextRecord.Enabled = allDataRows.Count > 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Đã xảy ra lỗi khi đọc dữ liệu từ tệp Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        Marshal.ReleaseComObject(xlRange);
        //        Marshal.ReleaseComObject(xlWorksheet);
        //        xlWorkbook.Close();
        //        Marshal.ReleaseComObject(xlWorkbook);
        //        xlApp.Quit();
        //        Marshal.ReleaseComObject(xlApp);
        //    }
        //}

        //private void DisplayCurrentDataRow()
        //{
        //    if (currentDataRowIndex >= 0 && currentDataRowIndex < allDataRows.Count)
        //    {
        //        string[] rowData = allDataRows[currentDataRowIndex];
        //        if (rowData.Length >= 16)
        //        {
        //            txtScientificName.Text = rowData[0];
        //            txtSynonym.Text = rowData[1];
        //            txtFormerName.Text = rowData[2];
        //            txtCommonName.Text = rowData[3];
        //            txtCellSize.Text = rowData[4];
        //            txtOrganization.Text = rowData[5];
        //            txtCharacterist.Text = rowData[6];
        //            txtCollectionSite.Text = rowData[7];
        //            txtContient.Text = rowData[8];
        //            txtCountry.Text = rowData[9];
        //            txtIsolationSource.Text = rowData[10];
        //            txtToxinProducer.Text = rowData[11];
        //            txtStateOfStrain.Text = rowData[12];
        //            txtAgitationResistance.Text = rowData[13];
        //            txtRemark.Text = rowData[14];
        //            txtGenInformation.Text = rowData[15];
        //            txtPublication.Text = rowData[16];
        //        }
        //    }
        //}

        private async Task LoadExcelDataAndSaveToDb(string filePath)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            try
            {
                for (int row = 2; row <= xlRange.Rows.Count; row++)
                {
                    string[] rowData = new string[xlRange.Columns.Count];

                    for (int col = 1; col <= xlRange.Columns.Count; col++)
                    {
                        rowData[col - 1] = xlRange.Cells[row, col].Value2?.ToString() ?? "";
                    }
                    if (string.IsNullOrEmpty(rowData[0]) && string.IsNullOrEmpty(rowData[1]))
                    {
                        break;
                    }

                    string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                    string strainNumberNull = null;

                    if (!int.TryParse(rowData[0], out int idSpeciesValue))
                    {
                        MessageBox.Show($"Định dạng không hợp lệ cho idSpeciesValue tại dòng {row}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    if (!int.TryParse(rowData[1], out int idConditionValue))
                    {
                        MessageBox.Show($"Định dạng không hợp lệ cho idConditionValue tại dòng {row}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    var newStrain = new StrainToAddDTO
                    {
                        strainNumber = strainNumberNull,
                        idSpecies = idSpeciesValue,
                        idCondition = idConditionValue,
                        imageStrain = null,
                        scientificName = rowData[2]?.Trim() ?? "",
                        synonymStrain = rowData[3]?.Trim() ?? "",
                        formerName = rowData[4]?.Trim() ?? "",
                        commonName = rowData[5]?.Trim() ?? "",
                        cellSize = rowData[6]?.Trim() ?? "",
                        organization = rowData[7]?.Trim() ?? "",
                        characteristics = rowData[8]?.Trim() ?? "",
                        collectionSite = rowData[9]?.Trim() ?? "",
                        continent = rowData[10]?.Trim() ?? "",
                        country = rowData[11]?.Trim() ?? "",
                        isolationSource = rowData[12]?.Trim() ?? "",
                        toxinProducer = rowData[13]?.Trim() ?? "",
                        stateOfStrain = rowData[14]?.Trim() ?? "",
                        agitationResistance = rowData[15]?.Trim() ?? "",
                        remarks = rowData[16]?.Trim() ?? "",
                        geneInformation = rowData[17]?.Trim() ?? "",
                        publications = rowData[18]?.Trim() ?? "",
                        recommendedForTeaching = rowData[19]?.Trim() ?? "",
                        dateAdd = dateTimeNow
                    };

                    string strainJson = JsonSerializer.Serialize(newStrain);
                    string result = await osbll.AddData(strainJson);
                    if (result != null)
                    {
                        int idLastRecord = await osbll.GetIdFromLastRecord();
                        string newReason = null;
                        string dateTime = null;

                        var newStrainHistory = new
                        {
                            idStrain = idLastRecord,
                            status = "Đang chờ xét duyệt",
                            dateApproval = dateTime,
                            reason = newReason
                        };
                        string strainHistoryJson = JsonSerializer.Serialize(newStrainHistory);
                        await osbll.AddDataStrainHistory(strainHistoryJson);

                        var newIdentifyStrain = new
                        {
                            iD_Employee = frmLogin.idEmployee,
                            iD_Strain = idLastRecord,
                            year_of_Identify = DateTime.Now.Year,
                        };
                        string identifyStrainJson = JsonSerializer.Serialize(newIdentifyStrain);
                        await osbll.AddDataIdentifyStrain(identifyStrainJson);

                        var newIsolatorStrain = new
                        {
                            iD_Employee = frmLogin.idEmployee,
                            iD_Strain = idLastRecord,
                            yearOfIsolator = DateTime.Now.Year,
                        };
                        string isolatorStrainJson = JsonSerializer.Serialize(newIsolatorStrain);
                        await osbll.AddDataIsolatorStrain(isolatorStrainJson);
                    }
                }

                MessageBox.Show("Nhập dữ liệu từ tệp Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi đọc dữ liệu từ tệp Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }
        }


        private async void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn tệp Excel";
            openFileDialog.Filter = "Tệp Excel|*.xlsx;*.xls";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                await LoadExcelDataAndSaveToDb(filePath);
                //LoadExcelData(filePath);
            }
        }

        private void btnNextRecord_Click(object sender, EventArgs e)
        {
            //currentDataRowIndex++;
            //DisplayCurrentDataRow();
            //if (currentDataRowIndex >= allDataRows.Count)
            //{
            //    btnNextRecord.Enabled = false;
            //    ResetFields();
            //}
        }

        private void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Chọn nơi để lưu tệp Excel";
            saveFileDialog.Filter = "Tệp Excel|*.xlsx";
            saveFileDialog.FileName = "template.xlsx";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel._Worksheet worksheet = workbook.Sheets[1];
                worksheet.Name = "Data";

                try
                {
                    string[] headers = {
                        "Id Species",
                        "Id ConditionalStrain",
                        "Scientific Name",
                        "Synonym",
                        "Former Name",
                        "Common Name",
                        "Cell Size",
                        "Organization",
                        "Characteristics",
                        "Collection Site",
                        "Continent",
                        "Country",
                        "Isolation Source",
                        "Toxin Producer",
                        "State of Strain",
                        "Agitation Resistance",
                        "Remark",
                        "Gene Information",
                        "Publication",
                        "Recommended"
                    };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cells[1, i + 1] = headers[i];
                    }
                    string outputPath = saveFileDialog.FileName;
                    workbook.SaveAs(outputPath);
                    excelApp.Quit();

                    MessageBox.Show("File Excel mẫu đã được tạo thành công và lưu tại: " + outputPath, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tạo tệp Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Marshal.ReleaseComObject(worksheet);
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(excelApp);
                }
            }
        }

        private async void cboConditionalStrain_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cboConditionalStrain.SelectedIndex == 0)
            {
                medium.Text = string.Empty;
                temperature.Text = string.Empty;
                lightIntensity.Text = string.Empty;
                duration.Text = string.Empty;
                return;
            }
            string text = cboConditionalStrain.Text;
            Match match = Regex.Match(text, @"\d+");
            var obj = await osbll.ViewDetailConditionalStrain(int.Parse(match.Value));
            medium.Text = obj.medium;
            temperature.Text = obj.temperature;
            lightIntensity.Text = obj.lightIntensity;
            duration.Text = obj.duration;
        }
    }
}
