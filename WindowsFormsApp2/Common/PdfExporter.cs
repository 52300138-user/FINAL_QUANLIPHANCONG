//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Windows.Forms;
//using iTextSharp.text;
//using iTextSharp.text.pdf;



//namespace WindowsFormsApp2.Exporters
//{
//    internal class PdfExporter
//    {
//        // Đường dẫn tới logo trong thư mục Resources (nếu có)
//        private string logoFileName = Path.Combine(Application.StartupPath, "Resources", "logo.png");

//        public void ExportQuotationToPdf(int quotationId, string outputPath)
//        {
//            DataTable dtQuotation = GetQuotation(quotationId);
//            DataTable dtDetails = GetQuotationDetails(quotationId);

//            if (dtQuotation.Rows.Count == 0)
//            {
//                MessageBox.Show("Không tìm thấy báo giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            DataRow q = dtQuotation.Rows[0];

//            Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
//            try
//            {
//                PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));
//                doc.Open();

//                // ✅ Font Unicode có sẵn trong Windows — KHÔNG cần tải thêm
//                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
//                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

//                Font fTitle = new Font(bf, 16, Font.BOLD);
//                Font fBold = new Font(bf, 12, Font.BOLD);
//                Font fNormal = new Font(bf, 11, Font.NORMAL);
//                Font fSmall = new Font(bf, 9, Font.NORMAL);

//                // Logo (nếu có)
//                if (File.Exists(logoFileName))
//                {
//                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoFileName);
//                    logo.ScaleToFit(130f,130f);
//                    logo.Alignment = Image.ALIGN_RIGHT;
//                    doc.Add(logo);
//                }

//                // Title
//                Paragraph title = new Paragraph("BẢNG BÁO GIÁ", fTitle);
//                title.Alignment = Element.ALIGN_CENTER;
//                doc.Add(title);
//                doc.Add(new Paragraph("\n"));

//                // Header thông tin khách hàng / báo giá - 2 cột
//                PdfPTable infoTable = new PdfPTable(2);
//                infoTable.WidthPercentage = 100;
//                infoTable.SetWidths(new float[] { 60f, 40f });

//                PdfPCell leftCell = new PdfPCell();
//                leftCell.Border = Rectangle.NO_BORDER;
//                leftCell.AddElement(new Paragraph("Kính gửi: " + q["CustomerName"]?.ToString(), fBold));
//                leftCell.AddElement(new Paragraph("Địa chỉ: " + q["Address"]?.ToString(), fNormal));
//                leftCell.AddElement(new Paragraph("Người liên hệ: " + q["ContactPerson"]?.ToString(), fNormal));
//                leftCell.AddElement(new Paragraph("\n"));
//                infoTable.AddCell(leftCell);

//                PdfPCell rightCell = new PdfPCell();
//                rightCell.Border = Rectangle.NO_BORDER;
//                rightCell.AddElement(new Paragraph("Số báo giá: " + q["QuotationNo"]?.ToString(), fNormal));
//                rightCell.AddElement(new Paragraph("Ngày báo giá: " + Convert.ToDateTime(q["QuotationDate"]).ToString("dd/MM/yyyy"), fNormal));
//                rightCell.AddElement(new Paragraph("Hiệu lực đến: " + Convert.ToDateTime(q["ValidUntil"]).ToString("dd/MM/yyyy"), fNormal));
//                rightCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                infoTable.AddCell(rightCell);

//                doc.Add(infoTable);
//                doc.Add(new Paragraph("\n"));

//                // Mô tả ngắn
//                doc.Add(new Paragraph("Công Ty TNHH Sản Xuất Thương Mại Dịch Vụ An Lâm chuyên thực hiện: Thiết kế mẫu, in ấn và gia công ...", fNormal));
//                doc.Add(new Paragraph("\n"));

//                // Bảng chi tiết
//                PdfPTable table = new PdfPTable(7);
//                table.WidthPercentage = 100;
//                table.SetWidths(new float[] { 6f, 18f, 18f, 8f, 10f, 12f, 14f });

//                // Header
//                string[] headers = { "STT", "Mã hàng", "Tên hàng", "ĐVT", "Số lượng", "Đơn giá (VNĐ)", "Thành tiền (VNĐ)" };
//                foreach (var h in headers)
//                {
//                    PdfPCell cell = new PdfPCell(new Phrase(h, fBold));
//                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
//                    cell.BackgroundColor = new BaseColor(200, 230, 200);
//                    cell.Padding = 5;
//                    table.AddCell(cell);
//                }

//                // Rows
//                int stt = 1;
//                decimal total = 0;
//                foreach (DataRow row in dtDetails.Rows)
//                {
//                    decimal qty = row["Quantity"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Quantity"]);
//                    decimal unitPrice = row["UnitPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(row["UnitPrice"]);
//                    decimal amount = row["Amount"] == DBNull.Value ? qty * unitPrice : Convert.ToDecimal(row["Amount"]);

//                    table.AddCell(new PdfPCell(new Phrase(stt.ToString(), fNormal)) { HorizontalAlignment = Element.ALIGN_CENTER });
//                    table.AddCell(new PdfPCell(new Phrase(row["ProductCode"]?.ToString(), fNormal)) { HorizontalAlignment = Element.ALIGN_LEFT });
//                    table.AddCell(new PdfPCell(new Phrase(row["ProductName"]?.ToString(), fNormal)) { HorizontalAlignment = Element.ALIGN_LEFT });
//                    table.AddCell(new PdfPCell(new Phrase(row["Unit"]?.ToString(), fNormal)) { HorizontalAlignment = Element.ALIGN_CENTER });
//                    table.AddCell(new PdfPCell(new Phrase(qty.ToString("N0"), fNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT });
//                    table.AddCell(new PdfPCell(new Phrase(unitPrice.ToString("N0"), fNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT });
//                    table.AddCell(new PdfPCell(new Phrase(amount.ToString("N0"), fNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT });

//                    total += amount;
//                    stt++;
//                }

//                doc.Add(table);
//                doc.Add(new Paragraph("\n"));

//                // Tổng tiền + thuế + thanh toán
//                decimal vat = 0;
//                PdfPTable sumTable = new PdfPTable(2);
//                sumTable.WidthPercentage = 40;
//                sumTable.HorizontalAlignment = Element.ALIGN_RIGHT;
//                sumTable.SetWidths(new float[] { 60f, 40f });

//                sumTable.AddCell(new PdfPCell(new Phrase("Tổng tiền hàng:", fBold)) { Border = Rectangle.NO_BORDER });
//                sumTable.AddCell(new PdfPCell(new Phrase(total.ToString("N0") + " VND", fNormal)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

//                sumTable.AddCell(new PdfPCell(new Phrase("Tiền thuế GTGT:", fBold)) { Border = Rectangle.NO_BORDER });
//                sumTable.AddCell(new PdfPCell(new Phrase((total * vat / 100).ToString("N0") + " VND", fNormal)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

//                sumTable.AddCell(new PdfPCell(new Phrase("Tổng thanh toán:", fBold)) { Border = Rectangle.NO_BORDER });
//                sumTable.AddCell(new PdfPCell(new Phrase((total + total * vat / 100).ToString("N0") + " VND", fBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

//                doc.Add(sumTable);
//                doc.Add(new Paragraph("\n"));

//                // Ghi chú
//                doc.Add(new Paragraph("- Giá trên chưa bao gồm thuế GTGT 8%.", fSmall));
//                doc.Add(new Paragraph("- Chưa bao gồm chi phí thiết kế.", fSmall));
//                doc.Add(new Paragraph("- Thời gian giao hàng: 07 - 10 ngày (không tính Chủ nhật).", fSmall));
//                doc.Add(new Paragraph("\nMọi chi tiết xin liên hệ hotline: " + "xxxxxxx"?.ToString(), fNormal));

//                MessageBox.Show("Xuất PDF thành công:\n" + outputPath, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

//        private DataTable GetQuotation(int id)
//        {
//            string sql = @"SELECT q.*, c.CustomerName, c.Address, c.SDT AS CustomerPhone
//                           FROM Quotation q
//                           LEFT JOIN Customer c ON q.CustomerID = c.CustomerID
//                           WHERE q.QuotationID = @id";
//            using (SqlConnection cn = new SqlConnection(connectionString))
//            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
//            {
//                da.SelectCommand.Parameters.AddWithValue("@id", id);
//                DataTable dt = new DataTable();
//                da.Fill(dt);
//                return dt;
//            }
//        }

//        private DataTable GetQuotationDetails(int quotationId)
//        {
//            string sql = @"SELECT ProductCode, ProductName, Quantity, UnitPrice, Amount, Unit, VAT
//                           FROM QuotationDetail
//                           WHERE QuotationID = @id";
//            using (SqlConnection cn = new SqlConnection(connectionString))
//            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
//            {
//                da.SelectCommand.Parameters.AddWithValue("@id", quotationId);
//                DataTable dt = new DataTable();
//                da.Fill(dt);
//                return dt;
//            }
//        }
//    }
//}
