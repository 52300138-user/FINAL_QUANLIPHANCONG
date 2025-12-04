using System;

namespace WindowsFormsApp2.DTO
{
    public class CongViecDTO
    {
        // IDs
        public int CongViecID { get; set; }
        public int? KeHoachID { get; set; } // Nullable (vì có thể có CV đột xuất)
        public int NguoiGiaoID { get; set; } // TBM

        // Thông tin chung
        public string TenCongViec { get; set; }
        public string Loai { get; set; } // 'GiangDay', 'NghienCuu', 'SuKien', 'HanhChinh'
        public DateTime? HanChot { get; set; } // Nullable
        public string MucUuTien { get; set; } // 'LOW', 'MED', 'HIGH'
        public string TrangThai { get; set; } // 'MOI', 'DANG_LAM'... (Trạng thái tổng thể)
        public string MoTa { get; set; }  // Ghi chú chung của TBM
        public DateTime CreatedAt { get; set; } // (DAL tự set)

        // === Chi tiết theo Loại ===

        // Nếu Loai = 'GiangDay'
        public int? MonHocID { get; set; } // Nullable
        public string LopPhuTrach { get; set; }
        public int? SoTiet { get; set; } // Nullable

        // Loai = 'NghienCuu'
        public string MaDeTai { get; set; }

        // Loai = 'SuKien'
        public string DiaDiem { get; set; }
    }
}