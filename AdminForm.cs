using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WindowsFormsApp1.Controller;
using WindowsFormsApp1.Controllers;
using WindowsFormsApp1.Dtos;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace WindowsFormsApp1
{
    public partial class AdminForm : Form
    {
        private string currentSearchKeyword = "";
        private int sortColumn = -1;
        private bool ascending = true;
        private TicketController ticketController;
        private UserController userController;
        private DeviceController deviceController;
        private StatusController statusController;
        private HistoryController historyController;

        public AdminForm()
        {
            InitializeComponent();
            this.Load += AdminForm_Load;
            ticketController = new TicketController();
            userController = new UserController();
            deviceController = new DeviceController();
            statusController = new StatusController();
            historyController = new HistoryController();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // Cấu hình ListView Ticket
            listview_all_ticket.View = View.Details;
            listview_all_ticket.Columns.Add("TicketID", 40);
            listview_all_ticket.Columns.Add("Nhân viên", 60);
            listview_all_ticket.Columns.Add("Mã thiết bị", 60);
            listview_all_ticket.Columns.Add("Loại thiết bị", 80);
            listview_all_ticket.Columns.Add("Trạng thái", 80);
            listview_all_ticket.Columns.Add("Miêu tả", 120);
            listview_all_ticket.Columns.Add("IT sửa chữa ", 60);
            listview_all_ticket.Columns.Add("Ngày tạo", 90);
            listview_all_ticket.Columns.Add("Ngày đóng", 90);
            listview_all_ticket.Columns.Add("Mô tả bởi IT", 120);
            listview_all_ticket.Columns.Add("Chi tiết sửa chữa", 120);
            listview_all_ticket.Columns.Add("Admin xác nhận", 100);
            listview_all_ticket.Columns.Add("Thiết bị cấp tạm thời", 120);
            listview_all_ticket.Columns.Add("Xác nhận người dùng", 100);
            listview_all_ticket.Columns.Add("DeviceTypeID", 55);
            listview_all_ticket.Columns.Add("DeviceTempID", 55);
            listview_all_ticket.Columns.Add("DeleteTicket", 0);

            listview_all_ticket.FullRowSelect = true;
            listview_all_ticket.GridLines = true;
            listview_all_ticket.MouseDoubleClick += Listview_all_ticket_DoubleClick;
            listview_all_ticket.ColumnClick += Listview_all_ticket_ColumnClick;

            // User 
            listview_user.View = View.Details;
            listview_user.Columns.Add("EmployeeID", 60);
            listview_user.Columns.Add("FullName", 100);
            listview_user.Columns.Add("DeviceCode", 60);
            listview_user.Columns.Add("DeviceName", 80);
            listview_user.Columns.Add("DepartmentName", 80);
            listview_user.Columns.Add("Email", 130);
            listview_user.Columns.Add("PhoneNumber", 120);
            listview_user.Columns.Add("Username", 60);
            listview_user.Columns.Add("Role", 80);
            listview_user.Columns.Add("CreateAt", 80);

            // ListView Device  


            listview_device.View = View.Details;
            listview_device.Columns.Add("DeviceID", 60);
            listview_device.Columns.Add("DeviceType", 60);
            listview_device.Columns.Add("DeviceName", 100);
            listview_device.Columns.Add("Brand", 60);
            listview_device.Columns.Add("Model", 60);
            listview_device.Columns.Add("PurchaseDate", 100);
            listview_device.Columns.Add("WarrantyEndDate", 60);
            listview_device.Columns.Add("Status", 60);
            listview_device.Columns.Add("Location", 100);
            listview_device.Columns.Add("Notes", 100);
            listview_device.FullRowSelect = true;
            txt_device_code.Text = "Nhập device code...";
            txt_device_code.ForeColor = Color.Gray;

            txt_employee_code.Text = "Nhập EmployeeID...";
            txt_employee_code.ForeColor = Color.Gray;
            RefreshTickets();
            LoadUsers();
            LoadDevice();
            LoadHistory();
        }

        private void RefreshTickets(string keyword = "")
        {
            var tickets = ticketController.SearchTickets(keyword); // search tất cả field
            listview_all_ticket.Items.Clear();
            listview_all_ticket.Items.AddRange(
                tickets.Select(t => t.ToListViewItem()).ToArray()
            );

            if (sortColumn >= 0)
            {
                listview_all_ticket.ListViewItemSorter = new ListViewTicketComparer(sortColumn, ascending, ticketController.columnMap);
                listview_all_ticket.Sort();
            }
        }

        private void LoadUsers()
        {
            var users = userController.GetAllUser();
            listview_user.Items.Clear();
            listview_user.Items.AddRange(
                users.Select(u => u.ToListViewUser()).ToArray()
            );
        }

        private void LoadHistory()
        {
            var historyList = historyController.GetAllHistory();

            // Sort giảm dần theo ChangeDate
            historyList = historyList
                .OrderByDescending(h => DateTime.TryParse(h.ChangeDate, out DateTime dt) ? dt : DateTime.MinValue)
                .ToList();

            rtb_history.Clear();

            // Nhóm theo ngày
            var grouped = historyList.GroupBy(h =>
                DateTime.TryParse(h.ChangeDate, out DateTime dt) ? dt.Date : DateTime.MinValue.Date
            );

            foreach (var group in grouped)
            {
                string dayLabel = group.Key == DateTime.Today ? "Hôm nay" : group.Key.ToString("dd/MM/yyyy");
                rtb_history.AppendText($"- {dayLabel}{Environment.NewLine}");

                foreach (var h in group)
                {
                    DateTime dt = DateTime.TryParse(h.ChangeDate, out DateTime t) ? t : DateTime.MinValue;
                    string timePart = dt.ToString("dd/MM/yyyy HH:mm");
                    string line;

                    if (h.IsCreator)
                    {
                        line = $"[{timePart}] {h.ChangedBy} tạo ticket mới";
                    }
                    else
                    {
                        line = $"[{timePart}] {h.ChangedBy} thay đổi \"{h.FieldChanged}\" từ \"{h.OldValue}\" → \"{h.NewValue}\"";
                    }

                    rtb_history.AppendText("  " + line + Environment.NewLine);
                }
            }
        }


        private void LoadDevice()
        {
            listview_device.Items.Clear();
            listview_device.Items.AddRange(
                deviceController.GetAllDevice()
                                .Select(t => t.ToListViewItem())
                                .ToArray()
            );

            var deviceTypes = deviceController.GetDeviceType();

            //cb_type_device.Items.Clear();
            //cb_type_device.Items.AddRange(deviceTypes.ToArray());
        }

        private void btn_search_click(object sender, EventArgs e)
        {
            currentSearchKeyword = txtSearch.Text.Trim();

            RefreshTickets(currentSearchKeyword);
        }

        private void Listview_all_ticket_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == sortColumn)
                ascending = !ascending; // click cùng cột → đổi chiều
            else
            {
                sortColumn = e.Column;
                ascending = true;
            }

            listview_all_ticket.ListViewItemSorter = new ListViewTicketComparer(e.Column, ascending, ticketController.columnMap);
            listview_all_ticket.Sort();
        }

        private void Listview_all_ticket_DoubleClick(object sender, MouseEventArgs e)
        {
            var info = listview_all_ticket.HitTest(e.Location);
            if (info.Item == null || info.SubItem == null) return;

            var item = info.Item;
            var subItem = info.SubItem;

            int colIndex = item.SubItems.IndexOf(subItem);
            string oldValue = subItem.Text;
            string ticketId = item.SubItems[0].Text;
            string newValue = null;
            string newValueStatus = null;
            string fieldName = null;
            string newValueTempDevice = null;

            // Map column → field trong DB
            switch (colIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 7:
                case 9:
                case 15:
                case 14:

                    break;
                case 4: // Status
                    fieldName = "StatusID";
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
                case 6: // IT sửa chữa
                    fieldName = "AssignedIT";
                    string statusText = item.SubItems[4].Text;
                    var statusDtosUser = statusController.GetStatus();
                    var matchedStatus = statusDtosUser.FirstOrDefault(s => s.StatusName == statusText);

                    if (matchedStatus != null && (matchedStatus.StatusID == "1" || matchedStatus.StatusID == "2"))
                    {
                        var itList = userController.GetITUsers();

                        using (var form = new ComboBoxForm("Chọn IT sửa chữa", itList, oldValue))
                        {
                            if (form.ShowDialog() == DialogResult.OK)
                                newValue = form.SelectedValue;
                        }
                    } else
                    {
                        MessageBox.Show("Chỉ có thể gán IT khi trạng thái là 'Đang xử lý' hoặc 'Đang chờ'.",
                                        "Không được phép",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }

                    break;
                case 8: // Ngày đóng
                    fieldName = "ClosedAt";
                    using (var form = new DatePickerFormDialog(DateTime.TryParse(oldValue, out var d2) ? d2 : (DateTime?)null))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            newValue = form.SelectedDate.ToString("yyyy-MM-dd");
                        }
                    }
                    break;
                case 11: // Admin xác nhận
                    fieldName = "AdminConfirm";
                    using (var form = new ComboBoxForm("Xác nhận của admin",
                        new List<string> { "Chưa xác nhận", "Thay thế sửa chữa", "Không thể sửa chữa", "Đổi mới" }, oldValue))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                            newValue = form.SelectedValue;
                    }
                    break;
                case 12: // Admin xác nhận TempDevice
                    fieldName = "TempDevice";

                    // kiểm tra UserConfirm trước
                    string userConfirmValue = item.SubItems[13].Text; // cột 13 = UserConfirm
                    if (userConfirmValue == "Hoàn thành")
                    {
                        MessageBox.Show("Ticket đã hoàn thành, không thể cấp thiết bị tạm.",
                                        "Không được phép",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        break;
                    }

                    string EmployeeID = item.SubItems[1].Text;
                    string DeviceTypeID = item.SubItems[14].Text; // anh đã map DeviceTypeID vào listview
                    string TicketID = item.SubItems[0].Text;
                    List<DeviceTypeTempDto> getDeviceTemp = deviceController.GetDeviceTemp(EmployeeID, DeviceTypeID, TicketID);

                    // Lấy danh sách tên thiết bị + thêm "Không"
                    var deviceTypeNames = getDeviceTemp.Select(s => s.DeviceTypeName).ToList();
                    deviceTypeNames.Insert(0, "Không"); // thêm option "Không" lên đầu

                    using (var form = new ComboBoxForm("Chọn thiết bị thay thế", deviceTypeNames, oldValue))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            newValue = form.SelectedValue;

                            if (newValue == "Không")
                            {
                                newValueTempDevice = null; // không lưu DeviceTempID
                            }
                            else
                            {
                                newValueTempDevice = getDeviceTemp
                                    .FirstOrDefault(d => d.DeviceTypeName == newValue)?.DeviceTypeCode;
                            }
                        }
                    }
                    break;


                case 13: // User xác nhận
                    fieldName = "UserConfirm";

                    statusText = item.SubItems[4].Text;
                    statusDtosUser = statusController.GetStatus();
                    matchedStatus = statusDtosUser.FirstOrDefault(s => s.StatusName == statusText);

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

                default:
                    string columnCaption = listview_all_ticket.Columns[colIndex].Text;
                    fieldName = listview_all_ticket.Columns[colIndex].Text.Replace(" ", "");
                    if (ticketController.columnMap.TryGetValue(columnCaption, out string dbColumnName))
                    {
                        fieldName = dbColumnName;
                    }
                    else
                    {
                        fieldName = columnCaption; // fallback
                    }
                    newValue = Prompt.ShowDialog(
                        $"Nhập giá trị mới cho cột: {listview_all_ticket.Columns[colIndex].Text}",
                        oldValue
                    );
                    break;
            }

            if (!string.IsNullOrEmpty(newValue))
            {
                if (fieldName == "UserConfirm")
                {
                    subItem.Text = newValue;
                    ticketController.ConfirmUserTicket(ticketId, newValue);
                    item.SubItems[8].Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (fieldName == "TempDevice")
                {
                    subItem.Text = newValue;
                    ticketController.ConfirmDeviceTempTicket(ticketId, newValue, newValueTempDevice);
                }
                else
                {
                    subItem.Text = fieldName == "StatusID" ? newValueStatus : newValue;
                    ticketController.UpdateTicketField(ticketId, fieldName, newValue);
                }
                var history = new HistoryDto
                {
                    HistoryID = Guid.NewGuid().ToString(),
                    TicketID = ticketId,
                    ChangeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    ChangedBy = "Admin",
                    FieldChanged = fieldName,
                    OldValue = oldValue,
                    NewValue = subItem.Text
                };
                historyController.AddHistory(history);

                RefreshTickets(currentSearchKeyword);
                LoadHistory();
            }
        }

        // Tạo 1 dictionary map caption -> columnName DB

        private void create_Ticket(object sender, EventArgs e)
        {
            string empId = txt_employee_code.Text.Trim();
            string deviceCode = txt_device_code.Text.Trim();

            if (string.IsNullOrEmpty(empId) || string.IsNullOrEmpty(deviceCode))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ EmployeeID, DeviceCode và loại thiết bị.",
                                "Thiếu thông tin",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Sinh TicketID tự động
            string newId = $"T{DateTime.Now.Ticks}";

            var ticket = new TicketDto
            {
                TicketID = newId,
                EmployeeID = empId,
                DeviceCode = deviceCode,
                Description = txt_decristion.Text.Trim()
            };
            string newTicketId;
            string result = ticketController.AddTicket(ticket, out newTicketId);

            if (result == "success")
            {
                RefreshTickets();
                txt_employee_code.Clear();
                txt_device_code.Clear();
                txt_decristion.Clear();

                MessageBox.Show("Tạo ticket thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(result, "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void cb_type_device_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_device_code_Enter(object sender, EventArgs e)
        {
            if (txt_device_code.Text == "Nhập device code...")
            {
                txt_device_code.Text = "";
                txt_device_code.ForeColor = Color.Black;
            }
        }

        private void txt_device_code_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_device_code.Text))
            {
                txt_device_code.Text = "Nhập device code...";
                txt_device_code.ForeColor = Color.Gray;
            }
        }

        private void txt_employee_code_Enter(object sender, EventArgs e)
        {
            if (txt_employee_code.Text == "Nhập EmployeeID...")
            {
                txt_employee_code.Text = "";
                txt_employee_code.ForeColor = Color.Black;
            }
        }

        private void txt_employee_code_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_employee_code.Text))
            {
                txt_employee_code.Text = "Nhập EmployeeID...";
                txt_employee_code.ForeColor = Color.Gray;
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (listview_all_ticket.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một ticket để xóa.",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = listview_all_ticket.SelectedItems[0];
            string ticketId = selectedItem.SubItems[0].Text;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa ticket {ticketId}?",
                                          "Xác nhận xóa",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    ticketController.DeleteTicket(ticketId);
                    MessageBox.Show("Xóa ticket thành công!",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    var history = new HistoryDto
                    {
                        HistoryID = Guid.NewGuid().ToString(),
                        TicketID = ticketId,
                        ChangeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                        ChangedBy = "Admin",
                        FieldChanged = "IsDeleted",
                        OldValue = "0",
                        NewValue = "1"
                    };
                    historyController.AddHistory(history);

                    RefreshTickets();
                    LoadHistory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa ticket: " + ex.Message,
                                    "Lỗi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (CreateTicketForm nextForm = new CreateTicketForm())
            {
                var result = nextForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    RefreshTickets();
                }
            }
        }

    } 



public class ListViewTicketComparer : IComparer
    {
        private int col;
        private bool ascending;
        private Dictionary<string, string> columnMap;

        public ListViewTicketComparer(int column, bool ascending, Dictionary<string, string> columnMap)
        {
            col = column;
            this.ascending = ascending;
            this.columnMap = columnMap;
        }

        public int Compare(object x, object y)
        {
            var itemX = (ListViewItem)x;
            var itemY = (ListViewItem)y;

            string a = itemX.SubItems[col].Text;
            string b = itemY.SubItems[col].Text;

            // Nếu là số
            if (decimal.TryParse(a, out var numA) && decimal.TryParse(b, out var numB))
                return ascending ? numA.CompareTo(numB) : numB.CompareTo(numA);

            // Nếu là ngày
            if (DateTime.TryParse(a, out var dateA) && DateTime.TryParse(b, out var dateB))
                return ascending ? dateA.CompareTo(dateB) : dateB.CompareTo(dateA);

            // Mặc định so sánh chuỗi
            return ascending ? string.Compare(a, b) : string.Compare(b, a);
        }
    }
}


