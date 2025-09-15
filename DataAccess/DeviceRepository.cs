using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Constant;
using WindowsFormsApp1.Dtos;

namespace WindowsFormsApp1.DataAccess
{
    internal class DeviceRepository
    {
        internal void AddDevice(TicketDto ticket)
        {
            throw new NotImplementedException();
        }

        internal void DeleteDevice(string ticketId)
        {
            throw new NotImplementedException();
        }

        internal List<DeviceDto> GetAllDevice()
        {
            List<DeviceDto> devices = new List<DeviceDto>();

            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                string sql = @" select d.DeviceCode,d.DeviceTypeID,
                dt.DeviceTypeName,d.DeviceName,d.Brand,d.Model,d.PurchaseDate,
                d.WarrantyEndDate,d.Status,d.Location,d.Notes 
                from Devices d join DeviceTypes dt on dt.DeviceTypeID = d.DeviceTypeID;";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    devices.Add(new DeviceDto
                    {
                        DeviceCode = reader["DeviceCode"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        DeviceType = reader["DeviceTypeName"].ToString(),
                        Brand = reader["Brand"].ToString(),
                        Model = reader["Model"].ToString(),
                        Status = reader["Status"].ToString(),

                        PurchaseDate = reader["PurchaseDate"] != DBNull.Value
                            ? Convert.ToDateTime(reader["PurchaseDate"]).ToString("yyyy-MM-dd")
                            : "",

                        WarrantyEndDate = reader["WarrantyEndDate"] != DBNull.Value
                            ? Convert.ToDateTime(reader["WarrantyEndDate"]).ToString("yyyy-MM-dd")
                            : "",

                        Location = reader["Location"].ToString(),
                        Notes = reader["Notes"].ToString(),
                        DeviceTypeID = reader["DeviceTypeID"].ToString()
                    });
                }
            }

            return devices;

        }

        public List<string> GetTypeDevice()
        {
            var deviceType = new List<string>();

            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                conn.Open();
                string query = "SELECT DeviceTypeName FROM DeviceTypes";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        deviceType.Add(reader["DeviceTypeName"].ToString());
                    }
                }
            }

            return deviceType;
        }

        public List<DeviceTypeTempDto> GetDeviceTemp(string employeeID, string deviceTypeID, string ticketID)
        {
            var deviceTypeTemp = new List<DeviceTypeTempDto>();

            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                conn.Open();
                string query = @"SELECT d.DeviceName, d.DeviceCode
                                 FROM Devices d
                                 LEFT JOIN UserDevices ud ON d.DeviceCode = ud.DeviceCode
                                 INNER JOIN Tickets t ON t.TicketID = @TicketID
                                 INNER JOIN Devices td ON t.DeviceCode = td.DeviceCode   -- ticket → device gốc
                                 WHERE ud.EmployeeID IS NULL                -- thiết bị chưa gán cho user
                                 AND t.DeviceTempID IS NULL               -- ticket này chưa có thiết bị tạm
                                 AND d.DeviceTypeID = td.DeviceTypeID     -- cùng loại
                                 AND NOT EXISTS (                         -- loại bỏ thiết bị đã bị mượn tạm ở ticket khác
                                 SELECT 1 
                                 FROM Tickets t2
                                 WHERE t2.DeviceTempID = d.DeviceCode 
                                 AND (t2.UserConfirm IS NULL OR t2.UserConfirm <> 'Hoàn thành')
  );
";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TicketID", ticketID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dto = new DeviceTypeTempDto
                            {
                                DeviceTypeCode = reader["DeviceCode"].ToString(),
                                DeviceTypeName = reader["DeviceName"].ToString()
                            };

                            deviceTypeTemp.Add(dto);
                        }
                    }
                }
            }

            return deviceTypeTemp;
        }


        internal List<TicketDto> SearchDevice(string keyword)
        {
            throw new NotImplementedException();
        }

        internal void UpdateDeviceField(string DeviceId, string fieldName, string newValue)
        {
            throw new NotImplementedException();
        }
    }
}
