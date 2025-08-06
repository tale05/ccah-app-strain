using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmApprovalStrain : Form
    {
        FormApprovalStrainBLL asbll;
        StrainBLL sbll;
        private static int idSelected = 0;
        public frmApprovalStrain()
        {
            InitializeComponent();
            asbll = new FormApprovalStrainBLL();
            sbll = new StrainBLL();
            LoadDataGridView();
            LoadDataListStrain();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }

        public async void LoadDataListStrain()
        {
            List<ListStrainDTO> lst = new List<ListStrainDTO>();
            lst = await asbll.GetListStrainNumber();
            tblListStrainnumber.DataSource = lst;
        }

        public async void LoadDataGridView()
        {
            tblStrainHistory.DataSource = null;
            tblStrainHistory.Rows.Clear();
            tblStrainHistory.Columns.Clear();

            List<ApiStrainApprovalHistoryDTO> lst = await asbll.LoadData();
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

        private async void btnDuyetSanPham_Click(object sender, EventArgs e)
        {
            string message = "";

            if (idSelected == 0 || string.IsNullOrEmpty(txtStrainNumber.Text))
            {
                message += "Bạn chưa chọn sản phẩm nào để duyệt hoặc chưa điền mã strain\n";
            }
            if (string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtQuanity.Text))
            {
                message += "Bạn chưa điền đủ thông tin giá và số lượng sản phẩm\n";
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật mã strain và trạng thái không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    string resultStrain = await sbll.UpdateStrainNumber(idSelected, $"CCAH-{txtStrainNumber.Text}");
                    if (resultStrain != null)
                    {
                        message += "Cập nhật mã thành công cho strain\n";
                    }
                    else
                    {
                        message += "Cập nhật mã thất bại cho strain\n";
                    }

                    string newreason = null;
                    var newStrainHistory = new
                    {
                        status = "Đã được duyệt",
                        dateApproval = DateTime.Now.ToString("yyyy-MM-dd"),
                        reason = newreason,
                    };
                    string strainHistoryJson = JsonSerializer.Serialize(newStrainHistory);
                    string result = await asbll.UpdateDataStrainHistory(idSelected, strainHistoryJson);
                    if (result != null)
                    {
                        message += "Cập nhật trạng thái strain thành công\n";
                    }
                    else
                    {
                        message += "Cập nhật trạng thái strain thất bại\n";
                    }

                    var newInventory = new
                    {
                        idStrain = idSelected,
                        quantity = txtQuanity.Text,
                        price = txtPrice.Text,
                        entryDate = DateTime.Now.ToString("yyyy-MM-dd"),
                        histories = $"{DateTime.Now.ToString("dd/MM/yyyy")} - SL: {txtQuanity.Text} - Giá: {txtPrice.Text}\n",
                    };
                    string inventoryJson = JsonSerializer.Serialize(newInventory);
                    string rs = await asbll.AddDataInventory(inventoryJson);
                    if (rs != null)
                    {
                        message += "Cập nhật số lượng sản phẩm thành công\n";
                    }
                    else
                    {
                        message += "Cập nhật số lượng sản phẩm thất bại\n";
                    }
                    Reset();
                }
                else
                {
                    message = "Hủy cập nhật";
                }
            }
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Reset();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
            Visible = false;
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
            phylum.Text = string.IsNullOrEmpty(obj.namePhylum) ? "-" : obj.namePhylum;
            classes.Text = string.IsNullOrEmpty(obj.nameClass) ? "-" : obj.nameClass;
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
            toxin.Text = string.IsNullOrEmpty(obj.ToxinProducer) ? "-" : obj.ToxinProducer;
            stateStrain.Text = string.IsNullOrEmpty(obj.StateOfStrain) ? "-" : obj.StateOfStrain;
            agitation.Text = string.IsNullOrEmpty(obj.AgitationResistance) ? "-" : obj.AgitationResistance;
            remarks.Text = string.IsNullOrEmpty(obj.Remarks) ? "-" : obj.Remarks;
            gen.Text = string.IsNullOrEmpty(obj.GeneInformation) ? "-" : obj.GeneInformation;
            publication.Text = string.IsNullOrEmpty(obj.Publications) ? "-" : obj.Publications;
            recommend.Text = string.IsNullOrEmpty(obj.RecommendedForTeaching) ? "-" : obj.RecommendedForTeaching;
            status.Text = string.IsNullOrEmpty(obj.Status) ? "-" : obj.Status;
            dateAdd.Text = string.IsNullOrEmpty(obj.DateAdd) ? "-" : obj.DateAdd;

            string str = await asbll.GetNameAndYearIsolator(idSelected);
            identify.Text = str;
            isolator.Text = str;

            if (selectedRow.Cells[4].Value == null)
            {
                label6.Visible = false;
                notification.Visible = false;
            }
            else
            {
                label6.Visible = true;
                notification.Visible = true;
                notification.Text = selectedRow.Cells[4].Value.ToString();
            }    
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            guna2GradientPanel1.Visible = false;
            btnHienthixacnhan.Visible = true;
            btnTuChoi.Visible = false;
            Reset();
        }
        private void txtStrainNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtStrainNumber.Text != string.Empty)
            {
                btnDuyetSanPham.Enabled = true;
                btnTuChoi.Enabled = false;
            }
            else
            {
                btnDuyetSanPham.Enabled = false;
                btnTuChoi.Enabled = true;
            }
        }

        private async void btnTuChoi_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn từ chối duyệt strain này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                string dateTime = null;
                var newStrainHistory = new
                {
                    status = "Đang chờ xét duyệt",
                    dateApproval = dateTime,
                    reason = txtNote.Text,
                };
                string strainHistoryJson = JsonSerializer.Serialize(newStrainHistory);
                string result = await asbll.UpdateDataStrainHistory(idSelected, strainHistoryJson);
                if (result != null)
                {
                    MessageBox.Show("Từ chối duyệt thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    guna2GradientPanel1.Visible = false;
                    btnHienthixacnhan.Visible = true;
                    btnTuChoi.Visible = false;
                    Reset();
                }
                else
                {
                    MessageBox.Show("Từ chối duyệt thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Hủy từ chối duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmApprovalStrain_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
            Hide();
        }

        private async void Reset()
        {
            LoadDataGridView();
            LoadDataListStrain();
            idSelected = 0;
            txtStrainNumber.Text = string.Empty;
            string str = await asbll.GetNextStrainNumber();
            string strSubstring = str.Substring(5);
            txtStrainNumber.Text = strSubstring;
            txtPrice.Text = string.Empty;
            txtQuanity.Text = string.Empty;

            strainNumber.Text = string.Empty;
            phylum.Text = string.Empty;
            classes.Text = string.Empty;
            scientificName.Text = string.Empty;
            synonym.Text = string.Empty;
            formerName.Text = string.Empty;
            commonName.Text = string.Empty;
            cellSize.Text = string.Empty;
            organization.Text = string.Empty;
            collectionSite.Text = string.Empty;
            continent.Text = string.Empty;
            country.Text = string.Empty;
            environment.Text = string.Empty;
            medium.Text = string.Empty;
            temperature.Text = string.Empty;
            lightIntensity.Text = string.Empty;
            duration.Text = string.Empty;
            toxin.Text = string.Empty;
            stateStrain.Text = string.Empty;
            agitation.Text = string.Empty;
            remarks.Text = string.Empty;
            gen.Text = string.Empty;
            publication.Text = string.Empty;
            recommend.Text = string.Empty;
            status.Text = string.Empty;
            dateAdd.Text = string.Empty;

            identify.Text = string.Empty;
            isolator.Text = string.Empty;
            pictureBox1.Image = null;
        }

        private async void frmApprovalStrain_Load(object sender, EventArgs e)
        {
            //labelHint.Text = $"Gợi ý mã strain tiếp theo là: {await asbll.GetNextStrainNumber()}";
            string str = await asbll.GetNextStrainNumber();
            string strSubstring = str.Substring(5);
            txtStrainNumber.Text = strSubstring;
        }

        private void txtStrainNumber_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyData & Keys.KeyCode;

            if (!char.IsDigit((char)key) &&
                key != Keys.Divide &&
                key != Keys.OemQuestion &&
                key != Keys.Left &&
                key != Keys.Right &&
                key != Keys.Back &&
                key != Keys.Delete &&
                key != Keys.Space)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void btnHienthixacnhan_Click(object sender, EventArgs e)
        {
            if (idSelected != 0)
            {
                guna2GradientPanel1.Visible = true;
                btnHienthixacnhan.Visible = false;
                btnTuChoi.Visible = true;
            }
            else
                MessageBox.Show("Bạn chưa chọn sản phẩm nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtQuanity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
