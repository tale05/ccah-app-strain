using BLL;
using System.Text.RegularExpressions;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using DTO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace IRT_Management_Project
{
    public partial class frmAddEmployee : Form
    {
        EmployeeBLL ebll;
        ProvincesBLL pbll;
        private static byte[] fileImage = null;
        public frmAddEmployee()
        {
            InitializeComponent();
            ebll = new EmployeeBLL();
            pbll = new ProvincesBLL();
            DesignForm();
        }

        private void DesignForm()
        {
            LoadCboVaiTro();
            LoadCboTinhThanhPho();
            LoadCboQuanHuyen();
            LoadCboPhuongXa();
        }

        private async void LoadCboVaiTro()
        {
            cboVaitro.Items.Add("Chọn--");
            foreach (string item in await ebll.GetListNameRole())
            {
                cboVaitro.Items.Add(item);
            }
            cboVaitro.SelectedIndex = 0;
            cboTrinhdo.SelectedIndex = 0;
            cboGioitinh.SelectedIndex = 0;
        }

        private async void LoadCboTinhThanhPho()
        {
            try
            {
                cboTinh.Items.Add("Chọn--");
                List<string> lst = await pbll.GetDataProvinces();
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
                foreach (string item in await pbll.GetDataDistrict_FromIdProvinces(await pbll.GetIdProvincesByName(cboTinh.Text.Trim())))
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
                int idDistrict = await pbll.GetIdDistrictByName(cboQuan.Text.Trim());
                foreach (string item in await pbll.GetDataWard_FromDistrictAndProvinces(idDistrict))
                {
                    cboPhuong.Items.Add(item);
                }
            }
        }

        private void txtSdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                if (txtSdt != null && txtSdt.Text.Length >= 10 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (txtSdt.Text.Length == 0 && e.KeyChar != '0')
                {
                    e.Handled = true;
                    MessageBox.Show("Chưa nhập đúng định dạng số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtCccd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                if (txtCccd != null && txtCccd.Text.Length >= 12 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(txtEmail.Text))
            {
                MessageBox.Show("Địa chỉ email không hợp lệ. Vui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
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
                txtTenFileAnh.Text = ofd.SafeFileName;
                picXemAnh.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void txtSdt_Leave(object sender, EventArgs e)
        {
            if (txtSdt.Text.Length != 10)
            {
                MessageBox.Show($"Số điện thoại phải là 10 số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSdt.Focus();
            }
        }

        private void txtCccd_Leave(object sender, EventArgs e)
        {
            if (txtCccd.Text.Length != 12)
            {
                MessageBox.Show($"CCCD/CMND phải là 12 số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCccd.Focus();
            }
        }

        private async void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                string RemoveDiacritics(string text)
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

                if (string.IsNullOrWhiteSpace(txtTennv.Text) ||
                    string.IsNullOrWhiteSpace(txtHonv.Text) ||
                    string.IsNullOrWhiteSpace(txtCccd.Text) ||
                    cboVaitro.Text.Equals("Chọn--") ||
                    cboGioitinh.Text.Equals("Chọn--") ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtSdt.Text) ||
                    cboTrinhdo.Text.Equals("Chọn--") ||
                    string.IsNullOrWhiteSpace(txtDiachi.Text) ||
                    cboPhuong.Text.Equals("Chọn--") ||
                    cboQuan.Text.Equals("Chọn--") ||
                    cboTinh.Text.Equals("Chọn--"))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime dateOfBirth = dateTimePicker1.Value;
                int age = DateTime.Now.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Now.AddYears(-age)) age--; // Điều chỉnh tuổi nếu ngày sinh chưa tới
                if (age < 18)
                {
                    MessageBox.Show("Người dùng phải từ 18 tuổi trở lên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string firstname = RemoveDiacritics(txtTennv.Text.Trim());
                string lastname = RemoveDiacritics(txtHonv.Text.Trim());
                string lowercaseFirstname = firstname.ToLower();
                string initials = string.Concat(lastname.Split(' ').Select(word => word[0])).ToLower();
                string finalUsser = lowercaseFirstname + initials;

                int idRole = await ebll.GetIdRoleByName(cboVaitro.Text.Trim());
                var newEmployee = new
                {
                    idRole = idRole,
                    firstName = txtTennv.Text.Trim(),
                    lastName = txtHonv.Text.Trim(),
                    fullName = txtHonv.Text + " " + txtTennv.Text,
                    idCard = txtCccd.Text.Trim(),
                    dateOfBirth = dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                    gender = cboGioitinh.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    phoneNumber = txtSdt.Text.Trim(),
                    degree = cboTrinhdo.Text.Trim(),
                    address = $"{txtDiachi.Text.Trim()}, {cboPhuong.Text.Trim()}, {cboQuan.Text.Trim()}, {cboTinh.Text.Trim()}",
                    joinDate = dateTimePicker2.Value.ToString("yyyy-MM-dd"),
                    imageEmployee = fileImage,
                    username = finalUsser,
                    password = finalUsser + "123",
                    status = "Đang hoạt động"
                };

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm thông tin này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    string employeeJson = JsonSerializer.Serialize(newEmployee);
                    string result = await ebll.AddData(employeeJson);
                    if (result != null)
                    {
                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtTennv.Text = "Nhập--";
                        txtHonv.Text = "Nhập--";
                        txtCccd.Text = "Nhập--";
                        cboVaitro.Text = "Chọn--";
                        cboGioitinh.Text = "Chọn--";
                        txtEmail.Text = "Nhập--";
                        txtSdt.Text = "Nhập--";
                        cboTrinhdo.Text = "Chọn--";
                        txtDiachi.Text = "Nhập--";
                        cboPhuong.Text = "Chọn--";
                        cboQuan.Text = "Chọn--";
                        cboTinh.Text = "Chọn--";
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker2.Value = DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            txtTennv.Text = "Nhập--";
            txtHonv.Text = "Nhập--";
            txtCccd.Text = "Nhập--";
            cboVaitro.Text = "Chọn--";
            cboGioitinh.Text = "Chọn--";
            txtEmail.Text = "Nhập--";
            txtSdt.Text = "Nhập--";
            cboTrinhdo.Text = "Chọn--";
            txtDiachi.Text = "Nhập--";
            cboPhuong.Text = "Chọn--";
            cboQuan.Text = "Chọn--";
            cboTinh.Text = "Chọn--";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmMain1 frmMain = new frmMain1();
            frmMain.Show();
            Hide();
        }
    }
}
