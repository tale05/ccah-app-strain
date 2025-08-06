using BLL;
using DTO;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRT_Management_Project
{
    public partial class frmApprovalOrdersCustomer : Form
    {
        private FormApprovalOrdersCustomerBLL ocbll;
        private List<OrderDetailToAddBillDetailDTO> lst = new List<OrderDetailToAddBillDetailDTO>();
        private static int idOrders = 0;
        private static float totalPriceValue = 0;
        private static string
            idCustomerValue = string.Empty,
            dateOrderValue = string.Empty,
            statusValue = string.Empty,
            noteValue = string.Empty,
            deliveryAddressValue = string.Empty,
            paymentMethodValue = string.Empty,
            statusPayValue = string.Empty,

            nameCustomerValue = string.Empty;

        public frmApprovalOrdersCustomer()
        {
            InitializeComponent();
            ocbll = new FormApprovalOrdersCustomerBLL();
            FillDataBy("Đang chờ xử lý");
            lblNameUser.Text = frmLogin.fullNameEmployee;
            DisplayCountOrder();
        }

        private async void DisplayCountOrder()
        {
            btnTatCa.Text = $"Tất cả:\n{await ocbll.CountOrders()}";
            btnChoXuLy.Text = $"Đang chờ xử lý:\n{await ocbll.CountOrders1()}";
            btnDuocXuLy.Text = $"Đang được xử lý:\n{await ocbll.CountOrders2()}";
            btnDangGiaoHang.Text = $"Đang giao hàng:\n{await ocbll.CountOrders3()}";
            btnDaHoanThanh.Text = $"Đã hoàn thành:\n{await ocbll.CountOrders4()}";
        }

        private async void LoadDataGridView()
        {
            tblOrder.DataSource = await ocbll.GetData();
            DesignTable();
        }

        private async void FillDataBy(string status)
        {
            tblOrder.DataSource = await ocbll.FillDataBy(status);
            DesignTable();
        }

        private void DesignTable()
        {
            tblOrder.Columns[0].HeaderText = "Mã";
            tblOrder.Columns[1].HeaderText = "Mã KH";
            tblOrder.Columns[2].HeaderText = "Tên khách hàng";
            tblOrder.Columns[3].HeaderText = "Mã NV";
            tblOrder.Columns[4].HeaderText = "Tên nhân viên";
            tblOrder.Columns[5].HeaderText = "Ngày đặt hàng";
            tblOrder.Columns[6].HeaderText = "Tổng tiền";
            tblOrder.Columns[7].HeaderText = "Trạng thái";
            tblOrder.Columns[8].HeaderText = "Địa chỉ";
            tblOrder.Columns[9].HeaderText = "Ghi chú";
            tblOrder.Columns[10].HeaderText = "Phương thức thanh toán";
            tblOrder.Columns[11].HeaderText = "Trạng thái thanh toán";

            tblOrder.Columns[1].Visible = false;
            tblOrder.Columns[3].Visible = false;
            tblOrder.Columns[4].Visible = false;
            tblOrder.Columns[6].Visible = false;
            tblOrder.Columns[8].Visible = false;
            tblOrder.Columns[9].Visible = false;
            tblOrder.Columns[10].Visible = false;

            tblOrder.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private async void LoadDataGridViewOrderDetail(int orderId)
        {
            tblOrderDetail.DataSource = await ocbll.GetOrderDetailByIdOrder(orderId);
            DesignTableOrderDetail();
        }

        private void DesignTableOrderDetail()
        {
            tblOrderDetail.Columns[0].HeaderText = "Mã";
            tblOrderDetail.Columns[1].HeaderText = "Mã đơn hàng";
            tblOrderDetail.Columns[2].HeaderText = "Mã strain";
            tblOrderDetail.Columns[3].HeaderText = "Mã strain";
            tblOrderDetail.Columns[4].HeaderText = "Số lượng";
            tblOrderDetail.Columns[5].HeaderText = "Giá";

            tblOrderDetail.Columns[2].Visible = false;
            tblOrderDetail.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void frmApprovalOrdersCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain1 frmMain1 = new frmMain1();
            frmMain1.Show();
            Hide();
        }

        private async void tblOrder_Click(object sender, EventArgs e)
        {
            if (tblOrder.CurrentRow != null)
            {
                int i = tblOrder.CurrentRow.Index;
                idOrders = int.Parse(tblOrder.Rows[i].Cells[0].Value.ToString());
                LoadDataGridViewOrderDetail(idOrders);

                madonhang.Text = $"{tblOrder.Rows[i].Cells[0].Value}";
                khachhang.Text = $"{tblOrder.Rows[i].Cells[1].Value} - {tblOrder.Rows[i].Cells[2].Value}";
                nhanvien.Text = $"{tblOrder.Rows[i].Cells[3].Value} - {tblOrder.Rows[i].Cells[4].Value}";
                thanhtoan.Text = $"{tblOrder.Rows[i].Cells[10].Value}";
                ngaydat.Text = $"{tblOrder.Rows[i].Cells[5].Value}";
                if (tblOrder.Rows[i].Cells[6].Value != null &&
                float.TryParse(tblOrder.Rows[i].Cells[6].Value.ToString(), out float totalPrice))
                {
                    tongtien.Text = totalPrice.ToString("N2") + " đ";
                }
                else
                {
                    tongtien.Text = "0 đ";
                }
                trangthai.Text = $"{tblOrder.Rows[i].Cells[7].Value}";
                diachi.Text = $"{tblOrder.Rows[i].Cells[8].Value}";
                trangthaithanhtoan.Text = $"{tblOrder.Rows[i].Cells[11].Value}";
                note.Text = $"{tblOrder.Rows[i].Cells[9].Value}";

                if (trangthai.Text.Equals("Đang chờ xử lý"))
                {
                    btnXuLyHoaDon.Visible = true;
                    btnGiaoHang.Visible = false;
                    btnHoanThanh.Visible = false;
                    btnInHoaDon.Visible = false;
                }
                else if (trangthai.Text.Equals("Đang được xử lý"))
                {
                    btnXuLyHoaDon.Visible = false;
                    btnGiaoHang.Visible = true;
                    btnHoanThanh.Visible = false;
                    btnInHoaDon.Visible = false;
                }
                else if (trangthai.Text.Equals("Đang vận chuyển"))
                {
                    btnXuLyHoaDon.Visible = false;
                    btnGiaoHang.Visible = false;
                    btnHoanThanh.Visible = true;
                    btnInHoaDon.Visible = false;
                }
                else if (trangthai.Text.Equals("Đã hoàn thành"))
                {
                    btnXuLyHoaDon.Visible = false;
                    btnGiaoHang.Visible = false;
                    btnHoanThanh.Visible = false;
                    btnInHoaDon.Visible = true;
                }

                idCustomerValue = tblOrder.Rows[i].Cells[1].Value.ToString();
                dateOrderValue = tblOrder.Rows[i].Cells[5].Value.ToString();
                totalPriceValue = float.Parse(tblOrder.Rows[i].Cells[6].Value.ToString());
                statusValue = tblOrder.Rows[i].Cells[7].Value.ToString();
                deliveryAddressValue = tblOrder.Rows[i].Cells[8].Value.ToString();
                noteValue = tblOrder.Rows[i].Cells[9].Value.ToString();
                paymentMethodValue = tblOrder.Rows[i].Cells[10].Value.ToString();
                statusPayValue = tblOrder.Rows[i].Cells[11].Value.ToString();

                nameCustomerValue = tblOrder.Rows[i].Cells[2].Value.ToString();

                lst = await ocbll.GetListOrderDetailById(idOrders);
            }
            else
                MessageBox.Show("Bạn chưa chọn dòng nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void btnXuLyHoaDon_Click(object sender, EventArgs e)
        {
            var confirmationResult = MessageBox.Show("Bạn có chắc chắn muốn thực hiện các thay đổi không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmationResult == DialogResult.Yes)
            {
                StringBuilder messageBuilder = new StringBuilder();

                // Cập nhật trạng thái đơn hàng
                var orderUpdate = new
                {
                    idCustomer = idCustomerValue,
                    idEmployee = frmLogin.idEmployee,
                    dateOrder = DateTime.Parse(dateOrderValue).ToString("yyyy-MM-dd"),
                    totalPrice = totalPriceValue,
                    status = "Đang được xử lý",
                    note = noteValue,
                    deliveryAddress = deliveryAddressValue,
                    paymentMethod = paymentMethodValue,
                    statusOrder = statusPayValue,
                };
                string json = JsonSerializer.Serialize(orderUpdate);
                string rs = await ocbll.Update(idOrders, json);
                if (rs != null)
                {
                    messageBuilder.AppendLine("Cập nhật trạng thái thành công.");
                }
                else
                {
                    messageBuilder.AppendLine("Cập nhật trạng thái thất bại.");
                }

                // Thêm hóa đơn mới
                var newBill = new
                {
                    idOrder = idOrders,
                    idCustomer = idCustomerValue,
                    idEmployee = frmLogin.idEmployee,
                    billDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    statusOfBill = statusPayValue,
                    typeOfBill = "Hóa đơn online",
                    total = totalPriceValue,
                };
                string json1 = JsonSerializer.Serialize(newBill);
                string rs1 = await ocbll.PostBill(json1);
                if (rs1 != null)
                {
                    messageBuilder.AppendLine("Thêm hóa đơn thành công.");
                }
                else
                {
                    messageBuilder.AppendLine("Thêm hóa đơn thất bại.");
                }

                // Thêm chi tiết hóa đơn
                int successCount = 0;
                int failureCount = 0;
                string lastIdBill = await ocbll.GetLastBillId();
                foreach (OrderDetailToAddBillDetailDTO item in lst)
                {
                    var newBillDetail = new
                    {
                        idBill = lastIdBill,
                        idStrain = item.idStrain,
                        quantity = item.quantity,
                    };
                    string json2 = JsonSerializer.Serialize(newBillDetail);
                    string rs2 = await ocbll.PostBillDetail(json2);
                    if (rs2 != null)
                    {
                        successCount++;
                    }
                    else
                    {
                        failureCount++;
                    }
                }

                if (successCount > 0)
                {
                    messageBuilder.AppendLine($"Thêm {successCount} chi tiết hóa đơn thành công.");
                }
                if (failureCount > 0)
                {
                    messageBuilder.AppendLine($"Thêm {failureCount} chi tiết hóa đơn thất bại.");
                }

                MessageBox.Show(messageBuilder.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataGridView();
                LoadDataGridViewOrderDetail(0);
                ResetFiled();
            }
        }

        private async void btnGiaoHang_Click(object sender, EventArgs e)
        {
            var confirmationResult = MessageBox.Show("Bạn có chắc chắn muốn thực hiện các thay đổi không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmationResult == DialogResult.Yes)
            {
                var orderUpdate = new
                {
                    idCustomer = idCustomerValue,
                    idEmployee = frmLogin.idEmployee,
                    dateOrder = DateTime.Parse(dateOrderValue).ToString("yyyy-MM-dd"),
                    totalPrice = totalPriceValue,
                    status = "Đang vận chuyển",
                    note = noteValue,
                    deliveryAddress = deliveryAddressValue,
                    paymentMethod = paymentMethodValue,
                    statusOrder = statusPayValue,
                };
                string json = JsonSerializer.Serialize(orderUpdate);
                string rs = await ocbll.Update(idOrders, json);
                if (rs != null)
                {
                    MessageBox.Show("Cập nhật trạng thái thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    LoadDataGridViewOrderDetail(0);
                    ResetFiled();
                }
                else
                {
                    MessageBox.Show("Cập nhật trạng thái thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnHoanThanh_Click(object sender, EventArgs e)
        {
            var confirmationResult = MessageBox.Show("Bạn có chắc chắn muốn thực hiện các thay đổi không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmationResult == DialogResult.Yes)
            {
                var orderUpdate = new
                {
                    idCustomer = idCustomerValue,
                    idEmployee = frmLogin.idEmployee,
                    dateOrder = DateTime.Parse(dateOrderValue).ToString("yyyy-MM-dd"),
                    totalPrice = totalPriceValue,
                    status = "Đã hoàn thành",
                    note = noteValue,
                    deliveryAddress = deliveryAddressValue,
                    paymentMethod = paymentMethodValue,
                    statusOrder = "Đã thanh toán",
                };

                string json = JsonSerializer.Serialize(orderUpdate);
                string rs = await ocbll.Update(idOrders, json);
                bool isOrderUpdateSuccess = rs != null;

                string idBillTask = await ocbll.GetIdBillByIdOrder(idOrders);
                string rs1 = await ocbll.UpdateStatusPay(idBillTask, "Đã thanh toán");
                bool isBillUpdateSuccess = rs1 != null;

                string message = "";
                if (isOrderUpdateSuccess && isBillUpdateSuccess)
                {
                    message = "Cập nhật trạng thái đơn hàng và hóa đơn thành công.";
                }
                else if (!isOrderUpdateSuccess && !isBillUpdateSuccess)
                {
                    message = "Cập nhật trạng thái đơn hàng và hóa đơn thất bại.";
                }
                else if (isOrderUpdateSuccess)
                {
                    message = "Cập nhật trạng thái đơn hàng thành công, nhưng cập nhật trạng thái hóa đơn thất bại.";
                }
                else
                {
                    message = "Cập nhật trạng thái đơn hàng thất bại, nhưng cập nhật trạng thái hóa đơn thành công.";
                }

                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataGridView();
                LoadDataGridViewOrderDetail(0);
                ResetFiled();
            }
        }

        private async void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                var customerTask = ocbll.GetDataCustomerById(idCustomerValue);
                var idBillTask = ocbll.GetIdBillByIdOrder(idOrders);

                await Task.WhenAll(customerTask, idBillTask);

                var custom = await customerTask;
                var idBill = await idBillTask;

                var billTask = ocbll.GetDataBillById(idBill);
                var billDetailsTask = ocbll.GetDataBillDetailByIdBill(idBill);

                await Task.WhenAll(billTask, billDetailsTask);

                var bill = await billTask;
                var lst = await billDetailsTask;

                SaveFilePdfInvoice(custom, bill, lst);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ResetFiled()
        {
            madonhang.Text = string.Empty;
            nhanvien.Text = string.Empty;
            khachhang.Text = string.Empty;
            ngaydat.Text = string.Empty;
            tongtien.Text = string.Empty;
            trangthai.Text = string.Empty;
            diachi.Text = string.Empty;
            trangthaithanhtoan.Text = string.Empty;
            thanhtoan.Text = string.Empty;
            note.Text = string.Empty;
            btnXuLyHoaDon.Visible = false;
            btnGiaoHang.Visible = false;
            btnHoanThanh.Visible = false;
            DisplayCountOrder();
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void btnChoXuLy_Click(object sender, EventArgs e)
        {
            FillDataBy("Đang chờ xử lý");
        }

        private void btnDuocXuLy_Click(object sender, EventArgs e)
        {
            FillDataBy("Đang được xử lý");
        }

        private void btnDangGiaoHang_Click(object sender, EventArgs e)
        {
            FillDataBy("Đang vận chuyển");
        }

        private void btnDaHoanThanh_Click(object sender, EventArgs e)
        {
            FillDataBy("Đã hoàn thành");
        }

        private void SaveFilePdfInvoice(
            CustomerCustomDTO custom,
            BillToExportPdfDTO itemBill,
            List<BillDetailToExportPdfDTO> lstBillDetail)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF Files|*.pdf";
                    saveFileDialog.Title = "Save PDF File";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        PdfDocument document = new PdfDocument();
                        document.Info.Title = "Invoice";

                        PdfPage page = document.AddPage();
                        XGraphics gfx = XGraphics.FromPdfPage(page);

                        XFont titleFont = new XFont("Verdana", 20);
                        XFont headerFont = new XFont("Verdana", 12);
                        XFont bodyFont = new XFont("Verdana", 10);

                        Action createNewPage = () =>
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                        };

                        int yPoint = 20;

                        gfx.DrawString("Hóa đơn", titleFont, XBrushes.Black, new XRect(0, yPoint, page.Width, 40), XStringFormats.TopCenter);
                        yPoint += 40;

                        gfx.DrawString("VIỆN NGHIÊN CỨU ỨNG DỤNG VÀ CHUYỂN GIAO CÔNG NGHỆ IRT", headerFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString("140 Đ. Lê Trọng Tấn, Tây Thạnh, Tân Phú, Thành phố Hồ Chí Minh", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString("SĐT: 028 6270 6275", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString("Email: info@hufi.edu.vn", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 40;

                        gfx.DrawString("Hóa đơn của khách hàng:", headerFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString($"Mã khách hàng: {custom.IdCustomer}", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString($"Tên khách hàng: {custom.FullName}", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString($"Email: {custom.Email}", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString($"SĐT: {custom.PhoneNumber}", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString($"Địa chỉ: {custom.Address}", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 40;

                        gfx.DrawString($"Mã số hóa đơn: {itemBill.idBill}", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString($"Ngày lập: {itemBill.dateBill}", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;
                        gfx.DrawString($"{itemBill.status}", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 40;

                        gfx.DrawString($"Chi tiết hóa đơn", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width, 20), XStringFormats.TopLeft);
                        yPoint += 20;

                        gfx.DrawString("STT", headerFont, XBrushes.Black, new XRect(40, yPoint, 40, 20), XStringFormats.TopLeft);
                        gfx.DrawString("Mã chủng", headerFont, XBrushes.Black, new XRect(80, yPoint, 150, 20), XStringFormats.TopLeft);
                        gfx.DrawString("Số lượng", headerFont, XBrushes.Black, new XRect(230, yPoint, 100, 20), XStringFormats.TopLeft);
                        yPoint += 20;

                        int indexNumber = 1;

                        foreach (BillDetailToExportPdfDTO item in lstBillDetail)
                        {
                            string indexNumberValue = indexNumber.ToString();
                            if (yPoint > page.Height - 40)
                            {
                                createNewPage();
                                yPoint = 40;
                                gfx.DrawString("STT", headerFont, XBrushes.Black, new XRect(40, yPoint, 40, 20), XStringFormats.TopLeft);
                                gfx.DrawString("Mã chủng", headerFont, XBrushes.Black, new XRect(80, yPoint, 150, 20), XStringFormats.TopLeft);
                                gfx.DrawString("Số lượng", headerFont, XBrushes.Black, new XRect(230, yPoint, 100, 20), XStringFormats.TopLeft);
                                //gfx.DrawString("Đơn giá", headerFont, XBrushes.Black, new XRect(330, yPoint, 100, 20), XStringFormats.TopLeft);
                                yPoint += 20;
                            }
                            gfx.DrawString(indexNumberValue, bodyFont, XBrushes.Black, new XRect(40, yPoint, 40, 20), XStringFormats.TopLeft);
                            gfx.DrawString($"{item.strainNumber}", bodyFont, XBrushes.Black, new XRect(80, yPoint, 150, 20), XStringFormats.TopLeft);
                            gfx.DrawString($"{item.quantity}", bodyFont, XBrushes.Black, new XRect(230, yPoint, 100, 20), XStringFormats.TopLeft);
                            //gfx.DrawString($"{item.price}/1 chủng", bodyFont, XBrushes.Black, new XRect(330, yPoint, 100, 20), XStringFormats.TopLeft);
                            yPoint += 20;
                            indexNumber++;
                        }

                        if (yPoint > page.Height - 80)
                        {
                            createNewPage();
                            yPoint = 40;
                        }
                        gfx.DrawString("Tổng tiền", headerFont, XBrushes.Black, new XRect(230, yPoint, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString($"{itemBill.totalPrice}", bodyFont, XBrushes.Black, new XRect(330, yPoint, 100, 20), XStringFormats.TopLeft);
                        yPoint += 40;

                        gfx.DrawString("Đã xác nhận: ____________________", bodyFont, XBrushes.Black, new XRect(40, yPoint, page.Width - 40, 20), XStringFormats.TopLeft);
                        yPoint += 40;

                        document.Save(saveFileDialog.FileName);

                        MessageBox.Show("PDF file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
