using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Controller;
using WindowsFormsApp1.Controllers;

namespace WindowsFormsApp1
{
    public partial class ItForm : Form
    {
        private string employeeId;
        private TicketController ticketController;
        private UserController userController;
        private DeviceController deviceController;
        private StatusController statusController;
        public ItForm(string employeeId)
        {
            ticketController = new TicketController();
            userController = new UserController();
            deviceController = new DeviceController();
            statusController = new StatusController();
            this.employeeId = employeeId;
            this.Load += ITForm_Load;
            InitializeComponent();
        }

        private void ITForm_Load(object sender, EventArgs e)
        {
            // Cấu hình ListView Ticket
            listViewIT.View = View.Details;
            listViewIT.Columns.Add("TicketID", 40);
            listViewIT.Columns.Add("Nhân viên", 60);
            listViewIT.Columns.Add("Mã thiết bị", 60);
            listViewIT.Columns.Add("Loại thiết bị", 60);
            listViewIT.Columns.Add("Trạng thái", 80);
            listViewIT.Columns.Add("Miêu tả bởi người dùng", 120);
            listViewIT.Columns.Add("Mô tả bởi IT", 120);
            listViewIT.Columns.Add("Chi tiết sửa chữa", 120);
            listViewIT.Columns.Add("Admin xác nhận", 100);
            listViewIT.Columns.Add("Thiết bị cấp tạm thời", 120);
            listViewIT.Columns.Add("Xác nhận người dùng", 100);
            listViewIT.MouseDoubleClick += ListViewIT_MouseDoubleClick;
            listViewIT.FullRowSelect = true;
            listViewIT.GridLines = true;

        
            RefreshTickets();
        }

        private void RefreshTickets()
        {
            listViewIT.Items.Clear();
            listViewIT.Items.AddRange(
                ticketController.GetTicketByIT(employeeId)
                                .Select(t => t.ToListViewItemIT())
                                .ToArray()
            );
        }
        private void ListViewIT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var info = listViewIT.HitTest(e.Location);
            if (info.Item == null || info.SubItem == null) return;

            var item = info.Item;
            var subItem = info.SubItem;

            int colIndex = item.SubItems.IndexOf(subItem);
            string oldValue = subItem.Text;
            string ticketId = item.SubItems[0].Text; // cột 0 = TicketID
            string newValue = null;
            string newValueStatus = null;
            string fieldName = null;

            // Map column → field trong DB

            string ticketStatus = item.SubItems[4].Text;
            //if (ticketStatus == "Đã xong" || ticketStatus == "Đã hủy")
            //{
            //    MessageBox.Show("Ticket này đã hoàn thành, không thể chỉnh sửa.",
            //                    "Thông báo",
            //                    MessageBoxButtons.OK,
            //                    MessageBoxIcon.Warning);
            //    return;
            //}
            switch (colIndex)
            {
                case 4: // Status
                    fieldName = "StatusID";
                    if (item.SubItems[8].Text == "Chưa xác nhận")
                    {
                        MessageBox.Show("Ticket này đã chưa được Admin xác nhận, không thể thay đổi trạng thái.",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return;
                    }
                    if (oldValue == "Đã xong" || oldValue == "Đã hủy")
                    {
                        MessageBox.Show("Ticket này đã hoàn thành, không thể thay đổi trạng thái.",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return;
                    }
                    var statusDtos = statusController.GetStatus();

                    var statusNames = statusDtos.Select(s => s.StatusName).ToList();

                    using (var form = new ComboBoxForm("Chọn trạng thái", statusNames, oldValue))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            string selectedName = form.SelectedValue;
                            var matched = statusDtos.FirstOrDefault(s => s.StatusName == selectedName);
                            newValueStatus = selectedName;
                            if (matched != null)
                            {
                                newValue = matched.StatusID;
                            }
                            else
                            {
                                newValue = selectedName;
                            }
                        }
                    }
                    break;
                case 7:
                case 6:
                    string columnCaption = listViewIT.Columns[colIndex].Text;
                    fieldName = listViewIT.Columns[colIndex].Text.Replace(" ", "");
                    if (ticketController.columnMap.TryGetValue(columnCaption, out string dbColumnName))
                    {
                        fieldName = dbColumnName;
                    }
                    else
                    {
                        fieldName = columnCaption; // fallback
                    }
                    newValue = Prompt.ShowDialog(
                        $"Nhập giá trị mới cho cột: {listViewIT.Columns[colIndex].Text}",
                        oldValue
                    );
                    break;

                case 10: // User xác nhận
                    fieldName = "UserConfirm";

                    var statusDtosUser = statusController.GetStatus();
                    var matchedStatus = statusDtosUser.FirstOrDefault(s => s.StatusName == ticketStatus);

                    if (matchedStatus != null && (matchedStatus.StatusID == "3" || matchedStatus.StatusID == "4"))
                    {
                        using (var form = new ComboBoxForm("Xác nhận của user",
                            new List<string> { "Đang chờ xác nhận", "Hoàn thành", "Chưa hoàn thành" }, oldValue))
                        {
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                newValue = form.SelectedValue;
                                subItem.Text = newValue;
                                ticketController.ConfirmUserTicket(ticketId, newValue);
                                RefreshTickets();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chỉ có thể xác nhận khi trạng thái là 'Đã hủy' hoặc 'Đã xong'.",
                                        "Không được phép",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(newValue))
            {
                subItem.Text = fieldName == "StatusID" ? newValueStatus : newValue;
                ticketController.UpdateTicketField(ticketId, fieldName, newValue);
            }
        }


    }
}
