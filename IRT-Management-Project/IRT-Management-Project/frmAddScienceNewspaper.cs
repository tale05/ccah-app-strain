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
using System.Collections.Generic;
using System;

namespace IRT_Management_Project
{
    public partial class frmAddScienceNewspaper : Form
    {
        private FormAddScienceNewspaper nsll;
        private static string IdNews;
        private static byte[] fileImage = null;
        private static string IdEmployeeValue = string.Empty;
        public frmAddScienceNewspaper()
        {
            InitializeComponent();
            nsll = new FormAddScienceNewspaper();
            lblNameUser.Text = frmLogin.fullNameEmployee;
            LoadComboBoxEmployee();
        }
        private async Task LoadDataGridView()
        {
            List<ScienceNewspaperCustomDTO> data = new List<ScienceNewspaperCustomDTO>();
            data = await nsll.LoadData();
            InsertDataAndDesignTable(data);
        }
        private async void SearchData(string search)
        {
            List<ScienceNewspaperCustomDTO> data = await nsll.SearchData(search);
            InsertDataAndDesignTable(data);
        }
        private async void LoadComboBoxEmployee()
        {
            cboEmployee.Items.Add("Chọn--");
            foreach (string item in await nsll.GetListNameEmployee())
            {
                cboEmployee.Items.Add(item);
            }
            cboEmployee.SelectedIndex = 0;
        }
        private void InsertDataAndDesignTable(List<ScienceNewspaperCustomDTO> lstInput)
        {
            tblNews.DataSource = null;
            tblNews.Rows.Clear();
            tblNews.Columns.Clear();
            tblNews.AutoGenerateColumns = false;


            tblNews.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdNewspaper",
                HeaderText = "Mã Bài Báo",
                DataPropertyName = "IdNewspaper"
            });
            tblNews.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Tiêu Đề",
                DataPropertyName = "Title"
            });
            tblNews.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Content",
                HeaderText = "Nội Dung",
                DataPropertyName = "Content"
            });
            tblNews.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Content2",
                HeaderText = "Nội Dung",
                DataPropertyName = "Content2"
            });
            tblNews.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Postdate",
                HeaderText = "Ngày đăng",
                DataPropertyName = "Postdate"
            });
            tblNews.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdEmployee",
                HeaderText = "Mã Nhân Viên",
                DataPropertyName = "IdEmployee"
            });

            tblNews.DataSource = lstInput;

            tblNews.Columns["IdNewspaper"].Visible = false;
            tblNews.Columns["Content"].Visible = false;
            tblNews.Columns["Content2"].Visible = false;


        }

        private async void frmAddScienceNewspaper_Load(object sender, System.EventArgs e)
        {
            await LoadDataGridView();
        }

        private async void tblNews_Click(object sender, System.EventArgs e)
        {
            if (tblNews.CurrentRow != null)
            {
                int i = tblNews.CurrentRow.Index;
                if (i >= 0 && i < tblNews.Rows.Count)
                {
                    IdNews = tblNews.Rows[i].Cells["IdNewspaper"].Value.ToString();
                    Id.Text = tblNews.Rows[i].Cells["IdNewspaper"].Value.ToString();
                    Tittle.Text = tblNews.Rows[i].Cells["Title"].Value.ToString();
                    guna2DateTimePicker1.Value = DateTime.Parse(tblNews.Rows[i].Cells["Postdate"].Value.ToString());
                    IdEmployeeValue = await nsll.GetIdByNameEmployee(tblNews.Rows[i].Cells["IdEmployee"].Value.ToString());

                    cboEmployee.Text = tblNews.Rows[i].Cells["IdEmployee"].Value.ToString();
                    nd1_txt.Text = tblNews.Rows[i].Cells["Content"].Value.ToString();
                    nd2_txt.Text = tblNews.Rows[i].Cells["Content2"].Value.ToString();

                    fileImage = await nsll.GetImageIdNewspaper(int.Parse(IdNews));

                    if (fileImage != null)
                    {
                        using (MemoryStream ms = new MemoryStream(fileImage))
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
        private async void Reset()
        {
            Id.Text = string.Empty;
            Tittle.Text = string.Empty;
            guna2DateTimePicker1.Value = DateTime.Now;
            picXemAnh.Image = null;
            cboEmployee.Items.Add("Chọn--");
            cboEmployee.SelectedIndex = 0;
            nd1_txt.Text = string.Empty;
            nd2_txt.Text = string.Empty;


            await LoadDataGridView();
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(Id.Text) ||
                string.IsNullOrEmpty(Tittle.Text) ||
                string.IsNullOrEmpty(nd1_txt.Text) ||
                string.IsNullOrEmpty(nd2_txt.Text))
            {
                return false;
            }

            if (cboEmployee.Text == "Chọn--")
            {
                return false;
            }

            return true;
        }
        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm bài báo khoa học này?", "Xác nhận thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    var newPaper = new
                    {
                        title = Tittle.Text,
                        content = nd1_txt.Text,
                        postDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"),
                        image = fileImage,
                        idEmployee = IdEmployeeValue,
                        content2 = nd2_txt.Text,

                    };

                    string json = JsonSerializer.Serialize(newPaper);
                    string result = await nsll.AddData(json);
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
                bool result = await nsll.DeleteData(IdNews);

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
            if (ValidateInputs())
            {

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa bài báo khoa học này?", "Xác nhận thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    var newPaper = new
                    {
                        title = Tittle.Text,
                        content = nd1_txt.Text,
                        postDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"),
                        image = fileImage,
                        idEmployee = IdEmployeeValue,
                        content2 = nd2_txt.Text,

                    };

                    string json = JsonSerializer.Serialize(newPaper);
                    string result = await nsll.Update(IdNews, json);
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

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                await LoadDataGridView();
            }
            else
            {
                SearchData(txtSearch.Text);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmMain1 frmMain1 = new frmMain1();
            frmMain1.Show();
            Hide();
        }

        private void frmAddScienceNewspaper_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frmMain1 = new frmMain1();
            frmMain1.Show();
            Hide();
        }
    }
}
