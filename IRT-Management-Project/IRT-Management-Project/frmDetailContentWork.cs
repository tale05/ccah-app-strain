using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmDetailContentWork : Form
    {
        ContentWorkBLL cwbll;
        public frmDetailContentWork()
        {
            InitializeComponent();
            cwbll = new ContentWorkBLL();
            LoadData();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }

        private async void LoadData()
        {
            DetailContentWorkCustomDTO obj = new DetailContentWorkCustomDTO();
            obj = await cwbll.GetDetailContentWork(frmListContentWork.idContentWork);

            if (obj.ennDateActual != null)
            {
                lblSTT.Text = obj.idContentWork.ToString() ?? "Chưa có dữ liệu";
                lblTennhanvien.Text = obj.nameEmployee ?? "Chưa có dữ liệu";
                lblNoidungcongviec.Text = obj.nameContent ?? "Chưa có dữ liệu";
                lblKetqua.Text = obj.result ?? "Chưa có dữ liệu";
                lblNgaybatdau.Text = DateTime.Parse(obj.startDate.ToString()).ToString("dd/MM/yyyy") ?? "Chưa có dữ liệu";
                lblNgayketthucdukien.Text = DateTime.Parse(obj.endDate.ToString()).ToString("dd/MM/yyyy") ?? "Chưa có dữ liệu";
                lblNgayketthucthucte.Text = DateTime.Parse(obj.ennDateActual.ToString()).ToString("dd/MM/yyyy") ?? "Chưa có dữ liệu";
                lblSohopdong.Text = obj.contractNo ?? "Chưa có dữ liệu";
                lblMucdouutien.Text = obj.priority.ToString() ?? "Chưa có dữ liệu";
                lblTrangthai.Text = obj.status ?? "Chưa có dữ liệu";

                DateTime date1 = DateTime.Parse(obj.endDate.ToString());
                DateTime date3 = DateTime.Parse(obj.ennDateActual.ToString());
                if (date1 < date3)
                {
                    lblThongbao.Text = obj.status + " trễ hạn";
                }
                else
                {
                    lblThongbao.Text = obj.status + " đúng hạn";
                }
            }
            else
            {
                lblSTT.Text = obj.idContentWork.ToString() ?? "Chưa có dữ liệu";
                lblTennhanvien.Text = obj.nameEmployee ?? "Chưa có dữ liệu";
                lblNoidungcongviec.Text = obj.nameContent ?? "Chưa có dữ liệu";
                lblKetqua.Text = obj.result ?? "Chưa có dữ liệu";
                lblNgaybatdau.Text = DateTime.Parse(obj.startDate.ToString()).ToString("dd/MM/yyyy") ?? "Chưa có dữ liệu";
                lblNgayketthucdukien.Text = DateTime.Parse(obj.endDate.ToString()).ToString("dd/MM/yyyy") ?? "Chưa có dữ liệu";
                lblNgayketthucthucte.Text = "Chưa có dữ liệu";
                lblSohopdong.Text = obj.contractNo ?? "Chưa có dữ liệu";
                lblMucdouutien.Text = obj.priority.ToString() ?? "Chưa có dữ liệu";
                lblTrangthai.Text = obj.status ?? "Chưa có dữ liệu";

                DateTime date1 = DateTime.Parse(obj.endDate.ToString());
                DateTime date2 = DateTime.Now;
                if (date1 < date2)
                {
                    lblThongbao.Text = "Đã quá hạn hoàn thành";
                }
                else
                {
                    lblThongbao.Text = "Chưa đến hạn kết thúc";
                }    
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmListContentWork frm = new frmListContentWork();
            frm.Show();
            Hide();
        }

        private void frmDetailContentWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmListContentWork frm = new frmListContentWork();
            frm.Show();
            Hide();
        }

        private async void btnViewFile_Click(object sender, EventArgs e)
        {
            byte[] fileSaved = await cwbll.GetFileSavedById(int.Parse(lblSTT.Text.Trim()));
            string fileName = await cwbll.GetFileNameById(int.Parse(lblSTT.Text.Trim()));

            if (fileSaved != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = fileName;
                    saveFileDialog.Filter = "All Files (*.*)|*.*";
                    saveFileDialog.Title = "Lưu tệp";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            File.WriteAllBytes(saveFileDialog.FileName, fileSaved);
                            MessageBox.Show("Tệp đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không thể lưu tệp. Chi tiết lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có tệp nào được lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
