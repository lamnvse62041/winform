using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Constant;
using WindowsFormsApp1.Dtos;

namespace WindowsFormsApp1.DataAccess
{
    internal class UserRepository
    {
        public List<string> GetITUsers()
        {
            var itUsers = new List<string>();

            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                conn.Open();
                string query = "SELECT EmployeeID FROM Users WHERE Role = 'IT'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itUsers.Add(reader["EmployeeID"].ToString());
                    }
                }
            }

            return itUsers;
        }
        public UserDto Login(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                conn.Open();
                string query = "SELECT EmployeeID, Role FROM Users WHERE Username = @username AND PasswordHash = @password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDto
                            {
                                EmployeeID = reader["EmployeeID"].ToString(),
                                Role = reader["Role"].ToString()
                            };
                        }
                    }
                }
            }

            return null; 
        }
        internal List<UserDtoAdmin> GetAllUser()
        {
            List<UserDtoAdmin> listUser = new List<UserDtoAdmin>();

            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                string sql = @" 
                        select u.EmployeeID,u.FullName,dd.DepartmentName,u.Email,u.Username,u.PhoneNumber,u.Role,d.DeviceCode,d.DeviceName,u.CreatedAt from Users u left join UserDevices ud on u.EmployeeID = ud.EmployeeID 
                        left join Devices d on ud.DeviceCode = d.DeviceCode 
                        left join Departments dd on u.DepartmentID = dd.DepartmentID 
                        group by u.EmployeeID,u.FullName,dd.DepartmentName,u.Email,u.Username,u.PhoneNumber,u.Role,d.DeviceCode,d.DeviceName,u.CreatedAt
                        order by dd.DepartmentName
                        ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    listUser.Add(new UserDtoAdmin
                    {
                        EmployeeID = reader["EmployeeID"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Username = reader["Username"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Role = reader["Role"].ToString(),
                        DeviceCode = reader["DeviceCode"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        CreateAt = reader["CreatedAt"] != DBNull.Value
                            ? Convert.ToDateTime(reader["CreatedAt"]).ToString("yyyy-MM-dd")
                            : "",
                    });
                }
            }

            return listUser;

        }




    }
}
