using API;
using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Helpers.GraphicsHelper;
using System.IO;
using System.Globalization;

namespace IRT_Management_Project
{
    public partial class frmEmployeeList : Form
    {
        private FormAddEmployee ebll;
        private ProvincesBLL provincesBLL;
        private static string IdEmployee;
        private static string nameDistrict = string.Empty, nameWard = string.Empty;
        private static byte[] fileImage = null;

        public frmEmployeeList()
        {
            InitializeComponent();
            ebll = new FormAddEmployee();
            provincesBLL = new ProvincesBLL();
            DesignForm();
        }

        private void DesignForm()
        {
            LoadCboVaiTro();
            LoadCboTinhThanhPho();
            LoadCboQuanHuyen();
            LoadCboPhuongXa();
        }

        private async void SearchData(string search)
        {
            List<EmployeeCustomDTO2> data = await ebll.SearchData(search);
            InsertDataAndDesignTable(data);
        }

        private async void LoadCboVaiTro()
        {
            cboVaiTro.Items.Add("Chọn--");
            foreach (string item in await ebll.GetListNameRole())
            {
                cboVaiTro.Items.Add(item);
            }
            cboVaiTro.SelectedIndex = 0;
            cboBangCap.SelectedIndex = 0;
            cboGioiTinh.SelectedIndex = 0;
        }

        private async Task LoadDataGridView()
        {
            List<EmployeeCustomDTO2> data = new List<EmployeeCustomDTO2>();
            data = await ebll.LoadData();
            InsertDataAndDesignTable(data);
        }

        private void InsertDataAndDesignTable(List<EmployeeCustomDTO2> lstInput)
        {
            tblEmployee.DataSource = null;
            tblEmployee.Rows.Clear();
            tblEmployee.Columns.Clear();
            tblEmployee.AutoGenerateColumns = false;

            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                HeaderText = "Username",
                DataPropertyName = "Username"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdEmployee",
                HeaderText = "Mã nhân viên",
                DataPropertyName = "IdEmployee"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NameRole",
                HeaderText = "Vai trò",
                DataPropertyName = "NameRole"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                HeaderText = "Họ và tên",
                DataPropertyName = "FullName"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdCard",
                HeaderText = "Cccd",
                DataPropertyName = "IdCard"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DateOfBirth",
                HeaderText = "Ngày sinh",
                DataPropertyName = "DateOfBirth"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Gender",
                HeaderText = "Giới tính",
                DataPropertyName = "Gender"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PhoneNumber",
                HeaderText = "Số điện thoại",
                DataPropertyName = "PhoneNumber"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Degree",
                HeaderText = "Bằng cấp",
                DataPropertyName = "Degree"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Địa chỉ",
                DataPropertyName = "Address"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "JoinDate",
                HeaderText = "Ngày tham gia",
                DataPropertyName = "JoinDate"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameWard",
                HeaderText = "nameWard",
                DataPropertyName = "nameWard"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameDistrict",
                HeaderText = "nameDistrict",
                DataPropertyName = "nameDistrict"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameProvince",
                HeaderText = "nameProvince",
                DataPropertyName = "nameProvince"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FirstName",
                HeaderText = "FirstName",
                DataPropertyName = "FirstName"
            });
            tblEmployee.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastName",
                HeaderText = "LastName",
                DataPropertyName = "LastName"
            });
            tblEmployee.DataSource = lstInput;

            tblEmployee.Columns["nameWard"].Visible = false;
            tblEmployee.Columns["nameDistrict"].Visible = false;
            tblEmployee.Columns["nameProvince"].Visible = false;
            tblEmployee.Columns["LastName"].Visible = false;
            tblEmployee.Columns["FirstName"].Visible = false;
            tblEmployee.Columns["Username"].Visible = false;
            tblEmployee.Columns["Status"].Visible = false;

            //tblEmployee.Columns["FullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //tblEmployee.Columns["Gender"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //tblEmployee.Columns["DateOfBirth"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //tblEmployee.Columns["NameRole"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //tblEmployee.Columns["IdCard"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //tblEmployee.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //tblEmployee.Columns["PhoneNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //tblEmployee.Columns["JoinDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //tblEmployee.Columns["Degree"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tblEmployee.Columns["Address"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
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

        private bool IsPhoneNumberValid()
        {
            if (txt_sdt.Text.Length == 0 || txt_sdt.Text[0] != '0')
            {
                MessageBox.Show("Chưa nhập đúng định dạng số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txt_sdt.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có đúng 10 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void txtSdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                if (txt_sdt != null && txt_sdt.Text.Length >= 10 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (txt_sdt.Text.Length == 0 && e.KeyChar != '0')
                {
                    e.Handled = true;
                    MessageBox.Show("Chưa nhập đúng định dạng số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool IsCccdValid()
        {
            if (txt_cccd.Text.Length != 12)
            {
                MessageBox.Show("CCCD phải có đúng 12 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void txtCccd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                if (txt_cccd != null && txt_cccd.Text.Length >= 12 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private bool IsEmailValid()
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(txt_email.Text))
            {
                MessageBox.Show("Địa chỉ email không hợp lệ. Vui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (!IsEmailValid())
            {
                txt_email.Focus();
            }
        }

        private async void frmEmployeeList_Load(object sender, EventArgs e)
        {
            await LoadDataGridView();
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
            if (string.IsNullOrEmpty(txt_ho.Text) ||
                string.IsNullOrEmpty(txt_ten.Text) ||
                string.IsNullOrEmpty(txt_cccd.Text) ||
                string.IsNullOrEmpty(txt_diachi.Text) ||
                string.IsNullOrEmpty(txt_email.Text) ||
                string.IsNullOrEmpty(txt_sdt.Text))
            {
                return false;
            }

            if (cboGioiTinh.Text == "Chọn--" ||
                cboVaiTro.Text == "Chọn--" ||
                cboBangCap.Text == "Chọn--" ||
                cboTinh.Text == "Chọn--" ||
                cboQuan.Text == "Chọn--" ||
                cboPhuong.Text == "Chọn--")
            {
                return false;
            }

            return true;
        }

        private string CreateUsername(string ho, string ten)
        {
            ho = RemoveDiacritics(ho);
            ten = RemoveDiacritics(ten);

            string initials = string.Concat(ho.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(word => word[0].ToString().ToLower()));

            string username = ten.ToLower() + initials;

            return username;
        }

        private string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {

            if (!IsPhoneNumberValid() || !IsCccdValid() || !IsEmailValid())
            {
                return;
            }

            int idRole = await ebll.GetIdRoleByName(cboVaiTro.Text.Trim());

            if (ValidateInputs())
            {
                DateTime dateOfBirth = txtDateTime.Value;
                int age = DateTime.Now.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Now.AddYears(-age)) age--;
                if (age < 18)
                {
                    MessageBox.Show("Người dùng phải từ 18 tuổi trở lên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm thông tin nhân viên này?", "Xác nhận thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    var newEmpoyee = new
                    {
                        idRole = idRole,
                        firstName = txt_ten.Text,
                        lastName = txt_ho.Text,
                        fullName = $"{txt_ho.Text} {txt_ten.Text}",
                        idCard = txt_cccd.Text,
                        dateOfBirth = txtDateTime.Value.ToString("yyyy-MM-dd"),
                        gender = cboGioiTinh.Text,
                        email = txt_email.Text,
                        phoneNumber = txt_sdt.Text,
                        degree = cboBangCap.Text,
                        address = txt_diachi.Text,
                        joinDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"),
                        imageEmployee = fileImage,
                        nameWard = cboPhuong.Text,
                        nameDistrict = cboQuan.Text,
                        nameProvince = cboTinh.Text,
                        //username = CreateUsername(txt_ho.Text.Trim(), txt_ten.Text.Trim()),
                        username = user.Text,
                        password = "123",
                        status = "Đang hoạt động"
                    };

                    string json = JsonSerializer.Serialize(newEmpoyee);
                    string result = await ebll.AddData(json);
                    if (result != null)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataGridView();
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                bool result = await ebll.DeleteData(IdEmployee);

                if (result != false)
                {
                    MessageBox.Show("Xóa dữ liệu thành công", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataGridView();
                    Reset();
                }
                else
                {
                    MessageBox.Show("Dữ liệu đang được sử dụng. Không thể xóa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {

            if (!IsPhoneNumberValid() || !IsCccdValid() || !IsEmailValid())
            {
                return;
            }

            DateTime startDate = txtDateTime.Value;
            DateTime endDate = guna2DateTimePicker1.Value;
            int idRole = await ebll.GetIdRoleByName(cboVaiTro.Text.Trim());

            if (ValidateInputs())
            {
                // Kiểm tra tuổi
                DateTime dateOfBirth = txtDateTime.Value;
                int age = DateTime.Now.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Now.AddYears(-age)) age--; // Điều chỉnh tuổi nếu ngày sinh chưa tới
                if (age < 18)
                {
                    MessageBox.Show("Người dùng phải từ 18 tuổi trở lên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin nhân viên này?", "Xác nhận thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    var newEmpoyee = new
                    {
                        idRole = idRole,
                        firstName = txt_ten.Text,
                        lastName = txt_ho.Text,
                        fullName = $"{txt_ho.Text} {txt_ten.Text}",
                        idCard = txt_cccd.Text,
                        dateOfBirth = txtDateTime.Value.ToString("yyyy-MM-dd"),
                        gender = cboGioiTinh.Text,
                        email = txt_email.Text,
                        phoneNumber = txt_sdt.Text,
                        degree = cboBangCap.Text,
                        address = txt_diachi.Text,
                        joinDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"),
                        imageEmployee = fileImage,
                        nameWard = cboPhuong.Text,
                        nameDistrict = cboQuan.Text,
                        nameProvince = cboTinh.Text,
                        username = user.Text,
                        password = "123",
                        status = "Đang hoạt động"
                    };

                    string json = JsonSerializer.Serialize(newEmpoyee);
                    string result = await ebll.Update(IdEmployee, json);
                    if (result != null)
                    {
                        MessageBox.Show("Cập nhật dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataGridView();
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

        private async void tblEmployee_Click(object sender, EventArgs e)
        {
            if (tblEmployee.CurrentRow != null)
            {
                user.ReadOnly = true;
                cboVaiTro.Enabled = false;
                txtDateTime.Enabled = false;
                guna2DateTimePicker1.Enabled = false;
                int i = tblEmployee.CurrentRow.Index;
                if (i >= 0 && i < tblEmployee.Rows.Count)
                {
                    IdEmployee = tblEmployee.Rows[i].Cells["IdEmployee"].Value.ToString();
                    txt_ho.Text = tblEmployee.Rows[i].Cells["LastName"].Value.ToString();
                    txt_ten.Text = tblEmployee.Rows[i].Cells["FirstName"].Value.ToString();
                    cboGioiTinh.Text = tblEmployee.Rows[i].Cells["Gender"].Value.ToString();
                    cboVaiTro.Text = tblEmployee.Rows[i].Cells["NameRole"].Value.ToString();

                    txt_cccd.Text = tblEmployee.Rows[i].Cells["IdCard"].Value.ToString();
                    txt_email.Text = tblEmployee.Rows[i].Cells["Email"].Value.ToString();
                    txt_sdt.Text = tblEmployee.Rows[i].Cells["PhoneNumber"].Value.ToString();
                    cboBangCap.Text = tblEmployee.Rows[i].Cells["Degree"].Value.ToString();
                    txtDateTime.Value = DateTime.Parse(tblEmployee.Rows[i].Cells["DateOfBirth"].Value.ToString());
                    guna2DateTimePicker1.Value = DateTime.Parse(tblEmployee.Rows[i].Cells["JoinDate"].Value.ToString());

                    txt_diachi.Text = SubStringInputAddress(tblEmployee.Rows[i].Cells["Address"].Value.ToString());
                    string province = tblEmployee.Rows[i].Cells["nameProvince"].Value.ToString();
                    string district = tblEmployee.Rows[i].Cells["nameDistrict"].Value.ToString();
                    string ward = tblEmployee.Rows[i].Cells["nameWard"].Value.ToString();
                    await SetComboBoxSelections(province, district, ward);

                    user.Text = tblEmployee.Rows[i].Cells["Username"].Value.ToString();
                    trangthai.Text = tblEmployee.Rows[i].Cells["Status"].Value.ToString();
                    byte[] data = await ebll.GetImageIdEmployee(IdEmployee);

                    if (data != null)
                    {
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            picXemAnh.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        picXemAnh.Image = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có hàng nào được chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn hình ảnh";
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileImage = null;
                fileImage = File.ReadAllBytes(ofd.FileName);
                picXemAnh.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void Xoahinhanh_Click(object sender, EventArgs e)
        {
            fileImage = null;
            picXemAnh.Image = null;
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

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
            Hide();
        }

        private void frmEmployeeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
            Hide();
        }

        private void Reset()
        {
            txt_ho.Text = string.Empty;
            txt_ten.Text = string.Empty;
            txt_cccd.Text = string.Empty;
            txt_email.Text = string.Empty;
            txt_sdt.Text = string.Empty;
            txt_diachi.Text = string.Empty;
            txtDateTime.Value = DateTime.Now;
            guna2DateTimePicker1.Value = DateTime.Now;
            picXemAnh.Image = null;
            cboGioiTinh.SelectedIndex = 0;

            cboVaiTro.SelectedIndex = 0;

            cboBangCap.SelectedIndex = 0;

            cboTinh.SelectedIndex = 0;
            cboQuan.Items.Clear();
            cboQuan.Items.Add("Chọn--");
            cboQuan.SelectedIndex = 0;

            cboPhuong.Items.Clear();
            cboPhuong.Items.Add("Chọn--");
            cboPhuong.SelectedIndex = 0;

            //idPartner = 0;

            LoadDataGridView();

            user.ReadOnly = false;
            user.Text = string.Empty;
            cboVaiTro.Enabled = true;
            txtDateTime.Enabled = true;
            guna2DateTimePicker1.Enabled = true;
        }

    }
}
