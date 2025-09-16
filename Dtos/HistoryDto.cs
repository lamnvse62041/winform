using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Dtos
{
    internal class HistoryDto
    {
        public String HistoryID { get; set; }
        public String TicketID { get; set; }
        public String ChangeDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public String ChangedBy { get; set; }
        public String FieldChanged { get; set; }
        public String OldValue { get; set; }
        public String NewValue { get; set; }
        public bool IsCreator { get; set; }

    }
}
