using System;
using System.Data.SqlClient;

public static class DbChecker
{
    public static bool TestConnection(string connectionString, out string error)
    {
        error = "";
        using (var conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
