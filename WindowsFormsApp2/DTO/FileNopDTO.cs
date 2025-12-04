using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.DTO
{
    public class FileNopDTO
    {
        public int FileID { get; set; }
        public int PC_ID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public int? FileSizeKB { get; set; }
        public string TrangThaiDuyet { get; set; }
        public string GhiChuDuyet { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public int? ApprovedBy { get; set; }
    }   
}
