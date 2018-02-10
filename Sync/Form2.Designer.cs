namespace Sync
{
    partial class Preferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
            this.frequencytext = new System.Windows.Forms.TextBox();
            this.frequency = new System.Windows.Forms.TextBox();
            this.minstext = new System.Windows.Forms.TextBox();
            this.backupdirectorytext = new System.Windows.Forms.TextBox();
            this.backupdirectory = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.backupsizetext = new System.Windows.Forms.TextBox();
            this.mbtext = new System.Windows.Forms.TextBox();
            this.directorysize = new System.Windows.Forms.TextBox();
            this.okbutton = new System.Windows.Forms.Button();
            this.email = new System.Windows.Forms.TextBox();
            this.emailtext = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.trustedlist = new System.Windows.Forms.CheckedListBox();
            this.selectallprocessbutton = new System.Windows.Forms.Button();
            this.noofmonthstext = new System.Windows.Forms.TextBox();
            this.noofmonths = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.directorysizetext = new System.Windows.Forms.TextBox();
            this.directorysizebar = new System.Windows.Forms.ProgressBar();
            this.deleteallversionbutton = new System.Windows.Forms.Button();
            this.months = new System.Windows.Forms.TextBox();
            this.runatstartupcheckbox = new System.Windows.Forms.CheckBox();
            this.deleteaccountbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.noofmonths)).BeginInit();
            this.SuspendLayout();
            // 
            // frequencytext
            // 
            this.frequencytext.BackColor = System.Drawing.SystemColors.Control;
            this.frequencytext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.frequencytext.Location = new System.Drawing.Point(5, 4);
            this.frequencytext.Name = "frequencytext";
            this.frequencytext.ReadOnly = true;
            this.frequencytext.Size = new System.Drawing.Size(138, 13);
            this.frequencytext.TabIndex = 1;
            this.frequencytext.Text = "Enter the backup frequency:";
            // 
            // frequency
            // 
            this.frequency.Location = new System.Drawing.Point(143, 1);
            this.frequency.Name = "frequency";
            this.frequency.Size = new System.Drawing.Size(28, 20);
            this.frequency.TabIndex = 2;
            this.frequency.TextChanged += new System.EventHandler(this.frequency_TextChanged);
            // 
            // minstext
            // 
            this.minstext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.minstext.Location = new System.Drawing.Point(176, 4);
            this.minstext.Name = "minstext";
            this.minstext.ReadOnly = true;
            this.minstext.Size = new System.Drawing.Size(24, 13);
            this.minstext.TabIndex = 3;
            this.minstext.Text = "mins";
            // 
            // backupdirectorytext
            // 
            this.backupdirectorytext.BackColor = System.Drawing.SystemColors.Control;
            this.backupdirectorytext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.backupdirectorytext.Location = new System.Drawing.Point(5, 27);
            this.backupdirectorytext.Name = "backupdirectorytext";
            this.backupdirectorytext.ReadOnly = true;
            this.backupdirectorytext.Size = new System.Drawing.Size(147, 13);
            this.backupdirectorytext.TabIndex = 4;
            this.backupdirectorytext.Text = "Choose your backup directory:";
            // 
            // backupdirectory
            // 
            this.backupdirectory.BackColor = System.Drawing.SystemColors.Control;
            this.backupdirectory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.backupdirectory.Location = new System.Drawing.Point(153, 27);
            this.backupdirectory.Name = "backupdirectory";
            this.backupdirectory.ReadOnly = true;
            this.backupdirectory.Size = new System.Drawing.Size(151, 13);
            this.backupdirectory.TabIndex = 5;
            this.backupdirectory.Click += new System.EventHandler(this.currentdirectory_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // backupsizetext
            // 
            this.backupsizetext.BackColor = System.Drawing.SystemColors.Control;
            this.backupsizetext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.backupsizetext.Location = new System.Drawing.Point(5, 51);
            this.backupsizetext.Name = "backupsizetext";
            this.backupsizetext.ReadOnly = true;
            this.backupsizetext.Size = new System.Drawing.Size(166, 13);
            this.backupsizetext.TabIndex = 6;
            this.backupsizetext.Text = "Choose your backup directory size:";
            // 
            // mbtext
            // 
            this.mbtext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mbtext.Location = new System.Drawing.Point(212, 51);
            this.mbtext.Name = "mbtext";
            this.mbtext.ReadOnly = true;
            this.mbtext.Size = new System.Drawing.Size(16, 13);
            this.mbtext.TabIndex = 8;
            this.mbtext.Text = "MB";
            // 
            // directorysize
            // 
            this.directorysize.Location = new System.Drawing.Point(172, 48);
            this.directorysize.Name = "directorysize";
            this.directorysize.Size = new System.Drawing.Size(39, 20);
            this.directorysize.TabIndex = 7;
            this.directorysize.TextChanged += new System.EventHandler(this.maxsize_TextChanged);
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(4, 323);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 11;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // email
            // 
            this.email.BackColor = System.Drawing.SystemColors.Control;
            this.email.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.email.Location = new System.Drawing.Point(108, 76);
            this.email.Name = "email";
            this.email.ReadOnly = true;
            this.email.Size = new System.Drawing.Size(218, 13);
            this.email.TabIndex = 13;
            // 
            // emailtext
            // 
            this.emailtext.BackColor = System.Drawing.SystemColors.Control;
            this.emailtext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.emailtext.Location = new System.Drawing.Point(5, 76);
            this.emailtext.Name = "emailtext";
            this.emailtext.ReadOnly = true;
            this.emailtext.Size = new System.Drawing.Size(105, 13);
            this.emailtext.TabIndex = 12;
            this.emailtext.Text = "Your registered email:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(5, 129);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(166, 13);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "Select your trusted list of programs:";
            // 
            // trustedlist
            // 
            this.trustedlist.FormattingEnabled = true;
            this.trustedlist.Location = new System.Drawing.Point(5, 152);
            this.trustedlist.Name = "trustedlist";
            this.trustedlist.Size = new System.Drawing.Size(316, 94);
            this.trustedlist.TabIndex = 16;
            this.trustedlist.SelectedIndexChanged += new System.EventHandler(this.trustedlist_SelectedIndexChanged);
            // 
            // selectallprocessbutton
            // 
            this.selectallprocessbutton.Location = new System.Drawing.Point(172, 125);
            this.selectallprocessbutton.Name = "selectallprocessbutton";
            this.selectallprocessbutton.Size = new System.Drawing.Size(75, 23);
            this.selectallprocessbutton.TabIndex = 17;
            this.selectallprocessbutton.Text = "Select All";
            this.selectallprocessbutton.UseVisualStyleBackColor = true;
            this.selectallprocessbutton.Click += new System.EventHandler(this.selectallprocessbutton_Click);
            // 
            // noofmonthstext
            // 
            this.noofmonthstext.BackColor = System.Drawing.SystemColors.Control;
            this.noofmonthstext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.noofmonthstext.Location = new System.Drawing.Point(5, 102);
            this.noofmonthstext.Name = "noofmonthstext";
            this.noofmonthstext.ReadOnly = true;
            this.noofmonthstext.Size = new System.Drawing.Size(201, 13);
            this.noofmonthstext.TabIndex = 18;
            this.noofmonthstext.Text = "Select number of months to keep backup:";
            // 
            // noofmonths
            // 
            this.noofmonths.Location = new System.Drawing.Point(214, 97);
            this.noofmonths.Maximum = 6;
            this.noofmonths.Minimum = 1;
            this.noofmonths.Name = "noofmonths";
            this.noofmonths.Size = new System.Drawing.Size(115, 45);
            this.noofmonths.TabIndex = 21;
            this.noofmonths.Value = 1;
            this.noofmonths.Scroll += new System.EventHandler(this.noofmonths_Scroll);
            // 
            // directorysizetext
            // 
            this.directorysizetext.BackColor = System.Drawing.SystemColors.Control;
            this.directorysizetext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.directorysizetext.Location = new System.Drawing.Point(5, 269);
            this.directorysizetext.Name = "directorysizetext";
            this.directorysizetext.ReadOnly = true;
            this.directorysizetext.Size = new System.Drawing.Size(319, 13);
            this.directorysizetext.TabIndex = 22;
            this.directorysizetext.Text = "Directory Size Text";
            // 
            // directorysizebar
            // 
            this.directorysizebar.Location = new System.Drawing.Point(5, 289);
            this.directorysizebar.Name = "directorysizebar";
            this.directorysizebar.Size = new System.Drawing.Size(319, 23);
            this.directorysizebar.TabIndex = 23;
            // 
            // deleteallversionbutton
            // 
            this.deleteallversionbutton.AutoSize = true;
            this.deleteallversionbutton.Location = new System.Drawing.Point(103, 323);
            this.deleteallversionbutton.Name = "deleteallversionbutton";
            this.deleteallversionbutton.Size = new System.Drawing.Size(100, 23);
            this.deleteallversionbutton.TabIndex = 24;
            this.deleteallversionbutton.Text = "Delete Versioning";
            this.deleteallversionbutton.UseVisualStyleBackColor = true;
            this.deleteallversionbutton.Click += new System.EventHandler(this.deleteallversionbutton_Click);
            // 
            // months
            // 
            this.months.BackColor = System.Drawing.SystemColors.Control;
            this.months.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.months.Location = new System.Drawing.Point(204, 102);
            this.months.Name = "months";
            this.months.ReadOnly = true;
            this.months.Size = new System.Drawing.Size(13, 13);
            this.months.TabIndex = 25;
            // 
            // runatstartupcheckbox
            // 
            this.runatstartupcheckbox.AutoSize = true;
            this.runatstartupcheckbox.Checked = true;
            this.runatstartupcheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.runatstartupcheckbox.Location = new System.Drawing.Point(5, 251);
            this.runatstartupcheckbox.Name = "runatstartupcheckbox";
            this.runatstartupcheckbox.Size = new System.Drawing.Size(123, 17);
            this.runatstartupcheckbox.TabIndex = 26;
            this.runatstartupcheckbox.Text = "Run Sync on startup";
            this.runatstartupcheckbox.UseVisualStyleBackColor = true;
            this.runatstartupcheckbox.CheckedChanged += new System.EventHandler(this.runatstartupcheckbox_CheckedChanged);
            // 
            // deleteaccountbutton
            // 
            this.deleteaccountbutton.AutoSize = true;
            this.deleteaccountbutton.Location = new System.Drawing.Point(225, 323);
            this.deleteaccountbutton.Name = "deleteaccountbutton";
            this.deleteaccountbutton.Size = new System.Drawing.Size(100, 23);
            this.deleteaccountbutton.TabIndex = 27;
            this.deleteaccountbutton.Text = "Delete Account";
            this.deleteaccountbutton.UseVisualStyleBackColor = true;
            this.deleteaccountbutton.Click += new System.EventHandler(this.deleteaccountbutton_Click);
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 349);
            this.Controls.Add(this.deleteaccountbutton);
            this.Controls.Add(this.runatstartupcheckbox);
            this.Controls.Add(this.months);
            this.Controls.Add(this.deleteallversionbutton);
            this.Controls.Add(this.directorysizebar);
            this.Controls.Add(this.directorysizetext);
            this.Controls.Add(this.selectallprocessbutton);
            this.Controls.Add(this.noofmonths);
            this.Controls.Add(this.noofmonthstext);
            this.Controls.Add(this.trustedlist);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.email);
            this.Controls.Add(this.emailtext);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.mbtext);
            this.Controls.Add(this.directorysize);
            this.Controls.Add(this.backupsizetext);
            this.Controls.Add(this.backupdirectory);
            this.Controls.Add(this.backupdirectorytext);
            this.Controls.Add(this.minstext);
            this.Controls.Add(this.frequency);
            this.Controls.Add(this.frequencytext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Preferences";
            this.Text = "Preferences";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.noofmonths)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox frequencytext;
        private System.Windows.Forms.TextBox frequency;
        private System.Windows.Forms.TextBox minstext;
        private System.Windows.Forms.TextBox backupdirectorytext;
        private System.Windows.Forms.TextBox backupdirectory;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox backupsizetext;
        private System.Windows.Forms.TextBox mbtext;
        private System.Windows.Forms.TextBox directorysize;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox emailtext;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckedListBox trustedlist;
        private System.Windows.Forms.Button selectallprocessbutton;
        private System.Windows.Forms.TextBox noofmonthstext;
        private System.Windows.Forms.TrackBar noofmonths;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox directorysizetext;
        private System.Windows.Forms.ProgressBar directorysizebar;
        private System.Windows.Forms.Button deleteallversionbutton;
        private System.Windows.Forms.TextBox months;
        private System.Windows.Forms.CheckBox runatstartupcheckbox;
        private System.Windows.Forms.Button deleteaccountbutton;
    }
}