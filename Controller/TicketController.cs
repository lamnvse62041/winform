using System.Collections.Generic;
using WindowsFormsApp1.Dtos;
using WindowsFormsApp1.Repositories;

namespace WindowsFormsApp1.Controllers
{
    public class TicketController
    {
        private TicketRepository ticketRepo;

        public TicketController()
        {
            ticketRepo = new TicketRepository();
        }

        public List<TicketDto> GetAllTickets()
        {
            return ticketRepo.GetAllTickets();
        }

        public string AddTicket(TicketDto ticket , out string newTicketId)
        {
            return  ticketRepo.AddTicket(ticket, out newTicketId);
        }

        public void UpdateTicketField(string ticketId, string fieldName, string newValue,bool ? isUpdate = false)
        {
            ticketRepo.UpdateTicketField(ticketId, fieldName, newValue);
        }

        public void ConfirmUserTicket(string ticketId, string newValue)
        {
            ticketRepo.ConfirmUserTicket(ticketId, newValue);
        }

        public void ConfirmDeviceTempTicket(string ticketId, string newValue,string DeviceID)
        {
            ticketRepo.ConfirmDeviceTempTicket(ticketId, newValue,DeviceID);
        }

        public void DeleteTicket(string ticketId)
        {
            ticketRepo.DeleteTicket(ticketId);
        }

        public List<TicketDto> GetTicketByIT(string ticketId)
        {
           return  ticketRepo.GetTicketITUsersByID(ticketId);
        }
        public List<TicketDto> SearchTickets(string keyword)
        {
            return ticketRepo.SearchTickets(keyword);
        }

        public Dictionary<string, string> columnMap = new Dictionary<string, string>
{
    { "TicketID", "TicketID" },
    { "Nhân viên", "EmployeeID" },
    { "Mã thiết bị", "DeviceCode" },
    { "Loại thiết bị", "DeviceType" },
    { "Trạng thái", "StatusID" },
    { "Miêu tả", "Description" },
    { "IT sửa chữa ", "AssignedIT" },
    { "Ngày tạo", "CreatedAt" },
    { "Ngày đóng", "ClosedAt" },
    { "Mô tả bởi IT", "ITNote" },
    { "Chi tiết sửa chữa", "RepairDetail" },
    { "Admin xác nhận", "AdminConfirm" },
    { "Thiết bị cấp tạm thời", "TempDevice" },
    { "Xác nhận người dùng", "UserConfirm" }
};

    }
}
