namespace Sync
{
    partial class WhichLab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhichLab));
            this.selectalabprompt = new System.Windows.Forms.TextBox();
            this.labdropdown = new System.Windows.Forms.ComboBox();
            this.okbutton = new System.Windows.Forms.Button();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.note = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // selectalabprompt
            // 
            this.selectalabprompt.BackColor = System.Drawing.SystemColors.Control;
            this.selectalabprompt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.selectalabprompt.Location = new System.Drawing.Point(5, 3);
            this.selectalabprompt.Name = "selectalabprompt";
            this.selectalabprompt.ReadOnly = true;
            this.selectalabprompt.Size = new System.Drawing.Size(248, 13);
            this.selectalabprompt.TabIndex = 0;
            this.selectalabprompt.TabStop = false;
            // 
            // labdropdown
            // 
            this.labdropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.labdropdown.FormattingEnabled = true;
            this.labdropdown.Location = new System.Drawing.Point(5, 19);
            this.labdropdown.Name = "labdropdown";
            this.labdropdown.Size = new System.Drawing.Size(245, 21);
            this.labdropdown.TabIndex = 1;
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(32, 60);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 2;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // cancelbutton
            // 
            this.cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbutton.Location = new System.Drawing.Point(138, 61);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.cancelbutton.TabIndex = 4;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // note
            // 
            this.note.BackColor = System.Drawing.SystemColors.Control;
            this.note.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.note.Location = new System.Drawing.Point(5, 43);
            this.note.Name = "note";
            this.note.ReadOnly = true;
            this.note.Size = new System.Drawing.Size(247, 13);
            this.note.TabIndex = 5;
            this.note.TabStop = false;
            this.note.Text = "Note: You cannot change this after installation.";
            // 
            // WhichLab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 89);
            this.ControlBox = false;
            this.Controls.Add(this.note);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.labdropdown);
            this.Controls.Add(this.selectalabprompt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WhichLab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select a lab";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox selectalabprompt;
        private System.Windows.Forms.ComboBox labdropdown;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.TextBox note;
    }
}