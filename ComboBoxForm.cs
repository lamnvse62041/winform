using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class ComboBoxForm : Form
{
    private ComboBox comboBox;
    private Button btnOk;
    private Button btnCancel;

    public string SelectedValue { get; private set; }

    public ComboBoxForm(string title, List<string> options, string currentValue = "")
    {
        this.Text = title;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Size = new Size(300, 150);

        comboBox = new ComboBox { Left = 20, Top = 20, Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
        comboBox.Items.AddRange(options.ToArray());

        if (!string.IsNullOrEmpty(currentValue) && options.Contains(currentValue))
            comboBox.SelectedItem = currentValue;

        btnOk = new Button { Text = "OK", Left = 60, Width = 80, Top = 60, DialogResult = DialogResult.OK };
        btnCancel = new Button { Text = "Hủy", Left = 160, Width = 80, Top = 60, DialogResult = DialogResult.Cancel };

        btnOk.Click += (s, e) => { SelectedValue = comboBox.SelectedItem?.ToString(); };
        btnCancel.Click += (s, e) => { this.Close(); };

        this.Controls.Add(comboBox);
        this.Controls.Add(btnOk);
        this.Controls.Add(btnCancel);

        this.AcceptButton = btnOk;
        this.CancelButton = btnCancel;
    }
}
