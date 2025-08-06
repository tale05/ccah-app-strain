using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmDetailOneStrain : Form
    {
        private FormDetailOneStrainBLL dosbll;
        private static int idStrainValue = 0;
        public frmDetailOneStrain()
        {
            InitializeComponent();
        }

        private async Task LoadData(int number)
        {
            try
            {
                StrainCustomWithIdDetailDTO obj = await dosbll.LoadFullProperties(number);

                if (obj == null)
                {
                    MessageBox.Show("No data found for the specified strain number.", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (obj.ImageStrain == null)
                {
                    pictureBox1.Image = Properties.Resources.no_pictures;
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(obj.ImageStrain))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                idStrainValue = obj.idStrain;
                strainNumber.Text = obj.StrainNumber;
                phylum.Text = obj.namePhylum;
                classes.Text = obj.nameClass;
                scientificName.Text = obj.ScientificName;
                synonym.Text = obj.SynonymStrain;
                formerName.Text = obj.FormerName;
                commonName.Text = obj.CommonName;
                cellSize.Text = obj.CellSize;
                organization.Text = obj.Organization;
                collectionSite.Text = obj.CollectionSite;
                continent.Text = obj.Continent;
                country.Text = obj.Country;
                environment.Text = obj.IsolationSource;
                medium.Text = obj.mediumCondition;
                temperature.Text = obj.temperatureCondition;
                lightIntensity.Text = obj.lightIntensityCondition;
                duration.Text = obj.durationCondition;
                toxin.Text = obj.ToxinProducer;
                stateStrain.Text = obj.StateOfStrain;
                agitation.Text = obj.AgitationResistance;
                remarks.Text = obj.Remarks;
                gen.Text = obj.GeneInformation;
                publication.Text = obj.Publications;
                recommend.Text = obj.RecommendedForTeaching;
                status.Text = obj.Status;

                string str = await dosbll.GetNameAndYearIsolator(idStrainValue);
                //identify.Text = await dosbll.GetNameAndYearIdentify(idStrainValue);
                identify.Text = str;
                isolator.Text = str;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void frmDetailOneStrain_Load(object sender, EventArgs e)
        {
            dosbll = new FormDetailOneStrainBLL();
            await LoadData(frmListStrain.idStrainFromListStrain);
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
    }
}
