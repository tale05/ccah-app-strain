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
    public partial class frmListProjectContent : Form
    {
        private ProjectContentBLL pcbll;
        public static int idProjectContent;
        public frmListProjectContent()
        {
            InitializeComponent();
            pcbll = new ProjectContentBLL();
            lblNameUser.Text = frmLogin.fullNameEmployee;
            LoadDataGridView();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
        public async void LoadDataGridView()
        {
            tblProjectContent.DataSource = null;
            tblProjectContent.Rows.Clear();
            tblProjectContent.Columns.Clear();

            List<ProjectContentCustomDTO> lst = await pcbll.LoadDataProjectContent(frmListProject.idProject);
            tblProjectContent.DataSource = lst;

            tblProjectContent.AutoGenerateColumns = true;
            tblProjectContent.Columns[0].HeaderText = "STT";
            tblProjectContent.Columns[1].HeaderText = "Mã/Tên dự án";
            tblProjectContent.Columns[2].HeaderText = "Nội dung công việc";
            tblProjectContent.Columns[3].HeaderText = "Kết quả";
            tblProjectContent.Columns[4].HeaderText = "Ngày bắt đầu";
            tblProjectContent.Columns[5].HeaderText = "Ngày kết thúc";
            tblProjectContent.Columns[6].HeaderText = "Số hợp đồng";
            tblProjectContent.Columns[7].HeaderText = "Độ ưu tiên";
            tblProjectContent.Columns[8].HeaderText = "Trạng thái";

            tblProjectContent.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tblProjectContent.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            tblProjectContent.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProjectContent.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProjectContent.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProjectContent.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProjectContent.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tblProjectContent.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            if (!tblProjectContent.Columns.Contains("Thao tác"))
            {
                DataGridViewLinkColumn actionColumn = new DataGridViewLinkColumn();
                actionColumn.Name = "ThaoTac";
                actionColumn.HeaderText = "Thao tác";
                actionColumn.Text = "Xem chi tiết";
                actionColumn.UseColumnTextForLinkValue = true;
                tblProjectContent.Columns.Add(actionColumn);
            }

            tblProjectContent.CellContentClick -= tblProjectContent_CellContentClick;
            tblProjectContent.CellContentClick += tblProjectContent_CellContentClick;
        }
        private void tblProjectContent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == tblProjectContent.Columns["ThaoTac"].Index && e.RowIndex >= 0)
            {
                var rowData = (ProjectContentCustomDTO)tblProjectContent.Rows[e.RowIndex].DataBoundItem;
                idProjectContent = rowData.idProjectContent;
                frmListContentWork frm = new frmListContentWork();
                frm.Show();
                Hide();
            }
        }
        private void frmListProjectContent_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmListProject frm = new frmListProject();
            frm.Show();
            Hide();
        }
    }
}
