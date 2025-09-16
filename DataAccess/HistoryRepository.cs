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
    internal class HistoryRepository
    {
        internal List<HistoryDto> GetAllUser()
        {
            List<HistoryDto> listHistory = new List<HistoryDto>();

            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                string sql = @" 
                        select * from TicketHistory";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    listHistory.Add(new HistoryDto
                    {
                        ChangedBy = reader["ChangedBy"].ToString(),
                        HistoryID = reader["HistoryID"].ToString(),
                        TicketID = reader["TicketID"].ToString(),
                        FieldChanged = reader["FieldChanged"].ToString(),
                        OldValue = reader["OldValue"].ToString(),
                        NewValue = reader["NewValue"].ToString(),
                        ChangeDate = reader["ChangeDate"] != DBNull.Value
                            ? Convert.ToDateTime(reader["ChangeDate"]).ToString("yyyy-MM-dd HH:mm:ss")
                            : "",
                        IsCreator = reader["IsCreator"] != DBNull.Value && (bool)reader["IsCreator"]
                    });
                }
            }

            return listHistory;

        }

        public void AddHistory(HistoryDto history)
        {
            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO TicketHistory (TicketID, ChangeDate, ChangedBy, FieldChanged, OldValue, NewValue,IsCreator) 
                                 VALUES (@TicketID, @ChangeDate, @ChangedBy, @FieldChanged, @OldValue, @NewValue,@IsCreator)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TicketID", history.TicketID);
                    cmd.Parameters.AddWithValue("@ChangeDate", history.ChangeDate);
                    cmd.Parameters.AddWithValue("@ChangedBy", history.ChangedBy);
                    cmd.Parameters.AddWithValue("@FieldChanged", history.FieldChanged);
                    cmd.Parameters.AddWithValue("@OldValue", history.OldValue);
                    cmd.Parameters.AddWithValue("@NewValue", history.NewValue);
                    cmd.Parameters.AddWithValue("@IsCreator", history.IsCreator);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
