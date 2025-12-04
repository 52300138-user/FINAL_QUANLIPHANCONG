using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient; // Chỉ file này được phép 'using' SqlClient

namespace WindowsFormsApp2.DAL // Hoặc WindowsFormsApp2.DAL
{
    public class Db
    {
        // Connection string LocalDB 
        public static readonly string connectionString = @"Data Source=LAPTOP-HP425JPJ; Initial Catalog=QuanLiPhanCong; Integrated Security=True;";

        /// Hàm helper private chuyển từ dictionary sang sqlparameter
        private static void AddParametersToCommand(SqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null)
                return;

            foreach (var param in parameters)
            {
                // Xử lý giá trị null -> DBNull.Value một cách an toàn
                object value = param.Value ?? DBNull.Value;
                command.Parameters.Add(new SqlParameter(param.Key, value));
            }
        }

        /// Dùng cho SELECT, trả về một DataTable
        public static DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddParametersToCommand(command, parameters);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ném lỗi lên DAL/BUS để xử lý
                throw new Exception("Db Error (ExecuteQuery): " + ex.Message, ex);
            }
            return dataTable;
        }

        /// Dùng cho INSERT, UPDATE, DELETE, trả về số dòng bị ảnh hưởng
        public static int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddParametersToCommand(command, parameters);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Db Error (ExecuteNonQuery): " + ex.Message, ex);
            }
            return rowsAffected;
        }

        /// Dùng cho SELECT COUNT(*), SUM(),... trả về giá trị duy nhất (ô đầu tiên)
        public static object ExecuteScalar(string query, Dictionary<string, object> parameters = null)
        {
            object result = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddParametersToCommand(command, parameters);
                        result = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Db Error (ExecuteScalar): " + ex.Message, ex);
            }
            return result;
        }

        public static SqlConnection GetConnection()
        {
            string conn = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            return new SqlConnection(conn);
        }
    }
}