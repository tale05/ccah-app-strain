using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmChangePassword : Form
    {
        FormChangePasswordBLL fcpbll;
        public frmChangePassword()
        {
            InitializeComponent();
            fcpbll = new FormChangePasswordBLL();
        }
        private async void btnDoimatkhau_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPass1.Text))
            {
                MessageBox.Show("Mật khẩu cũ không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPass2.Text))
            {
                MessageBox.Show("Mật khẩu mới không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPass3.Text))
            {
                MessageBox.Show("Mật khẩu xác nhận không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string hashedPassword = await fcpbll.GetPasswordCurrent(frmLogin.idEmployee);

                if (!VerifyPassword(txtPass1.Text, hashedPassword))
                {
                    MessageBox.Show("Mật khẩu bạn nhập không trùng khớp với mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!txtPass3.Text.Equals(txtPass2.Text))
                {
                    MessageBox.Show("Mật khẩu xác nhận không trùng khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string rs = await fcpbll.ChangePassword(frmLogin.idEmployee, txtPass2.Text);

                if (rs != null)
                {
                    txtPass1.Text = string.Empty;
                    txtPass2.Text = string.Empty;
                    txtPass3.Text = string.Empty;
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHashedPassword);
        }
    }
}
