namespace Sync
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startbackupbutton = new System.Windows.Forms.Button();
            this.statustext = new System.Windows.Forms.TextBox();
            this.excludefilebutton = new System.Windows.Forms.Button();
            this.excludefolderbutton = new System.Windows.Forms.Button();
            this.selectallbutton = new System.Windows.Forms.Button();
            this.changepreferencesbutton = new System.Windows.Forms.Button();
            this.selectextensiontext = new System.Windows.Forms.TextBox();
            this.extensioncheckbox = new System.Windows.Forms.CheckedListBox();
            this.listexcludedfiles = new System.Windows.Forms.ListView();
            this.listfiles = new System.Windows.Forms.ListView();
            this.selectfilebutton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.listexcludedfolders = new System.Windows.Forms.ListView();
            this.listfolders = new System.Windows.Forms.ListView();
            this.selectfolderbutton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.notificationtext = new System.Windows.Forms.RichTextBox();
            this.tabpane = new System.Windows.Forms.TabControl();
            this.localrestore = new System.Windows.Forms.TabPage();
            this.localrestoretablelayout = new System.Windows.Forms.TableLayoutPanel();
            this.filerestorecheckbox = new System.Windows.Forms.CheckedListBox();
            this.localrestorebuttonpanel = new System.Windows.Forms.Panel();
            this.selectdatebutton = new System.Windows.Forms.Button();
            this.selectallfilebutton = new System.Windows.Forms.Button();
            this.refreshbuttton = new System.Windows.Forms.Button();
            this.restorebutton = new System.Windows.Forms.Button();
            this.cloudrestore = new System.Windows.Forms.TabPage();
            this.cloudrestoretablelayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.selectfilerestorebutton = new System.Windows.Forms.Button();
            this.refreshbutton2 = new System.Windows.Forms.Button();
            this.website = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabpane.SuspendLayout();
            this.localrestore.SuspendLayout();
            this.localrestoretablelayout.SuspendLayout();
            this.localrestorebuttonpanel.SuspendLayout();
            this.cloudrestore.SuspendLayout();
            this.cloudrestoretablelayout.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Sync";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            this.notifyIcon1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseMove);
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 4);
            this.panel1.Controls.Add(this.startbackupbutton);
            this.panel1.Controls.Add(this.statustext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 525);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 23);
            this.panel1.TabIndex = 16;
            // 
            // startbackupbutton
            // 
            this.startbackupbutton.AutoSize = true;
            this.startbackupbutton.Location = new System.Drawing.Point(0, 0);
            this.startbackupbutton.Name = "startbackupbutton";
            this.startbackupbutton.Size = new System.Drawing.Size(120, 23);
            this.startbackupbutton.TabIndex = 8;
            this.startbackupbutton.Text = "Start Backup";
            this.startbackupbutton.UseVisualStyleBackColor = true;
            this.startbackupbutton.Click += new System.EventHandler(this.startbackupbutton_Click);
            // 
            // statustext
            // 
            this.statustext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statustext.BackColor = System.Drawing.SystemColors.Control;
            this.statustext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statustext.Location = new System.Drawing.Point(126, 3);
            this.statustext.Name = "statustext";
            this.statustext.ReadOnly = true;
            this.statustext.Size = new System.Drawing.Size(681, 13);
            this.statustext.TabIndex = 15;
            // 
            // excludefilebutton
            // 
            this.excludefilebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.excludefilebutton.AutoSize = true;
            this.excludefilebutton.Location = new System.Drawing.Point(411, 264);
            this.excludefilebutton.Name = "excludefilebutton";
            this.excludefilebutton.Size = new System.Drawing.Size(120, 23);
            this.excludefilebutton.TabIndex = 6;
            this.excludefilebutton.Text = "Select files to exclude";
            this.excludefilebutton.UseVisualStyleBackColor = true;
            this.excludefilebutton.Click += new System.EventHandler(this.excludefilebutton_Click);
            // 
            // excludefolderbutton
            // 
            this.excludefolderbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.excludefolderbutton.AutoSize = true;
            this.excludefolderbutton.Location = new System.Drawing.Point(411, 3);
            this.excludefolderbutton.Name = "excludefolderbutton";
            this.excludefolderbutton.Size = new System.Drawing.Size(133, 23);
            this.excludefolderbutton.TabIndex = 2;
            this.excludefolderbutton.Text = "Select folders to exclude";
            this.excludefolderbutton.UseVisualStyleBackColor = true;
            this.excludefolderbutton.Click += new System.EventHandler(this.excludefolderbutton_Click);
            // 
            // selectallbutton
            // 
            this.selectallbutton.AutoSize = true;
            this.selectallbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectallbutton.Location = new System.Drawing.Point(819, 525);
            this.selectallbutton.Name = "selectallbutton";
            this.selectallbutton.Size = new System.Drawing.Size(134, 23);
            this.selectallbutton.TabIndex = 13;
            this.selectallbutton.Text = "Select All";
            this.selectallbutton.UseVisualStyleBackColor = true;
            this.selectallbutton.Click += new System.EventHandler(this.selectallbutton_Click);
            // 
            // changepreferencesbutton
            // 
            this.changepreferencesbutton.AutoSize = true;
            this.changepreferencesbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changepreferencesbutton.Location = new System.Drawing.Point(819, 554);
            this.changepreferencesbutton.Name = "changepreferencesbutton";
            this.changepreferencesbutton.Size = new System.Drawing.Size(134, 24);
            this.changepreferencesbutton.TabIndex = 14;
            this.changepreferencesbutton.Text = "Change Preferences";
            this.changepreferencesbutton.UseVisualStyleBackColor = true;
            this.changepreferencesbutton.Click += new System.EventHandler(this.changepreferencesbutton_Click);
            // 
            // selectextensiontext
            // 
            this.selectextensiontext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectextensiontext.BackColor = System.Drawing.SystemColors.Control;
            this.selectextensiontext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.selectextensiontext.Location = new System.Drawing.Point(819, 13);
            this.selectextensiontext.Name = "selectextensiontext";
            this.selectextensiontext.Size = new System.Drawing.Size(104, 13);
            this.selectextensiontext.TabIndex = 10;
            this.selectextensiontext.Text = "Select file extensions:";
            // 
            // extensioncheckbox
            // 
            this.extensioncheckbox.BackColor = System.Drawing.SystemColors.Control;
            this.extensioncheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extensioncheckbox.FormattingEnabled = true;
            this.extensioncheckbox.Location = new System.Drawing.Point(819, 32);
            this.extensioncheckbox.Name = "extensioncheckbox";
            this.tableLayoutPanel1.SetRowSpan(this.extensioncheckbox, 3);
            this.extensioncheckbox.Size = new System.Drawing.Size(134, 487);
            this.extensioncheckbox.TabIndex = 9;
            this.extensioncheckbox.SelectedIndexChanged += new System.EventHandler(this.extensioncheckbox_SelectedIndexChanged);
            // 
            // listexcludedfiles
            // 
            this.listexcludedfiles.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.listexcludedfiles, 2);
            this.listexcludedfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listexcludedfiles.Location = new System.Drawing.Point(411, 293);
            this.listexcludedfiles.Name = "listexcludedfiles";
            this.listexcludedfiles.Size = new System.Drawing.Size(402, 226);
            this.listexcludedfiles.TabIndex = 7;
            this.listexcludedfiles.UseCompatibleStateImageBehavior = false;
            this.listexcludedfiles.View = System.Windows.Forms.View.List;
            this.listexcludedfiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listexcludedfiles_KeyDown);
            // 
            // listfiles
            // 
            this.listfiles.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.listfiles, 2);
            this.listfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listfiles.Location = new System.Drawing.Point(3, 293);
            this.listfiles.Name = "listfiles";
            this.listfiles.Size = new System.Drawing.Size(402, 226);
            this.listfiles.TabIndex = 5;
            this.listfiles.UseCompatibleStateImageBehavior = false;
            this.listfiles.View = System.Windows.Forms.View.List;
            this.listfiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listfiles_KeyDown);
            // 
            // selectfilebutton
            // 
            this.selectfilebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectfilebutton.AutoSize = true;
            this.selectfilebutton.Location = new System.Drawing.Point(3, 264);
            this.selectfilebutton.Name = "selectfilebutton";
            this.selectfilebutton.Size = new System.Drawing.Size(119, 23);
            this.selectfilebutton.TabIndex = 4;
            this.selectfilebutton.Text = "Select files to backup";
            this.selectfilebutton.UseVisualStyleBackColor = true;
            this.selectfilebutton.Click += new System.EventHandler(this.selectfilebutton_Click);
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 4);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(3, 554);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(810, 24);
            this.progressBar1.TabIndex = 11;
            // 
            // listexcludedfolders
            // 
            this.listexcludedfolders.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.listexcludedfolders, 2);
            this.listexcludedfolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listexcludedfolders.Location = new System.Drawing.Point(411, 32);
            this.listexcludedfolders.Name = "listexcludedfolders";
            this.listexcludedfolders.Size = new System.Drawing.Size(402, 226);
            this.listexcludedfolders.TabIndex = 3;
            this.listexcludedfolders.UseCompatibleStateImageBehavior = false;
            this.listexcludedfolders.View = System.Windows.Forms.View.List;
            this.listexcludedfolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listexcludedfolders_KeyDown);
            // 
            // listfolders
            // 
            this.listfolders.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.listfolders, 2);
            this.listfolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listfolders.Location = new System.Drawing.Point(3, 32);
            this.listfolders.Name = "listfolders";
            this.listfolders.Size = new System.Drawing.Size(402, 226);
            this.listfolders.TabIndex = 1;
            this.listfolders.UseCompatibleStateImageBehavior = false;
            this.listfolders.View = System.Windows.Forms.View.List;
            this.listfolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listfolders_KeyDown);
            // 
            // selectfolderbutton
            // 
            this.selectfolderbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectfolderbutton.AutoSize = true;
            this.selectfolderbutton.Location = new System.Drawing.Point(3, 3);
            this.selectfolderbutton.Name = "selectfolderbutton";
            this.selectfolderbutton.Size = new System.Drawing.Size(132, 23);
            this.selectfolderbutton.TabIndex = 0;
            this.selectfolderbutton.Text = "Select folders to backup";
            this.selectfolderbutton.UseVisualStyleBackColor = true;
            this.selectfolderbutton.Click += new System.EventHandler(this.selectfolderbutton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.22449F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.46939F));
            this.tableLayoutPanel1.Controls.Add(this.selectfolderbutton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listfolders, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listexcludedfolders, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.selectfilebutton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.listfiles, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.listexcludedfiles, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.extensioncheckbox, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.selectextensiontext, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.changepreferencesbutton, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.selectallbutton, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.excludefolderbutton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.excludefilebutton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.notificationtext, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.tabpane, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1255, 581);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // notificationtext
            // 
            this.notificationtext.BackColor = System.Drawing.SystemColors.Window;
            this.notificationtext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationtext.Location = new System.Drawing.Point(959, 293);
            this.notificationtext.Name = "notificationtext";
            this.notificationtext.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.notificationtext, 3);
            this.notificationtext.Size = new System.Drawing.Size(293, 285);
            this.notificationtext.TabIndex = 21;
            this.notificationtext.Text = "";
            this.notificationtext.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.notificationtext_LinkClicked);
            // 
            // tabpane
            // 
            this.tabpane.Controls.Add(this.localrestore);
            this.tabpane.Controls.Add(this.cloudrestore);
            this.tabpane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabpane.Location = new System.Drawing.Point(959, 3);
            this.tabpane.Name = "tabpane";
            this.tableLayoutPanel1.SetRowSpan(this.tabpane, 3);
            this.tabpane.SelectedIndex = 0;
            this.tabpane.Size = new System.Drawing.Size(293, 284);
            this.tabpane.TabIndex = 22;
            // 
            // localrestore
            // 
            this.localrestore.BackColor = System.Drawing.SystemColors.Control;
            this.localrestore.Controls.Add(this.localrestoretablelayout);
            this.localrestore.Location = new System.Drawing.Point(4, 22);
            this.localrestore.Name = "localrestore";
            this.localrestore.Padding = new System.Windows.Forms.Padding(3);
            this.localrestore.Size = new System.Drawing.Size(285, 258);
            this.localrestore.TabIndex = 0;
            this.localrestore.Text = "Local Restore";
            // 
            // localrestoretablelayout
            // 
            this.localrestoretablelayout.ColumnCount = 1;
            this.localrestoretablelayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.localrestoretablelayout.Controls.Add(this.filerestorecheckbox, 0, 0);
            this.localrestoretablelayout.Controls.Add(this.localrestorebuttonpanel, 0, 1);
            this.localrestoretablelayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localrestoretablelayout.Location = new System.Drawing.Point(3, 3);
            this.localrestoretablelayout.Name = "localrestoretablelayout";
            this.localrestoretablelayout.RowCount = 2;
            this.localrestoretablelayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.localrestoretablelayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.localrestoretablelayout.Size = new System.Drawing.Size(279, 252);
            this.localrestoretablelayout.TabIndex = 24;
            // 
            // filerestorecheckbox
            // 
            this.filerestorecheckbox.BackColor = System.Drawing.SystemColors.Control;
            this.filerestorecheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filerestorecheckbox.FormattingEnabled = true;
            this.filerestorecheckbox.HorizontalScrollbar = true;
            this.filerestorecheckbox.Location = new System.Drawing.Point(3, 3);
            this.filerestorecheckbox.Name = "filerestorecheckbox";
            this.filerestorecheckbox.Size = new System.Drawing.Size(273, 215);
            this.filerestorecheckbox.TabIndex = 0;
            // 
            // localrestorebuttonpanel
            // 
            this.localrestorebuttonpanel.Controls.Add(this.selectdatebutton);
            this.localrestorebuttonpanel.Controls.Add(this.selectallfilebutton);
            this.localrestorebuttonpanel.Controls.Add(this.refreshbuttton);
            this.localrestorebuttonpanel.Controls.Add(this.restorebutton);
            this.localrestorebuttonpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localrestorebuttonpanel.Location = new System.Drawing.Point(3, 224);
            this.localrestorebuttonpanel.Name = "localrestorebuttonpanel";
            this.localrestorebuttonpanel.Size = new System.Drawing.Size(273, 25);
            this.localrestorebuttonpanel.TabIndex = 1;
            // 
            // selectdatebutton
            // 
            this.selectdatebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectdatebutton.AutoSize = true;
            this.selectdatebutton.Location = new System.Drawing.Point(130, 2);
            this.selectdatebutton.Name = "selectdatebutton";
            this.selectdatebutton.Size = new System.Drawing.Size(78, 23);
            this.selectdatebutton.TabIndex = 25;
            this.selectdatebutton.Text = "Select Dates";
            this.selectdatebutton.UseVisualStyleBackColor = true;
            this.selectdatebutton.Click += new System.EventHandler(this.selectdatebutton_Click);
            // 
            // selectallfilebutton
            // 
            this.selectallfilebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectallfilebutton.AutoSize = true;
            this.selectallfilebutton.Location = new System.Drawing.Point(0, 2);
            this.selectallfilebutton.Name = "selectallfilebutton";
            this.selectallfilebutton.Size = new System.Drawing.Size(72, 23);
            this.selectallfilebutton.TabIndex = 24;
            this.selectallfilebutton.Text = "Select All";
            this.selectallfilebutton.UseVisualStyleBackColor = true;
            this.selectallfilebutton.Click += new System.EventHandler(this.selectallfilebutton_Click);
            // 
            // refreshbuttton
            // 
            this.refreshbuttton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.refreshbuttton.AutoSize = true;
            this.refreshbuttton.Location = new System.Drawing.Point(210, 2);
            this.refreshbuttton.Name = "refreshbuttton";
            this.refreshbuttton.Size = new System.Drawing.Size(60, 23);
            this.refreshbuttton.TabIndex = 24;
            this.refreshbuttton.Text = "Refresh";
            this.refreshbuttton.UseVisualStyleBackColor = true;
            this.refreshbuttton.Click += new System.EventHandler(this.refreshbuttton_Click);
            // 
            // restorebutton
            // 
            this.restorebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.restorebutton.AutoSize = true;
            this.restorebutton.Location = new System.Drawing.Point(74, 2);
            this.restorebutton.Name = "restorebutton";
            this.restorebutton.Size = new System.Drawing.Size(54, 23);
            this.restorebutton.TabIndex = 22;
            this.restorebutton.Text = "Restore";
            this.restorebutton.UseVisualStyleBackColor = true;
            this.restorebutton.Click += new System.EventHandler(this.restorebutton_Click);
            // 
            // cloudrestore
            // 
            this.cloudrestore.BackColor = System.Drawing.SystemColors.Control;
            this.cloudrestore.Controls.Add(this.cloudrestoretablelayout);
            this.cloudrestore.Location = new System.Drawing.Point(4, 22);
            this.cloudrestore.Name = "cloudrestore";
            this.cloudrestore.Padding = new System.Windows.Forms.Padding(3);
            this.cloudrestore.Size = new System.Drawing.Size(285, 258);
            this.cloudrestore.TabIndex = 1;
            this.cloudrestore.Text = "Cloud Restore";
            // 
            // cloudrestoretablelayout
            // 
            this.cloudrestoretablelayout.ColumnCount = 1;
            this.cloudrestoretablelayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.cloudrestoretablelayout.Controls.Add(this.panel3, 0, 1);
            this.cloudrestoretablelayout.Controls.Add(this.website, 0, 0);
            this.cloudrestoretablelayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cloudrestoretablelayout.Location = new System.Drawing.Point(3, 3);
            this.cloudrestoretablelayout.Name = "cloudrestoretablelayout";
            this.cloudrestoretablelayout.RowCount = 2;
            this.cloudrestoretablelayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.cloudrestoretablelayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.cloudrestoretablelayout.Size = new System.Drawing.Size(279, 252);
            this.cloudrestoretablelayout.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.selectfilerestorebutton);
            this.panel3.Controls.Add(this.refreshbutton2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 224);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 25);
            this.panel3.TabIndex = 1;
            // 
            // selectfilerestorebutton
            // 
            this.selectfilerestorebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectfilerestorebutton.AutoSize = true;
            this.selectfilerestorebutton.Location = new System.Drawing.Point(2, 2);
            this.selectfilerestorebutton.Name = "selectfilerestorebutton";
            this.selectfilerestorebutton.Size = new System.Drawing.Size(73, 23);
            this.selectfilerestorebutton.TabIndex = 25;
            this.selectfilerestorebutton.Text = "Restore File";
            this.selectfilerestorebutton.UseVisualStyleBackColor = true;
            this.selectfilerestorebutton.Click += new System.EventHandler(this.selectfilerestorebutton_Click);
            // 
            // refreshbutton2
            // 
            this.refreshbutton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.refreshbutton2.AutoSize = true;
            this.refreshbutton2.Location = new System.Drawing.Point(210, 2);
            this.refreshbutton2.Name = "refreshbutton2";
            this.refreshbutton2.Size = new System.Drawing.Size(60, 23);
            this.refreshbutton2.TabIndex = 24;
            this.refreshbutton2.Text = "Refresh";
            this.refreshbutton2.UseVisualStyleBackColor = true;
            this.refreshbutton2.Click += new System.EventHandler(this.refreshbuttton_Click);
            // 
            // website
            // 
            this.website.AcceptsTab = true;
            this.website.BackColor = System.Drawing.SystemColors.Control;
            this.website.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.website.Dock = System.Windows.Forms.DockStyle.Fill;
            this.website.Location = new System.Drawing.Point(3, 3);
            this.website.Name = "website";
            this.website.ReadOnly = true;
            this.website.Size = new System.Drawing.Size(273, 215);
            this.website.TabIndex = 2;
            this.website.Text = "";
            this.website.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.website_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 581);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Sync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabpane.ResumeLayout(false);
            this.localrestore.ResumeLayout(false);
            this.localrestoretablelayout.ResumeLayout(false);
            this.localrestorebuttonpanel.ResumeLayout(false);
            this.localrestorebuttonpanel.PerformLayout();
            this.cloudrestore.ResumeLayout(false);
            this.cloudrestoretablelayout.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button selectfolderbutton;
        private System.Windows.Forms.ListView listfolders;
        private System.Windows.Forms.ListView listexcludedfolders;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button selectfilebutton;
        private System.Windows.Forms.ListView listfiles;
        private System.Windows.Forms.ListView listexcludedfiles;
        private System.Windows.Forms.CheckedListBox extensioncheckbox;
        private System.Windows.Forms.TextBox selectextensiontext;
        private System.Windows.Forms.Button changepreferencesbutton;
        private System.Windows.Forms.Button selectallbutton;
        private System.Windows.Forms.Button excludefolderbutton;
        private System.Windows.Forms.Button excludefilebutton;
        private System.Windows.Forms.RichTextBox notificationtext;
        private System.Windows.Forms.Button startbackupbutton;
        private System.Windows.Forms.TextBox statustext;
        private System.Windows.Forms.TabControl tabpane;
        private System.Windows.Forms.TabPage localrestore;
        private System.Windows.Forms.TabPage cloudrestore;
        private System.Windows.Forms.CheckedListBox filerestorecheckbox;
        private System.Windows.Forms.Button selectfilerestorebutton;
        private System.Windows.Forms.Button refreshbuttton;
        private System.Windows.Forms.Button restorebutton;
        private System.Windows.Forms.TableLayoutPanel localrestoretablelayout;
        private System.Windows.Forms.Panel localrestorebuttonpanel;
        private System.Windows.Forms.Button selectallfilebutton;
        private System.Windows.Forms.TableLayoutPanel cloudrestoretablelayout;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button refreshbutton2;
        private System.Windows.Forms.RichTextBox website;
        private System.Windows.Forms.Button selectdatebutton;
    }
}

