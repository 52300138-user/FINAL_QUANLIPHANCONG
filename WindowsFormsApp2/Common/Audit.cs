//using System;
//using System.Data.SqlClient;
//using WindowsFormsApp2.DAL;
//namespace WindowsFormsApp2.Common
//{
//    static class Audit
//    {
//        public static void Log(string actionCode, string targetInfo, string result)
//        {
//            try
//            {
//                using (var conn = Db.Open())
//                using (var cmd = new SqlCommand(@"
//INSERT INTO PermissionAudit(UserId,UserName,ActionCode,TargetInfo,Result,EventTime)
//VALUES(@uid,@un,@ac,@ti,@rs,SYSUTCDATETIME());", conn))
//                {
//                    cmd.Parameters.AddWithValue("@uid", Program.CurrentUserId);
//                    cmd.Parameters.AddWithValue("@un", Environment.UserName ?? (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@ac", actionCode ?? "");
//                    cmd.Parameters.AddWithValue("@ti", (object)targetInfo ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@rs", result ?? "SUCCESS");
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            catch { /* không để log làm crash app */ }
//        }
//    }
//}
