//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using Microsoft.VisualBasic.ApplicationServices;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace WindowsFormsApp2.Exporters
//{
//    internal class ExportInvoiceToPDF
//    {
//        private string connectionString = Program.connStr;
//        private string logoFileName = Path.Combine(Application.StartupPath, "Resources", "logo.png");

//        public void ExportInvoiceToPdf(int invoiceId, string outputPath)
//        {
//            DataTable dtInvoice = GetInvoice(invoiceId);
//            DataTable dtDetails = GetInvoiceDetails(invoiceId);

//            if (dtInvoice.Rows.Count == 0)
//            {
//                MessageBox.Show("Không tìm thấy hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            DataRow inv = dtInvoice.Rows[0];

//            Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
//            try
//            {
//                PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));
//                doc.Open();

//                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
//                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
//                Font fTitle = new Font(bf, 16, Font.BOLD);
//                Font fBold = new Font(bf, 12, Font.BOLD);
//                Font fNormal = new Font(bf, 11, Font.NORMAL);
//                Font fSmall = new Font(bf, 9, Font.NORMAL);

//                // Logo
//                if (File.Exists(logoFileName))
//                {
//                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoFileName);
//                    logo.ScaleToFit(120f, 120f);
//                    logo.Alignment = Image.ALIGN_RIGHT;
//                    doc.Add(logo);
//                }

//                // Tiêu đề
//                Paragraph title = new Paragraph("HÓA ĐƠN BÁN HÀNG", fTitle);
//                title.Alignment = Element.ALIGN_CENTER;
//                doc.Add(title);
//                doc.Add(new Paragraph("\n"));

//                // Thông tin hóa đơn & khách hàng
//                PdfPTable infoTable = new PdfPTable(2);
//                infoTable.WidthPercentage = 100;
//                infoTable.SetWidths(new float[] { 60f, 40f });

//                PdfPCell leftCell = new PdfPCell();
//                leftCell.Border = Rectangle.NO_BORDER;
//                leftCell.AddElement(new Paragraph("Khách hàng: " + inv["CustomerName"], fBold));
//                leftCell.AddElement(new Paragraph("Địa chỉ: " + inv["Address"], fNormal));
//                leftCell.AddElement(new Paragraph("SĐT: " + inv["CustomerPhone"], fNormal));
//                leftCell.AddElement(new Paragraph("\n"));
//                infoTable.AddCell(leftCell);

//                PdfPCell rightCell = new PdfPCell();
//                rightCell.Border = Rectangle.NO_BORDER;
//                rightCell.AddElement(new Paragraph("Số HĐ: " + inv["InvoiceNo"], fNormal));
//                rightCell.AddElement(new Paragraph("Ngày lập: " + Convert.ToDateTime(inv["InvoiceDate"]).ToString("dd/MM/yyyy"), fNormal));
//                //rightCell.AddElement(new Paragraph("Trạng thái: " + inv["Status"], fNormal));

//                //rightCell.AddElement(new Paragraph("Người lập: " + inv["CreatedBy"], fNormal));
//                infoTable.AddCell(rightCell);

//                doc.Add(infoTable);
//                doc.Add(new Paragraph("\n"));

//                // Bảng chi tiết
//                PdfPTable table = new PdfPTable(6);
//                table.WidthPercentage = 100;
//                table.SetWidths(new float[] { 6f, 30f, 10f, 10f, 12f, 12f });

//                string[] headers = { "STT", "Tên hàng", "ĐVT", "SL", "Đơn giá", "Thành tiền" };
//                foreach (var h in headers)
//                {
//                    PdfPCell cell = new PdfPCell(new Phrase(h, fBold));
//                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                    cell.BackgroundColor = new BaseColor(230, 230, 230);
//                    cell.Padding = 5;
//                    table.AddCell(cell);
//                }

//                int stt = 1;
//                decimal total = 0;
//                foreach (DataRow row in dtDetails.Rows)
//                {
//                    decimal qty = Convert.ToDecimal(row["Quantity"]);
//                    decimal price = Convert.ToDecimal(row["UnitPrice"]);
//                    decimal amount = qty * price;

//                    table.AddCell(new PdfPCell(new Phrase(stt.ToString(), fNormal)) { HorizontalAlignment = Element.ALIGN_CENTER });
//                    table.AddCell(new PdfPCell(new Phrase(row["ProductName"].ToString(), fNormal)));
//                    table.AddCell(new PdfPCell(new Phrase(row["Unit"].ToString(), fNormal)) { HorizontalAlignment = Element.ALIGN_CENTER });
//                    table.AddCell(new PdfPCell(new Phrase(qty.ToString("N0"), fNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT });
//                    table.AddCell(new PdfPCell(new Phrase(price.ToString("N0"), fNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT });
//                    table.AddCell(new PdfPCell(new Phrase(amount.ToString("N0"), fNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT });

//                    total += amount;
//                    stt++;
//                }

//                doc.Add(table);
//                doc.Add(new Paragraph("\n"));

//                // Tổng tiền
//                PdfPTable sumTable = new PdfPTable(2);
//                sumTable.WidthPercentage = 40;
//                sumTable.HorizontalAlignment = Element.ALIGN_RIGHT;
//                sumTable.SetWidths(new float[] { 60f, 40f });

//                sumTable.AddCell(new PdfPCell(new Phrase("Tổng cộng:", fBold)) { Border = Rectangle.NO_BORDER });
//                sumTable.AddCell(new PdfPCell(new Phrase(total.ToString("N0") + " VND", fBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

//                doc.Add(sumTable);

//                // Ghi chú
//                doc.Add(new Paragraph("\nCảm ơn quý khách đã mua hàng!", fNormal));
//                doc.Add(new Paragraph("\nMọi thắc mắc vui lòng liên hệ hotline:" + "xxxxx", fNormal));


//                MessageBox.Show("Xuất hóa đơn PDF thành công:\n" + outputPath, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Lỗi khi xuất PDF:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {
//                doc.Close();
//            }
//        }

//        // Lấy thông tin hóa đơn
//        private DataTable GetInvoice(int id)
//        {
//            string sql = @"SELECT i.*, c.CustomerName, c.Address, c.SDT AS CustomerPhone
//                           FROM Invoice i
//                           LEFT JOIN Customer c ON i.CustomerID = c.CustomerID
                         
//                           WHERE i.InvoiceID = @id";
//            using (SqlConnection cn = new SqlConnection(connectionString))
//            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
//            {
//                da.SelectCommand.Parameters.AddWithValue("@id", id);
//                DataTable dt = new DataTable();
//                da.Fill(dt);
//                return dt;
//            }
//        }

//        // Lấy chi tiết hóa đơn
//        private DataTable GetInvoiceDetails(int invoiceId)
//        {
//            string sql = @"SELECT d.ProductName, d.Unit, d.Quantity, d.UnitPrice
//                           FROM InvoiceDetail d
//                           WHERE d.InvoiceID = @id";
//            using (SqlConnection cn = new SqlConnection(connectionString))
//            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
//            {
//                da.SelectCommand.Parameters.AddWithValue("@id", invoiceId);
//                DataTable dt = new DataTable();
//                da.Fill(dt);
//                return dt;
//            }
//        }
//    }
//}
