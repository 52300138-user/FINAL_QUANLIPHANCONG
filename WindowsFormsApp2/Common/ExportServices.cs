using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.Common
{
    public static class ExportHelper
    {
        // ================== EXCEL xuất chi tiết công việc ==================
        public static void ExportCongViecDetailToExcel(CongViecDTO congViec, DataTable dtPhanCong,string filePath)
        {
            if (congViec == null)
                throw new ArgumentNullException(nameof(congViec));

            if (dtPhanCong == null)
                throw new ArgumentNullException(nameof(dtPhanCong));

            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("ChiTietCongViec");

                int row = 1;

                // Tiêu đề
                ws.Cell(row, 1).Value = "CHI TIẾT CÔNG VIỆC";
                ws.Range(row, 1, row, 8).Merge();
                ws.Row(row).Style.Font.Bold = true;
                ws.Row(row).Style.Font.FontSize = 16;
                ws.Row(row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                row++;

                // Thông tin chung
                ws.Cell(row, 1).Value = "Tên công việc:";
                ws.Cell(row, 2).Value = congViec.TenCongViec ?? "";
                row++;

                ws.Cell(row, 1).Value = "Loại:";
                ws.Cell(row, 2).Value = ConvertLoaiToText(congViec.Loai);
                row++;

                ws.Cell(row, 1).Value = "Mức ưu tiên:";
                ws.Cell(row, 2).Value = ConvertPriorityToText(congViec.MucUuTien);
                row++;

                ws.Cell(row, 1).Value = "Hạn chót:";
                ws.Cell(row, 2).Value = congViec.HanChot.HasValue
                    ? congViec.HanChot.Value.ToString("dd/MM/yyyy")
                    : "";
                row++;

                ws.Cell(row, 1).Value = "Ghi chú:";
                ws.Cell(row, 2).Value = congViec.MoTa ?? "";
                ws.Range(row, 2, row, 8).Merge();
                ws.Row(row).Height = 30;
                ws.Row(row).Style.Alignment.WrapText = true;
                ws.Row(row).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                row += 2; // chừa 1 dòng trống

                // Header bảng phân công
                int headerRow = row;

                ws.Cell(headerRow, 1).Value = "STT";
                ws.Cell(headerRow, 2).Value = "Họ và tên";
                ws.Cell(headerRow, 3).Value = "Chức vụ";
                ws.Cell(headerRow, 4).Value = "Nhiệm vụ";
                ws.Cell(headerRow, 5).Value = "Ngày bắt đầu";
                ws.Cell(headerRow, 6).Value = "Ngày kết thúc";
                ws.Cell(headerRow, 7).Value = "Trạng thái";
                ws.Cell(headerRow, 8).Value = "Thời gian nộp";

                var headerRange = ws.Range(headerRow, 1, headerRow, 8);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                row = headerRow + 1;

                // Dữ liệu
                int stt = 1;
                foreach (DataRow dr in dtPhanCong.Rows)
                {
                    ws.Cell(row, 1).Value = stt; // STT luôn int

                    // Giả định các cột này có trong dtPhanCong (đã Join)
                    ws.Cell(row, 2).Value = dr["HoTenGV"]?.ToString() ?? "";
                    ws.Cell(row, 3).Value = dr["ChucVu"]?.ToString() ?? "";
                    ws.Cell(row, 4).Value = dr["NhiemVu"]?.ToString() ?? "";

                    // Ngày bắt đầu (NgayGiao/HanChot từ Công việc hoặc NgayBatDau từ Plan)
                    if (dr["NgayBatDau"] != DBNull.Value &&
                        DateTime.TryParse(dr["NgayBatDau"].ToString(), out DateTime ngayBD))
                    {
                        ws.Cell(row, 5).Value = ngayBD;
                        ws.Cell(row, 5).Style.DateFormat.Format = "dd/MM/yyyy";
                    }
                    else
                    {
                        ws.Cell(row, 5).Value = "";
                    }

                    // Ngày kết thúc (HanChot từ Công việc hoặc NgayKetThuc từ Plan)
                    if (dr["NgayKetThuc"] != DBNull.Value &&
                        DateTime.TryParse(dr["NgayKetThuc"].ToString(), out DateTime ngayKT))
                    {
                        ws.Cell(row, 6).Value = ngayKT;
                        ws.Cell(row, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                    }
                    else
                    {
                        ws.Cell(row, 6).Value = "";
                    }

                    ws.Cell(row, 7).Value = dr["TrangThai"]?.ToString() ?? "";

                    // Thời gian nộp
                    if (dr.Table.Columns.Contains("ThoiGianNop") &&
                        dr["ThoiGianNop"] != DBNull.Value &&
                        DateTime.TryParse(dr["ThoiGianNop"].ToString(), out DateTime tgNop))
                    {
                        ws.Cell(row, 8).Value = tgNop;
                        ws.Cell(row, 8).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                    }
                    else
                    {
                        ws.Cell(row, 8).Value = "";
                    }

                    row++;
                    stt++;
                }

                // Kẻ khung
                var dataRange = ws.Range(headerRow, 1, row - 1, 8);
                dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Columns().AdjustToContents();

                wb.SaveAs(filePath);
            }
        }

        // Hàm export chung
        public static void ExportDataTableToExcel(DataTable dtSource, string filePath, string sheetName)
        {
            if (dtSource == null)
                throw new ArgumentNullException(nameof(dtSource));

            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(sheetName);

                // 1. Thêm data (Tự động tạo header và điền dữ liệu)
                ws.Cell(1, 1).InsertTable(dtSource, false);

                // 2. Định dạng heade
                int lastColumn = dtSource.Columns.Count;
                var headerRange = ws.Range(1, 1, 1, lastColumn);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

                // 3. Auto-fit cột
                ws.Columns().AdjustToContents();

                wb.SaveAs(filePath);
            }
        }

        public static void ExportCongViecDetailToPdf(CongViecDTO congViec, DataTable dtPhanCong,string filePath)
        {
            if (congViec == null)
                throw new ArgumentNullException(nameof(congViec));

            if (dtPhanCong == null)
                throw new ArgumentNullException(nameof(dtPhanCong));

            // Khởi tạo Font Unicode (Arial)
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");

            if (!File.Exists(fontPath))
            {
                throw new FileNotFoundException("Lỗi: Không tìm thấy font Arial (arial.ttf) trong thư mục Fonts của Windows.");
            }

            // Khai báo Font (FIX LỖI CÚ PHÁP)
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fTitle = new Font(bf, 16, Font.BOLD);
            Font fBold = new Font(bf, 12, Font.BOLD);
            Font fNormal = new Font(bf, 11, Font.NORMAL);

            Document doc = new Document(PageSize.A4, 40, 40, 40, 40);

            try
            {
                // Bắt lỗi I/O trước khi mở
                if (File.Exists(filePath))
                {
                    try { File.Delete(filePath); }
                    catch (IOException)
                    {
                        throw new IOException("Lỗi: File đang được mở bởi ứng dụng khác. Vui lòng đóng file PDF trước khi xuất.");
                    }
                }

                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // --- TIÊU ĐỀ ---
                Paragraph title = new Paragraph("CHI TIẾT CÔNG VIỆC ĐƯỢC PHÂN CÔNG", fTitle);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

                // --- THÔNG TIN CHUNG (Dùng PdfPTable 2 cột) ---
                PdfPTable infoTable = new PdfPTable(2);
                infoTable.WidthPercentage = 100;
                infoTable.SetWidths(new float[] { 30f, 70f });
                infoTable.DefaultCell.Border = Rectangle.NO_BORDER;
                infoTable.DefaultCell.Padding = 2;

                infoTable.AddCell(new Phrase("Tên công việc:", fBold));
                infoTable.AddCell(new Phrase(congViec.TenCongViec ?? "", fNormal));
                infoTable.AddCell(new Phrase("Loại:", fBold));
                infoTable.AddCell(new Phrase(ConvertLoaiToText(congViec.Loai), fNormal));

                infoTable.AddCell(new Phrase("Mức ưu tiên:", fBold));
                infoTable.AddCell(new Phrase(ConvertPriorityToText(congViec.MucUuTien), fNormal));
                infoTable.AddCell(new Phrase("Hạn chót:", fBold));
                infoTable.AddCell(new Phrase(congViec.HanChot.HasValue ? congViec.HanChot.Value.ToString("dd/MM/yyyy") : "N/A", fNormal));

                doc.Add(infoTable);
                doc.Add(new Paragraph("\n"));

                // --- BẢNG CHI TIẾT PHÂN CÔNG (8 CỘT) ---
                doc.Add(new Paragraph("Chi tiết Phân công:", fBold));

                PdfPTable table = new PdfPTable(8);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 5f, 15f, 10f, 15f, 15f, 15f, 15f, 10f });
                table.SpacingBefore = 10f;

                // Header
                string[] headers = { "STT", "Họ và tên", "Chức vụ", "Nhiệm vụ", "Ngày bắt đầu", "Ngày kết thúc", "Trạng thái", "Thời gian nộp" };

                foreach (var h in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(h, fBold));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.BackgroundColor = new BaseColor(230, 230, 230);
                    cell.Padding = 5;
                    table.AddCell(cell);
                }

                // Rows
                int stt = 1;
                foreach (DataRow dr in dtPhanCong.Rows)
                {
                    // Lấy giá trị an toàn (NULL / DBNull check)
                    string chucVu = dr["ChucVu"]?.ToString() ?? "";
                    string nhiemVu = dr["NhiemVu"]?.ToString() ?? "";
                    string trangThai = dr["TrangThai"]?.ToString() ?? "";

                    // Ngày tháng
                    string ngayBDText = dr["NgayBatDau"] != DBNull.Value && DateTime.TryParse(dr["NgayBatDau"].ToString(), out DateTime ngayBD) ? ngayBD.ToString("dd/MM/yyyy") : "";
                    string ngayKTText = dr["NgayKetThuc"] != DBNull.Value && DateTime.TryParse(dr["NgayKetThuc"].ToString(), out DateTime ngayKT) ? ngayKT.ToString("dd/MM/yyyy") : "";
                    string tgNopText = dr.Table.Columns.Contains("ThoiGianNop") && dr["ThoiGianNop"] != DBNull.Value && DateTime.TryParse(dr["ThoiGianNop"].ToString(), out DateTime tgNop) ? tgNop.ToString("dd/MM/yyyy HH:mm") : "";

                    table.AddCell(new Phrase(stt.ToString(), fNormal));
                    table.AddCell(new Phrase(dr["HoTenGV"]?.ToString() ?? "", fNormal));
                    table.AddCell(new Phrase(chucVu, fNormal));
                    table.AddCell(new Phrase(nhiemVu, fNormal));
                    table.AddCell(new Phrase(ngayBDText, fNormal));
                    table.AddCell(new Phrase(ngayKTText, fNormal));
                    table.AddCell(new Phrase(trangThai, fNormal));
                    table.AddCell(new Phrase(tgNopText, fNormal));

                    stt++;
                }

                doc.Add(table);
                doc.Add(new Paragraph("\n"));

                // --- GHI CHÚ CHUNG ---
                doc.Add(new Paragraph("Ghi chú của TBM (Lịch sử sửa đổi):", fBold));
                doc.Add(new Paragraph(congViec.MoTa ?? "", fNormal));

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xuất PDF (iTextSharp Logic): " + ex.Message, ex);
            }
            finally
            {
                if (doc != null && doc.IsOpen())
                {
                    doc.Close();
                }
            }
        }

        public static void ExportBaoCaoTongHopToExcel(BaoCaoTongHopResult result,string filePath,string tieuDeBaoCao,string moTaBoLoc)
        {
            if (result == null)
                throw new ArgumentNullException("result");

            using (var wb = new XLWorkbook())
            {
                // ===== Sheet 1: Tóm tắt =====
                var wsSummary = wb.Worksheets.Add("TongQuan");

                int row = 1;

                wsSummary.Cell(row, 1).Value = tieuDeBaoCao;
                wsSummary.Range(row, 1, row, 4).Merge();
                wsSummary.Row(row).Style.Font.Bold = true;
                wsSummary.Row(row).Style.Font.FontSize = 16;
                wsSummary.Row(row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                row += 2;

                if (!string.IsNullOrWhiteSpace(moTaBoLoc))
                {
                    wsSummary.Cell(row, 1).Value = "Bộ lọc:";
                    wsSummary.Cell(row, 2).Value = moTaBoLoc;
                    wsSummary.Range(row, 2, row, 4).Merge();
                    row += 2;
                }

                wsSummary.Cell(row, 1).Value = "Tổng công việc";
                wsSummary.Cell(row, 2).Value = result.TongCongViec; row++;

                wsSummary.Cell(row, 1).Value = "Mới";
                wsSummary.Cell(row, 2).Value = result.SoMoi; row++;

                wsSummary.Cell(row, 1).Value = "Đang làm";
                wsSummary.Cell(row, 2).Value = result.SoDangLam; row++;

                wsSummary.Cell(row, 1).Value = "Chờ duyệt";
                wsSummary.Cell(row, 2).Value = result.SoChoDuyet; row++;

                wsSummary.Cell(row, 1).Value = "Hoàn thành";
                wsSummary.Cell(row, 2).Value = result.SoHoanThanh; row++;

                wsSummary.Cell(row, 1).Value = "Trễ hạn";
                wsSummary.Cell(row, 2).Value = result.SoTreHan; row++;

                wsSummary.Cell(row, 1).Value = "% Hoàn thành";
                wsSummary.Cell(row, 2).Value = result.TiLeHoanThanh.ToString("0.0") + "%"; row++;

                wsSummary.Cell(row, 1).Value = "Số phân công đã nộp kết quả";
                wsSummary.Cell(row, 2).Value = result.SoDaNopKetQua; row++;

                wsSummary.Cell(row, 1).Value = "Số phân công đã duyệt kết quả";
                wsSummary.Cell(row, 2).Value = result.SoDaDuyetKetQua; row++;

                wsSummary.Columns().AdjustToContents();

                // ===== Sheet 2: Chi tiết =====
                var wsDetail = wb.Worksheets.Add("ChiTiet");

                wsDetail.Cell(1, 1).InsertTable(result.ChiTiet, true);
                wsDetail.Columns().AdjustToContents();

                wb.SaveAs(filePath);
            }
        }

        public static void ExportBaoCaoTongHopToPdf( BaoCaoTongHopResult result, string filePath,string tieuDeBaoCao,string moTaBoLoc
        )
        {
            if (result == null)
                throw new ArgumentNullException("result");

            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            if (!File.Exists(fontPath))
                throw new FileNotFoundException("Không tìm thấy font Arial (arial.ttf).");

            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fTitle = new Font(bf, 16, Font.BOLD);
            Font fBold = new Font(bf, 12, Font.BOLD);
            Font fNormal = new Font(bf, 10, Font.NORMAL);

            Document doc = new Document(PageSize.A4.Rotate(), 30, 30, 30, 30);

            try
            {
                if (File.Exists(filePath))
                {
                    try { File.Delete(filePath); }
                    catch (IOException)
                    {
                        throw new IOException("File PDF đang mở. Vui lòng đóng trước khi xuất.");
                    }
                }

                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // Tiêu đề
                Paragraph title = new Paragraph(tieuDeBaoCao, fTitle);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

                // Bộ lọc
                if (!string.IsNullOrWhiteSpace(moTaBoLoc))
                {
                    Paragraph filter = new Paragraph("Bộ lọc: " + moTaBoLoc, fNormal);
                    doc.Add(filter);
                    doc.Add(new Paragraph("\n"));
                }

                // Bảng tóm tắt
                doc.Add(new Paragraph("I. TÓM TẮT", fBold));
                doc.Add(new Paragraph("\n"));

                PdfPTable sumTable = new PdfPTable(2);
                sumTable.WidthPercentage = 50;
                sumTable.HorizontalAlignment = Element.ALIGN_LEFT;
                sumTable.SetWidths(new float[] { 60f, 40f });

                void AddSumRow(string label, string value)
                {
                    PdfPCell c1 = new PdfPCell(new Phrase(label, fNormal));
                    c1.Padding = 4;
                    c1.Border = Rectangle.BOX;

                    PdfPCell c2 = new PdfPCell(new Phrase(value, fNormal));
                    c2.Padding = 4;
                    c2.Border = Rectangle.BOX;

                    sumTable.AddCell(c1);
                    sumTable.AddCell(c2);
                }

                AddSumRow("Tổng công việc", result.TongCongViec.ToString());
                AddSumRow("Mới", result.SoMoi.ToString());
                AddSumRow("Đang làm", result.SoDangLam.ToString());
                AddSumRow("Chờ duyệt", result.SoChoDuyet.ToString());
                AddSumRow("Hoàn thành", result.SoHoanThanh.ToString());
                AddSumRow("Trễ hạn", result.SoTreHan.ToString());
                AddSumRow("% Hoàn thành", result.TiLeHoanThanh.ToString("0.0") + "%");
                AddSumRow("Số phân công đã nộp kết quả", result.SoDaNopKetQua.ToString());
                AddSumRow("Số phân công đã duyệt kết quả", result.SoDaDuyetKetQua.ToString());

                doc.Add(sumTable);
                doc.Add(new Paragraph("\n"));

                // Bảng chi tiết
                doc.Add(new Paragraph("II. CHI TIẾT CÔNG VIỆC", fBold));
                doc.Add(new Paragraph("\n"));

                DataTable dt = result.ChiTiet;
                if (dt != null && dt.Rows.Count > 0)
                {
                    PdfPTable table = new PdfPTable(dt.Columns.Count);
                    table.WidthPercentage = 100;

                    // Header
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dt.Columns[i].ColumnName, fBold));
                        cell.BackgroundColor = new BaseColor(230, 230, 230);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.Padding = 3;
                        table.AddCell(cell);
                    }

                    // Rows
                    foreach (DataRow r in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            string text = r[i] == DBNull.Value ? "" : r[i].ToString();
                            PdfPCell cell = new PdfPCell(new Phrase(text, fNormal));
                            cell.Padding = 3;
                            table.AddCell(cell);
                        }
                    }

                    doc.Add(table);
                }
                else
                {
                    doc.Add(new Paragraph("Không có dữ liệu.", fNormal));
                }
            }
            finally
            {
                if (doc != null && doc.IsOpen())
                    doc.Close();
            }
        }

        // ================== Helper ==================
        private static string ConvertLoaiToText(string loai)
        {
            switch (loai)
            {
                case "GiangDay": return "Giảng dạy";
                case "NghienCuu": return "Nghiên cứu";
                case "SuKien": return "Sự kiện";
                case "HanhChinh": return "Hành chính";
                default: return "Khác";
            }
        }

        private static string ConvertPriorityToText(string muc)
        {
            switch (muc)
            {
                case "LOW": return "Thấp";
                case "MED": return "Trung bình";
                case "HIGH": return "Cao";
                default: return "";
            }
        }
    }
}