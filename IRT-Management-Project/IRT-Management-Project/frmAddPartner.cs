using API;
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
using System.Windows.Documents;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Helpers.GraphicsHelper;

namespace IRT_Management_Project
{
    public partial class frmAddPartner : Form
    {
        private FormListPartnerBLL pbll;
        private ProvincesBLL provincesBLL;
        private static int idPartner;
        private static string nameDistrict = string.Empty, nameWard = string.Empty;
        public frmAddPartner()
        {
            InitializeComponent();
            pbll = new FormListPartnerBLL();
            provincesBLL = new ProvincesBLL();
            idPartner = 0;
            LoadDataGridView();
            LoadCboTinhThanhPho();
            LoadCboQuanHuyen();
            LoadCboPhuongXa();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
        private async void LoadDataGridView()
        {
            List<PartnerDTO> data = await pbll.GetDataFullProperties();
            InsertDataAndDesignTable(data);
        }
        private async void SearchData(string search)
        {
            List<PartnerDTO> data = await pbll.SearchData(search);
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
            tblPartner.Columns["namePartner"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblPartner.Columns["phoneNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblPartner.Columns["bankNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblPartner.Columns["bankName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblPartner.Columns["qhnsNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tblPartner.Columns["nameCompany"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tblPartner.Columns["addressCompany"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
        private async void LoadCboTinhThanhPho()
        {
            try
            {
                cboTinh.Items.Add("Chọn--");
                List<string> lst = await provincesBLL.GetDataProvinces();
                if (lst == null)
                {
                    MessageBox.Show("Lỗi API không thể lấy dữ liệu các tỉnh thành phố");
                    return;
                }
                else
                {
                    foreach (string item in lst)
                    {
                        cboTinh.Items.Add(item.Trim());
                    }
                    cboTinh.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi API không thể lấy dữ liệu các tỉnh thành phố \n" + ex.Message);
                return;
            }
        }
        private void LoadCboQuanHuyen()
        {
            cboQuan.Items.Add("Chọn--");
        }

        private void LoadCboPhuongXa()
        {
            cboPhuong.Items.Add("Chọn--");
        }

        private async void cboTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTinh.SelectedIndex == 0)
            {
                cboQuan.Items.Clear();
                cboQuan.Items.Add("Chọn--");
                cboQuan.SelectedIndex = 0;
            }
            else
            {
                cboQuan.Items.Clear();
                cboQuan.Items.Add("Chọn--");
                cboQuan.SelectedIndex = 0;
                foreach (string item in await provincesBLL.GetDataDistrict_FromIdProvinces(await provincesBLL.GetIdProvincesByName(cboTinh.Text.Trim())))
                {
                    cboQuan.Items.Add(item.Trim());
                }
            }
        }

        private async void cboQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQuan.SelectedIndex == 0)
            {
                cboPhuong.Items.Clear();
                cboPhuong.Items.Add("Chọn--");
                cboPhuong.SelectedIndex = 0;
            }
            else
            {
                cboPhuong.Items.Clear();
                cboPhuong.Items.Add("Chọn--");
                cboPhuong.SelectedIndex = 0;
                int idDistrict = await provincesBLL.GetIdDistrictByName(cboQuan.Text.Trim());
                foreach (string item in await provincesBLL.GetDataWard_FromDistrictAndProvinces(idDistrict))
                {
                    cboPhuong.Items.Add(item);
                }
            }
        }

        private string SubStringInputAddress(string input)
        {
            int commaIndex = input.IndexOf(',');
            if (commaIndex != -1)
            {
                return input.Substring(0, commaIndex).Trim();
            }
            else
            {
                return input;
            }
        }

        private async void tblPartner_Click(object sender, EventArgs e)
        {
            if (tblPartner.CurrentRow != null)
            {
                int i = tblPartner.CurrentRow.Index;
                if (i >= 0 && i < tblPartner.Rows.Count)
                {
                    idPartner = int.Parse(tblPartner.Rows[i].Cells["idPartner"].Value.ToString());
                    Name_Company.Text = tblPartner.Rows[i].Cells["nameCompany"].Value.ToString();
                    Address_Company.Text = SubStringInputAddress(tblPartner.Rows[i].Cells["addressCompany"].Value.ToString());
                    Name_Partner.Text = tblPartner.Rows[i].Cells["namePartner"].Value.ToString();
                    Position.Text = tblPartner.Rows[i].Cells["position"].Value.ToString();
                    Phone_Number.Text = tblPartner.Rows[i].Cells["phoneNumber"].Value.ToString();
                    Bank_Number.Text = tblPartner.Rows[i].Cells["bankNumber"].Value.ToString();
                    Bank_Name.Text = tblPartner.Rows[i].Cells["bankName"].Value.ToString();
                    QHNS_Number.Text = tblPartner.Rows[i].Cells["qhnsNumber"].Value.ToString();

                    string province = tblPartner.Rows[i].Cells["nameProvince"].Value.ToString();
                    string district = tblPartner.Rows[i].Cells["nameDistrict"].Value.ToString();
                    string ward = tblPartner.Rows[i].Cells["nameWard"].Value.ToString();

                    await SetComboBoxSelections(province, district, ward);
                }
            }
            else
            {
                MessageBox.Show("Không có hàng nào được chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SetComboBoxSelections(string province, string district, string ward)
        {
            cboTinh.SelectedIndexChanged -= cboTinh_SelectedIndexChanged;
            cboQuan.SelectedIndexChanged -= cboQuan_SelectedIndexChanged;

            cboTinh.Text = province;

            cboQuan.Items.Clear();
            cboQuan.Items.Add("Chọn--");
            int provinceId = await provincesBLL.GetIdProvincesByName(province);
            List<string> districts = await provincesBLL.GetDataDistrict_FromIdProvinces(provinceId);
            foreach (string item in districts)
            {
                cboQuan.Items.Add(item.Trim());
            }
            cboQuan.Text = district;

            cboPhuong.Items.Clear();
            cboPhuong.Items.Add("Chọn--");
            int districtId = await provincesBLL.GetIdDistrictByName(district);
            List<string> wards = await provincesBLL.GetDataWard_FromDistrictAndProvinces(districtId);
            foreach (string item in wards)
            {
                cboPhuong.Items.Add(item.Trim());
            }
            cboPhuong.Text = ward;

            cboTinh.SelectedIndexChanged += cboTinh_SelectedIndexChanged;
            cboQuan.SelectedIndexChanged += cboQuan_SelectedIndexChanged;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(Name_Company.Text) ||
                string.IsNullOrEmpty(Address_Company.Text) ||
                string.IsNullOrEmpty(Name_Partner.Text) ||
                string.IsNullOrEmpty(Position.Text) ||
                string.IsNullOrEmpty(Phone_Number.Text) ||
                string.IsNullOrEmpty(Bank_Number.Text) ||
                string.IsNullOrEmpty(Bank_Name.Text) ||
                string.IsNullOrEmpty(QHNS_Number.Text))
            {
                return false;
            }

            if (cboTinh.Text == "Chọn--" ||
                cboQuan.Text == "Chọn--" ||
                cboPhuong.Text == "Chọn--")
            {
                return false;
            }

            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm thông tin đối tác này?", "Xác nhận thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    var newPartner = new
                    {
                        nameCompany = Name_Company.Text,
                        addressCompany = Address_Company.Text,
                        namePartner = Name_Partner.Text,
                        position = Position.Text,
                        phoneNumber = Phone_Number.Text,
                        bankNumber = Bank_Number.Text,
                        bankName = Bank_Name.Text,
                        qhnsNumber = QHNS_Number.Text,
                        nameWard = cboPhuong.Text,
                        nameDistrict = cboQuan.Text,
                        nameProvince = cboTinh.Text,
                    };
                    string json = JsonSerializer.Serialize(newPartner);
                    string result = await pbll.Post(json);
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
                MessageBox.Show("Phải nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string result = await pbll.Delete(idPartner);

                if (result == "Deleted")
                {
                    MessageBox.Show("Xóa dữ liệu thành công", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    Reset();
                }
                else
                {
                    MessageBox.Show("Dữ liệu đang được sử dụng. Không thể xóa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            Name_Company.Text = string.Empty;
            Address_Company.Text = string.Empty;
            Name_Partner.Text = string.Empty;
            Position.Text = string.Empty;
            Phone_Number.Text = string.Empty;
            Bank_Number.Text = string.Empty;
            Bank_Name.Text = string.Empty;
            QHNS_Number.Text = string.Empty;

            cboTinh.SelectedIndex = 0;
            cboQuan.Items.Clear();
            cboQuan.Items.Add("Chọn--");
            cboQuan.SelectedIndex = 0;

            cboPhuong.Items.Clear();
            cboPhuong.Items.Add("Chọn--");
            cboPhuong.SelectedIndex = 0;

            idPartner = 0;

            LoadDataGridView();
        }

        private bool IsAllDigits(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void Phone_Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                if (Phone_Number != null && Phone_Number.Text.Length >= 10 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (Phone_Number.Text.Length == 0 && e.KeyChar != '0')
                {
                    e.Handled = true;
                    MessageBox.Show("Chưa nhập đúng định dạng số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Phone_Number_Leave(object sender, EventArgs e)
        {
            if (Phone_Number.Text.Length != 10)
            {
                MessageBox.Show($"Số điện thoại phải là 12 số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Phone_Number.Focus();
            }
        }

        private void Bank_Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Hide();
            frmMain1 frm = new frmMain1();
            frm.Show();
        }

        private void frmAddPartner_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
            Hide();
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
            if (ValidateInputs())
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật thông tin đối tác này?", "Xác nhận cập nhật", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    var newPartner = new
                    {
                        nameCompany = Name_Company.Text,
                        addressCompany = Address_Company.Text,
                        namePartner = Name_Partner.Text,
                        position = Position.Text,
                        phoneNumber = Phone_Number.Text,
                        bankNumber = Bank_Number.Text,
                        bankName = Bank_Name.Text,
                        qhnsNumber = QHNS_Number.Text,
                        nameWard = cboPhuong.Text,
                        nameDistrict = cboQuan.Text,
                        nameProvince = cboTinh.Text,
                    };
                    string json = JsonSerializer.Serialize(newPartner);
                    string result = await pbll.Update(idPartner, json);
                    if (result != null)
                    {
                        MessageBox.Show("Cập nhật dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridView();
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật dữ liệu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Phải nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
