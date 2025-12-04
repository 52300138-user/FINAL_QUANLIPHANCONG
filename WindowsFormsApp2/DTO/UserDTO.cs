using System;

namespace WindowsFormsApp2.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = "";
        public string UserName { get; set; } = "";
        public string PassWord { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Role { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public string SDT { get; set; } = "";
        public bool IsLocked { get; set; }
    }
}
