namespace WindowsFormsApp1
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tab_controller = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btn_Accept = new System.Windows.Forms.Button();
            this.listview_all_ticket = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_cancel_user = new System.Windows.Forms.Button();
            this.btn_accept_user = new System.Windows.Forms.Button();
            this.listview_user = new System.Windows.Forms.ListView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_device = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_device_code = new System.Windows.Forms.TextBox();
            this.txt_decristion = new System.Windows.Forms.TextBox();
            this.txt_employee_code = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listview_device = new System.Windows.Forms.ListView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.rtb_history = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_create_ticket = new System.Windows.Forms.Button();
            this.tab_controller.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_controller
            // 
            this.tab_controller.Controls.Add(this.tabPage1);
            this.tab_controller.Controls.Add(this.tabPage2);
            this.tab_controller.Controls.Add(this.tabPage3);
            this.tab_controller.Controls.Add(this.tabPage4);
            this.tab_controller.Controls.Add(this.tabPage5);
            this.tab_controller.Location = new System.Drawing.Point(12, 12);
            this.tab_controller.Name = "tab_controller";
            this.tab_controller.SelectedIndex = 0;
            this.tab_controller.Size = new System.Drawing.Size(776, 386);
            this.tab_controller.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_create_ticket);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.txtSearch);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.btn_Accept);
            this.tabPage1.Controls.Add(this.listview_all_ticket);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 360);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tất cả vé";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(659, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btn_search_click);
            // 
            // txtSearch
            // 
            this.txtSearch.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSearch.Location = new System.Drawing.Point(534, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(572, 311);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btn_Accept
            // 
            this.btn_Accept.Location = new System.Drawing.Point(88, 311);
            this.btn_Accept.Name = "btn_Accept";
            this.btn_Accept.Size = new System.Drawing.Size(105, 23);
            this.btn_Accept.TabIndex = 1;
            this.btn_Accept.Text = "Xóa ticket";
            this.btn_Accept.UseVisualStyleBackColor = true;
            this.btn_Accept.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // listview_all_ticket
            // 
            this.listview_all_ticket.HideSelection = false;
            this.listview_all_ticket.Location = new System.Drawing.Point(0, 43);
            this.listview_all_ticket.Name = "listview_all_ticket";
            this.listview_all_ticket.Size = new System.Drawing.Size(768, 247);
            this.listview_all_ticket.TabIndex = 0;
            this.listview_all_ticket.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_cancel_user);
            this.tabPage2.Controls.Add(this.btn_accept_user);
            this.tabPage2.Controls.Add(this.listview_user);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 360);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tất cả người dùng";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_cancel_user
            // 
            this.btn_cancel_user.Location = new System.Drawing.Point(581, 322);
            this.btn_cancel_user.Name = "btn_cancel_user";
            this.btn_cancel_user.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel_user.TabIndex = 3;
            this.btn_cancel_user.Text = "Hủy";
            this.btn_cancel_user.UseVisualStyleBackColor = true;
            // 
            // btn_accept_user
            // 
            this.btn_accept_user.Location = new System.Drawing.Point(92, 322);
            this.btn_accept_user.Name = "btn_accept_user";
            this.btn_accept_user.Size = new System.Drawing.Size(75, 23);
            this.btn_accept_user.TabIndex = 2;
            this.btn_accept_user.Text = "Đồng ý";
            this.btn_accept_user.UseVisualStyleBackColor = true;
            // 
            // listview_user
            // 
            this.listview_user.HideSelection = false;
            this.listview_user.Location = new System.Drawing.Point(0, 0);
            this.listview_user.Name = "listview_user";
            this.listview_user.Size = new System.Drawing.Size(768, 303);
            this.listview_user.TabIndex = 0;
            this.listview_user.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.btnCreate);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label_device);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.txt_device_code);
            this.tabPage3.Controls.Add(this.txt_decristion);
            this.tabPage3.Controls.Add(this.txt_employee_code);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(768, 360);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tạo vé sửa chữa";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(282, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "(*)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(282, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "(*)";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(528, 175);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Tạo";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.create_Ticket);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mô tả tình trạng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mã nhân viên";
            // 
            // label_device
            // 
            this.label_device.Location = new System.Drawing.Point(0, 0);
            this.label_device.Name = "label_device";
            this.label_device.Size = new System.Drawing.Size(100, 23);
            this.label_device.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã thiết bị";
            // 
            // txt_device_code
            // 
            this.txt_device_code.Location = new System.Drawing.Point(305, 46);
            this.txt_device_code.Name = "txt_device_code";
            this.txt_device_code.Size = new System.Drawing.Size(121, 20);
            this.txt_device_code.TabIndex = 3;
            this.txt_device_code.Enter += new System.EventHandler(this.txt_device_code_Enter);
            this.txt_device_code.Leave += new System.EventHandler(this.txt_device_code_Leave);
            // 
            // txt_decristion
            // 
            this.txt_decristion.Location = new System.Drawing.Point(305, 122);
            this.txt_decristion.Name = "txt_decristion";
            this.txt_decristion.Size = new System.Drawing.Size(178, 20);
            this.txt_decristion.TabIndex = 1;
            // 
            // txt_employee_code
            // 
            this.txt_employee_code.Location = new System.Drawing.Point(305, 82);
            this.txt_employee_code.Name = "txt_employee_code";
            this.txt_employee_code.Size = new System.Drawing.Size(121, 20);
            this.txt_employee_code.TabIndex = 0;
            this.txt_employee_code.Enter += new System.EventHandler(this.txt_employee_code_Enter);
            this.txt_employee_code.Leave += new System.EventHandler(this.txt_employee_code_Leave);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.listview_device);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(768, 360);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tất cả thiết bị";
            // 
            // listview_device
            // 
            this.listview_device.HideSelection = false;
            this.listview_device.Location = new System.Drawing.Point(3, 3);
            this.listview_device.Name = "listview_device";
            this.listview_device.Size = new System.Drawing.Size(762, 323);
            this.listview_device.TabIndex = 0;
            this.listview_device.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.rtb_history);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(768, 360);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Lịch sử thay đổi";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // rtb_history
            // 
            this.rtb_history.BackColor = System.Drawing.Color.White;
            this.rtb_history.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_history.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtb_history.Location = new System.Drawing.Point(0, 0);
            this.rtb_history.Name = "rtb_history";
            this.rtb_history.ReadOnly = true;
            this.rtb_history.Size = new System.Drawing.Size(768, 360);
            this.rtb_history.TabIndex = 0;
            this.rtb_history.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // btn_create_ticket
            // 
            this.btn_create_ticket.Location = new System.Drawing.Point(43, 15);
            this.btn_create_ticket.Name = "btn_create_ticket";
            this.btn_create_ticket.Size = new System.Drawing.Size(105, 23);
            this.btn_create_ticket.TabIndex = 5;
            this.btn_create_ticket.Text = "Tạo ticket";
            this.btn_create_ticket.UseVisualStyleBackColor = true;
            this.btn_create_ticket.Click += new System.EventHandler(this.button1_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tab_controller);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.tab_controller.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab_controller;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2; 
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btn_Accept;
        private System.Windows.Forms.ListView listview_all_ticket;
        private System.Windows.Forms.ListView listview_user;
        private System.Windows.Forms.Button btn_cancel_user;
        private System.Windows.Forms.Button btn_accept_user;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListView listview_device;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_device;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_device_code;
        private System.Windows.Forms.TextBox txt_decristion;
        private System.Windows.Forms.TextBox txt_employee_code;
        //private System.Windows.Forms.ComboBox cb_type_device;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtb_history;
        private System.Windows.Forms.Button btn_create_ticket;
    }
}