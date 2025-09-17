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

namespace WindowsFormsApp1
{
    public partial class CreateTicketForm : Form

        
    {
        TicketController ticketController = new TicketController();
        HistoryController historyController = new HistoryController();
        public CreateTicketForm()
        {
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
        private void btnCreate_Click(object sender, EventArgs e)
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
                txt_employee_code.Clear();
                txt_device_code.Clear();
                txt_decristion.Clear();
                var history = new HistoryDto
                {
                    HistoryID = Guid.NewGuid().ToString(),
                    TicketID = newTicketId,
                    ChangeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    ChangedBy = "Admin",
                    FieldChanged = "",
                    OldValue = "",
                    NewValue = "",
                    IsCreator = true
                };

                historyController.AddHistory(history);
                MessageBox.Show("Tạo ticket thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result, "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
