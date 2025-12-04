using System.Data;

namespace WindowsFormsApp2.DTO
{
    public class BaoCaoTongHopResult
    {
        public int TongCongViec { get; set; }
        public int SoMoi { get; set; }
        public int SoDangLam { get; set; }
        public int SoChoDuyet { get; set; }
        public int SoHoanThanh { get; set; }
        public int SoTreHan { get; set; }

        public double TiLeHoanThanh { get; set; }

        // Số phân công đã có kết quả (đã nộp file)
        public int SoDaNopKetQua { get; set; }

        // Số phân công đã duyệt kết quả
        public int SoDaDuyetKetQua { get; set; }

        // Bảng chi tiết để bind ra UI / export
        public DataTable ChiTiet { get; set; }
    }
}
