using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmAddInventory : Form
    {
        private FormAddInventoryBLL aibll;
        private int idInventory = 0, idStrain = 0, quantityValue = 0;

        public frmAddInventory()
        {
            InitializeComponent();
            aibll = new FormAddInventoryBLL();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }

        private async void frmAddInventory_Load(object sender, EventArgs e)
        {
            await LoadDataGridView();
        }

        private async Task LoadDataGridView()
        {
            List<InventoryDTO> lst = await aibll.GetData();
            tblStrainInventory.DataSource = lst;
            DesignTable();
        }

        private async Task Fill_1()
        {
            List<InventoryDTO> lst = await aibll.Fill_1();
            tblStrainInventory.DataSource = lst;
            DesignTable();
        }

        private async Task Fill_2()
        {
            List<InventoryDTO> lst = await aibll.Fill_2();
            tblStrainInventory.DataSource = lst;
            DesignTable();
        }

        private async Task Fill_3()
        {
            List<InventoryDTO> lst = await aibll.Fill_3();
            tblStrainInventory.DataSource = lst;
            DesignTable();
        }

        private void DesignTable()
        {
            tblStrainInventory.Columns[0].HeaderText = "Mã";
            tblStrainInventory.Columns[1].HeaderText = "Mã";
            tblStrainInventory.Columns[2].HeaderText = "Mã strain";
            tblStrainInventory.Columns[3].HeaderText = "Số lượng";
            tblStrainInventory.Columns[4].HeaderText = "Giá";
            tblStrainInventory.Columns[5].HeaderText = "Ngày nhập";
            tblStrainInventory.Columns[6].HeaderText = "Lịch sử";
            tblStrainInventory.Columns[1].Visible = false;
            tblStrainInventory.Columns[6].Visible = false;
            tblStrainInventory.Columns[7].Visible = false;
            tblStrainInventory.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tblStrainInventory.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tblStrainInventory.RowPrePaint -= tblStrainInventory_RowPrePaint;
            tblStrainInventory.RowPrePaint += tblStrainInventory_RowPrePaint;
        }

        private void tblStrainInventory_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var dgv = sender as DataGridView;

            if (dgv.Rows[e.RowIndex].Cells[3].Value != null)
            {
                int quanity = int.Parse(dgv.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());

                if (quanity == 0)
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LavenderBlush;
                }
                else if (quanity < 5)
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void tblStrainInventory_Click(object sender, EventArgs e)
        {
            if (tblStrainInventory.CurrentRow != null)
            {
                int i = tblStrainInventory.CurrentRow.Index;
                txtStrainNumber.Text = tblStrainInventory.Rows[i].Cells[2].Value.ToString();
                txtQuanity.Text = tblStrainInventory.Rows[i].Cells[3].Value.ToString();
                quantityValue = int.Parse(tblStrainInventory.Rows[i].Cells[3].Value.ToString());
                txtPrice.Text = tblStrainInventory.Rows[i].Cells[7].Value.ToString();
                txtDateAdd.Text = tblStrainInventory.Rows[i].Cells[5].Value.ToString();
                txtHistory.Text = tblStrainInventory.Rows[i].Cells[6].Value.ToString();
                panel9.Visible = true;
                idInventory = int.TryParse(tblStrainInventory.Rows[tblStrainInventory.CurrentRow.Index].Cells[0].Value.ToString(), out int tempIdInventory) ? tempIdInventory : 0;
                idStrain = int.TryParse(tblStrainInventory.Rows[tblStrainInventory.CurrentRow.Index].Cells[1].Value.ToString(), out int tempIdStrain) ? tempIdStrain : 0;
            }
            else
                MessageBox.Show("Bạn chưa chọn hàng nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            frmMain1 frm = new frmMain1();
            frm.Show();
        }

        private void frmAddInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            frmMain1 frm = new frmMain1();
            frm.Show();
        }

        private void txtQuanity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private async void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedIndex == 0)
                await LoadDataGridView();
            else if (guna2ComboBox1.SelectedIndex == 1)
                await Fill_3();
            else if (guna2ComboBox1.SelectedIndex == 2)
                await Fill_1();
            else if (guna2ComboBox1.SelectedIndex == 3)
                await Fill_2();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (idInventory != 0)
            {
                int newQuantity = quantityValue + int.Parse(txtQuanity.Text);
                var newInventoryUpdate = new
                {
                    idStrain = idStrain,
                    quantity = newQuantity,
                    price = txtPrice.Text,
                    entryDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    histories = $"{txtHistory.Text}\n{DateTime.Now.ToString("dd/MM/yyyy")} - SL: {txtQuanity.Text} - Giá: {txtPrice.Text}",
                };
                string json = JsonSerializer.Serialize(newInventoryUpdate);
                string rs = await aibll.Update(idInventory, json);
                if (rs != null)
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataGridView();
                    txtStrainNumber.Text = string.Empty;
                    txtQuanity.Text = string.Empty;
                    txtPrice.Text = string.Empty;
                    txtDateAdd.Text = string.Empty;
                    txtHistory.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Chưa chọn dữ liệu nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
