namespace IRT_Management_Project
{
    partial class frmMain1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain1));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Xem danh sách", -2, -2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Thêm sản phẩm mới", -2, -2);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Cập nhật sản phẩm", -2, -2);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Duyệt/Cấp mã sản phẩm");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Xem các strain chờ duyệt");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Quản lý kho hàng");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Quản lý sản phẩm", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Tất cả dự án", -2, -2);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Tiến độ công việc");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Quản lý dự án", -2, -2);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Dự án/Công việc", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Danh sách đối tác", -2, -2);
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Thêm thông tin mới", -2, -2);
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Quản lý đối tác", 4, 4, new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Cập nhật thông tin bài báo", -2, -2);
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Quản lý bài báo khoa học", 5, 5, new System.Windows.Forms.TreeNode[] {
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Cập nhật thông tin", -2, -2);
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Quản lý tài khoản khách hàng");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Quản lý khách hàng", 6, 6, new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Cập nhật thông tin", -2, -2);
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Quản lý tài khoản nhân viên");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Quản lý nhân viên", 7, 7, new System.Windows.Forms.TreeNode[] {
            treeNode20,
            treeNode21});
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Thống kê");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Quản lý đơn hàng", new System.Windows.Forms.TreeNode[] {
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("DANH MỤC", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode11,
            treeNode14,
            treeNode16,
            treeNode19,
            treeNode22,
            treeNode24});
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel_Username = new Guna.UI2.WinForms.Guna2Panel();
            this.lblNameUser = new System.Windows.Forms.Label();
            this.menuForAvatar = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.btnXemTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaoLuuDuLieu = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRestoreDB = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.container = new System.Windows.Forms.Panel();
            this.panel_Admin = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tblTaskEmployee = new Guna.UI2.WinForms.Guna2DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.lblChaoMung = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.guna2GradientButton4 = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDuyetStrain = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnNhanvien = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDonHang = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panel_Employee = new System.Windows.Forms.Panel();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel_Username.SuspendLayout();
            this.menuForAvatar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.container.SuspendLayout();
            this.panel_Admin.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblTaskEmployee)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.SeaGreen;
            this.header.Controls.Add(this.label1);
            this.header.Controls.Add(this.pictureBox2);
            this.header.Controls.Add(this.panel_Username);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Padding = new System.Windows.Forms.Padding(15);
            this.header.Size = new System.Drawing.Size(1493, 69);
            this.header.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(54, 15);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(1167, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "VIỆN NGHIÊN CỨU ỨNG DỤNG VÀ CHUYỂN GIAO CÔNG NGHỆ (IRT)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(15, 15);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(39, 39);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // panel_Username
            // 
            this.panel_Username.BackColor = System.Drawing.Color.DarkCyan;
            this.panel_Username.BorderColor = System.Drawing.Color.Transparent;
            this.panel_Username.BorderThickness = 5;
            this.panel_Username.Controls.Add(this.lblNameUser);
            this.panel_Username.Controls.Add(this.pictureBox1);
            this.panel_Username.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Username.Location = new System.Drawing.Point(1221, 15);
            this.panel_Username.Name = "panel_Username";
            this.panel_Username.Size = new System.Drawing.Size(257, 39);
            this.panel_Username.TabIndex = 0;
            // 
            // lblNameUser
            // 
            this.lblNameUser.BackColor = System.Drawing.Color.SeaGreen;
            this.lblNameUser.ContextMenuStrip = this.menuForAvatar;
            this.lblNameUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNameUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameUser.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblNameUser.Image = global::IRT_Management_Project.Properties.Resources.dropdown;
            this.lblNameUser.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNameUser.Location = new System.Drawing.Point(38, 0);
            this.lblNameUser.Name = "lblNameUser";
            this.lblNameUser.Size = new System.Drawing.Size(219, 39);
            this.lblNameUser.TabIndex = 1;
            this.lblNameUser.Text = "PHẠM LÊ TUẤN ANH";
            this.lblNameUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNameUser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblNameUser_MouseDown);
            // 
            // menuForAvatar
            // 
            this.menuForAvatar.AllowDrop = true;
            this.menuForAvatar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuForAvatar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnXemTaiKhoan,
            this.btnDoiMatKhau,
            this.btnDangXuat,
            this.btnSaoLuuDuLieu,
            this.btnRestoreDB});
            this.menuForAvatar.Name = "menuForAvatar";
            this.menuForAvatar.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.menuForAvatar.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.menuForAvatar.RenderStyle.ColorTable = null;
            this.menuForAvatar.RenderStyle.RoundedEdges = true;
            this.menuForAvatar.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.menuForAvatar.RenderStyle.SelectionBackColor = System.Drawing.Color.MediumSeaGreen;
            this.menuForAvatar.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.menuForAvatar.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.menuForAvatar.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.menuForAvatar.Size = new System.Drawing.Size(185, 124);
            this.menuForAvatar.MouseLeave += new System.EventHandler(this.menuForAvatar_MouseLeave);
            // 
            // btnXemTaiKhoan
            // 
            this.btnXemTaiKhoan.Name = "btnXemTaiKhoan";
            this.btnXemTaiKhoan.Size = new System.Drawing.Size(184, 24);
            this.btnXemTaiKhoan.Text = "Xem tài khoản";
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(184, 24);
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(184, 24);
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnSaoLuuDuLieu
            // 
            this.btnSaoLuuDuLieu.Name = "btnSaoLuuDuLieu";
            this.btnSaoLuuDuLieu.Size = new System.Drawing.Size(184, 24);
            this.btnSaoLuuDuLieu.Text = "Sao lưu dữ liệu";
            this.btnSaoLuuDuLieu.Click += new System.EventHandler(this.btnSaoLuuDuLieu_Click);
            // 
            // btnRestoreDB
            // 
            this.btnRestoreDB.Name = "btnRestoreDB";
            this.btnRestoreDB.Size = new System.Drawing.Size(184, 24);
            this.btnRestoreDB.Text = "Phục hồi dữ liệu";
            this.btnRestoreDB.Click += new System.EventHandler(this.btnRestoreDB_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.SeaGreen;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::IRT_Management_Project.Properties.Resources.user1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(10);
            this.pictureBox1.Size = new System.Drawing.Size(38, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.SeaGreen;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.ItemHeight = 40;
            this.treeView1.LineColor = System.Drawing.Color.White;
            this.treeView1.Location = new System.Drawing.Point(0, 69);
            this.treeView1.Name = "treeView1";
            treeNode1.ForeColor = System.Drawing.Color.White;
            treeNode1.ImageIndex = -2;
            treeNode1.Name = "Node1";
            treeNode1.NodeFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode1.SelectedImageIndex = -2;
            treeNode1.Tag = "tagXemSanPham";
            treeNode1.Text = "Xem danh sách";
            treeNode2.ForeColor = System.Drawing.Color.White;
            treeNode2.ImageIndex = -2;
            treeNode2.Name = "Node2";
            treeNode2.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode2.SelectedImageIndex = -2;
            treeNode2.Tag = "tagThemSP";
            treeNode2.Text = "Thêm sản phẩm mới";
            treeNode3.ForeColor = System.Drawing.Color.White;
            treeNode3.ImageIndex = -2;
            treeNode3.Name = "Node3";
            treeNode3.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode3.SelectedImageIndex = -2;
            treeNode3.Tag = "tagUpdateStrain";
            treeNode3.Text = "Cập nhật sản phẩm";
            treeNode4.ForeColor = System.Drawing.Color.White;
            treeNode4.Name = "Node0";
            treeNode4.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode4.Tag = "tagDuyetSP";
            treeNode4.Text = "Duyệt/Cấp mã sản phẩm";
            treeNode5.ForeColor = System.Drawing.Color.White;
            treeNode5.Name = "Node0";
            treeNode5.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode5.Tag = "tagDSStrainNV";
            treeNode5.Text = "Xem các strain chờ duyệt";
            treeNode6.ForeColor = System.Drawing.Color.White;
            treeNode6.Name = "Node0";
            treeNode6.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode6.Tag = "tagQLKhoHang";
            treeNode6.Text = "Quản lý kho hàng";
            treeNode7.ForeColor = System.Drawing.Color.White;
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "Node0";
            treeNode7.NodeFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode7.SelectedImageKey = "strain.png";
            treeNode7.Text = "Quản lý sản phẩm";
            treeNode8.ForeColor = System.Drawing.Color.White;
            treeNode8.ImageIndex = -2;
            treeNode8.Name = "Node5";
            treeNode8.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode8.SelectedImageIndex = -2;
            treeNode8.Tag = "tagAllDuAn";
            treeNode8.Text = "Tất cả dự án";
            treeNode9.ForeColor = System.Drawing.Color.White;
            treeNode9.ImageIndex = -2;
            treeNode9.Name = "Node6";
            treeNode9.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode9.Tag = "tagTiendo";
            treeNode9.Text = "Tiến độ công việc";
            treeNode10.ForeColor = System.Drawing.Color.White;
            treeNode10.ImageIndex = -2;
            treeNode10.Name = "Node7";
            treeNode10.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode10.SelectedImageIndex = -2;
            treeNode10.Tag = "tagThemDuAn";
            treeNode10.Text = "Quản lý dự án";
            treeNode11.ForeColor = System.Drawing.Color.White;
            treeNode11.ImageIndex = 3;
            treeNode11.Name = "Node4";
            treeNode11.NodeFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            treeNode11.Text = "Dự án/Công việc";
            treeNode12.ForeColor = System.Drawing.Color.White;
            treeNode12.ImageIndex = -2;
            treeNode12.Name = "Node9";
            treeNode12.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode12.SelectedImageIndex = -2;
            treeNode12.Tag = "tagListPartner";
            treeNode12.Text = "Danh sách đối tác";
            treeNode13.ForeColor = System.Drawing.Color.White;
            treeNode13.ImageIndex = -2;
            treeNode13.Name = "Node10";
            treeNode13.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode13.SelectedImageIndex = -2;
            treeNode13.Tag = "tagAddPartner";
            treeNode13.Text = "Thêm thông tin mới";
            treeNode14.ForeColor = System.Drawing.Color.White;
            treeNode14.ImageIndex = 4;
            treeNode14.Name = "Node8";
            treeNode14.NodeFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            treeNode14.SelectedImageIndex = 4;
            treeNode14.Tag = "tagQLDoiTac";
            treeNode14.Text = "Quản lý đối tác";
            treeNode15.ForeColor = System.Drawing.Color.White;
            treeNode15.ImageIndex = -2;
            treeNode15.Name = "Node15";
            treeNode15.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode15.SelectedImageIndex = -2;
            treeNode15.Tag = "tagBaiBao";
            treeNode15.Text = "Cập nhật thông tin bài báo";
            treeNode16.ForeColor = System.Drawing.Color.White;
            treeNode16.ImageIndex = 5;
            treeNode16.Name = "Node11";
            treeNode16.NodeFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            treeNode16.SelectedImageIndex = 5;
            treeNode16.Text = "Quản lý bài báo khoa học";
            treeNode17.ForeColor = System.Drawing.Color.White;
            treeNode17.ImageIndex = -2;
            treeNode17.Name = "Node19";
            treeNode17.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode17.SelectedImageIndex = -2;
            treeNode17.Tag = "tagKhachHang";
            treeNode17.Text = "Cập nhật thông tin";
            treeNode18.ForeColor = System.Drawing.Color.White;
            treeNode18.Name = "Node0";
            treeNode18.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode18.Tag = "tagQLAccKhachHang";
            treeNode18.Text = "Quản lý tài khoản khách hàng";
            treeNode19.ForeColor = System.Drawing.Color.White;
            treeNode19.ImageIndex = 6;
            treeNode19.Name = "Node16";
            treeNode19.NodeFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            treeNode19.SelectedImageIndex = 6;
            treeNode19.Text = "Quản lý khách hàng";
            treeNode20.ForeColor = System.Drawing.Color.White;
            treeNode20.ImageIndex = -2;
            treeNode20.Name = "Node23";
            treeNode20.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode20.SelectedImageIndex = -2;
            treeNode20.Tag = "tagNhanVien";
            treeNode20.Text = "Cập nhật thông tin";
            treeNode21.ForeColor = System.Drawing.Color.White;
            treeNode21.Name = "Node1";
            treeNode21.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode21.Tag = "tagQLAccNhanVien";
            treeNode21.Text = "Quản lý tài khoản nhân viên";
            treeNode22.ForeColor = System.Drawing.Color.White;
            treeNode22.ImageIndex = 7;
            treeNode22.Name = "Node20";
            treeNode22.NodeFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            treeNode22.SelectedImageIndex = 7;
            treeNode22.Tag = "tagQLNhanVien";
            treeNode22.Text = "Quản lý nhân viên";
            treeNode23.ForeColor = System.Drawing.Color.White;
            treeNode23.Name = "Node2";
            treeNode23.NodeFont = new System.Drawing.Font("Segoe UI", 12F);
            treeNode23.Tag = "tagThongKe";
            treeNode23.Text = "Thống kê";
            treeNode24.ForeColor = System.Drawing.Color.White;
            treeNode24.Name = "Node0";
            treeNode24.NodeFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            treeNode24.Tag = "tagQLDonHang";
            treeNode24.Text = "Quản lý đơn hàng";
            treeNode25.ForeColor = System.Drawing.Color.White;
            treeNode25.ImageIndex = -2;
            treeNode25.Name = "Node24";
            treeNode25.NodeFont = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            treeNode25.Text = "DANH MỤC";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode25});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(320, 709);
            this.treeView1.TabIndex = 1;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "list.png");
            this.imageList1.Images.SetKeyName(1, "strain.png");
            this.imageList1.Images.SetKeyName(2, "arrow.png");
            this.imageList1.Images.SetKeyName(3, "work.png");
            this.imageList1.Images.SetKeyName(4, "partner.png");
            this.imageList1.Images.SetKeyName(5, "newspaper.png");
            this.imageList1.Images.SetKeyName(6, "customer.png");
            this.imageList1.Images.SetKeyName(7, "user2.png");
            // 
            // container
            // 
            this.container.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.container.Controls.Add(this.panel_Admin);
            this.container.Controls.Add(this.panel_Employee);
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(320, 69);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(1173, 709);
            this.container.TabIndex = 2;
            // 
            // panel_Admin
            // 
            this.panel_Admin.Controls.Add(this.tableLayoutPanel1);
            this.panel_Admin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Admin.Location = new System.Drawing.Point(0, 0);
            this.panel_Admin.Name = "panel_Admin";
            this.panel_Admin.Size = new System.Drawing.Size(1173, 709);
            this.panel_Admin.TabIndex = 0;
            this.panel_Admin.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.05501F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.94499F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1173, 709);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.cartesianChart1, 10, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 209);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.31797F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.68203F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1167, 497);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChart1.Location = new System.Drawing.Point(3, 218);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(1161, 276);
            this.cartesianChart1.TabIndex = 1;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tblTaskEmployee);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1161, 209);
            this.panel5.TabIndex = 2;
            // 
            // tblTaskEmployee
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.tblTaskEmployee.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tblTaskEmployee.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MediumSeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MediumSeaGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tblTaskEmployee.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tblTaskEmployee.ColumnHeadersHeight = 35;
            this.tblTaskEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tblTaskEmployee.DefaultCellStyle = dataGridViewCellStyle3;
            this.tblTaskEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTaskEmployee.GridColor = System.Drawing.Color.SeaGreen;
            this.tblTaskEmployee.Location = new System.Drawing.Point(0, 0);
            this.tblTaskEmployee.Name = "tblTaskEmployee";
            this.tblTaskEmployee.ReadOnly = true;
            this.tblTaskEmployee.RowHeadersVisible = false;
            this.tblTaskEmployee.RowTemplate.Height = 35;
            this.tblTaskEmployee.RowTemplate.ReadOnly = true;
            this.tblTaskEmployee.Size = new System.Drawing.Size(1161, 209);
            this.tblTaskEmployee.TabIndex = 21;
            this.tblTaskEmployee.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tblTaskEmployee.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tblTaskEmployee.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tblTaskEmployee.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tblTaskEmployee.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tblTaskEmployee.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tblTaskEmployee.ThemeStyle.GridColor = System.Drawing.Color.SeaGreen;
            this.tblTaskEmployee.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.tblTaskEmployee.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tblTaskEmployee.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblTaskEmployee.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.tblTaskEmployee.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tblTaskEmployee.ThemeStyle.HeaderStyle.Height = 35;
            this.tblTaskEmployee.ThemeStyle.ReadOnly = true;
            this.tblTaskEmployee.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tblTaskEmployee.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tblTaskEmployee.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblTaskEmployee.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tblTaskEmployee.ThemeStyle.RowsStyle.Height = 35;
            this.tblTaskEmployee.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tblTaskEmployee.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.guna2GradientPanel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(15, 15, 15, 5);
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1167, 200);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.BorderRadius = 15;
            this.guna2GradientPanel1.Controls.Add(this.lblChaoMung);
            this.guna2GradientPanel1.Controls.Add(this.pictureBox3);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2GradientPanel1.Location = new System.Drawing.Point(18, 18);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(1131, 30);
            this.guna2GradientPanel1.TabIndex = 1;
            // 
            // lblChaoMung
            // 
            this.lblChaoMung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChaoMung.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChaoMung.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblChaoMung.Location = new System.Drawing.Point(55, 0);
            this.lblChaoMung.Name = "lblChaoMung";
            this.lblChaoMung.Size = new System.Drawing.Size(1076, 30);
            this.lblChaoMung.TabIndex = 1;
            this.lblChaoMung.Text = "label2";
            this.lblChaoMung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox3.Image = global::IRT_Management_Project.Properties.Resources.waving_hand;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(55, 30);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Controls.Add(this.panel4, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(18, 54);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1131, 138);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.guna2GradientButton4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(849, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.panel4.Size = new System.Drawing.Size(279, 132);
            this.panel4.TabIndex = 3;
            // 
            // guna2GradientButton4
            // 
            this.guna2GradientButton4.BorderRadius = 15;
            this.guna2GradientButton4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton4.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2GradientButton4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientButton4.FillColor = System.Drawing.Color.SeaGreen;
            this.guna2GradientButton4.FillColor2 = System.Drawing.Color.PaleGreen;
            this.guna2GradientButton4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GradientButton4.ForeColor = System.Drawing.Color.White;
            this.guna2GradientButton4.Image = ((System.Drawing.Image)(resources.GetObject("guna2GradientButton4.Image")));
            this.guna2GradientButton4.ImageSize = new System.Drawing.Size(55, 55);
            this.guna2GradientButton4.Location = new System.Drawing.Point(5, 10);
            this.guna2GradientButton4.Name = "guna2GradientButton4";
            this.guna2GradientButton4.Size = new System.Drawing.Size(274, 122);
            this.guna2GradientButton4.TabIndex = 1;
            this.guna2GradientButton4.Text = "Khách hàng";
            this.guna2GradientButton4.Click += new System.EventHandler(this.guna2GradientButton4_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDuyetStrain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(285, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.panel1.Size = new System.Drawing.Size(276, 132);
            this.panel1.TabIndex = 0;
            // 
            // btnDuyetStrain
            // 
            this.btnDuyetStrain.BorderRadius = 15;
            this.btnDuyetStrain.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDuyetStrain.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDuyetStrain.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDuyetStrain.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDuyetStrain.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDuyetStrain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDuyetStrain.FillColor = System.Drawing.Color.DeepPink;
            this.btnDuyetStrain.FillColor2 = System.Drawing.Color.DarkViolet;
            this.btnDuyetStrain.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuyetStrain.ForeColor = System.Drawing.Color.White;
            this.btnDuyetStrain.Image = global::IRT_Management_Project.Properties.Resources.hourglass;
            this.btnDuyetStrain.ImageSize = new System.Drawing.Size(50, 50);
            this.btnDuyetStrain.Location = new System.Drawing.Point(0, 10);
            this.btnDuyetStrain.Name = "btnDuyetStrain";
            this.btnDuyetStrain.Size = new System.Drawing.Size(271, 122);
            this.btnDuyetStrain.TabIndex = 0;
            this.btnDuyetStrain.Click += new System.EventHandler(this.btnDuyetStrain_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnNhanvien);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(567, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5, 10, 5, 0);
            this.panel3.Size = new System.Drawing.Size(276, 132);
            this.panel3.TabIndex = 2;
            // 
            // btnNhanvien
            // 
            this.btnNhanvien.BorderRadius = 15;
            this.btnNhanvien.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNhanvien.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNhanvien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNhanvien.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNhanvien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNhanvien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNhanvien.FillColor = System.Drawing.Color.Teal;
            this.btnNhanvien.FillColor2 = System.Drawing.Color.LightBlue;
            this.btnNhanvien.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhanvien.ForeColor = System.Drawing.Color.White;
            this.btnNhanvien.Image = global::IRT_Management_Project.Properties.Resources.staff;
            this.btnNhanvien.ImageSize = new System.Drawing.Size(58, 58);
            this.btnNhanvien.Location = new System.Drawing.Point(5, 10);
            this.btnNhanvien.Name = "btnNhanvien";
            this.btnNhanvien.Size = new System.Drawing.Size(266, 122);
            this.btnNhanvien.TabIndex = 1;
            this.btnNhanvien.Text = "Nhân viên";
            this.btnNhanvien.Click += new System.EventHandler(this.btnNhanvien_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDonHang);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 10, 5, 0);
            this.panel2.Size = new System.Drawing.Size(276, 132);
            this.panel2.TabIndex = 1;
            // 
            // btnDonHang
            // 
            this.btnDonHang.BorderRadius = 15;
            this.btnDonHang.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDonHang.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDonHang.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDonHang.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDonHang.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDonHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDonHang.FillColor = System.Drawing.Color.Orange;
            this.btnDonHang.FillColor2 = System.Drawing.Color.OrangeRed;
            this.btnDonHang.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnDonHang.ForeColor = System.Drawing.Color.White;
            this.btnDonHang.Image = global::IRT_Management_Project.Properties.Resources.cart;
            this.btnDonHang.ImageSize = new System.Drawing.Size(60, 60);
            this.btnDonHang.Location = new System.Drawing.Point(5, 10);
            this.btnDonHang.Name = "btnDonHang";
            this.btnDonHang.Size = new System.Drawing.Size(266, 122);
            this.btnDonHang.TabIndex = 1;
            this.btnDonHang.Click += new System.EventHandler(this.btnDonHang_Click);
            // 
            // panel_Employee
            // 
            this.panel_Employee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Employee.Location = new System.Drawing.Point(0, 0);
            this.panel_Employee.Name = "panel_Employee";
            this.panel_Employee.Size = new System.Drawing.Size(1173, 709);
            this.panel_Employee.TabIndex = 1;
            this.panel_Employee.Visible = false;
            // 
            // frmMain1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1493, 778);
            this.Controls.Add(this.container);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.header);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Màn hình chính";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain1_FormClosing);
            this.Load += new System.EventHandler(this.frmMain1_Load);
            this.header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel_Username.ResumeLayout(false);
            this.menuForAvatar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.container.ResumeLayout(false);
            this.panel_Admin.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblTaskEmployee)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.guna2GradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel header;
        private Guna.UI2.WinForms.Guna2Panel panel_Username;
        private System.Windows.Forms.Label lblNameUser;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip menuForAvatar;
        private System.Windows.Forms.ToolStripMenuItem btnXemTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem btnDoiMatKhau;
        private System.Windows.Forms.ToolStripMenuItem btnDangXuat;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel_Admin;
        private System.Windows.Forms.Panel panel_Employee;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel panel4;
        private Guna.UI2.WinForms.Guna2GradientButton guna2GradientButton4;
        private System.Windows.Forms.Panel panel3;
        private Guna.UI2.WinForms.Guna2GradientButton btnNhanvien;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2GradientButton btnDonHang;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2GradientButton btnDuyetStrain;
        private System.Windows.Forms.Label lblChaoMung;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel5;
        private Guna.UI2.WinForms.Guna2DataGridView tblTaskEmployee;
        private System.Windows.Forms.ToolStripMenuItem btnSaoLuuDuLieu;
        private System.Windows.Forms.ToolStripMenuItem btnRestoreDB;
    }
}