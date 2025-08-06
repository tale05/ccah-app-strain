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
    public partial class frmImageStrain : Form
    {
        public frmImageStrain(byte[] img)
        {
            InitializeComponent();
            if (img == null)
            {
                picStrain.Image = Properties.Resources.no_pictures;
            }
            else
            {
                using (MemoryStream ms = new MemoryStream(img))
                {
                    picStrain.Image = Image.FromStream(ms);
                }
            }
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            for (double i = 1.0; i >= 0; i -= 0.1)
            {
                this.Opacity = i;
                await Task.Delay(5);
            }
            Close();
        }
    }
}
