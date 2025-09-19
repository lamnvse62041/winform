using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WindowsFormsApp1.Constant;
using WindowsFormsApp1.Dtos;

namespace WindowsFormsApp1.Repositories
{
    public class TicketRepository
    {
        private string connectionString = AppConfig.connectionString;

        // Lấy tất cả tickets
        public List<TicketDto> GetAllTickets()
        {
            List<TicketDto> tickets = new List<TicketDto>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT t.TicketID,
       t.EmployeeID,
       t.DeviceCode,
       ts.StatusName,
       t.Description,
       dt.DeviceTypeName,
       dt.DeviceTypeID,
       t.AssignedIT,
       t.CreatedAt,
       t.ClosedAt,
       t.ITNote,
       t.RepairDetail,
       t.AdminConfirm,
       t.TempDevice,
       t.UserConfirm,
       t.DeviceTempID
FROM Tickets t
LEFT JOIN TicketStatus ts ON t.StatusID = ts.StatusID
LEFT JOIN Devices d ON t.DeviceCode = d.DeviceCode
LEFT JOIN DeviceTypes dt ON d.DeviceTypeID = dt.DeviceTypeID
WHERE t.IsDeleted = 0;
";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tickets.Add(new TicketDto
                    {
                        TicketID = reader["TicketID"].ToString(),
                        EmployeeID = reader["EmployeeID"].ToString(),
                        DeviceCode = reader["DeviceCode"].ToString(),
                        Status = reader["StatusName"].ToString(), // đổi từ ID sang tên
                        DeviceType = reader["DeviceTypeName"].ToString(),
                        Description = reader["Description"].ToString(),
                        AssignedIT = reader["AssignedIT"].ToString(),

                        CreatedAt = reader["CreatedAt"] != DBNull.Value
                            ? Convert.ToDateTime(reader["CreatedAt"]).ToString("yyyy-MM-dd")
                            : "",

                        ClosedAt = reader["ClosedAt"] != DBNull.Value
                            ? Convert.ToDateTime(reader["ClosedAt"]).ToString("yyyy-MM-dd")
                            : "",

                        ITNote = reader["ITNote"].ToString(),
                        RepairDetail = reader["RepairDetail"].ToString(),
                        AdminConfirm = reader["AdminConfirm"].ToString(),
                        TempDevice = reader["TempDevice"].ToString(),
                        UserConfirm = reader["UserConfirm"].ToString(),
                        DeviceTypeID = reader["DeviceTypeID"].ToString(),
                        DeviceTempID = reader["DeviceTempID"].ToString(),
                    });
                }
            }

            return tickets;
        }


        // Thêm ticket mới
        public string AddTicket(TicketDto ticket, out string newTicketId)
        {
            newTicketId = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Check EmployeeID + DeviceCode có tồn tại không
                string checkSql = @"SELECT COUNT(*) 
                            FROM UserDevices 
                            WHERE EmployeeID = @EmployeeID AND DeviceCode = @DeviceCode";

                using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@EmployeeID", ticket.EmployeeID);
                    checkCmd.Parameters.AddWithValue("@DeviceCode", ticket.DeviceCode);

                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists == 0)
                    {
                        return "EmployeeID và DeviceCode không khớp hoặc không tồn tại.";
                    }
                }

                // 2. Check thiết bị đã có ticket chưa xử lý chưa
                string checkTicketSql = @"SELECT COUNT(*) 
                                  FROM Tickets 
                                  WHERE DeviceCode = @DeviceCode 
                                  AND StatusID NOT IN (3, 4)     
                                  AND IsDeleted = 0";
;

                using (SqlCommand checkTicketCmd = new SqlCommand(checkTicketSql, conn))
                {
                    checkTicketCmd.Parameters.AddWithValue("@DeviceCode", ticket.DeviceCode);

                    int openTickets = (int)checkTicketCmd.ExecuteScalar();
                    if (openTickets > 0)
                    {
                        return "Thiết bị này đã có ticket đang xử lý. Không thể tạo thêm.";
                    }
                }

                // 3. Insert ticket và lấy TicketID
                string sql = @"INSERT INTO Tickets 
                       (EmployeeID, DeviceCode, StatusID, Description)
                       VALUES (@EmployeeID, @DeviceCode, 1, @Description);
                       SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", ticket.EmployeeID);
                    cmd.Parameters.AddWithValue("@DeviceCode", ticket.DeviceCode);
                    cmd.Parameters.AddWithValue("@Description", ticket.Description);

                    object result = cmd.ExecuteScalar();
                    newTicketId = result.ToString();
                }

                return "success";
            }
        }

        public void UpdateTicketField(string ticketId, string fieldName, string newValue)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Tickets SET {fieldName} = @Value WHERE TicketID = @TicketID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Value", (object)newValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TicketID", ticketId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ConfirmUserTicket(string ticketId, string userConfirmValue)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql;

                if (userConfirmValue == "Hoàn thành")
                {
                    sql = @"UPDATE Tickets 
                    SET UserConfirm = @UserConfirm, 
                        ClosedAt = GETDATE(),
                        TempDevice = @TempDevice,
                        DeviceTempID = @DeviceTempID
                    WHERE TicketID = @TicketID";
                }
                else
                {
                    sql = @"UPDATE Tickets 
                    SET UserConfirm = @UserConfirm
                    WHERE TicketID = @TicketID";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserConfirm", userConfirmValue);
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);

                    if (userConfirmValue == "Hoàn thành")
                    {
                        cmd.Parameters.AddWithValue("@TempDevice", "Không");
                        cmd.Parameters.AddWithValue("@DeviceTempID", DBNull.Value); // set null
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // Xóa ticket
        public void DeleteTicket(string ticketId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"UPDATE Tickets 
                       SET IsDeleted = 1
                       WHERE TicketID = @TicketID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TicketDto> SearchTickets(string keyword)
        {
            List<TicketDto> tickets = new List<TicketDto>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT t.TicketID,
       t.EmployeeID,
       t.DeviceCode,
       ts.StatusName,
       t.Description,
       dt.DeviceTypeName,
       dt.DeviceTypeID,
       t.AssignedIT,
       t.CreatedAt,
       t.ClosedAt,
       t.ITNote,
       t.RepairDetail,
       t.AdminConfirm,
       t.TempDevice,
       t.UserConfirm,
       t.DeviceTempID
FROM Tickets t
LEFT JOIN TicketStatus ts ON t.StatusID = ts.StatusID
LEFT JOIN Devices d ON t.DeviceCode = d.DeviceCode
LEFT JOIN DeviceTypes dt ON d.DeviceTypeID = dt.DeviceTypeID
WHERE (
           t.TicketID LIKE @kw
        OR t.EmployeeID LIKE @kw
        OR t.DeviceCode LIKE @kw
        OR ts.StatusName LIKE @kw
        OR t.Description LIKE @kw
        OR t.AssignedIT LIKE @kw
        OR t.AdminConfirm LIKE @kw
        OR t.UserConfirm LIKE @kw
        OR dt.DeviceTypeName LIKE @kw
        OR t.DeviceTempID LIKE @kw
        OR dt.DeviceTypeID LIKE @kw
      )
  AND t.IsDeleted = 0;
";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tickets.Add(new TicketDto
                    {
                        TicketID = reader["TicketID"].ToString(),
                        EmployeeID = reader["EmployeeID"].ToString(),
                        DeviceCode = reader["DeviceCode"].ToString(),
                        Status = reader["StatusName"].ToString(),
                        DeviceType = reader["DeviceTypeName"].ToString(),
                        Description = reader["Description"].ToString(),
                        AssignedIT = reader["AssignedIT"].ToString(),

                        CreatedAt = reader["CreatedAt"] != DBNull.Value
                            ? Convert.ToDateTime(reader["CreatedAt"]).ToString("yyyy-MM-dd")
                            : "",

                        ClosedAt = reader["ClosedAt"] != DBNull.Value
                            ? Convert.ToDateTime(reader["ClosedAt"]).ToString("yyyy-MM-dd")
                            : "",

                        ITNote = reader["ITNote"].ToString(),
                        RepairDetail = reader["RepairDetail"].ToString(),
                        AdminConfirm = reader["AdminConfirm"].ToString(),
                        TempDevice = reader["TempDevice"].ToString(),
                        UserConfirm = reader["UserConfirm"].ToString(),
                        DeviceTypeID = reader["DeviceTypeID"].ToString(),
                        DeviceTempID = reader["DeviceTempID"].ToString(),
                    });
                }
            }

            return tickets;
        }

        public List<TicketDto> GetTicketITUsersByID(String id)
        {
            List<TicketDto> tickets = new List<TicketDto>();

            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                string sql = @"
                            SELECT t.TicketID,
                            t.EmployeeID,
                            t.DeviceCode,
                            ts.StatusName,
		                    dt.DeviceTypeName,
                            t.Description,
                            t.ITNote,
                            t.RepairDetail,
                            t.AdminConfirm,
                            t.DeviceTempID,
                            t.TempDevice,
                            t.UserConfirm
                            FROM  Tickets t LEFT JOIN TicketStatus ts ON t.StatusID = ts.StatusID 
                            LEFT JOIN Devices d ON t.DeviceCode = d.DeviceCode
                            LEFT JOIN DeviceTypes dt ON d.DeviceTypeID = dt.DeviceTypeID  where t.AssignedIT = @id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tickets.Add(new TicketDto
                    {
                        TicketID = reader["TicketID"].ToString(),
                        EmployeeID = reader["EmployeeID"].ToString(),
                        DeviceCode = reader["DeviceCode"].ToString(),
                        DeviceType = reader["DeviceTypeName"].ToString(),
                        Status = reader["StatusName"].ToString(),
                        Description = reader["Description"].ToString(),
                        ITNote = reader["ITNote"].ToString(),
                        RepairDetail = reader["RepairDetail"].ToString(),
                        AdminConfirm = reader["AdminConfirm"].ToString(),
                        TempDevice = reader["TempDevice"].ToString() + "   " + reader["DeviceTempID"].ToString(),
                        UserConfirm = reader["UserConfirm"].ToString()
                    });
                }
            }

            return tickets;
        }


        public void ConfirmDeviceTempTicket(string ticketId, string tempDeviceName, string deviceTempID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
            UPDATE Tickets 
            SET TempDevice = @TempDevice,
                DeviceTempID = @DeviceTempID
            WHERE TicketID = @TicketID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TempDevice", (object)tempDeviceName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DeviceTempID", (object)deviceTempID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TicketID", ticketId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
