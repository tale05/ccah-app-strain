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
    public partial class frmListProject : Form
    {
        private ProjectBLL pbll;
        public static string idProject, nameProject;
        public frmListProject()
        {
            InitializeComponent();
            pbll = new ProjectBLL();
            LoadDataGridView();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
        public async void LoadDataGridView()
        {
            tblProject.DataSource = null;
            tblProject.Rows.Clear();
            tblProject.Columns.Clear();

            List<ProjectCustom1DTO> lst = await pbll.LoadDataProject();
            tblProject.DataSource = lst;

            tblProject.AutoGenerateColumns = true;
            tblProject.Columns[0].HeaderText = "Mã dự án";
            tblProject.Columns[1].HeaderText = "Trưởng dự án";
            tblProject.Columns[2].HeaderText = "Đối tác";
            tblProject.Columns[3].HeaderText = "Tên dự án";
            tblProject.Columns[4].HeaderText = "Kết quả";
            tblProject.Columns[5].HeaderText = "Ngày bắt đầu";
            tblProject.Columns[6].HeaderText = "Ngày kết thúc";
            tblProject.Columns[7].HeaderText = "Số hợp đồng";
            tblProject.Columns[8].HeaderText = "Mô tả";
            tblProject.Columns[9].HeaderText = "Trạng thái";

            tblProject.Columns[4].Visible = false;
            tblProject.Columns[7].Visible = false;

            if (!tblProject.Columns.Contains("Thao tác"))
            {
                DataGridViewLinkColumn actionColumn = new DataGridViewLinkColumn();
                actionColumn.Name = "ThaoTac";
                actionColumn.HeaderText = "Thao tác";
                actionColumn.Text = "Xem chi tiết";
                actionColumn.UseColumnTextForLinkValue = true;
                tblProject.Columns.Add(actionColumn);
            }

            tblProject.CellContentClick -= tblProject_CellContentClick;
            tblProject.CellContentClick += tblProject_CellContentClick;
        }

        private void frmListProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frm = new frmMain1();
            frm.Show();
            Hide();
        }

        private void tblProject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == tblProject.Columns["ThaoTac"].Index && e.RowIndex >= 0)
            {
                var rowData = (ProjectCustom1DTO)tblProject.Rows[e.RowIndex].DataBoundItem;
                idProject = rowData.idProject;
                nameProject = rowData.projectName;
                frmListProjectContent frm = new frmListProjectContent();
                frm.Show();
                Hide();
                //MessageBox.Show(rowData.idProject);
            }
        }
    }
}
