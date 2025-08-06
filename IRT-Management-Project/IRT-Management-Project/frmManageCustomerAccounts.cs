using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using System.Net;

namespace IRT_Management_Project
{
    public partial class frmManageCustomerAccounts : Form
    {
        private ManageCustomerAccountsBLL acbll;
        private string idCustomerValue = string.Empty, statusAccountValue = string.Empty;
        public frmManageCustomerAccounts()
        {
            InitializeComponent();
            acbll = new ManageCustomerAccountsBLL();
        }

        private void DesignTable()
        {
            tblAccountCustomer.Columns[0].HeaderText = "Mã khách hàng";
            tblAccountCustomer.Columns[1].HeaderText = "Tên khách hàng";
            tblAccountCustomer.Columns[2].HeaderText = "Tài khoản";
            tblAccountCustomer.Columns[3].HeaderText = "Trạng thái";
        }

        private async Task LoadData()
        {
            tblAccountCustomer.DataSource = await acbll.GetData();
            DesignTable();
        }

        private async Task SearchData(string str)
        {
            tblAccountCustomer.DataSource = await acbll.SearchData(str);
            DesignTable();
        }

        private async void frmManageCustomerAccounts_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private void tblAccountCustomer_Click(object sender, EventArgs e)
        {
            if (tblAccountCustomer.CurrentRow != null)
            {
                int i = tblAccountCustomer.CurrentRow.Index;
                idCustomerValue = tblAccountCustomer.Rows[i].Cells[0].Value.ToString();
                txtTenTaiKhoan.Text = tblAccountCustomer.Rows[i].Cells[2].Value.ToString();
                txtTrangThai.Text = tblAccountCustomer.Rows[i].Cells[3].Value.ToString();
                statusAccountValue = tblAccountCustomer.Rows[i].Cells[3].Value.ToString();
            }
            else
                MessageBox.Show("Bạn chưa chọn dòng nào");
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text)) { await LoadData(); }
            else { await SearchData(guna2TextBox1.Text); }
        }

        private async void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (statusAccountValue.Equals("Đang hoạt động"))
            {
                string rs = await acbll.LockAccount(idCustomerValue);
                if (rs != null)
                {
                    MessageBox.Show("Khóa tài khoản thành công");
                    await LoadData();
                    idCustomerValue = string.Empty;
                    txtTenTaiKhoan.Text = string.Empty;
                    txtTrangThai.Text = string.Empty;
                    statusAccountValue = string.Empty;
                }
                else
                    MessageBox.Show("Thao tác thất bại");
            }
            else
            {
                string rs = await acbll.OpenAccount(idCustomerValue);
                if (rs != null)
                {
                    MessageBox.Show("Mở khóa tài khoản thành công");
                    await LoadData();
                    idCustomerValue = string.Empty;
                    txtTenTaiKhoan.Text = string.Empty;
                    txtTrangThai.Text = string.Empty;
                    statusAccountValue = string.Empty;
                }
                else
                    MessageBox.Show("Thao tác thất bại");
            }
        }
    }
}
