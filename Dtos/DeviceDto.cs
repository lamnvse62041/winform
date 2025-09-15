using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Dtos
{
    public class DeviceDto
    {
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; } = "Mới";
        public string PurchaseDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string WarrantyEndDate { get; set; } = "";
        public string Status { get; set; } = "";
        public string Location { get; set; } = "";
        public string Notes { get; set; } = "";

        public string DeviceTypeID { get; set; } = "";
        public string DeviceTempID { get; set; } = "";

        // Hàm convert sang ListViewItem để dễ add vào listview
        public ListViewItem ToListViewItem()
        {
            return new ListViewItem(new[]
            {
                DeviceCode,
                DeviceType,
                DeviceName,
                Brand,
                Model,
                PurchaseDate,
                WarrantyEndDate,
                Status,
                Location,
                Notes,
            });
        }
    }
}
