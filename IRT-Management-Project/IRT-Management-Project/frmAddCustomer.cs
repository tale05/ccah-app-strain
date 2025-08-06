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
using API;
using BLL;
using DTO;

namespace IRT_Management_Project
{
    public partial class frmAddCustomer : Form
    {
        private FormAddCustomerBLL ctll;
        private ProvincesBLL provincesBLL;
        private static string IdCustomer, userNameCustomerValue, passwordCustomerValue;
        private static string nameDistrict = string.Empty, nameWard = string.Empty;
        private static byte[] fileImage = null;
        public frmAddCustomer()
        {
            InitializeComponent();
            ctll = new FormAddCustomerBLL();
            provincesBLL = new ProvincesBLL();
            DesignForm();
        }
        private void DesignForm()
        {
            LoadCboTinhThanhPho();
            LoadCboQuanHuyen();
            LoadCboPhuongXa();
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
        private async void SearchData(string search)
        {
            List<CustomerCustomDTO> data = await ctll.SearchData(search);
            InsertDataAndDesignTable(data);
        }
        private async Task LoadDataGridView()
        {
            List<CustomerCustomDTO> data = new List<CustomerCustomDTO>();
            data = await ctll.LoadData();
            InsertDataAndDesignTable(data);
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

        private void InsertDataAndDesignTable(List<CustomerCustomDTO> lstInput)
        {
            tblCustomer.DataSource = null;
            tblCustomer.Rows.Clear();
            tblCustomer.Columns.Clear();
            tblCustomer.AutoGenerateColumns = false;


            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdCustomer",
                HeaderText = "Mã khách hàng",
                DataPropertyName = "IdCustomer"
            });

            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                HeaderText = "Họ và tên",
                DataPropertyName = "FullName"
            });

            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DateOfBirth",
                HeaderText = "Ngày sinh",
                DataPropertyName = "DateOfBirth"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Gender",
                HeaderText = "Giới tính",
                DataPropertyName = "Gender"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PhoneNumber",
                HeaderText = "Số điện thoại",
                DataPropertyName = "PhoneNumber"
            });

            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Địa chỉ",
                DataPropertyName = "Address"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "JoinDate",
                HeaderText = " tham gia",
                DataPropertyName = "JoinDate"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "JoinDate",
                HeaderText = "gia",
                DataPropertyName = "JoinDate"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameWard",
                HeaderText = "nameWard",
                DataPropertyName = "nameWard"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameDistrict",
                HeaderText = "nameDistrict",
                DataPropertyName = "nameDistrict"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nameProvince",
                HeaderText = "nameProvince",
                DataPropertyName = "nameProvince"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FirstName",
                HeaderText = "FirstName",
                DataPropertyName = "FirstName"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastName",
                HeaderText = "LastName",
                DataPropertyName = "LastName"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                HeaderText = "Username",
                DataPropertyName = "Username"
            });
            tblCustomer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status"
            });
            tblCustomer.DataSource = lstInput;

            tblCustomer.Columns["nameWard"].Visible = false;
            tblCustomer.Columns["nameDistrict"].Visible = false;
            tblCustomer.Columns["nameProvince"].Visible = false;
            tblCustomer.Columns["LastName"].Visible = false;
            tblCustomer.Columns["FirstName"].Visible = false;
            tblCustomer.Columns["Username"].Visible = false;
            tblCustomer.Columns["Status"].Visible = false;


            tblCustomer.Columns["Address"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
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

        private async void tblCustomer_Click(object sender, EventArgs e)
        {
            if (tblCustomer.CurrentRow != null)
            {
                int i = tblCustomer.CurrentRow.Index;
                if (i >= 0 && i < tblCustomer.Rows.Count)
                {


                    IdCustomer = tblCustomer.Rows[i].Cells["IdCustomer"].Value.ToString();
                    txt_ho.Text = tblCustomer.Rows[i].Cells["LastName"].Value.ToString();
                    txt_ten.Text = tblCustomer.Rows[i].Cells["FirstName"].Value.ToString();
                    cboGioiTinh.Text = tblCustomer.Rows[i].Cells["Gender"].Value.ToString();

                    txt_email.Text = tblCustomer.Rows[i].Cells["Email"].Value.ToString();
                    var phoneNumber = tblCustomer.Rows[i].Cells["PhoneNumber"].Value?.ToString();
                    txt_sdt.Text = !string.IsNullOrWhiteSpace(phoneNumber) ? phoneNumber : "";

                    var dateOfBirthValue = tblCustomer.Rows[i].Cells["DateOfBirth"].Value?.ToString();
                    DateTime dateOfBirth;

                    if (!DateTime.TryParse(dateOfBirthValue, out dateOfBirth))
                    {
                        dateOfBirth = DateTime.Now;
                    }
                    guna2DateTimePicker1.Value = dateOfBirth;

                    txt_diachi.Text = SubStringInputAddress(tblCustomer.Rows[i].Cells["Address"].Value.ToString());
                    string province = tblCustomer.Rows[i].Cells["nameProvince"].Value.ToString();
                    string district = tblCustomer.Rows[i].Cells["nameDistrict"].Value.ToString();
                    string ward = tblCustomer.Rows[i].Cells["nameWard"].Value.ToString();
                    await SetComboBoxSelections(province, district, ward);

                    user.Text = tblCustomer.Rows[i].Cells["Username"].Value.ToString();
                    userNameCustomerValue = tblCustomer.Rows[i].Cells["Username"].Value.ToString();
                    trangthai.Text = tblCustomer.Rows[i].Cells["Status"].Value.ToString();
                    byte[] data = await ctll.GetImageIdCustomer(IdCustomer);

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
        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txt_ho.Text) ||
                string.IsNullOrEmpty(txt_ten.Text) ||
                string.IsNullOrEmpty(txt_diachi.Text) ||
                string.IsNullOrEmpty(txt_email.Text) ||
                string.IsNullOrEmpty(txt_sdt.Text))
            {
                return false;
            }

            if (cboGioiTinh.Text == "Chọn--" ||

                cboTinh.Text == "Chọn--" ||
                cboQuan.Text == "Chọn--" ||
                cboPhuong.Text == "Chọn--")
            {
                return false;
            }

            return true;
        }
        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!IsPhoneNumberValid() || !IsEmailValid())
            {
                return;
            }

            DateTime endDate = guna2DateTimePicker1.Value;


            if (ValidateInputs())
            {
                // Kiểm tra tuổi
                DateTime dateOfBirth = guna2DateTimePicker1.Value;
                int age = DateTime.Now.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Now.AddYears(-age)) age--; // Điều chỉnh tuổi nếu ngày sinh chưa tới
                if (age < 18)
                {
                    MessageBox.Show("Người dùng phải từ 18 tuổi trở lên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm thông tin nhân viên này?", "Xác nhận thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    var newEmpoyee = new CustomerToAddDTO
                    {
                        firstName = txt_ten.Text,
                        LastName = txt_ho.Text,
                        fullName = $"{txt_ho.Text} {txt_ten.Text}",
                        dateOfBirth = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"),
                        gender = cboGioiTinh.Text,
                        email = txt_email.Text,
                        phoneNumber = txt_sdt.Text,
                        address = txt_diachi.Text,
                        image = fileImage,
                        nameWard = cboPhuong.Text,
                        nameDistrict = cboQuan.Text,
                        nameProvince = cboTinh.Text,
                        username = txt_email.Text,
                        password = "123",
                        status = "Đang hoạt động"
                    };

                    string json = JsonSerializer.Serialize(newEmpoyee);
                    string result = await ctll.AddData(json);
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

        private async void frmAddCustomer_Load_1(object sender, EventArgs e)
        {
            await LoadDataGridView();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
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

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string result = await ctll.DeleteData(IdCustomer);

                if (result != null)
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
            if (!IsPhoneNumberValid() || !IsEmailValid())
            {
                return;
            }

            DateTime endDate = guna2DateTimePicker1.Value;


            if (ValidateInputs())
            {
                // Kiểm tra tuổi
                DateTime dateOfBirth = guna2DateTimePicker1.Value;
                int age = DateTime.Now.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Now.AddYears(-age)) age--; // Điều chỉnh tuổi nếu ngày sinh chưa tới
                if (age < 18)
                {
                    MessageBox.Show("Người dùng phải từ 18 tuổi trở lên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng này?", "Xác nhận thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    var newEmpoyee = new CustomerToUpdateDTO
                    {
                        firstName = txt_ten.Text,
                        LastName = txt_ho.Text,
                        fullName = $"{txt_ho.Text} {txt_ten.Text}",
                        dateOfBirth = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"),
                        gender = cboGioiTinh.Text,
                        email = txt_email.Text,
                        phoneNumber = txt_sdt.Text,
                        address = txt_diachi.Text,
                        image = fileImage,
                        nameWard = cboPhuong.Text,
                        nameDistrict = cboQuan.Text,
                        nameProvince = cboTinh.Text,
                        username = userNameCustomerValue,
                        status = "Đang hoạt động"
                    };

                    string json = JsonSerializer.Serialize(newEmpoyee);
                    string result = await ctll.Update(IdCustomer, json);
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

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Hide();
            frmMain1 frm = new frmMain1();
            frm.Show();
        }

        private void frmAddCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            frmMain1 frm = new frmMain1();
            frm.Show();
        }

        private void Reset()
        {
            txt_ho.Text = string.Empty;
            txt_ten.Text = string.Empty;
            txt_email.Text = string.Empty;
            txt_sdt.Text = string.Empty;
            txt_diachi.Text = string.Empty;
            guna2DateTimePicker1.Value = DateTime.Now;
            picXemAnh.Image = null;
            cboGioiTinh.Items.Add("Chọn--");
            cboGioiTinh.SelectedIndex = 0;


            cboTinh.SelectedIndex = 0;
            cboQuan.Items.Clear();
            cboQuan.Items.Add("Chọn--");
            cboQuan.SelectedIndex = 0;

            cboPhuong.Items.Clear();
            cboPhuong.Items.Add("Chọn--");
            cboPhuong.SelectedIndex = 0;

            //idPartner = 0;

            LoadDataGridView();
        }
    }
}
