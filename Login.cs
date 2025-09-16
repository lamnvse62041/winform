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
using WindowsFormsApp1.Dtos;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        private TicketController ticketController;
        private HistoryController historyController = new HistoryController();
        public Login()
        {
            ticketController = new TicketController();
            SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
            InitializeComponent();
            txt_device_code.Text = "Nhập device code...";
            txt_device_code.ForeColor = Color.Gray;

            txt_employee_code.Text = "Nhập EmployeeID...";
            txt_employee_code.ForeColor = Color.Gray;
            this.ActiveControl = txt_device_code;
            txt_device_code.TabIndex = 0;
            txt_employee_code.TabIndex = 1;
            txt_decristion.TabIndex = 2;
            btnCreate.TabIndex = 3;
        }

        private void btn_login(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ User và Password!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            UserController controller = new UserController();
            UserDto userDto = controller.Login(username, password);

            // 👉 Check null trước
            if (userDto == null || string.IsNullOrEmpty(userDto.Role))
            {
                MessageBox.Show("User hoặc Password không đúng!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // Mở form theo role
            this.Hide();
            Form nextForm;
            switch (userDto.Role)
            {
                case "Admin":
                    nextForm = new AdminForm();
                    break;
                case "IT":
                    nextForm = new ItForm(userDto.EmployeeID);
                    break;
                case "Employee":
                    nextForm = new EmployeeForm();
                    break;
                default:
                    MessageBox.Show("Role không hợp lệ!",
                                    "Lỗi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    this.Show();
                    return;
            }

            nextForm.ShowDialog();
            this.Show();
        }

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
            string result = ticketController.AddTicket(ticket , out newTicketId);

            if (result == "success")
            {
                txt_employee_code.Clear();
                txt_device_code.Clear();
                txt_decristion.Clear();

                MessageBox.Show("Tạo ticket thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var history = new HistoryDto
                    {
                        HistoryID = Guid.NewGuid().ToString(),
                        TicketID = newTicketId,
                        ChangeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                        ChangedBy = "Người dùng",
                        FieldChanged = "",
                        OldValue = "",
                        NewValue = "",
                        IsCreator = true
                    };

                    historyController.AddHistory(history);
            }
            else
            {
                MessageBox.Show(result, "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void txt_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
