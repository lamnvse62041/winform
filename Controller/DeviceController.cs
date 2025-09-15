using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DataAccess;
using WindowsFormsApp1.Dtos;
using WindowsFormsApp1.Repositories;

namespace WindowsFormsApp1.Controller
{
    internal class DeviceController
    {
        private DeviceRepository deviceRepo;

        public DeviceController()
        {
            deviceRepo = new DeviceRepository();
        }

        public List<DeviceDto> GetAllDevice()
        {
            return deviceRepo.GetAllDevice();
        }

        public void AddTicket(TicketDto ticket)
        {
            deviceRepo.AddDevice(ticket);
        }

        public List<String> GetDeviceType()
        {
            return deviceRepo.GetTypeDevice();
        }


        public void DeleteTicket(string ticketId)
        {
            deviceRepo.DeleteDevice(ticketId);
        }

        public List<TicketDto> SearchTickets(string keyword)
        {
            return null;
        }

        public List<DeviceTypeTempDto> GetDeviceTemp(String EmployeeID, String DeviceTypeID, String TicketID)
        {
            return deviceRepo.GetDeviceTemp(EmployeeID, DeviceTypeID,TicketID);
        }
    }
}
