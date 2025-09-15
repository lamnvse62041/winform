using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class DatePickerFormDialog : Form
    {
        public DateTime SelectedDate { get; private set; }

        public DatePickerFormDialog(DateTime? currentDate = null)
        {
            InitializeComponent();

            // Tạo MonthCalendar
            var calendar = new MonthCalendar
            {
                MaxSelectionCount = 1
            };
            if (currentDate.HasValue)
                calendar.SetDate(currentDate.Value);

            Controls.Add(calendar);

            // Panel chứa 2 nút
            var panelButtons = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Bottom,
                Height = 45,
                Padding = new Padding(10)
            };
            Controls.Add(panelButtons);

            // Nút Cancel
            var btnCancel = new Button
            {
                Text = "Hủy",
                Width = 80,
                Height = 30
            };
            btnCancel.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            panelButtons.Controls.Add(btnCancel);

            // Nút OK
            var btnOk = new Button
            {
                Text = "OK",
                Width = 80,
                Height = 30
            };
            btnOk.Click += (s, e) =>
            {
                SelectedDate = calendar.SelectionStart;
                DialogResult = DialogResult.OK;
                Close();
            };
            panelButtons.Controls.Add(btnOk);

            // Auto size form theo calendar + button
            this.ClientSize = new System.Drawing.Size(
                calendar.PreferredSize.Width+50,
                calendar.PreferredSize.Height + panelButtons.Height
            );

            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Chọn ngày";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
    }
}
