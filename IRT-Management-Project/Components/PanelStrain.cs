using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;
using Guna.UI2.WinForms;
using System.Resources;
using static Guna.UI2.WinForms.Suite.Descriptions;

namespace Components
{
    public partial class PanelStrain : UserControl
    {
        public PanelStrain(byte[] img, string num, string species, string genus, string cls, string phy, int stock)
        {
            InitializeComponent();

            if (stock == 1)
            {
                picWarning.Visible = false;
                lblStock.Visible = false;
            }
            else if (stock == 2)
            {
                picWarning.FillColor = Color.Red;
                picWarning.FillColor2 = Color.DarkRed;
                picWarning.Visible = true;
                lblStock.Text = "Đã hết hàng!";
                lblStock.Visible = true;
            }
            else if (stock == 3)
            {
                picWarning.FillColor = Color.Orange;
                picWarning.FillColor2 = Color.DarkOrange;
                picWarning.Visible = true;
                lblStock.Text = "Sắp hết hàng!";
                lblStock.Visible = true;
            }

            if (img == null)
            {
                imgStrain.Image = Properties.Resources.no_pictures;
            }
            else
            {
                using (MemoryStream ms = new MemoryStream(img))
                {
                    imgStrain.Image = Image.FromStream(ms);
                }
            }
            numberStrain.Text = num;
            speciesStrain.Text = species;
            genusStrain.Text = genus;
            classStrain.Text = cls;
            phylumStrain.Text = phy;

            SetRoundedShape();
        }

        private void SetRoundedShape()
        {
            GraphicsPath path = new GraphicsPath();
            int radius = 20;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            this.Region = new Region(path);
        }
    }
}
