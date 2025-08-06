using BLL;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmLogin : Form
    {
        private LoginBLL context;
        private Timer opacityTimer = new Timer();
        private Timer animationTimer;
        private int targetX;
        public static string fullNameEmployee, idEmployee, positionEmployee;
        public static byte[] imageValue;
        public static int role;
        private string captchaCode;
        private bool isCheck = true;

        public frmLogin()
        {
            InitializeComponent();
            context = new LoginBLL();
            customForm();
            animationTimer = new Timer();
            animationTimer.Interval = 1;
            animationTimer.Tick += AnimationTimer_Tick;
            GenerateCaptcha();
            txtUserName.Focus();
            txtUserName.TabIndex = 0;
            txtPassword.TabIndex = 1;
            txtCaptcha.TabIndex = 2;
        }

        public async void LoginForEmployee()
        {
            if (txtUserName.Text.Equals(string.Empty))
            {
                MessageBox.Show("Chưa nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCaptcha.Text = string.Empty;
                GenerateCaptcha();
            }
            else
            {
                if (txtPassword.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Chưa nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCaptcha.Text = string.Empty;
                    GenerateCaptcha();
                }
                else
                {
                    if (txtCaptcha.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Chưa nhập mã xác thực!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCaptcha.Text = string.Empty;
                        GenerateCaptcha();
                    }
                    else
                    {
                        string user = await context.GetUserNameAsync(txtUserName.Text);
                        string pass = await context.GetPasswordAsync(txtUserName.Text);
                        string status = await context.GetActivityAsync(txtUserName.Text);
                        role = (int)await context.GetRoleIdByUserNameAsync(txtUserName.Text.Trim());
                        if (txtCaptcha.Text.Equals(captchaCode, StringComparison.OrdinalIgnoreCase))
                        {
                            if (!(user.Equals(txtUserName.Text.Trim()) && VerifyPassword(txtPassword.Text, pass)))
                            {
                                GenerateCaptcha();
                                MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                if (role <= 3)
                                {
                                    if (status.Equals("Đang hoạt động"))
                                    {
                                        fullNameEmployee = await context.GetFullNameEmployeeByUsernameAsync(txtUserName.Text.Trim());
                                        idEmployee = await context.GetIdEmployeeByUsernameAsync(txtUserName.Text.Trim());
                                        positionEmployee = await context.GetPositionByUsernameAsync(txtUserName.Text.Trim());
                                        imageValue = await context.GetEmployeeImageAsync(txtUserName.Text.Trim());

                                        frmMain1 frm = new frmMain1();
                                        frm.Show();
                                        Hide();
                                    }
                                    else
                                    {
                                        GenerateCaptcha();
                                        MessageBox.Show("Tài khoản đã bị khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }    
                                }
                                else
                                {
                                    GenerateCaptcha();
                                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }    
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect captcha.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCaptcha.Text = string.Empty;
                            GenerateCaptcha();
                        }

                    }
                }
            }
        }


        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private void customForm()
        {
            this.Visible = true;
            effectOpenForm();
            SetRoundedForm();
            SetTextboxDefault();
            picBackground.Location = new Point(0, 0);
            picBackground.Size = new Size(325, 484);
            btnHidePassword.Visible = false;
            btnHidePasswordAd.Visible = false;
            panelUser.Visible = true;
            panelAdmin.Visible = false;
        }

        private void SetTextboxDefault()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUserNameAdmin.Text = string.Empty;
            txtPasswordAdmin.Text = string.Empty;
        }

        private void ResetTabIndex(Control.ControlCollection controls)
        {
            int tabIndex = 0;

            foreach (Control control in controls)
            {
                if (control.HasChildren)
                {
                    ResetTabIndex(control.Controls);
                }
                control.TabIndex = tabIndex++;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            targetX = 325;
            panelUser.Visible = false;
            panelAdmin.Visible = true;
            //SetTextboxDefault();
            animationTimer.Start();
            checkBoxUser.Checked = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            targetX = 0;
            panelUser.Visible = true;
            panelAdmin.Visible = false;
            //SetTextboxDefault();
            animationTimer.Start();
            checkBoxAdmin.Checked = false;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int sobuoc = 25;
            if (picBackground.Location.X < targetX)
            {
                int khoangcach = targetX - picBackground.Location.X;
                picBackground.Location = new Point(picBackground.Location.X + Math.Min(sobuoc, khoangcach), picBackground.Location.Y);
            }
            else if (picBackground.Location.X > targetX)
            {
                int khoangcach = picBackground.Location.X - targetX;
                picBackground.Location = new Point(picBackground.Location.X - Math.Min(sobuoc, khoangcach), picBackground.Location.Y);
            }
            else
            {
                animationTimer.Stop();
            }
        }

        private void effectOpenForm()
        {
            this.Opacity = 0;
            opacityTimer.Interval = 5;
            opacityTimer.Tick += new EventHandler(OnTimerTick);
            opacityTimer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0)
                this.Opacity += 0.09;
            else
                opacityTimer.Stop();
        }

        private void SetRoundedForm()
        {
            int radius = 30;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddLine(radius, 0, this.Width - radius, 0);
            path.AddArc(this.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddLine(this.Width, radius, this.Width, this.Height - radius);
            path.AddArc(this.Width - radius * 2, this.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddLine(this.Width - radius, this.Height, radius, this.Height);
            path.AddArc(0, this.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.AddLine(0, this.Height - radius, 0, radius);
            this.Region = new Region(path);
        }

        private void btnViewPassword_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '\0';
            btnViewPassword.Visible = false;
            btnHidePassword.Visible = true;
        }

        private void btnHidePassword_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            btnViewPassword.Visible = true;
            btnHidePassword.Visible = false;
        }

        private void btnViewPasswordAd_Click(object sender, EventArgs e)
        {
            txtPasswordAdmin.PasswordChar = '\0';
            btnViewPasswordAd.Visible = false;
            btnHidePasswordAd.Visible = true;
        }

        private void btnHidePasswordAd_Click(object sender, EventArgs e)
        {
            txtPasswordAdmin.PasswordChar = '*';
            btnViewPasswordAd.Visible = true;
            btnHidePasswordAd.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmMain1.isOpenForm = true;
            Environment.Exit(0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginForEmployee();
        }

        private void btnLoginAdmin_Click(object sender, EventArgs e)
        {
            frmListStrain frm = new frmListStrain();
            frm.Show();
            this.Visible = false;
        }

        private void btnOpenFacebook_Click(object sender, EventArgs e)
        {
            string url = "https://www.google.com.vn/";
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở trang web: " + ex.Message);
            }
        }

        private void btnOpenWeb_Click(object sender, EventArgs e)
        {
            string url = "https://www.google.com.vn/";
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở trang web: " + ex.Message);
            }
        }

        private void btnOpenLink_Click(object sender, EventArgs e)
        {
            string url = "https://www.google.com.vn/";
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở trang web: " + ex.Message);
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkBoxUser.Checked)
            {
                Properties.Settings.Default.TextBox1Value = txtUserName.Text;
                Properties.Settings.Default.TextBox2Value = txtPassword.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.TextBox1Value = string.Empty;
                Properties.Settings.Default.TextBox2Value = string.Empty;
                Properties.Settings.Default.Save();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //isCheck = frmMain1.isOpenForm;
            //if (isCheck == false)
            //{
            //    txtUserName.Text = frmMain1.userNameFormMain;
            //    txtPassword.Text = frmMain1.passwordFormMain;
            //}
            //else
            //{
            //    txtUserName.Text = Properties.Settings.Default.TextBox1Value;
            //    txtPassword.Text = Properties.Settings.Default.TextBox2Value;

            //    if (txtUserName.Text.Equals(string.Empty) || txtPassword.Text.Equals(string.Empty))
            //        checkBoxUser.Enabled = false;
            //    else
            //        checkBoxUser.Enabled = true;
            //}

            txtUserName.Text = Properties.Settings.Default.TextBox1Value;
            txtPassword.Text = Properties.Settings.Default.TextBox2Value;
        }

        private void txtForUser_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text.Equals(string.Empty) || txtPassword.Text.Equals(string.Empty))
                checkBoxUser.Enabled = false;
            else
                checkBoxUser.Enabled = true;
        }

        private void txtForAdmin_TextChanged(object sender, EventArgs e)
        {
            if (txtUserNameAdmin.Text.Equals(string.Empty) || txtPasswordAdmin.Text.Equals(string.Empty))
                checkBoxAdmin.Enabled = false;
            else
                checkBoxAdmin.Enabled = true;
        }

        private void GenerateCaptcha()
        {
            captchaCode = GenerateRandomCode(4);
            Bitmap bitmap = new Bitmap(200, 50);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.DrawString(captchaCode, new Font("Arial", 20), Brushes.Gray, 5, 2);
                DrawRandomLines(g, bitmap);
            }

            bitmap = ApplyBlurEffect(bitmap);
            bitmap = ApplySimpleDistortion(bitmap);

            pictureBoxCaptcha.Image = bitmap;
        }

        private Bitmap ApplySimpleDistortion(Bitmap image)
        {
            Bitmap distorted = new Bitmap(image.Width, image.Height);
            Random rand = new Random();
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (rand.Next(20) == 1)
                    {
                        int k = rand.Next(20) - 10;
                        int l = rand.Next(20) - 10;
                        if (i + k >= 0 && i + k < image.Width && j + l >= 0 && j + l < image.Height)
                        {
                            distorted.SetPixel(i + k, j + l, image.GetPixel(i, j));
                        }
                    }
                    else
                    {
                        distorted.SetPixel(i, j, image.GetPixel(i, j));
                    }
                }
            }
            return distorted;
        }

        private Bitmap ApplyBlurEffect(Bitmap image)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
                g.FillRectangle(new SolidBrush(Color.FromArgb(30, 100, 200, 255)), rectangle);
            }
            return image;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        private void txtForUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginForEmployee();
            }
        }

        private void DrawRandomLines(Graphics g, Bitmap bitmap)
        {
            Random rand = new Random();
            Pen pen = new Pen(Color.Gray, 2);
            for (int i = 0; i < 5; i++)
            {
                int x1 = rand.Next(bitmap.Width);
                int y1 = rand.Next(bitmap.Height);
                int x2 = rand.Next(bitmap.Width);
                int y2 = rand.Next(bitmap.Height);
                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        private string GenerateRandomCode(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
