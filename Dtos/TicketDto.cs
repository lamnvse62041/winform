using System;
using System.Windows.Forms;

namespace WindowsFormsApp1.Dtos
{
    public class TicketDto
    {
        public string TicketID { get; set; }
        public string EmployeeID { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceType { get; set; }
        public string Status { get; set; } = "Mới"; // default
        public string Description { get; set; }

        public string DeviceID { get; set; }
        public string AssignedIT { get; set; } = "Chưa gán"; // default
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string ClosedAt { get; set; } = "";
        public string ITNote { get; set; } = "";
        public string RepairDetail { get; set; } = "";
        public string AdminConfirm { get; set; } = "Chưa Xác nhận";
        public string TempDevice { get; set; } = "Không";
        public string UserConfirm { get; set; } = "Đang chờ xác nhận";

        public string DeviceTypeID { get; set; }

        public string DeviceTempID { get; set; }

        // Hàm convert sang ListViewItem để dễ add vào listview
        public ListViewItem ToListViewItem()
        {
            return new ListViewItem(new[]
            {
                TicketID,
                EmployeeID,
                DeviceCode,
                DeviceType,
                Status,
                Description,
                AssignedIT,
                CreatedAt,
                ClosedAt,
                ITNote,
                RepairDetail,
                AdminConfirm,
                TempDevice,
                UserConfirm,
                DeviceTypeID,
                DeviceTempID
            });
        }

        public ListViewItem ToListViewItemIT()
        {
            return new ListViewItem(new[]
            {
                TicketID,
                EmployeeID,
                DeviceCode,
                DeviceType,
                Status,
                Description,
                ITNote,
                RepairDetail,
                AdminConfirm,
                TempDevice,
                UserConfirm
            });
        }
    }
}
