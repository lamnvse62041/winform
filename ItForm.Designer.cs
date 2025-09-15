namespace WindowsFormsApp1
{
    partial class ItForm
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
            this.listViewIT = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewIT
            // 
            this.listViewIT.HideSelection = false;
            this.listViewIT.Location = new System.Drawing.Point(29, 12);
            this.listViewIT.Name = "listViewIT";
            this.listViewIT.Size = new System.Drawing.Size(741, 331);
            this.listViewIT.TabIndex = 0;
            this.listViewIT.UseCompatibleStateImageBehavior = false;
            // 
            // ItForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewIT);
            this.Name = "ItForm";
            this.Text = "it_form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewIT;
    }
}