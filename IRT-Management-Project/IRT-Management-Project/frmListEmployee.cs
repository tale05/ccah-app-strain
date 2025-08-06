using BLL;
using DTO;
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
    public partial class frmListEmployee : Form
    {
        EmployeeBLL ebll;
        public frmListEmployee()
        {
            InitializeComponent();
            ebll = new EmployeeBLL();
            LoadDataGridView();
        }
        public async void LoadDataGridView()
        {
            List<EmployeeCustomDTO1> lst = await ebll.LoadData();
            tblEmployee.DataSource = lst;

            tblEmployee.AutoGenerateColumns = true;
            tblEmployee.Columns["IdEmployee"].HeaderText = "Mã Nhân Viên";
            tblEmployee.Columns["NameRole"].HeaderText = "Vai Trò";
            tblEmployee.Columns["FullName"].HeaderText = "Họ và Tên";
            tblEmployee.Columns["IdCard"].HeaderText = "CCCD";
            tblEmployee.Columns["DateOfBirth"].HeaderText = "Ngày Sinh";
            tblEmployee.Columns["Gender"].HeaderText = "Giới Tính";
            tblEmployee.Columns["Email"].HeaderText = "Email";
            tblEmployee.Columns["PhoneNumber"].HeaderText = "Số Điện Thoại";
            tblEmployee.Columns["Degree"].HeaderText = "Bằng Cấp";
            tblEmployee.Columns["Address"].HeaderText = "Địa Chỉ";
            tblEmployee.Columns["JoinDate"].HeaderText = "Ngày Tham Gia";

            tblEmployee.Columns["Gender"].Visible = false;
            tblEmployee.Columns["Email"].Visible = false;
            tblEmployee.Columns["PhoneNumber"].Visible = false;
            tblEmployee.Columns["Degree"].Visible = false;
            tblEmployee.Columns["Address"].Visible = false;
            tblEmployee.Columns["JoinDate"].Visible = false;
        }
        private void ClearTextBoxes()
        {
            txtId.Text = string.Empty;
            txtVaitro.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtCCCD.Text = string.Empty;
            txtNgaysinh.Text = string.Empty;
            txtGioitinh.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtTrinhdo.Text = string.Empty;
            txtDiachi.Text = string.Empty;
            txtNgaythamgia.Text = string.Empty;
        }
        private void tblEmployee_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = tblEmployee.SelectedRows[0];
            txtId.Text = selectedRow.Cells[0].Value.ToString();
            txtVaitro.Text = selectedRow.Cells[1].Value.ToString();
            txtTen.Text = selectedRow.Cells[2].Value.ToString();
            txtCCCD.Text = selectedRow.Cells[3].Value.ToString();
            txtNgaysinh.Text = selectedRow.Cells[4].Value.ToString().Substring(0, 10);
            txtGioitinh.Text = selectedRow.Cells[5].Value.ToString();
            txtEmail.Text = selectedRow.Cells[6].Value.ToString();
            txtSDT.Text = selectedRow.Cells[7].Value.ToString();
            txtTrinhdo.Text = selectedRow.Cells[8].Value.ToString();
            txtDiachi.Text = selectedRow.Cells[9].Value.ToString();
            txtNgaythamgia.Text = selectedRow.Cells[10].Value.ToString().Substring(0, 10);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            frmMain1 frmMain = new frmMain1();
            frmMain.Show();
            Hide();
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Equals(string.Empty))
                MessageBox.Show("Bạn chưa chọn dữ liệu nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    bool result = await ebll.DeleteData(txtId.Text.Trim());
                    if (result == true)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearTextBoxes();
                        LoadDataGridView();
                    }    
                    else
                        MessageBox.Show("Không thể xóa thông tin nhân viên này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmMain1 frmMain = new frmMain1();
            frmMain.Show();
            Hide();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void frmListEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
        }
    }
}
