using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.DTO
{
    public class PlanDTO
        {
            public int KeHoachID { get; set; }       
            public string TenKeHoach { get; set; } = "";   
            public string Loai { get; set; } = "";       
            public DateTime NgayBatDau { get; set; }     
            public DateTime NgayKetThuc { get; set; }    
            public int NguoiTaoID { get; set; }      
            public DateTime CreatedAt { get; set; }      
        }
    }


