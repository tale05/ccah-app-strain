using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace IRT_Management_Project
{
    public partial class frmManageEmployeeAccounts : Form
    {
        private ManageEmployeeAccountsBLL acbll;
        private string idEmployeeValue = string.Empty, statusAccountValue = string.Empty, usernameValue = string.Empty;
        public frmManageEmployeeAccounts()
        {
            InitializeComponent();
            acbll = new ManageEmployeeAccountsBLL();
        }

        private void DesignTable()
        {
            tblAccountEmployee.Columns[0].HeaderText = "Mã nhân viên";
            tblAccountEmployee.Columns[1].HeaderText = "Tên nhân viên";
            tblAccountEmployee.Columns[2].HeaderText = "Tài khoản";
            tblAccountEmployee.Columns[3].HeaderText = "Trạng thái";
            tblAccountEmployee.Columns[4].HeaderText = "Id quyền";
            tblAccountEmployee.Columns[5].HeaderText = "Tên quyền";

            tblAccountEmployee.Columns[4].Visible = false;
        }

        private async Task LoadData()
        {
            tblAccountEmployee.DataSource = await acbll.GetData();
            DesignTable();
        }

        private async Task SearchData(string str)
        {
            tblAccountEmployee.DataSource = await acbll.SearchData(str);
            DesignTable();
        }

        private async Task LoadComboboxRole()
        {
            cboQuyen.Items.Clear();
            foreach (string item in await acbll.GetRoleName())
            {
                cboQuyen.Items.Add(item);
            }
            cboQuyen.SelectedIndex = 0;
        }

        private async void frmManageEmployeeAccounts_Load(object sender, EventArgs e)
        {
            await LoadData();
            await LoadComboboxRole();
        }

        private async void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text)) { await LoadData(); }
            else { await SearchData(guna2TextBox1.Text); }
        }

        private void tblAccountEmployee_Click(object sender, EventArgs e)
        {
            if (tblAccountEmployee.CurrentRow != null)
            {
                int i = tblAccountEmployee.CurrentRow.Index;
                idEmployeeValue = tblAccountEmployee.Rows[i].Cells[0].Value.ToString();
                txtTenTaiKhoan.Text = tblAccountEmployee.Rows[i].Cells[2].Value.ToString();
                usernameValue = tblAccountEmployee.Rows[i].Cells[2].Value.ToString();
                txtTrangThai.Text = tblAccountEmployee.Rows[i].Cells[3].Value.ToString();
                statusAccountValue = tblAccountEmployee.Rows[i].Cells[3].Value.ToString();
                cboQuyen.Text = tblAccountEmployee.Rows[i].Cells[5].Value.ToString();
            }
            else
                MessageBox.Show("Bạn chưa chọn dòng nào");
        }

        private async void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            int idRole = await acbll.GetIdRoleByName(cboQuyen.Text);
            string rs = await acbll.ChangeRole(idEmployeeValue, idRole);
            if (rs != null)
            {
                MessageBox.Show("Cấp quyền thành công");
                await LoadData();
                idEmployeeValue = string.Empty;
                txtTenTaiKhoan.Text = string.Empty;
                txtTrangThai.Text = string.Empty;
                statusAccountValue = string.Empty;
                cboQuyen.SelectedIndex = 0;
            }
            else
                MessageBox.Show("Thao tác thất bại");
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thực hiện thao tác này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string rs = await acbll.ChangePassword(idEmployeeValue, $"{usernameValue}123");
                if (rs != null)
                {
                    MessageBox.Show("Đặt lại mật khẩu thành công");
                }
                else
                {
                    MessageBox.Show("Khóa tài khoản thành công");
                }

                await LoadData();
                idEmployeeValue = string.Empty;
                txtTenTaiKhoan.Text = string.Empty;
                txtTrangThai.Text = string.Empty;
                statusAccountValue = string.Empty;
                cboQuyen.SelectedIndex = 0;
            }
        }


        private async void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (statusAccountValue.Equals("Đang hoạt động"))
            {
                string rs = await acbll.LockAccount(idEmployeeValue);
                if (rs != null)
                {
                    MessageBox.Show("Khóa tài khoản thành công");
                    await LoadData();
                    idEmployeeValue = string.Empty;
                    txtTenTaiKhoan.Text = string.Empty;
                    txtTrangThai.Text = string.Empty;
                    statusAccountValue = string.Empty;
                    cboQuyen.SelectedIndex = 0;
                }
                else
                    MessageBox.Show("Thao tác thất bại");
            }
            else
            {
                string rs = await acbll.OpenAccount(idEmployeeValue);
                if (rs != null)
                {
                    MessageBox.Show("Mở khóa tài khoản thành công");
                    await LoadData();
                    idEmployeeValue = string.Empty;
                    txtTenTaiKhoan.Text = string.Empty;
                    txtTrangThai.Text = string.Empty;
                    statusAccountValue = string.Empty;
                    cboQuyen.SelectedIndex = 0;
                }
                else
                    MessageBox.Show("Thao tác thất bại");
            }
        }
    }
}
