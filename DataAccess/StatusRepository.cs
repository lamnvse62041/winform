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
    internal class StatusRepository
    {
        public List<StatusDto> GetStatus()
        {
            var statuses = new List<StatusDto>();
            using (SqlConnection conn = new SqlConnection(AppConfig.connectionString))
            {
                conn.Open();
                string query = "SELECT StatusID, StatusName FROM TicketStatus";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statuses.Add(new StatusDto
                        {
                            StatusID = reader["StatusID"].ToString(),
                            StatusName = reader["StatusName"].ToString()
                        });
                    }
                }
            }

            return statuses;
        }
    }
    
}
