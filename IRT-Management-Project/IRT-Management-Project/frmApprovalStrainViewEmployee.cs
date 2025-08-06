using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmApprovalStrainViewEmployee : Form
    {
        private FormApprovalStrainBLL asbll;
        private FormAddOneStrainBLL osbll;
        private static int idSelected = 0;
        private static byte[] fileImage = null;

        public frmApprovalStrainViewEmployee()
        {
            InitializeComponent();
            asbll = new FormApprovalStrainBLL();
            osbll = new FormAddOneStrainBLL();
            LoadDataGridView();
            ApplyAlternatingRowColors(tableLayoutPanel1, Color.Honeydew, Color.White);
            LoadComboBoxSpecies();
            LoadComboBoxIdConditional();
            lblNameUser.Text = frmLogin.fullNameEmployee;
            //------------------------------
        }

        private void ApplyAlternatingRowColors(TableLayoutPanel table, Color color1, Color color2)
        {
            for (int row = 0; row < table.RowCount; row++)
            {
                Color rowColor = (row % 2 == 0) ? color1 : color2;

                for (int col = 0; col < table.ColumnCount; col++)
                {
                    var control = table.GetControlFromPosition(col, row);
                    if (control != null)
                    {
                        control.BackColor = rowColor;
                    }
                }
            }
        }

        public async void LoadDataGridView()
        {
            tblStrainHistory.DataSource = null;
            tblStrainHistory.Rows.Clear();
            tblStrainHistory.Columns.Clear();

            List<ApiStrainApprovalHistoryDTO> lst = await asbll.LoadDataBelongToEmployee(frmLogin.idEmployee);
            tblStrainHistory.DataSource = lst;

            tblStrainHistory.AutoGenerateColumns = true;
            tblStrainHistory.Columns[1].HeaderText = "Id";
            tblStrainHistory.Columns[2].HeaderText = "Trạng thái";
            tblStrainHistory.Columns[3].HeaderText = "Ngày xét duyệt";
            tblStrainHistory.Columns[4].HeaderText = "Lý do";

            tblStrainHistory.Columns[0].Visible = false;
            tblStrainHistory.Columns[3].Visible = false;
            tblStrainHistory.Columns[4].Visible = false;
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

        private void frmApprovalStrainViewEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
            Hide();
        }

        private async void tblStrainHistory_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = tblStrainHistory.SelectedRows[0];
            idSelected = int.Parse(selectedRow.Cells[1].Value.ToString());


            StrainCustomDetailDTO obj = await asbll.LoadFullProperties(idSelected);
            if (obj == null)
            {
                MessageBox.Show("No data found for the specified strain number.", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (obj.ImageStrain == null)
            {
                pictureBox1.Image = null;
            }
            else
            {
                using (MemoryStream ms = new MemoryStream(obj.ImageStrain))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            strainNumber.Text = string.IsNullOrEmpty(obj.StrainNumber) ? "-" : obj.StrainNumber;
            //phylum.Text = string.IsNullOrEmpty(obj.namePhylum) ? "-" : obj.namePhylum;
            //classes.Text = string.IsNullOrEmpty(obj.nameClass) ? "-" : obj.nameClass;
            cboSpecies.Text = string.IsNullOrEmpty(obj.nameSpecies) ? "-" : obj.nameSpecies;
            scientificName.Text = string.IsNullOrEmpty(obj.ScientificName) ? "-" : obj.ScientificName;
            synonym.Text = string.IsNullOrEmpty(obj.SynonymStrain) ? "-" : obj.SynonymStrain;
            formerName.Text = string.IsNullOrEmpty(obj.FormerName) ? "-" : obj.FormerName;
            commonName.Text = string.IsNullOrEmpty(obj.CommonName) ? "-" : obj.CommonName;
            cellSize.Text = string.IsNullOrEmpty(obj.CellSize) ? "-" : obj.CellSize;
            organization.Text = string.IsNullOrEmpty(obj.Organization) ? "-" : obj.Organization;
            collectionSite.Text = string.IsNullOrEmpty(obj.CollectionSite) ? "-" : obj.CollectionSite;
            continent.Text = string.IsNullOrEmpty(obj.Continent) ? "-" : obj.Continent;
            country.Text = string.IsNullOrEmpty(obj.Country) ? "-" : obj.Country;
            environment.Text = string.IsNullOrEmpty(obj.IsolationSource) ? "-" : obj.IsolationSource;
            medium.Text = string.IsNullOrEmpty(obj.mediumCondition) ? "-" : obj.mediumCondition;
            temperature.Text = string.IsNullOrEmpty(obj.temperatureCondition) ? "-" : obj.temperatureCondition;
            lightIntensity.Text = string.IsNullOrEmpty(obj.lightIntensityCondition) ? "-" : obj.lightIntensityCondition;
            duration.Text = string.IsNullOrEmpty(obj.durationCondition) ? "-" : obj.durationCondition;

            cboConditionalStrain.Text = await osbll.getListIdConditionalStrainByMedium(obj.IdConditionStrain);
            
            toxin.Text = string.IsNullOrEmpty(obj.ToxinProducer) ? "-" : obj.ToxinProducer;
            stateStrain.Text = string.IsNullOrEmpty(obj.StateOfStrain) ? "-" : obj.StateOfStrain;
            agitation.Text = string.IsNullOrEmpty(obj.AgitationResistance) ? "-" : obj.AgitationResistance;
            remarks.Text = string.IsNullOrEmpty(obj.Remarks) ? "-" : obj.Remarks;
            gen.Text = string.IsNullOrEmpty(obj.GeneInformation) ? "-" : obj.GeneInformation;
            publication.Text = string.IsNullOrEmpty(obj.Publications) ? "-" : obj.Publications;
            recommend.Text = string.IsNullOrEmpty(obj.RecommendedForTeaching) ? "-" : obj.RecommendedForTeaching;
            status.Text = string.IsNullOrEmpty(obj.Status) ? "-" : obj.Status;
            dateAdd.Text = string.IsNullOrEmpty(obj.DateAdd) ? "-" : obj.DateAdd;

            fileImage = obj.ImageStrain;

            string str = await asbll.GetNameAndYearIsolator(idSelected);
            identify.Text = str;
            isolator.Text = str;

            if (selectedRow.Cells[4].Value == null ||
                string.IsNullOrEmpty(selectedRow.Cells[4].Value.ToString()) ||
                selectedRow.Cells[4].Value.ToString().Equals("Nhân viên đã xem yêu cầu"))
            {
                panel4.Visible = false;
                txtNote.Text = string.Empty;
            }
            else
            {
                panel4.Visible = true;
                txtNote.Text = selectedRow.Cells[4].Value.ToString();
                string dateTime = null;
                var newStrainHistory = new
                {
                    status = "Đang chờ xét duyệt",
                    dateApproval = dateTime,
                    reason = "Nhân viên đã xem yêu cầu",
                };
                string strainHistoryJson = JsonSerializer.Serialize(newStrainHistory);
                string result = await asbll.UpdateDataStrainHistory(idSelected, strainHistoryJson);
            }

            btnThemhinh.Visible = true;
            btnXoahinh.Visible = true;
        }

        private async void btnThemhinh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (idSelected != 0)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Chọn hình ảnh";
                ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileImage = null;
                    fileImage = File.ReadAllBytes(ofd.FileName);
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    string rs = await asbll.UpdateImageStrain(idSelected, fileImage);
                    if (rs != null)
                    {
                        MessageBox.Show("Hình đã được lưu vào database", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Lưu hình thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Bạn chưa chọn strain nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void btnXoahinh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (idSelected != 0)
            {
                fileImage = null;
                string rs = await asbll.UpdateImageStrain(idSelected, fileImage);
                if (rs != null)
                {
                    MessageBox.Show("Hình đã xóa khỏi database", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pictureBox1.Image = null;
                }
                else
                {
                    MessageBox.Show("Xóa hình thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Bạn chưa chọn strain nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            await UpdateStrain();
        }

        private async Task UpdateStrain()
        {
            if (idSelected != 0)
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

                string strainNumberNull = null;
                int idSpeciesValue = await osbll.GetIdSpeciesByName(cboSpecies.Text.Trim());
                int idConditionValue = 0;
                string firstPart = cboConditionalStrain.Text.Split(',')[0];
                Match match = Regex.Match(firstPart, @"\d+");
                if (match.Success)
                {
                    idConditionValue = int.Parse(match.Value);
                }

                var newStrain = new
                {
                    strainNumber = strainNumberNull,
                    idSpecies = idSpeciesValue,
                    idCondition = idConditionValue,
                    imageStrain = fileImage,
                    scientificName = scientificName.Text.Trim(),
                    synonymStrain = synonym.Text.Trim(),
                    formerName = formerName.Text.Trim(),
                    commonName = commonName.Text.Trim(),
                    cellSize = cellSize.Text.Trim(),
                    organization = organization.Text.Trim(),
                    characteristics = characteristic.Text.Trim(),
                    collectionSite = collectionSite.Text.Trim(),
                    continent = continent.Text.Trim(),
                    country = country.Text.Trim(),
                    isolationSource = environment.Text.Trim(),
                    toxinProducer = toxin.Text.Trim(),
                    stateOfStrain = stateStrain.Text.Trim(),
                    agitationResistance = agitation.Text.Trim(),
                    remarks = remarks.Text.Trim(),
                    geneInformation = gen.Text.Trim(),
                    publications = publication.Text.Trim(),
                    recommendedForTeaching = recommend.Text.Trim(),
                    dateAdd = DateTime.Parse(dateAdd.Text.Trim()).ToString("yyyy-MM-dd"),
                };

                string strainJson = JsonSerializer.Serialize(newStrain);

                try
                {
                    string result = await osbll.UpdateStrain(idSelected, strainJson);
                    if (result != null)
                    {
                        MessageBox.Show("Cập nhật strain thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFiled();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật strain thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn strain nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cboConditionalStrain_SelectedIndexChanged(object sender, EventArgs e)
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

        private void ResetFiled()
        {
            idSelected = 0;
            strainNumber.Text = string.Empty;
            scientificName.Text = string.Empty;
            synonym.Text = string.Empty;
            formerName.Text = string.Empty;
            commonName.Text = string.Empty;
            cellSize.Text = string.Empty;
            organization.Text = string.Empty;
            characteristic.Text = string.Empty;
            collectionSite.Text = string.Empty;
            continent.Text = string.Empty;
            country.Text = string.Empty;
            environment.Text = string.Empty;
            isolator.Text = string.Empty;
            toxin.Text = string.Empty;
            stateStrain.Text = string.Empty;
            agitation.Text = string.Empty;
            remarks.Text = string.Empty;
            gen.Text = string.Empty;
            publication.Text = string.Empty;
            recommend.Text = string.Empty;
            status.Text = string.Empty;
            identify.Text = string.Empty;

            dateAdd.Text = string.Empty;
            cboConditionalStrain.SelectedIndex = 0;
            cboSpecies.SelectedIndex = 0;
            recommend.SelectedIndex = 0;
            pictureBox1.Image = null;

            btnThemhinh.Visible = false;
            btnXoahinh.Visible = false;

            LoadDataGridView();
        }
    }
}
