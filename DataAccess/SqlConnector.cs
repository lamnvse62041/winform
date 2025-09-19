using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp1.Constant;

namespace ManagerDevice.DataAccess
{
    public class SqlConnector
    {
        // Chuỗi kết nối SQL Server
        //private string connectionString = @"Server=LOCALHOST\SQLEXPRESS01;Database=DeviceIT;Trusted_Connection=True;";

        // Mở kết nối
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(AppConfig.connectionString);
            return conn;
        }

        // Truy vấn dữ liệu trả về DataTable
        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi truy vấn: " + ex.Message);
                }
            }
            return dt;
        }

        // Thực thi câu lệnh Insert/Update/Delete
        public int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            int affectedRows = 0;
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        affectedRows = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi thực thi: " + ex.Message);
                }
            }
            return affectedRows;
        }
    }
}
