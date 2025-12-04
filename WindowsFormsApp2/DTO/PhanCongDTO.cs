using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.DTO
{
    public class PhanCongDTO
    {
        public int PC_ID { get; set; } // Primary Key (của bảng PhanCong)
        public int CongViecID { get; set; } // FK
        public int UserID { get; set; } // FK (Giảng viên được giao)

        // === Phần của TBM ===
        public string VaiTro { get; set; } // "ChuTri" hoặc "HoTro"
        public DateTime? NgayGiao { get; set; } // Nullable
        public string GhiChu_TBM { get; set; }

        // === Phần của GV dùng để Báo cáo ===
        public string TrangThaiGV { get; set; } // "MOI", "DANG_LAM", "HOAN_THANH"
        public int PhanTram { get; set; }
        public string GhiChu_GV { get; set; }
        public string TepDinhKem_GV { get; set; } // Đường dẫn file
        public DateTime ThoiDiemCapNhatCuoi { get; set; } // DB tự set
    }
}
