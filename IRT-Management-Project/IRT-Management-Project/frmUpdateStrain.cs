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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmUpdateStrain : Form
    {
        private static int idStrain;
        private static string strainNumberValue;
        private static byte[] imgValue = null;
        FormUpdateStrainBLL fusbll;
        public frmUpdateStrain()
        {
            InitializeComponent();
            fusbll = new FormUpdateStrainBLL();
            ApplyAlternatingRowColors(tableLayoutPanel2, Color.Honeydew, Color.White);
            LoadTable();
            LoadComboBoxSpecies();
            LoadComboBoxIdConditional();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
        // Thiết kế giao diện
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
        // Xử lý back-end
        public async void LoadTable()
        {
            tbl1.DataSource = null;
            tbl1.Rows.Clear();
            tbl1.Columns.Clear();
            List<ApiStrainDTO> list = new List<ApiStrainDTO>();
            list = await fusbll.GetData();
            tbl1.DataSource = list;

            tbl1.Columns[0].HeaderText = "STT";
            tbl1.Columns[1].HeaderText = "Mã số strain";
            tbl1.Columns[5].HeaderText = "Tên khoa học";
            tbl1.Columns[6].HeaderText = "Tên đồng danh";
            tbl1.Columns[7].HeaderText = "Tên ban đầu";
            tbl1.Columns[2].Visible = false;
            tbl1.Columns[3].Visible = false;
            tbl1.Columns[4].Visible = false;

            for (int i = 10; i <= 23; i++)
            {
                tbl1.Columns[i].Visible = false;
            }
        }
        private async void tbl1_Click(object sender, EventArgs e)
        {
            if (tbl1.CurrentRow != null)
            {
                int i = tbl1.CurrentRow.Index;
                idStrain = int.Parse(tbl1.Rows[i].Cells[0].Value.ToString());
                strainNumberValue = tbl1.Rows[i].Cells[1].Value.ToString();
                cboSpecies.Text = await fusbll.GetNameSpeciesById(int.Parse(tbl1.Rows[i].Cells[2].Value.ToString()));
                imgValue = await fusbll.GetDataImageById(int.Parse(tbl1.Rows[i].Cells[0].Value.ToString()));

                if (imgValue != null)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(imgValue))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi tải hình ảnh: {ex.Message}");
                    }
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.no_pictures;
                }
                txtScientificName.Text = tbl1.Rows[i].Cells["scientificName"].Value.ToString();
                txtSynonym.Text = tbl1.Rows[i].Cells["synonymStrain"].Value.ToString();
                txtFormerName.Text = tbl1.Rows[i].Cells["formerName"].Value.ToString();
                txtCommonName.Text = tbl1.Rows[i].Cells["commonName"].Value.ToString();
                txtCellSize.Text = tbl1.Rows[i].Cells["cellSize"].Value.ToString();
                txtOrganization.Text = tbl1.Rows[i].Cells["organization"].Value.ToString();
                txtCharacterist.Text = tbl1.Rows[i].Cells["characteristics"].Value.ToString();
                txtCollectionSite.Text = tbl1.Rows[i].Cells["collectionSite"].Value.ToString();
                txtContient.Text = tbl1.Rows[i].Cells["continent"].Value.ToString();
                txtCountry.Text = tbl1.Rows[i].Cells["country"].Value.ToString();
                txtIsolationSource.Text = tbl1.Rows[i].Cells["isolationSource"].Value.ToString();
                txtToxinProducer.Text = tbl1.Rows[i].Cells["toxinProducer"].Value.ToString();
                txtStateOfStrain.Text = tbl1.Rows[i].Cells["stateOfStrain"].Value.ToString();
                txtStateOfStrain.Text = tbl1.Rows[i].Cells["agitationResistance"].Value.ToString();
                txtRemark.Text = tbl1.Rows[i].Cells["remarks"].Value.ToString();
                txtGenInformation.Text = tbl1.Rows[i].Cells["geneInformation"].Value.ToString();
                txtPublication.Text = tbl1.Rows[i].Cells["publications"].Value.ToString();
                cboRecommended.Text = tbl1.Rows[i].Cells["recommendedForTeaching"].Value.ToString();

                ApiConditionalStrainDTO obj = null;
                obj = await fusbll.GetConditionByName(int.Parse(tbl1.Rows[i].Cells[3].Value.ToString()));
                cboConditionalStrain.Text = "Điều kiện loại: " + tbl1.Rows[i].Cells[3].Value.ToString();
                if (obj == null)
                {
                    medium.Text = "Không có dữ liệu";
                    temperature.Text = "Không có dữ liệu";
                    duration.Text = "Không có dữ liệu";
                    lightIntensity.Text = "Không có dữ liệu";
                }
                else
                {
                    medium.Text = obj.medium;
                    temperature.Text = obj.temperature;
                    duration.Text = obj.duration;
                    lightIntensity.Text = obj.lightIntensity;
                }    
            }
        }
        private async void LoadComboBoxSpecies()
        {
            cboSpecies.Items.Add("Chọn--");
            foreach (string item in await fusbll.getListSpecies())
            {
                cboSpecies.Items.Add(item);
            }
            cboSpecies.SelectedIndex = 0;
            cboSpecies.SelectedIndex = 0;
            cboSpecies.SelectedIndex = 0;
        }
        private async void LoadComboBoxIdConditional()
        {
            cboConditionalStrain.Items.Add("Chọn--");
            foreach (string item in await fusbll.getListIdConditionalStrain())
            {
                cboConditionalStrain.Items.Add("Điều kiện loại: " + item);
            }
            cboConditionalStrain.SelectedIndex = 0;
            cboConditionalStrain.SelectedIndex = 0;
            cboConditionalStrain.SelectedIndex = 0;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            int idSpeciesValue = await fusbll.GetIdSpeciesByName(cboSpecies.Text.Trim());
            int idConditionValue = 0;
            MatchCollection matches = Regex.Matches(cboConditionalStrain.Text, @"\d+");
            foreach (Match match in matches)
            {
                idConditionValue = int.Parse(match.Value);
            }
            var newStrain1 = new ApiStrainDTO
            {
                strainNumber = strainNumberValue.ToString(),
                idSpecies = idSpeciesValue,
                idCondition = idConditionValue,
                imageStrain = imgValue,
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
            };
            string strainJson = JsonSerializer.Serialize(newStrain1);
            string result = await fusbll.UpdateStrain(idStrain, strainJson);
            if (result != null)
            {
                MessageBox.Show("Cập nhật sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTable();
                ResetFields();
            }
            else
                MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            pictureBox1.Image = null;
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn hình ảnh";
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgValue = null;
                imgValue = File.ReadAllBytes(ofd.FileName);
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            imgValue = null;
            pictureBox1.Image = null;
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
            var obj = await fusbll.ViewDetailConditionalStrain(int.Parse(match.Value));
            medium.Text = obj.medium;
            temperature.Text = obj.temperature;
            lightIntensity.Text = obj.lightIntensity;
            duration.Text = obj.duration;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetFields();
            LoadTable();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dispose();
            frmMain1 frm = new frmMain1();
            frm.Show();
        }

        private void frmUpdateStrain_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
        }
    }
}
