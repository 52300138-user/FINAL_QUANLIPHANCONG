using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsFormsApp2.DAL;
using System.Data;

namespace WindowsFormsApp2.BUS
{
    internal class FileNopBUS
    {
        public static bool InsertFileNop(int pcId, string fileName, string filePath)
        {
            try
            {
                return FileNopDAL.InsertFileNop(pcId, fileName, filePath);
            }
            catch
            {
                MessageBox.Show("Lỗi ghi file vào database!");
                return false;
            }
        }
        public static string GetTenGVByPCID(int pcId)
        {
            return FileNopDAL.GetTenGVByPCID(pcId);
        }

        public static DataTable GetFileChoDuyet(int? userId = null, DateTime? createdDate = null)
        {
            try
            {
                return FileNopDAL.GetFilesChoDuyet(userId, createdDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu duyệt file: " + ex.Message);
                return new DataTable();
            }
        }
        public static DataTable GetFileById(int fileId)
        {
            return FileNopDAL.GetFileById(fileId);
        }
        public static bool ApproveFile(int fileId, string comment)
        {
            int userId = Program.CurrentUserId;

            return FileNopDAL.ApproveFile(fileId, userId, comment);
        }
        public static bool RejectFile(int fileId, string comment)
        {
            int userId = Program.CurrentUserId;

            return FileNopDAL.RejectFile(fileId, userId, comment);
        }


    }
}
