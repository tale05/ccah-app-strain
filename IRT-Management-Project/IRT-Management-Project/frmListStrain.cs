using BLL;
using Components;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmListStrain : Form
    {
        StrainBLL sbll;
        InventoryBLL inventorybll;
        List<StrainCustomWithIdStrainDTO> lstData = new List<StrainCustomWithIdStrainDTO>();
        public static string strainNumberFromListStrain = string.Empty;
        public static int idStrainFromListStrain = 0;
        public frmListStrain()
        {
            InitializeComponent();
            sbll = new StrainBLL();
            inventorybll = new InventoryBLL();
            treeView1.ExpandAll();

            LoadDataStrain();
            InitializeTreeViewWithData();
            FillterData();
            lblNameUser.Text = frmLogin.fullNameEmployee;
        }
        public async void FillterData()
        {
            int countPhy = await sbll.CountPhylums();
            int countCls = await sbll.CountClass();
            int countGen = await sbll.CountGenus();
            int countSpe = await sbll.CountSpecies();
            int countStr = await sbll.CountStrain();

            lblThongke.Text = "Ngành (" + countPhy.ToString() + "), Lớp (" + countCls.ToString() + "), Chi (" + countGen.ToString() + "), Loài (" + countSpe.ToString() + "), Strain (" + countStr.ToString() + ")";
        }
        private void MenuStripForPanelStrain(PanelStrain panelStrain, string strainNumber, byte[] img, int idStrain)
        {
            ContextMenuStrip menuStrain = new ContextMenuStrip();
            ToolStripMenuItem detailMenuItem = new ToolStripMenuItem("Xem Chi Tiết");
            ToolStripMenuItem imageViewMenuItem = new ToolStripMenuItem("Xem Hình Ảnh");
            menuStrain.Items.AddRange(new ToolStripItem[] { detailMenuItem, imageViewMenuItem });
            detailMenuItem.Click += (sender, e) => {
                strainNumberFromListStrain = strainNumber;
                idStrainFromListStrain = idStrain;
                frmDetailOneStrain frm = new frmDetailOneStrain();
                frm.ShowDialog();
            };
            imageViewMenuItem.Click += (sender, e) => {
                frmImageStrain frm = new frmImageStrain(img);
                frm.ShowDialog();
            };
            //---------------------------
            Label numberStrain = panelStrain.Controls.Find("numberStrain", true).FirstOrDefault() as Label;
            if (numberStrain != null)
            {
                numberStrain.ContextMenuStrip = menuStrain;
            }
            //---------------------------
            PictureBox pictureBox = panelStrain.Controls.Find("imgStrain", true).FirstOrDefault() as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.ContextMenuStrip = menuStrain;
            }
            //---------------------------
            TableLayoutPanel tableLayout = panelStrain.Controls.Find("tableLayoutPanel2", true).FirstOrDefault() as TableLayoutPanel;
            foreach (Control item in tableLayout.Controls)
            {
                if (item is Label)
                {
                    item.ContextMenuStrip = menuStrain;
                }    
            }
        }
        public async void LoadDataStrain()
        {
            lstData = await sbll.LoadData();
            layoutAllStrains.Controls.Clear();
            foreach (StrainCustomWithIdStrainDTO item in lstData)
            {
                int rs = await inventorybll.CheckStockStrain(item.idStrain);
                PanelStrain panelStrain = new PanelStrain(item.imageStrain, item.strainNumber,
                    item.nameSpecies, item.nameGenus, item.nameClass, item.namePhylum, rs);
                panelStrain.Margin = new Padding(7);
                layoutAllStrains.Controls.Add(panelStrain);
                MenuStripForPanelStrain(panelStrain, item.strainNumber.Trim(), item.imageStrain, item.idStrain);
            }
            layoutAllStrains.AutoScroll = true;
            layoutAllStrains.FlowDirection = FlowDirection.LeftToRight;
            layoutAllStrains.WrapContents = true;
        }
        public async void SearchDataStrain(string strainNumber)
        {
            List<StrainCustomWithIdStrainDTO> lstSearch = await sbll.SearchData(strainNumber);
            layoutAllStrains.Controls.Clear();
            foreach (StrainCustomWithIdStrainDTO item in lstSearch)
            {
                int rs = await inventorybll.CheckStockStrain(item.idStrain);
                PanelStrain panelStrain = new PanelStrain(item.imageStrain, item.strainNumber,
                    item.nameSpecies, item.nameGenus, item.nameClass, item.namePhylum, rs);
                panelStrain.Margin = new Padding(7);
                layoutAllStrains.Controls.Add(panelStrain);
                MenuStripForPanelStrain(panelStrain, item.strainNumber, item.imageStrain, item.idStrain);
            }
            layoutAllStrains.AutoScroll = true;
            layoutAllStrains.FlowDirection = FlowDirection.LeftToRight;
            layoutAllStrains.WrapContents = true;
        }
        public async void FillDataByPhylum(string namePhylum)
        {
            List<StrainCustomWithIdStrainDTO> lstSearch = await sbll.FillDataByPhylum(namePhylum);
            layoutAllStrains.Controls.Clear();
            foreach (StrainCustomWithIdStrainDTO item in lstSearch)
            {
                int rs = await inventorybll.CheckStockStrain(item.idStrain);
                PanelStrain panelStrain = new PanelStrain(item.imageStrain, item.strainNumber,
                    item.nameSpecies, item.nameGenus, item.nameClass, item.namePhylum, rs);
                panelStrain.Margin = new Padding(7);
                layoutAllStrains.Controls.Add(panelStrain);
                MenuStripForPanelStrain(panelStrain, item.strainNumber, item.imageStrain, item.idStrain);
            }
            layoutAllStrains.AutoScroll = true;
            layoutAllStrains.FlowDirection = FlowDirection.LeftToRight;
            layoutAllStrains.WrapContents = true;
        }
        public async void FillDataBySpecies(string nameSpecies)
        {
            List<StrainCustomWithIdStrainDTO> lstSearch = await sbll.FillDataBySpecies(nameSpecies);
            layoutAllStrains.Controls.Clear();
            foreach (StrainCustomWithIdStrainDTO item in lstSearch)
            {
                int rs = await inventorybll.CheckStockStrain(item.idStrain);
                PanelStrain panelStrain = new PanelStrain(item.imageStrain, item.strainNumber,
                    item.nameSpecies, item.nameGenus, item.nameClass, item.namePhylum, rs);
                panelStrain.Margin = new Padding(7);
                layoutAllStrains.Controls.Add(panelStrain);
                MenuStripForPanelStrain(panelStrain, item.strainNumber, item.imageStrain, item.idStrain);
            }
            layoutAllStrains.AutoScroll = true;
            layoutAllStrains.FlowDirection = FlowDirection.LeftToRight;
            layoutAllStrains.WrapContents = true;
        }
        public async void FillDataByGenus(string nameGenus)
        {
            List<StrainCustomWithIdStrainDTO> lstSearch = await sbll.FillDataByGenus(nameGenus);
            layoutAllStrains.Controls.Clear();
            foreach (StrainCustomWithIdStrainDTO item in lstSearch)
            {
                int rs = await inventorybll.CheckStockStrain(item.idStrain);
                PanelStrain panelStrain = new PanelStrain(item.imageStrain, item.strainNumber,
                    item.nameSpecies, item.nameGenus, item.nameClass, item.namePhylum, rs);
                panelStrain.Margin = new Padding(7);
                layoutAllStrains.Controls.Add(panelStrain);
                MenuStripForPanelStrain(panelStrain, item.strainNumber, item.imageStrain, item.idStrain);
            }
            layoutAllStrains.AutoScroll = true;
            layoutAllStrains.FlowDirection = FlowDirection.LeftToRight;
            layoutAllStrains.WrapContents = true;
        }
        public async void FillDataByClass(string nameClass)
        {
            List<StrainCustomWithIdStrainDTO> lstSearch = await sbll.FillDataByClass(nameClass);
            layoutAllStrains.Controls.Clear();
            foreach (StrainCustomWithIdStrainDTO item in lstSearch)
            {
                int rs = await inventorybll.CheckStockStrain(item.idStrain);
                PanelStrain panelStrain = new PanelStrain(item.imageStrain, item.strainNumber,
                    item.nameSpecies, item.nameGenus, item.nameClass, item.namePhylum, rs);
                panelStrain.Margin = new Padding(7);
                layoutAllStrains.Controls.Add(panelStrain);
                MenuStripForPanelStrain(panelStrain, item.strainNumber, item.imageStrain, item.idStrain);
            }
            layoutAllStrains.AutoScroll = true;
            layoutAllStrains.FlowDirection = FlowDirection.LeftToRight;
            layoutAllStrains.WrapContents = true;
        }
        private async void InitializeTreeViewWithData()
        {
            treeView1.BeginUpdate();
            try
            {
                TreeNode allNode = treeView1.Nodes.Add("All");
                allNode.ForeColor = Color.Black;
                allNode.NodeFont = new Font(treeView1.Font.FontFamily, 14, FontStyle.Regular);

                List<string> phylums = await sbll.getListPhylum();
                foreach (string phylum in phylums)
                {
                    TreeNode phylumNode = allNode.Nodes.Add("phylum: " + phylum.Trim());
                    phylumNode.ForeColor = Color.Black;
                    phylumNode.NodeFont = new Font(treeView1.Font.FontFamily, 14, FontStyle.Regular);

                    List<string> classes = await sbll.getListClassByPhylum(phylum.Trim());
                    foreach (string cls in classes)
                    {
                        TreeNode classNode = phylumNode.Nodes.Add("class: " + cls.Trim());
                        classNode.ForeColor = Color.Black;
                        classNode.NodeFont = new Font(treeView1.Font.FontFamily, 12, FontStyle.Regular);

                        List<string> genera = await sbll.getListGenusByClass(cls.Trim());
                        foreach (string genus in genera)
                        {
                            TreeNode genusNode = classNode.Nodes.Add("genus: " + genus.Trim());
                            genusNode.ForeColor = Color.Black;
                            genusNode.NodeFont = new Font(treeView1.Font.FontFamily, 12, FontStyle.Regular);

                            List<string> species = await sbll.getListSpeciesByGenus(genus.Trim());
                            foreach (string spec in species)
                            {
                                TreeNode speciesNode = genusNode.Nodes.Add("species: " + spec.Trim());
                                speciesNode.ForeColor = Color.Black;
                                speciesNode.NodeFont = new Font(treeView1.Font.FontFamily, 12, FontStyle.Regular);
                            }
                        }
                    }
                }
            }
            finally
            {
                treeView1.ItemHeight = 40;
                treeView1.Scrollable = true;
                treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView1_NodeMouseClick);
                treeView1.ExpandAll();
                treeView1.EndUpdate();
            }
        }


        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string nodeText = e.Node.Text;
            int colonIndex = nodeText.IndexOf(':');
            if (e.Node.Text.StartsWith("phylum: "))
            {
                string cleanText = nodeText.Substring(colonIndex + 2);
                //MessageBox.Show(cleanText.Trim());
                FillDataByPhylum(cleanText.Trim());
            }
            else if (e.Node.Text.StartsWith("class: "))
            {
                string cleanText = nodeText.Substring(colonIndex + 2);
                //MessageBox.Show(cleanText.Trim());
                FillDataByClass(cleanText);
            }
            else if (e.Node.Text.StartsWith("genus: "))
            {
                string cleanText = nodeText.Substring(colonIndex + 2);
                //MessageBox.Show(cleanText.Trim());
                FillDataByGenus(cleanText);
            }
            else if (e.Node.Text.StartsWith("species: "))
            {
                string cleanText = nodeText.Substring(colonIndex + 2);
                //MessageBox.Show(cleanText.Trim());
                FillDataBySpecies(cleanText);
            }
            else if (e.Node.Text.Equals("All"))
            {
                LoadDataStrain();
            }
        }

        private void frmListStrain_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 newForm = new frmMain1();
            newForm.Show();
            Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                LoadDataStrain();
            }
            else
            {
                SearchDataStrain(txtSearch.Text);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    LoadDataStrain();
                }
                else
                {
                    SearchDataStrain(txtSearch.Text);
                }
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            List<StrainCustomExcelDTO> list = await sbll.GetDataToExportExcel();
            System.Data.DataTable strainData = ConvertToDataTable(list);

            if (strainData.Rows.Count > 0)
            {
                SaveDataToExcel(strainData);
            }
            else
            {
                MessageBox.Show("No data found to export.");
            }
        }

        private System.Data.DataTable ConvertToDataTable(List<StrainCustomExcelDTO> list)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("idStrain", typeof(int));
            dataTable.Columns.Add("strainNumber", typeof(string));
            dataTable.Columns.Add("nameSpecies", typeof(string));
            dataTable.Columns.Add("scientificName", typeof(string));
            dataTable.Columns.Add("synonymStrain", typeof(string));
            dataTable.Columns.Add("formerName", typeof(string));
            dataTable.Columns.Add("commonName", typeof(string));
            dataTable.Columns.Add("cellSize", typeof(string));
            dataTable.Columns.Add("organization", typeof(string));
            dataTable.Columns.Add("medium", typeof(string));
            dataTable.Columns.Add("temperature", typeof(string));
            dataTable.Columns.Add("lightIntensity", typeof(string));
            dataTable.Columns.Add("duration", typeof(string));
            dataTable.Columns.Add("characteristics", typeof(string));
            dataTable.Columns.Add("collectionSite", typeof(string));
            dataTable.Columns.Add("continent", typeof(string));
            dataTable.Columns.Add("country", typeof(string));
            dataTable.Columns.Add("isolationSource", typeof(string));
            dataTable.Columns.Add("toxinProducer", typeof(string));
            dataTable.Columns.Add("stateOfStrain", typeof(string));
            dataTable.Columns.Add("agitationResistance", typeof(string));
            dataTable.Columns.Add("remarks", typeof(string));
            dataTable.Columns.Add("geneInformation", typeof(string));
            dataTable.Columns.Add("publications", typeof(string));
            dataTable.Columns.Add("recommendedForTeaching", typeof(string));
            dataTable.Columns.Add("dateAdd", typeof(string));
            dataTable.Columns.Add("dateApproval", typeof(string));
            dataTable.Columns.Add("status", typeof(string));

            foreach (var item in list)
            {
                DataRow row = dataTable.NewRow();
                row["idStrain"] = item.idStrain;
                row["strainNumber"] = item.strainNumber;
                row["nameSpecies"] = item.nameSpecies;
                row["scientificName"] = item.scientificName;
                row["synonymStrain"] = item.synonymStrain;
                row["formerName"] = item.formerName;
                row["commonName"] = item.commonName;
                row["cellSize"] = item.cellSize;
                row["organization"] = item.organization;
                row["medium"] = item.medium;
                row["temperature"] = item.temperature;
                row["lightIntensity"] = item.lightIntensity;
                row["duration"] = item.duration;
                row["characteristics"] = item.characteristics;
                row["collectionSite"] = item.collectionSite;
                row["continent"] = item.continent;
                row["country"] = item.country;
                row["isolationSource"] = item.isolationSource;
                row["toxinProducer"] = item.toxinProducer;
                row["stateOfStrain"] = item.stateOfStrain;
                row["agitationResistance"] = item.agitationResistance;
                row["remarks"] = item.remarks;
                row["geneInformation"] = item.geneInformation;
                row["publications"] = item.publications;
                row["recommendedForTeaching"] = item.recommendedForTeaching;
                row["dateAdd"] = item.dateAdd;
                row["dateApproval"] = item.dateApproval;
                row["status"] = item.status;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private void SaveDataToExcel(System.Data.DataTable dataTable)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            if (excelApp == null)
            {
                MessageBox.Show("Excel is not properly installed.");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;
            worksheet.Name = "Danh sách strain";

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j];
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Save an Excel File",
                FileName = "Danh sách strain.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    workbook.SaveAs(filePath);
                    MessageBox.Show("Đã lưu file thành công. đường dẫn: " + filePath, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workbook.Close(0);
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                }
            }
        }
    }
}
