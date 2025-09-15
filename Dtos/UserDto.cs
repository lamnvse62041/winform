using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1.Dtos
{
    public class UserDto
    {
        public string EmployeeID { get; set; }
        public string Role { get; set; }
    }

    public class DeviceTypeTempDto
    {
        public string DeviceTypeCode { get; set; }
        public string DeviceTypeName { get; set; }
    }
    public class StatusDto
    {
        public string StatusID { get; set; }
        public string StatusName { get; set; }
    }

    public class UserDtoAdmin
    {
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }   
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string DeviceCode { get; set; }       
        public string DeviceName { get; set; }     
        public string CreateAt { get; set; }


        public ListViewItem ToListViewUser()
        {
            return new ListViewItem(new[]
            {
            EmployeeID,
            FullName,
            DeviceCode,
            DeviceName,
            DepartmentName,
            Email,
            PhoneNumber,
            Username,
            Role,
            CreateAt
            });
        }


    }
}
