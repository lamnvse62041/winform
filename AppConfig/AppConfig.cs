using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Constant
{
    internal static class AppConfig
    {
        //public static string connectionString = @"Server=LOCALHOST\SQLEXPRESS01;Database=DeviceIT;Trusted_Connection=True;";
        public static string appuser = "devtest";
        //    public static string password = "Test@123";
            public static string connectionString =
        @"Server=192.168.20.80;Database=DWCDevice;User Id=devtest;Password=Test@123;";

    }
}
