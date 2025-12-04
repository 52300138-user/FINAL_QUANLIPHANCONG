using System;
using System.Data;
using WindowsFormsApp2.DAL;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.BUS
{
    public static class BaoCaoBUS
    {
        public static BaoCaoTongHopResult Generate(
            string loaiKeHoach,
            string loaiCongViec,
            int? giangVienId
        )
        {
            // Lấy raw data từ DB
            DataTable raw = BaoCaoDAL.GetChiTietBaoCaoTongHop(loaiKeHoach, loaiCongViec, giangVienId);

            // Build bảng dễ đọc
            DataTable dt = BuildChiTietTable(raw);

            // Format tiếng Việt
            FormatBaoCaoColumns(dt);

            // Tạo object kết quả trả về
            BaoCaoTongHopResult result = new BaoCaoTongHopResult();
            result.ChiTiet = dt;

            // ==== Tính tổng hợp ====
            int tong = 0, soMoi = 0, soDangLam = 0, soChoDuyet = 0, soHoanThanh = 0, soTre = 0, soDaNop = 0, soDaDuyet = 0;
            DateTime today = DateTime.Today;

            foreach (DataRow row in raw.Rows)
            {
                tong++;

                string trangThai = row["TrangThaiGV"] == DBNull.Value ? "" : row["TrangThaiGV"].ToString();

                DateTime? hanChot = null;
                if (row["HanChot"] != DBNull.Value)
                {
                    DateTime tmp;
                    if (DateTime.TryParse(row["HanChot"].ToString(), out tmp))
                        hanChot = tmp.Date;
                }

                switch (trangThai)
                {
                    case "MOI": soMoi++; break;
                    case "DANG_LAM": soDangLam++; break;
                    case "CHO_DUYET": soChoDuyet++; soDaNop++; break;
                    case "HOAN_THANH": soHoanThanh++; soDaNop++; soDaDuyet++; break;
                }

                if (hanChot.HasValue && hanChot.Value < today && trangThai != "HOAN_THANH")
                    soTre++;
            }

            result.TongCongViec = tong;
            result.SoMoi = soMoi;
            result.SoDangLam = soDangLam;
            result.SoChoDuyet = soChoDuyet;
            result.SoHoanThanh = soHoanThanh;
            result.SoTreHan = soTre;
            result.SoDaNopKetQua = soDaNop;
            result.SoDaDuyetKetQua = soDaDuyet;
            result.TiLeHoanThanh = tong > 0 ? (double)soHoanThanh * 100 / tong : 0;

            return result;
        }

        // ==========================================
        //  BUILD BẢNG CHI TIẾT
        // ==========================================

        private static DataTable BuildChiTietTable(DataTable raw)
        {
            DataTable dt = new DataTable("ChiTietBaoCao");

            dt.Columns.Add("TenGV", typeof(string));
            dt.Columns.Add("TenCongViec", typeof(string));
            dt.Columns.Add("LoaiKeHoach", typeof(string));
            dt.Columns.Add("LoaiCongViec", typeof(string));
            dt.Columns.Add("TrangThai", typeof(string));
            dt.Columns.Add("NgayGiao", typeof(string));
            dt.Columns.Add("HanChot", typeof(string));
            dt.Columns.Add("TreHan", typeof(string));
            dt.Columns.Add("DaNopKetQua", typeof(string));
            dt.Columns.Add("DaDuyetKetQua", typeof(string));

            DateTime today = DateTime.Today;

            foreach (DataRow r in raw.Rows)
            {
                DataRow nr = dt.NewRow();

                string trangThaiKey = r["TrangThaiGV"]?.ToString() ?? "";

                // map trạng thái
                string trangThaiText =
                    trangThaiKey == "MOI" ? "Mới" :
                    trangThaiKey == "DANG_LAM" ? "Đang làm" :
                    trangThaiKey == "CHO_DUYET" ? "Chờ duyệt" :
                    trangThaiKey == "HOAN_THANH" ? "Hoàn thành" : "Khác";

                DateTime? ngayGiao = ParseDate(r["NgayGiao"]);
                DateTime? hanChot = ParseDate(r["HanChot"]);
                bool isLate = hanChot.HasValue && hanChot.Value < today && trangThaiKey != "HOAN_THANH";

                nr["TenGV"] = r["TenGV"]?.ToString();
                nr["TenCongViec"] = r["TenCongViec"]?.ToString();
                nr["LoaiKeHoach"] = r["LoaiKeHoach"]?.ToString();
                nr["LoaiCongViec"] = r["LoaiCongViec"]?.ToString();
                nr["TrangThai"] = trangThaiText;
                nr["NgayGiao"] = ngayGiao.HasValue ? ngayGiao.Value.ToString("dd/MM/yyyy") : "";
                nr["HanChot"] = hanChot.HasValue ? hanChot.Value.ToString("dd/MM/yyyy") : "";
                nr["TreHan"] = isLate ? "Có" : "Không";
                nr["DaNopKetQua"] = (trangThaiKey == "CHO_DUYET" || trangThaiKey == "HOAN_THANH") ? "Có" : "Không";
                nr["DaDuyetKetQua"] = (trangThaiKey == "HOAN_THANH") ? "Có" : "Không";

                dt.Rows.Add(nr);
            }

            return dt;
        }

        private static DateTime? ParseDate(object obj)
        {
            if (obj == DBNull.Value) return null;
            DateTime t;
            return DateTime.TryParse(obj.ToString(), out t) ? t : (DateTime?)null;
        }

        // ==========================================
        // FORMAT CỘT TIẾNG VIỆT
        // ==========================================

        private static void FormatBaoCaoColumns(DataTable dt)
        {
            if (dt.Columns.Contains("TenGV")) dt.Columns["TenGV"].ColumnName = "Giảng viên";
            if (dt.Columns.Contains("TenCongViec")) dt.Columns["TenCongViec"].ColumnName = "Tên công việc";
            if (dt.Columns.Contains("LoaiKeHoach")) dt.Columns["LoaiKeHoach"].ColumnName = "Loại kế hoạch";
            if (dt.Columns.Contains("LoaiCongViec")) dt.Columns["LoaiCongViec"].ColumnName = "Loại công việc";
            if (dt.Columns.Contains("TrangThai")) dt.Columns["TrangThai"].ColumnName = "Trạng thái";
            if (dt.Columns.Contains("NgayGiao")) dt.Columns["NgayGiao"].ColumnName = "Ngày giao";
            if (dt.Columns.Contains("HanChot")) dt.Columns["HanChot"].ColumnName = "Hạn chót";
            if (dt.Columns.Contains("TreHan")) dt.Columns["TreHan"].ColumnName = "Trễ hạn";
            if (dt.Columns.Contains("DaNopKetQua")) dt.Columns["DaNopKetQua"].ColumnName = "Đã nộp";
            if (dt.Columns.Contains("DaDuyetKetQua")) dt.Columns["DaDuyetKetQua"].ColumnName = "Đã duyệt";
        }
    }
}
