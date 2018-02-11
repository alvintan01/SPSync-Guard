using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using Newtonsoft.Json;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sync
{
    public partial class Form1 : Form
    {
        List<String> folderpathlist = new List<String>();
        List<String> excludefolderpathlist = new List<String>();
        List<String> filepathlist = new List<String>();
        List<String> excludefilepathlist = new List<String>();
        List<String> filepath = new List<string>();
        List<String> distinctextension = new List<string>();
        List<String> checkedextension = new List<string>();
        List<String> filepathtobackup = new List<string>();
        private ContextMenu contextMenu1;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        Boolean running = false;
        List<String> ransomwareextensions = new List<string>();
        List<String> ransomwarefiles = new List<string>();
        Boolean ransomware = false;
        int notificationid = 0;
        BackgroundWorker bgw = new BackgroundWorker();
        Dictionary<string, string> hashdictionary = new Dictionary<string, string>();
        List<String> zippathnotsent = new List<string>();
        Timer timer1;
        String lastsync = "";
        Boolean unknownprocess = false;
        int nooffilechanged = 0;
        BackgroundWorker bgw2 = new BackgroundWorker();
        Boolean tripwire = false;
        Boolean emailsent = true;
        BackgroundWorker bgw3 = new BackgroundWorker();
        List<String> trustedadminprocesslist = new List<string>();
        List<String> blackadminprocesslist = new List<string>();
        String notification = "";
        List<String> secretkeylist = new List<string>();
        String backuphash = "";
        List<String> sentprocesslist = new List<string>();
        String lastupdatetimestamp = "01-01-1970 00-00-00";
        Boolean firstlaunch = false;

        public Form1()
        {
            InitializeComponent();
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //InstallCertificate("cert.cert");
            getsecretkey();
            if (Properties.Settings.Default.email != "")
            {
                if (Properties.Settings.Default.includedfolders != "")
                    folderpathlist = Properties.Settings.Default.includedfolders.Split(',').ToList();
                if (Properties.Settings.Default.excludedfolders != "")
                    excludefolderpathlist = Properties.Settings.Default.excludedfolders.Split(',').ToList();
                if (Properties.Settings.Default.includedfiles != "")
                    filepathlist = Properties.Settings.Default.includedfiles.Split(',').ToList();
                if (Properties.Settings.Default.excludedfiles != "")
                    excludefilepathlist = Properties.Settings.Default.excludedfiles.Split(',').ToList();
                if (Properties.Settings.Default.fileextension != "")
                    checkedextension = Properties.Settings.Default.fileextension.Split(',').ToList();
                if (Properties.Settings.Default.usersecret != "")
                    Preference.usersecret = Properties.Settings.Default.usersecret;
                if (Properties.Settings.Default.zippathnotsent != "")
                    zippathnotsent = Properties.Settings.Default.zippathnotsent.Split(',').ToList();
                if (Properties.Settings.Default.backupfrequency != 0)
                    Preference.backupfrequency = Properties.Settings.Default.backupfrequency;
                if (Properties.Settings.Default.backupdirectory != "")
                    Preference.backupdirectory = Properties.Settings.Default.backupdirectory;
                if (Properties.Settings.Default.directorysize != 0)
                    Preference.directorysize = Properties.Settings.Default.directorysize;
                if (Properties.Settings.Default.email != "")
                    Preference.email = Properties.Settings.Default.email;
                if (Properties.Settings.Default.processlist != "")
                    Preference.processlist = Properties.Settings.Default.processlist.Split(',').ToList();
                if (Properties.Settings.Default.trustedprocesslist != "")
                    Preference.trustedprocesslist = Properties.Settings.Default.trustedprocesslist.Split(',').ToList();
                if (Properties.Settings.Default.lastsync != "")
                    lastsync = Properties.Settings.Default.lastsync;
                if (Properties.Settings.Default.ransomwareextensions != "")
                    ransomwareextensions = Properties.Settings.Default.ransomwareextensions.Split(',').ToList();
                if (Properties.Settings.Default.ransomwarefiles != "")
                    ransomwarefiles = Properties.Settings.Default.ransomwarefiles.Split(',').ToList();
                if (Properties.Settings.Default.trustedadminprocesslist != "")
                    trustedadminprocesslist = Properties.Settings.Default.trustedadminprocesslist.Split(',').ToList();
                if (Properties.Settings.Default.blackadminprocesslist != "")
                    blackadminprocesslist = Properties.Settings.Default.blackadminprocesslist.Split(',').ToList();
                if (Properties.Settings.Default.notificationtext != "")
                    notification = Properties.Settings.Default.notificationtext;
                if (Properties.Settings.Default.secretkey != "")
                    Preference.secretkey = Properties.Settings.Default.secretkey;
                if (Properties.Settings.Default.secretkeylist != "")
                    secretkeylist = Properties.Settings.Default.secretkeylist.Split(',').ToList();
                if (Properties.Settings.Default.noofmonths != 0)
                    Preference.noofmonths = Properties.Settings.Default.noofmonths;
                if (Properties.Settings.Default.usage != 0)
                    Preference.usage = Properties.Settings.Default.usage;
                if (Properties.Settings.Default.hashdictionary != "")
                    backuphash = Properties.Settings.Default.hashdictionary;
                if (Properties.Settings.Default.sentprocesslist != "")
                    sentprocesslist = Properties.Settings.Default.sentprocesslist.Split(',').ToList();
                if (Properties.Settings.Default.lastupdatetimestamp != "")
                    lastupdatetimestamp = Properties.Settings.Default.lastupdatetimestamp;
                if (Properties.Settings.Default.notificationid != 0)
                    notificationid = Properties.Settings.Default.notificationid;
                if (Properties.Settings.Default.piip != "")
                    Preference.piip = Properties.Settings.Default.piip;
                if (Properties.Settings.Default.pisshfingerprint != "")
                    Preference.pisshfingerprint = Properties.Settings.Default.pisshfingerprint;
                if (Properties.Settings.Default.roomname != "")
                    Preference.roomname = Properties.Settings.Default.roomname;
                if (Properties.Settings.Default.roomid != 0)
                    Preference.roomid = Properties.Settings.Default.roomid;
                populatehashdictionary();
            }
            else //First launch
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\#Donottouch.txt"))
                {
                    using (StreamWriter sw = File.CreateText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\#Donottouch.txt"))
                    {
                        sw.Write("Do not edit this file.");
                    }
                }
                firstlaunch = true;
                SharedFunctions.setstartup(true);
                folderpathlist.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                folderpathlist.Add(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                folderpathlist.Add(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString());//Download folder
                Preference.salt = GenerateString(10);

                DialogResult dialogResult = MessageBox.Show("Is this your first installation?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    WhichLab popup = new WhichLab(false);
                    popup.ShowDialog();

                    if (Preference.closeform)//user cancelled
                    {
                        Environment.Exit(1);
                    }
                    while (!Regex.IsMatch(Preference.email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                    {
                        Preference.email = promptinput("Enter email", "Enter your email. This email will be used to recover your password and to login to the web.", false);
                        if (!Regex.IsMatch(Preference.email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                            MessageBox.Show("Please enter a valid email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    String userexist = "true";

                    while (Preference.webuipassword == "" || Preference.webuipassword.Length < 8 || Preference.webuipassword != Preference.webuipassword2)
                    {
                        Preference.webuipassword = promptinput("Enter password", "Enter a password to login at the WebUI.", true);
                        if (Preference.webuipassword == "" || Preference.webuipassword.Length < 8)
                        {
                            MessageBox.Show("Please enter a valid password. It must at least be 8 characters long.", "Enter password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            continue;
                        }
                        Preference.webuipassword2 = promptinput("Enter password", "Enter your password again.", true);
                        if (Preference.webuipassword != Preference.webuipassword2)
                        {
                            MessageBox.Show("Password does not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    while (Preference.usersecret == "" || userexist == "true" || userexist == "No internet" || Preference.usersecret.Length < 8 || Preference.usersecret != Preference.usersecret2)
                    {
                        Preference.usersecret = promptinput("Enter secretkey", "Enter a secretkey.", true);
                        if (Preference.usersecret == "" || Preference.usersecret.Length < 8)
                        {
                            MessageBox.Show("Please enter a valid secretkey. It must at least be 8 characters long.", "Enter secretkey", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            continue;
                        }
                        Preference.usersecret2 = promptinput("Enter secretkey", "Enter your secretkey again.", true);
                        if (Preference.usersecret != Preference.usersecret2)
                        {
                            MessageBox.Show("Secretkey does not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            userexist = SharedFunctions.createuser();
                            if (userexist == "true")
                            {
                                SharedFunctions.getsession();
                                using (WebClient client = new WebClient())
                                {
                                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/validateUserSecret", new NameValueCollection()
                                {
                                   { "auth", Preference.auth },
                                   { "sessionkey", Preference.sessionkey},
                                   { "username", Preference.email},
                                   { "hash", SharedFunctions.getstringhash(Preference.salt + Preference.usersecret)}
                                });
                                    string result = Encoding.UTF8.GetString(response).Replace("\n", "");
                                    if (result != "Success")
                                    {
                                        MessageBox.Show("Invalid secretkey. Enter your existing secretkey.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        userexist = "true";
                                    }
                                    else
                                    {
                                        userexist = "false";//to break loop once password is correct
                                    }
                                }
                            }
                            else if (userexist == "No internet")
                            {
                                MessageBox.Show("Please connect to the internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
                else
                {
                    WhichLab popup = new WhichLab(true);
                    popup.ShowDialog();

                    if (Preference.closeform)//user cancelled
                    {
                        Environment.Exit(1);
                    }
                    String userexist = "true";
                    while (!Regex.IsMatch(Preference.email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase) || userexist == "true" || userexist == "No internet")
                    {
                        Preference.email = promptinput("Enter email", "Enter your registered email", false);
                        if (!Regex.IsMatch(Preference.email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                            MessageBox.Show("Please enter a valid email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            try
                            {
                                SharedFunctions.getsession();
                                using (WebClient client = new WebClient())
                                {
                                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getSalt", new NameValueCollection()
                                {
                                   { "auth", Preference.auth },
                                   { "sessionkey", Preference.sessionkey},
                                   { "key", Preference.secretkey },
                                   { "username", Preference.email},
                                });
                                    string result = Encoding.UTF8.GetString(response).Replace("\n", "");
                                    if (result == "Invalid User")
                                    {
                                        MessageBox.Show("Invalid email. Check if you entered your registered email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        userexist = "true";
                                    }
                                    else
                                    {
                                        userexist = "false";//to break loop once password is correct
                                        Preference.salt = result;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                userexist = "No internet";
                                MessageBox.Show("Please connect to the internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                    String validated = "false";
                    while (Preference.usersecret == "" || validated == "false" || validated == "No internet")
                    {
                        Preference.usersecret = promptinput("Enter secretkey", "Enter your registered secretkey", true);
                        if (Preference.usersecret == "")
                            MessageBox.Show("Please enter a secretkey.", "Enter secretkey", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            SharedFunctions.getsession();
                            using (WebClient client = new WebClient())
                            {
                                byte[] response = client.UploadValues("https://" + Preference.serverip + "/validateUserSecret", new NameValueCollection()
                                {
                                   { "auth", Preference.auth },
                                   { "sessionkey", Preference.sessionkey},
                                   { "username", Preference.email},
                                   { "hash", SharedFunctions.getstringhash(Preference.salt + Preference.usersecret)}
                                });
                                string result = Encoding.UTF8.GetString(response).Replace("\n", "");
                                if (result != "Success")
                                {
                                    MessageBox.Show("Invalid secretkey. Enter your existing secretkey.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    validated = "true";//to break loop once password is correct
                                }
                            }
                        }
                    }

                }
                MessageBox.Show("Please store your secretkey in a secure location as you will not be able to recover your files if you forget your secretkey. Also, we only guarantee that your files will only be kept up to your file retention period and that file versions will only be kept for 10 days.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            foreach (var path in folderpathlist)
            {
                listfolders.Items.Add(path);
            }
            foreach (var path in excludefolderpathlist)
            {
                listexcludedfolders.Items.Add(path);
            }
            foreach (var path in filepathlist)
            {
                listfiles.Items.Add(path);
            }
            foreach (var path in excludefilepathlist)
            {
                listexcludedfiles.Items.Add(path);
            }
            populateextension();

            contextMenu1 = new ContextMenu();
            menuItem1 = new MenuItem();
            menuItem2 = new MenuItem();

            // Initialize contextMenu1
            contextMenu1.MenuItems.AddRange(new MenuItem[] { menuItem1, menuItem2 });

            // Initialize menuItem1
            menuItem1.Index = 0;
            menuItem1.Text = "Sync Now";
            menuItem1.Click += new EventHandler(startbackupbutton_Click);

            // Initialize menuItem2
            menuItem2.Index = 1;
            menuItem2.Text = "Exit";
            menuItem2.Click += new EventHandler(menuItem2_Click);
            notifyIcon1.ContextMenu = contextMenu1;
            progressBar1.Visible = false;
            getfromdatabase();
            populatefilerestore();
            InitTimer();
            checkprocess();
            savesettings();
            bgw2.DoWork += monitorfile;
            bgw2.RunWorkerAsync();

            bgw3.DoWork += sendprocesslist;
            bgw3.RunWorkerAsync();
            website.Text = "Visit the cloud website at https://" + Preference.serverip + "/index.jsp or the local site at https://" + Preference.piip + "/login.html. Use the restore file option to restore the downloaded file or zip folder.\n\nFor more information about the latest ransomware threats, visit https://" + Preference.serverip + "/viewRfile.jsp.";
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!firstlaunch)
            {
                WindowState = FormWindowState.Minimized;//hide form
                ShowInTaskbar = false;
            }
        }

        private void selectfolderbutton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string newfolderpath = folderBrowserDialog.SelectedPath;
                if (!folderpathlist.Any(path => newfolderpath.Contains(path + "\\")) && !folderpathlist.Any(path => newfolderpath == path))
                {
                    if (folderpathlist.Any(path => path.Contains(newfolderpath + "\\")))
                    {
                        List<String> toberemoved = new List<string>();
                        foreach (var a in folderpathlist)
                        {
                            if (a.Contains(newfolderpath))
                            {
                                toberemoved.Add(a);
                            }
                        }
                        foreach (var item in toberemoved)
                        {
                            folderpathlist.Remove(item);
                            for (int i = 0; i < listfolders.Items.Count; i++)
                            {
                                if (item == listfolders.Items[i].Text)
                                {
                                    listfolders.Items[i].Remove();
                                }
                            }
                        }
                    }
                    folderpathlist.Add(newfolderpath);
                    listfolders.Items.Add(newfolderpath);
                    populateextension();
                }
                else
                {
                    MessageBox.Show("Error! The folder selected is already under the directory of the included folders.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                List<String> toberemoved2 = new List<string>();//check if new exluded folder is excluded in file path
                foreach (var a in filepathlist)
                {
                    if (folderpathlist.Any(path => a.Contains(path)))
                    {
                        foreach (var b in filepathlist)
                        {
                            if (a.Contains(b))
                            {
                                toberemoved2.Add(b);
                            }
                        }
                    }
                }
                foreach (var item in toberemoved2)
                {
                    filepathlist.Remove(item);
                    for (int i = 0; i < listfiles.Items.Count; i++)
                    {
                        if (item == listfiles.Items[i].Text)
                        {
                            listfiles.Items[i].Remove();
                        }
                    }
                }

            }
        }

        private void listfolders_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                foreach (ListViewItem v in listfolders.SelectedItems)
                {
                    listfolders.Items.Remove(v);
                    folderpathlist.Remove(v.Text);
                    for (int i = excludefolderpathlist.Count - 1; i >= 0; i--)
                    {
                        if (excludefolderpathlist[i].Contains(v.Text))
                        {
                            listexcludedfolders.Items.RemoveAt(i);
                            excludefolderpathlist.RemoveAt(i);
                        }
                    }

                    for (int i = excludefilepathlist.Count - 1; i >= 0; i--)
                    {
                        if (excludefilepathlist[i].Contains(v.Text))
                        {
                            listexcludedfiles.Items.RemoveAt(i);
                            excludefilepathlist.RemoveAt(i);
                        }
                    }
                    populateextension();
                }
            }
        }

        private void excludefolderbutton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                String newexcludepath = folderBrowserDialog.SelectedPath;

                if (folderpathlist.Any(path => newexcludepath == path))
                {
                    MessageBox.Show("Error! The excluded folder should be removed from the included folders.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (folderpathlist.Any(path => newexcludepath.Contains(path + "\\")) && !folderpathlist.Any(path => path == newexcludepath) && !excludefolderpathlist.Any(path => path == newexcludepath))
                {
                    if (excludefolderpathlist.Any(path => newexcludepath.Contains(path + "\\")))
                    {
                        MessageBox.Show("Error! The selected excluded folder is already excluded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (excludefolderpathlist.Any(path => path.Contains(newexcludepath + "\\")))
                    {
                        List<String> toberemoved = new List<string>();
                        foreach (var a in excludefolderpathlist)
                        {
                            if (a.Contains(newexcludepath))
                            {
                                toberemoved.Add(a);
                            }
                        }
                        foreach (var item in toberemoved)
                        {
                            excludefolderpathlist.Remove(item);
                            for (int i = 0; i < listexcludedfolders.Items.Count; i++)
                            {
                                if (item == listexcludedfolders.Items[i].Text)
                                {
                                    listexcludedfolders.Items[i].Remove();
                                }
                            }
                        }
                        excludefolderpathlist.Add(newexcludepath);
                        listexcludedfolders.Items.Add(newexcludepath);

                    }
                    else
                    {
                        excludefolderpathlist.Add(newexcludepath);
                        listexcludedfolders.Items.Add(newexcludepath);
                    }
                }
                else if (excludefolderpathlist.Any(path => path == newexcludepath))
                {
                    MessageBox.Show("Error! The excluded folder is already selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Error! The excluded folder selected is not under the directory of the included folders.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                List<String> toberemoved2 = new List<string>();//check if new exluded folder is excluded in file path
                foreach (var a in excludefilepathlist)
                {
                    if (excludefolderpathlist.Any(path => a.Contains(path)))
                    {

                        foreach (var b in excludefilepathlist)
                        {
                            if (a.Contains(b))
                            {
                                toberemoved2.Add(b);
                            }
                        }
                    }
                }
                foreach (var item in toberemoved2)
                {
                    excludefilepathlist.Remove(item);
                    for (int i = 0; i < listexcludedfiles.Items.Count; i++)
                    {
                        if (item == listexcludedfiles.Items[i].Text)
                        {
                            listexcludedfiles.Items[i].Remove();
                        }
                    }
                }
            }
            populateextension();
        }

        private void listexcludedfolders_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                foreach (ListViewItem v in listexcludedfolders.SelectedItems)
                {
                    listexcludedfolders.Items.Remove(v);
                    excludefolderpathlist.Remove(v.Text);

                    for (int i = filepathlist.Count - 1; i >= 0; i--)
                    {
                        if (filepathlist[i].Contains(v.Text))
                        {
                            listfiles.Items.RemoveAt(i);
                            filepathlist.RemoveAt(i);
                        }
                    }
                }
                populateextension();
            }
        }

        private void selectfilebutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filename = openFileDialog.FileName;

                if (((folderpathlist.Any(path => filename.Contains(path + "\\")) && excludefolderpathlist.Any(path2 => filename.Contains(path2 + "\\"))) || (!folderpathlist.Any(path => filename.Contains(path + "\\")) && !filepathlist.Any(path2 => filename == path2))))
                {
                    filepathlist.Add(filename);
                    listfiles.Items.Add(filename);
                }
                else
                {
                    MessageBox.Show("Error! The selected file is already included", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            populateextension();
        }
        private void listfiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                foreach (ListViewItem v in listfiles.SelectedItems)
                {
                    listfiles.Items.Remove(v);
                    filepathlist.Remove(v.Text);
                }
                populateextension();
            }
        }

        private void excludefilebutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filename = openFileDialog.FileName;
                if (filepathlist.Any(path => filename == path))
                {
                    MessageBox.Show("Error! The excluded file should be removed from the included files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if ((folderpathlist.Any(path => filename.Contains(path + "\\")) && !excludefolderpathlist.Any(path => filename.Contains(path + "\\"))) && !excludefilepathlist.Any(path => path == filename))
                {
                    excludefilepathlist.Add(filename);
                    listexcludedfiles.Items.Add(filename);
                }
                else
                {
                    MessageBox.Show("Error! The selected file is already excluded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            populateextension();
        }

        private void listexcludedfiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                foreach (ListViewItem v in listexcludedfiles.SelectedItems)
                {
                    listexcludedfiles.Items.Remove(v);
                    excludefilepathlist.Remove(v.Text);
                }
                populateextension();
            }
        }

        private void startbackupbutton_Click(object sender, EventArgs e)
        {
            //if backup is running
            if (bgw != null && bgw.IsBusy)
            {
                bgw.CancelAsync();
                running = false;
                startbackupbutton.Text = "Start Backup";
                progressBar1.Visible = false;
            }
            else
            {
                running = true;
                startbackupbutton.Text = "Stop Backup";
                progressBar1.Visible = true;
                bgw.ProgressChanged += bgw_ProgressChanged;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                bgw.DoWork += start_backup;
                bgw.WorkerReportsProgress = true;
                bgw.WorkerSupportsCancellation = true;
                bgw.RunWorkerAsync();

            }

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (!Preference.closeform)
                savesettings();
            if (Preference.usercloseform)
            {
                notifyIcon1.Visible = false;
                timer1.Stop();
                try
                {
                    Environment.Exit(0);
                }
                catch (Exception)
                {

                }
            }
            else
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    this.WindowState = FormWindowState.Minimized;
                }
            }
        }

        public List<String> getallfilepath()
        {
            List<String> finalfilepath = new List<string>();
            foreach (var v in folderpathlist)
            {
                try
                {
                    finalfilepath.AddRange(Directory.GetFiles(v, "*", SearchOption.TopDirectoryOnly));
                    foreach (var directory in Directory.GetDirectories(v))
                    {
                        try
                        {
                            finalfilepath.AddRange(Directory.GetFiles(directory, "*", SearchOption.AllDirectories));
                        }
                        catch (UnauthorizedAccessException) { }
                    }

                }
                catch (Exception) { }
            }

            //Remove excluded folders
            foreach (var v in excludefolderpathlist)
            {
                finalfilepath.RemoveAll(path => path.Contains(v));
            }

            finalfilepath.AddRange(filepathlist);

            //Remove excluded files
            foreach (var v in excludefilepathlist)
            {
                finalfilepath.RemoveAll(path => path == v);
            }
            return finalfilepath;
        }

        private void populateextension()
        {
            filepath = getallfilepath();
            List<String> extension = new List<string>();
            extension.Add(".docx");
            extension.Add(".pptx");
            extension.Add(".txt");
            extension.Add(".xlsx");
            extension.Add(".zip");
            if (checkedextension.Count == 0)
            {
                checkedextension.Add(".docx");
                checkedextension.Add(".pptx");
                checkedextension.Add(".txt");
                checkedextension.Add(".xlsx");
                checkedextension.Add(".zip");
            }

            foreach (var a in filepath)
            {
                extension.Add(Path.GetExtension(a));
            }
            distinctextension = new List<String>(extension.Select(x => x).Distinct().OrderBy(x => x));
            extensioncheckbox.Items.Clear();
            //add all possible file extension
            foreach (var a in distinctextension)
            {
                extensioncheckbox.Items.Add(a);
            }
            //check the file extension
            checkedextension.ForEach(item =>
            {
                var index = extensioncheckbox.Items.IndexOf(item);
                if (index != -1)//if can find extension, check it, else ignore.
                    extensioncheckbox.SetItemChecked(index, true);
            });
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon1.ShowBalloonTip(3000, "Notification", "Sync is still running in the background.", ToolTipIcon.Info);
                ShowInTaskbar = false;
                // Hide();
            }
            else
            {
                ShowInTaskbar = true;
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            if (running)
                notifyIcon1.Text = "Sync running...";
            else if (Preference.usage > 10 * 1024 * 1024 * 1024L)
                notifyIcon1.Text = "You have exceed your file limit.";
            else if (ransomware || tripwire)
                notifyIcon1.Text = "Ransomware has been detected on the system.";
            else if (unknownprocess)
                notifyIcon1.Text = "There is a non-trusted process running in your system.";
            else if (lastsync != "")
                notifyIcon1.Text = "Sync was completed at " + lastsync + ".";
            else
                notifyIcon1.Text = "No backup was ran yet.";
        }

        private void menuItem2_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            Preference.usercloseform = true;
            this.Close();
        }

        private void selectallbutton_Click(object sender, EventArgs e)
        {
            if (selectallbutton.Text == "Select All")
            {
                for (int i = 0; i < extensioncheckbox.Items.Count; i++)
                {
                    extensioncheckbox.SetItemChecked(i, true);
                }
                selectallbutton.Text = "Deselect All";
            }
            else
            {
                for (int i = 0; i < extensioncheckbox.Items.Count; i++)
                {
                    extensioncheckbox.SetItemChecked(i, false);
                }
                selectallbutton.Text = "Select All";
            }
        }

        private void changepreferencesbutton_Click(object sender, EventArgs e)
        {
            Preferences form2 = new Preferences();
            form2.ShowDialog();

            if (Preference.closeform)
            {
                Close();
            }
        }

        private String promptinput(String title, String message, Boolean password)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = message;
            if (password)
                textBox.PasswordChar = '*';

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.Cancel)
            {
                Environment.Exit(1);
            }
            return textBox.Text;
        }

        private void getsecretkey()
        {
            try
            {

                SharedFunctions.getsession();
                //get secretkey

                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getSecretKey", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey}
                    });

                    string result = Encoding.UTF8.GetString(response).Replace("\n", "");

                    if (result != "No changes")
                    {
                        secretkeylist.Add(Preference.secretkey);
                        String newkey = SharedFunctions.DecryptStringFromBytes_Aes(Convert.FromBase64String(result), Encoding.ASCII.GetBytes(Preference.secretkey));
                        Preference.secretkey = newkey;
                        Console.WriteLine(newkey);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                notifyIcon1.ShowBalloonTip(3000, "Key Error", "Could not get latest key.", ToolTipIcon.Warning);
            }
        }


        private void getfromdatabase()
        {
            try
            {
                //Get notifications
                notificationtext.Clear();
                if (notification == "")
                {
                    notification = "Ransomware Prevention Tips:" + Environment.NewLine + Environment.NewLine;
                    notificationtext.AppendText("Ransomware Prevention Tips:" + Environment.NewLine + Environment.NewLine);
                }
                else
                    notificationtext.AppendText(notification);

                SharedFunctions.getsession();
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getNotification", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey},
                       { "timestamp", lastupdatetimestamp}
                    });

                    string result = Encoding.UTF8.GetString(response);

                    Notification[] jsonlist = JsonConvert.DeserializeObject<Notification[]>(result);
                    if (jsonlist.Length != 0)//if updates
                    {
                        notificationtext.Clear();
                        notificationtext.AppendText("Ransomware Prevention Tips:" + Environment.NewLine + Environment.NewLine);
                        notification = "";
                        notification = "Ransomware Prevention Tips:" + Environment.NewLine + Environment.NewLine;
                    }

                    foreach (var a in jsonlist)
                    {
                        int currentid = a.ID;
                        String message = a.Message;
                        String date = a.TimeStamp.Substring(0, a.TimeStamp.IndexOf("."));//remove miliseconds
                        if (currentid > notificationid)
                        {
                            notifyIcon1.ShowBalloonTip(10000, "Ransomware Prevention Tips:", message, ToolTipIcon.Info);
                            notificationid = currentid;
                        }
                        notificationtext.AppendText(message + Environment.NewLine + date + Environment.NewLine + Environment.NewLine);
                        notification += message + Environment.NewLine + date + Environment.NewLine + Environment.NewLine;
                    }
                }


                //get ransomwareextension
                SharedFunctions.getsession();
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getRansomwareExtension", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey},
                       { "timestamp", lastupdatetimestamp}
                    });

                    string result = Encoding.UTF8.GetString(response).Replace("\n", "");

                    if (result != "No changes")
                    {
                        RansomwareExtension[] jsonlist = JsonConvert.DeserializeObject<RansomwareExtension[]>(result);
                        ransomwareextensions.Clear();
                        foreach (var a in jsonlist)
                        {
                            String extension = a.Extension;
                            ransomwareextensions.Add(extension);
                        }
                    }

                }

                //get ransomwarefile
                SharedFunctions.getsession();
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getRansomwareFile", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey},
                       { "timestamp", lastupdatetimestamp}
                    });

                    string result = Encoding.UTF8.GetString(response).Replace("\n", "");

                    if (result != "No changes")
                    {
                        RansomwareFile[] jsonlist = JsonConvert.DeserializeObject<RansomwareFile[]>(result);
                        ransomwarefiles.Clear();
                        foreach (var a in jsonlist)
                        {
                            String filename = a.File;
                            ransomwarefiles.Add(filename);
                        }
                    }
                }


                //get trustedprocess
                SharedFunctions.getsession();
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getTrustedProcess", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey},
                       { "timestamp", lastupdatetimestamp}
                    });

                    string result = Encoding.UTF8.GetString(response).Replace("\n", "");

                    if (result != "No changes")
                    {
                        TrustedProcess[] jsonlist = JsonConvert.DeserializeObject<TrustedProcess[]>(result);
                        trustedadminprocesslist.Clear();
                        foreach (var a in jsonlist)
                        {
                            String processname = a.ProcessName;
                            trustedadminprocesslist.Add(processname);
                        }
                    }
                }

                //get ransomwareprocess
                SharedFunctions.getsession();
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getRansomProcess", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey},
                       { "timestamp", lastupdatetimestamp}
                    });

                    string result = Encoding.UTF8.GetString(response).Replace("\n", "");
                    Console.WriteLine(result);
                    if (result != "No changes")
                    {
                        RansomProcess[] jsonlist = JsonConvert.DeserializeObject<RansomProcess[]>(result);
                        blackadminprocesslist.Clear();
                        foreach (var a in jsonlist)
                        {
                            String processname = a.ProcessName;
                            blackadminprocesslist.Add(processname);
                        }
                    }
                }

                //get usage
                SharedFunctions.getsession();
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getUsage", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey},
                       { "username", Preference.email}
                    });
                    string result = Encoding.UTF8.GetString(response);
                    Preference.usage = Convert.ToInt64(result);
                }

                //getroomdetails
                SharedFunctions.getsession();
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getRoomDetails", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey},
                       { "id", Preference.roomid.ToString()},
                       { "timestamp", lastupdatetimestamp}
                    });

                    string result = Encoding.UTF8.GetString(response).Replace("\n", "");
                    if (result != "No changes")
                    {
                        pi[] jsonlist = JsonConvert.DeserializeObject<pi[]>(result);
                        foreach (var a in jsonlist)
                        {
                            Preference.roomid = a.RoomID;
                            Preference.roomname = a.RoomName;
                            Preference.piip = a.IP;
                            Preference.pisshfingerprint = a.SSHfingerprint;
                        }
                    }
                }
                //update lastupdatetimestamp
                lastupdatetimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//mysql uses this format
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                notifyIcon1.ShowBalloonTip(3000, "Notification Error", "Could not connect to notification server.", ToolTipIcon.Warning);

                notificationtext.AppendText("Could not connect to notification server.");

                //use default if cannot connect to database
                if (ransomwareextensions.Count == 0)
                {
                    String[] knownransomwareextensions = { ".epic", ".ecc", ".ezz", ".exx", ".zzz", ".xyz", ".aaa", ".abc", ".ccc", ".vvv", ".xxx", ".ttt", ".micro", ".encrypted", ".locked", ".crypto", "_crypt", ".crinf", ".r5a", ".XRNT", ".XTBL", ".crypt", ".R16M01D05", ".pzdc", ".good", ".LOL!", ".OMG!", ".RDM", ".RRK", ".encryptedRSA", ".crjoker", ".EnCiPhErEd", ".LeChiffre", ".keybtc@inbox_com", ".0x0", ".bleep", ".1999", ".vault", ".HA3", ".toxcrypt", ".magic", ".SUPERCRYPT", ".CTBL", ".CTB2", ".locky" };
                    foreach (var a in knownransomwareextensions)
                    {
                        ransomwareextensions.Add(a);
                    }
                }
                if (ransomwarefiles.Count == 0)
                {
                    String[] knownransomwarefiles = { "HELPDECRYPT.TXT", "HELP_YOUR_FILES.TXT", "HELP_TO_DECRYPT_YOUR_FILES.txt", "RECOVERY_KEY.txt", "HELP_RESTORE_FILES.txt", "HELP_RECOVER_FILES.txt", "HELP_TO_SAVE_FILES.txt", "DecryptAllFiles.txt", "DECRYPT_INSTRUCTIONS.TXT", "INSTRUCCIONES_DESCIFRADO.TXT", "How_To_Recover_Files.txt", "YOUR_FILES.HTML", "YOUR_FILES.url", "encryptor_raas_readme_liesmich.txt", "Help_Decrypt.txt", "DECRYPT_INSTRUCTION.TXT", "HOW_TO_DECRYPT_FILES.TXT", "ReadDecryptFilesHere.txt", "Coin.Locker.txt", "_secret_code.txt", "About_Files.txt", "Read.txt", "DECRYPT_ReadMe.TXT", "DecryptAllFiles.txt", "FILESAREGONE.TXT", "IAMREADYTOPAY.TXT", "HELLOTHERE.TXT", "READTHISNOW!!!.TXT", "SECRETIDHERE.KEY", "IHAVEYOURSECRET.KEY", "SECRET.KEY", "HELPDECYPRT_YOUR_FILES.HTML", "help_decrypt_your_files.html", "HELP_TO_SAVE_FILES.txt", "RECOVERY_FILES.txt", "RECOVERY_FILE.TXT" };
                    foreach (var a in knownransomwarefiles)
                    {
                        ransomwarefiles.Add(a);
                    }
                }
            }

        }

        public void updatestatustext(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(updatestatustext), new object[] { value });
                return;
            }
            statustext.Text = value;
        }

        void start_backup(object sender, DoWorkEventArgs e)
        {
            //tripwire = false;
            if (extensioncheckbox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one file extension.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Preference.usage < 10 * 1024 * 1024 * 1024L)
            {
                notifyIcon1.Icon = new Icon("sync.ico");
                ransomware = false;
                checkprocess();
                if (zippathnotsent.Count != 0)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Email Backlog", "Sending files that were not sent the previous time.", ToolTipIcon.Warning);
                    try
                    {
                        foreach (var path in zippathnotsent)
                        {
                            updatestatustext("Sending " + path);
                            sendemail(path);
                        }
                        zippathnotsent.Clear();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                filepath = getallfilepath();
                var checkedextensionarray = extensioncheckbox.CheckedItems.Cast<string>().ToArray();
                filepathtobackup = new List<string>();
                emailsent = true;
                nooffilechanged = 0;
                unknownprocess = false;

                foreach (var process in Preference.processlist)
                {
                    if (!Preference.trustedprocesslist.Contains(process))
                    {
                        if (!Preference.tempprocesslist.Contains(process))
                        {
                            unknownprocess = true;
                        }
                    }
                }
                //Preference.tempprocesslist.Clear();
                //unknownprocess = !Enumerable.SequenceEqual(Preference.processlist.OrderBy(t => t), Preference.trustedprocesslist.OrderBy(t => t));


                foreach (var a in filepath)
                {
                    String filehash = GetFileHash(a);
                    if (!filehash.Equals((hashdictionary.ContainsKey(a)) ? hashdictionary[a] : string.Empty))//need to backup file
                    {
                        if (ransomwareextensions.Contains(Path.GetExtension(a)))
                        {
                            notifyIcon1.ShowBalloonTip(10000, "Rasomware detected", "Ransomware file extensions are found in your system. Backup halted.", ToolTipIcon.Warning);
                            updatestatustext("Ransomware file extension '" + Path.GetExtension(a) + "' found in '" + a + "'. Backup halted.");
                            ransomware = true;
                            break;
                        }
                        else if (ransomwarefiles.Any(file => file.ToLower() == a.Split('\\').ToList().Last().ToLower()))
                        {
                            notifyIcon1.ShowBalloonTip(10000, "Ransomware detected", "Ransomware files are found in your system. Backup halted.", ToolTipIcon.Warning);
                            updatestatustext("Ransomware file '" + Path.GetFileName(a) + "' found in '" + a + "'. Backup halted.");
                            ransomware = true;
                            break;
                        }
                        else if (unknownprocess)
                        {
                            notifyIcon1.ShowBalloonTip(3000, "Unknown process", "There is an unknown process that has not been approved. Backup halted.", ToolTipIcon.Warning);
                            updatestatustext("There is an unknown process that has not been approved. Backup halted.");
                            break;
                        }
                        else if (checkedextensionarray.Contains(Path.GetExtension(a)))
                        {
                            FileInfo fileinfo = new FileInfo(a);

                            if (fileinfo.Length > 20000000)
                            {
                                notifyIcon1.ShowBalloonTip(3000, "File skipped", a + " was skipped as it was too big", ToolTipIcon.Warning);
                            }
                            else
                            {
                                if (!hashdictionary.ContainsKey(a))//increment counter if new file
                                    nooffilechanged++;
                                hashdictionary[a] = filehash;
                                filepathtobackup.Add(a);
                            }
                        }
                    }

                }

                if (unknownprocess)
                {
                    notifyIcon1.Icon = new Icon("error.ico");
                    running = false;
                }
                else if (ransomware || tripwire)
                {
                    //sendpassword();
                    notifyIcon1.Icon = new Icon("error.ico");
                    running = false;
                    populatehashdictionary();
                }
                else if (filepathtobackup.Count == 0)
                {
                    updatestatustext("No files changes.");
                    running = false;
                }
                else
                {
                    if (nooffilechanged > 10 && lastsync != "")//ensure it is not first backup
                    {
                        DialogResult dialogResult = MessageBox.Show(nooffilechanged + " files has changed on the computer. Continue to backup?", "Too many file changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            populatehashdictionary();
                            running = false;
                            updatestatustext("Backup cancelled.");
                        }
                    }
                    if (running)
                    {
                        int total = filepathtobackup.Count + 2;
                        int count = 0;
                        lastsync = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                        String temppath = Preference.backupdirectory + "\\" + Preference.email + " " + lastsync + "tmp.zip";
                        String attachmentpath = Preference.backupdirectory + "\\" + Preference.email + " " + lastsync + ".sync";
                        DirectoryInfo di = new DirectoryInfo(Preference.backupdirectory);
                        di.Create();
                        di.Attributes |= FileAttributes.Hidden;
                        List<String> encryptedpath = new List<string>();

                        try
                        {
                            List<String> filelist = new List<string>();
                            long size = 0;
                            for (int i = 0; i < filepathtobackup.Count; i++)
                            {
                                FileInfo fileinfo = new FileInfo(filepathtobackup[i]);
                                size += fileinfo.Length;
                                filelist.Add(filepathtobackup[i]);
                                if (size > 20000000L || i == filepathtobackup.Count - 1)//send email when 25MB is hit or last file
                                {
                                    if (size > 20000000L)
                                    {
                                        filelist.RemoveAt(filelist.Count - 1);
                                        i--;//decrement counter
                                    }


                                    foreach (string file in filelist)
                                    {
                                        encryptedpath.Add(Preference.backupdirectory + "\\" + lastsync + "\\" + Path.GetDirectoryName(file).Substring(Path.GetPathRoot(file).Length) + "\\" + Path.GetFileNameWithoutExtension(file) + " " + lastsync + Path.GetExtension(file));
                                        Directory.CreateDirectory(Preference.backupdirectory + "\\" + lastsync + "\\" + Path.GetDirectoryName(file).Substring(Path.GetPathRoot(file).Length));
                                        EncryptFile(file, Preference.backupdirectory + "\\" + lastsync + "\\" + Path.GetDirectoryName(file).Substring(Path.GetPathRoot(file).Length) + "\\" + Path.GetFileNameWithoutExtension(file) + " " + lastsync + Path.GetExtension(file));
                                    }

                                    using (ZipOutputStream s = new ZipOutputStream(File.Create(temppath)))
                                    {

                                        s.SetLevel(9); // 0-9, 9 being the highest compression
                                        //s.Password = Preference.secretkey;//for zip password

                                        byte[] buffer = new byte[4096];

                                        foreach (string file in encryptedpath)
                                        {
                                            ZipEntry entry = new ZipEntry(ZipEntry.CleanName(file));
                                            count++;
                                            bgw.ReportProgress((int)((float)count / (float)total * 100));
                                            updatestatustext("Backing up " + file);

                                            entry.DateTime = DateTime.Now;
                                            s.PutNextEntry(entry);

                                            using (FileStream fs = File.OpenRead(file))
                                            {
                                                int sourceBytes;
                                                do
                                                {
                                                    sourceBytes = fs.Read(buffer, 0, buffer.Length);

                                                    s.Write(buffer, 0, sourceBytes);

                                                } while (sourceBytes > 0);
                                            }

                                            if (bgw.CancellationPending)
                                            {
                                                e.Cancel = true;
                                                updatestatustext("Backup Stopped.");
                                                //reset the hash dictionary
                                                populatehashdictionary();
                                                return;
                                            }
                                        }
                                        s.Finish();
                                        s.Close();

                                    }

                                    Directory.Delete(Preference.backupdirectory + "\\" + lastsync, true);
                                    SharpAESCrypt.SharpAESCrypt.Encrypt(Preference.secretkey, temppath, attachmentpath);
                                    File.Delete(temppath);

                                    FileInfo zipinfo = new FileInfo(attachmentpath);
                                    if (zipinfo.Length < 25000000)
                                    {
                                        try
                                        {
                                            if (i == filepathtobackup.Count - 1)
                                                count++;
                                            bgw.ReportProgress((int)((float)count / (float)total * 100));
                                            updatestatustext("Sending email...");
                                            sendemail(attachmentpath);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex);
                                            notifyIcon1.ShowBalloonTip(3000, "Backup Error", "Could not connect to email server.", ToolTipIcon.Warning);
                                            updatestatustext("Backup completed but email is not sent.");
                                            zippathnotsent.Add(attachmentpath);
                                            emailsent = false;
                                        }

                                        try
                                        {
                                            updatestatustext("Attempting to connect to local server...");
                                            var client = new SftpClient(Preference.piip, 22, Preference.piusername, Preference.pipassword);
                                            client.HostKeyReceived += delegate (object senders, HostKeyEventArgs key)
                                            {
                                                Console.WriteLine(BitConverter.ToString(key.FingerPrint).Replace("-", ":"));
                                                if (key.FingerPrint.SequenceEqual(Preference.pisshfingerprint.Split(':').Select(s => Convert.ToByte(s, 16)).ToArray()))
                                                    key.CanTrust = true;
                                                else
                                                    key.CanTrust = false;
                                            };
                                            client.Connect();
                                            if (i == filepathtobackup.Count - 1)
                                                count++;
                                            bgw.ReportProgress((int)((float)count / (float)total * 100));
                                            updatestatustext("Copying to local server...");
                                            var fileStream = new FileStream(attachmentpath, FileMode.Open);
                                            client.BufferSize = 4 * 1024;
                                            if (!client.Exists("/media/USBHDD1/attachments/" + Preference.email))
                                                client.CreateDirectory("/media/USBHDD1/attachments/" + Preference.email);
                                            if (emailsent)
                                            {
                                                client.UploadFile(fileStream, "/media/USBHDD1/attachments/" + Preference.email + "/" + zipinfo.Name, null);
                                            }
                                            else
                                            {
                                                client.UploadFile(fileStream, "/media/USBHDD1/attachments/" + Preference.email + "/" + zipinfo.Name + "noemail", null);
                                            }
                                            fileStream.Close();
                                            client.Disconnect();
                                            client.Dispose();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex);
                                            notifyIcon1.ShowBalloonTip(3000, "Backup Error", "Could not connect to local server.", ToolTipIcon.Warning);
                                        }
                                        running = false;
                                        updatestatustext("Backup completed at " + lastsync + ".");
                                        savesettings();
                                    }
                                    else
                                    {
                                        notifyIcon1.ShowBalloonTip(3000, "Backup Error", "Sync file is too big to be emailed. Please exclude some files first.", ToolTipIcon.Warning);
                                        updatestatustext("Sync file is too big to be emailed. Please exclude some files first.");
                                        //reset the hash dictionary
                                        populatehashdictionary();
                                    }
                                    Console.WriteLine(new DirectoryInfo(Preference.backupdirectory).GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length));

                                    //check and delete if file size exceeds
                                    if (new DirectoryInfo(Preference.backupdirectory).GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length) > Preference.directorysize * 1024 * 1024)//MB >> KB >> B
                                    {
                                        do
                                        {
                                            FileSystemInfo fileInfo = new DirectoryInfo(Preference.backupdirectory).GetFileSystemInfos().First();
                                            Console.WriteLine("Deleting " + fileInfo);
                                            fileInfo.Delete();
                                        } while (new DirectoryInfo(Preference.backupdirectory).GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length) > Preference.directorysize * 1024 * 1024);
                                    }
                                    size = 0;//reset counter
                                    filelist.Clear();//reset list
                                    encryptedpath.Clear();
                                    lastsync = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");//update lastsync to change file name
                                    attachmentpath = Preference.backupdirectory + "\\" + Preference.email + " " + lastsync + ".sync";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
            }
            else
            {
                notifyIcon1.Icon = new Icon("error.ico");
                DialogResult dialogResult = MessageBox.Show("You have exceed your 10GB limit. You can delete your old file versions or reduce your file retention period.", "Exceeded Limit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                running = false;
            }

        }


        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString(), "Error");
            }
            else
            {
                progressBar1.Visible = false;
                startbackupbutton.Text = "Start Backup";
                bgw = new BackgroundWorker();
                populatefilerestore();
                timer1.Stop();
                InitTimer();//reset timer
                backuphash = Properties.Settings.Default.hashdictionary;
            }
        }

        private void populatefilerestore()
        {
            try
            {
                string[] allfiles = Directory.GetFiles(Preference.backupdirectory, "*.sync");
                filerestorecheckbox.Items.Clear();
                foreach (var a in allfiles)
                {
                    String date = Regex.Match(a, ".+ ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]).sync").Groups[1].Value;
                    DateTime parsedDate;
                    if (DateTime.TryParseExact(date, "dd-MM-yyyy HH-mm-ss", null, DateTimeStyles.None, out parsedDate))
                    {
                        if (parsedDate.Ticks >= DateTime.Now.AddDays(-7).Ticks)
                        {
                            filerestorecheckbox.Items.Add(a);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                updatestatustext("No backup was ran.");
            }
        }
        private void restorebutton_Click(object sender, EventArgs e)
        {
            String tempdirectory = Preference.backupdirectory + "\\" + "TempExtract";
            String restoredirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Restored Files";
            String[] filestorestore = filerestorecheckbox.CheckedItems.Cast<string>().ToArray();


            progressBar1.Visible = true;
            if (Directory.Exists(tempdirectory))
                Directory.Delete(tempdirectory, true);
            if (Directory.Exists(restoredirectory))
                Directory.Delete(restoredirectory, true);
            Directory.CreateDirectory(tempdirectory);
            Directory.CreateDirectory(restoredirectory);

            List<String> decryptedzip = new List<string>();
            //String password = promptpassword("Enter a password to restore your zip files");
            foreach (var a in filestorestore)
            {
                String decryptpath = Preference.backupdirectory + "\\" + Path.GetFileNameWithoutExtension(a) + "decrypted";
                DecryptZipFile(a, decryptpath);
                decryptedzip.Add(decryptpath);
            }

            foreach (var a in decryptedzip)
            {
                statustext.Text = "Unzipping " + a;
                ExtractZipFile(a, tempdirectory);
                File.Delete(a);
            }

            String[] extractedfiles = Directory.GetFiles(tempdirectory, "*", SearchOption.AllDirectories);
            String path = "";
            int i = 0;
            Boolean decryptsuccess = false;
            foreach (var file in extractedfiles)
            {
                statustext.Text = "Restoring " + file;
                path = restoredirectory + file.Substring(new Regex("-[0-9][0-9]\\\\").Match(file).Index + 3);//remove Sync Backup and Date folder
                path = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path).Substring(0, new Regex(" [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]").Match(Path.GetFileNameWithoutExtension(path)).Index) + Path.GetExtension(file);//remove date from file name
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                decryptsuccess = DecryptFile(file, path);
                i++;
                progressBar1.Value = (int)((float)i / (float)extractedfiles.Length * 100);
            }
            Directory.Delete(tempdirectory, true);
            progressBar1.Visible = false;
            if (decryptsuccess)
            {
                statustext.Text = "Restore complete.";
                MessageBox.Show("Your files have been decrypted and placed on your Desktop.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (extractedfiles.Length == 0)
                {
                    statustext.Text = "No files selected.";
                    MessageBox.Show("No files selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    statustext.Text = "Restore failed.";
                    MessageBox.Show("Your files cannot be decrypted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public Boolean DecryptZipFile(string filein, string fileout)
        {
            try
            {
                SharpAESCrypt.SharpAESCrypt.Decrypt(Preference.secretkey, filein, fileout);
                return true;
            }
            catch
            {
                int index = secretkeylist.Count - 1;
                while (index >= 0)
                {
                    try
                    {
                        SharpAESCrypt.SharpAESCrypt.Decrypt(secretkeylist[index], filein, fileout);
                        return true;
                    }
                    catch (Exception)
                    {
                        index = index - 1;
                        Console.WriteLine("Key Error!");
                    }
                }
                return false;
            }
        }


        public void ExtractZipFile(string archiveFilenameIn, string outFolder)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);

                for (int i = 0; i < zf.Count; i++)
                {

                    if (!zf[i].IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zf[i].Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    try
                    {
                        Stream zipStream = zf.GetInputStream(zf[i]);

                        // Manipulate the output filename here as desired.
                        String fullZipToPath = Path.Combine(outFolder, entryFileName);
                        string directoryName = Path.GetDirectoryName(fullZipToPath);
                        if (directoryName.Length > 0)
                            Directory.CreateDirectory(directoryName);

                        // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                        // of the file, but does not waste memory.
                        // The "using" will close the stream even if an exception occurs.
                        using (FileStream streamWriter = File.Create(fullZipToPath))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }

        private void selectallfilebutton_Click(object sender, EventArgs e)
        {
            if (selectallfilebutton.Text == "Select All")
            {
                for (int i = 0; i < filerestorecheckbox.Items.Count; i++)
                {
                    filerestorecheckbox.SetItemChecked(i, true);
                }
                selectallfilebutton.Text = "Deselect All";
            }
            else
            {
                for (int i = 0; i < filerestorecheckbox.Items.Count; i++)
                {
                    filerestorecheckbox.SetItemChecked(i, false);
                }
                selectallfilebutton.Text = "Select All";
            }

            //byte[] encrypted = SharedFunctions.EncryptStringToBytes_Aes("thisatestmessage", Encoding.ASCII.GetBytes("passwordpassword"), Encoding.ASCII.GetBytes("1234567890SPSync"));
            //Console.WriteLine(Convert.ToBase64String(encrypted));
            //Console.WriteLine(encrypted.Length);
        }


        private string GetFileHash(string fileName)
        {
            HashAlgorithm sha1 = HashAlgorithm.Create();
            StringBuilder sb = new StringBuilder();
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                foreach (byte b in sha1.ComputeHash(stream))
                    sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }



        private void savesettings()
        {
            Properties.Settings.Default.includedfolders = string.Join(",", folderpathlist);
            Properties.Settings.Default.excludedfolders = string.Join(",", excludefolderpathlist);
            Properties.Settings.Default.includedfiles = string.Join(",", filepathlist);
            Properties.Settings.Default.excludedfiles = string.Join(",", excludefilepathlist);
            var checkedextensionarray = extensioncheckbox.CheckedItems.Cast<string>().ToArray();
            Properties.Settings.Default.fileextension = string.Join(",", checkedextensionarray);
            Properties.Settings.Default.usersecret = Preference.usersecret;
            Properties.Settings.Default.notificationid = notificationid;
            String dictionarystring = "";
            for (int i = 0; i < hashdictionary.Count; i++)
            {
                dictionarystring += hashdictionary.Keys.ElementAt(i) + "|" + hashdictionary.Values.ElementAt(i) + "|";
            }
            Properties.Settings.Default.hashdictionary = dictionarystring;
            Properties.Settings.Default.zippathnotsent = string.Join(",", zippathnotsent);
            Properties.Settings.Default.processlist = string.Join(",", Preference.processlist);
            Properties.Settings.Default.email = Preference.email;
            Properties.Settings.Default.trustedprocesslist = string.Join(",", Preference.trustedprocesslist);
            Properties.Settings.Default.lastsync = lastsync;
            Properties.Settings.Default.ransomwareextensions = string.Join(",", ransomwareextensions);
            Properties.Settings.Default.ransomwarefiles = string.Join(",", ransomwarefiles);
            Properties.Settings.Default.trustedadminprocesslist = string.Join(",", trustedadminprocesslist);
            Properties.Settings.Default.blackadminprocesslist = string.Join(",", blackadminprocesslist);
            Properties.Settings.Default.notificationtext = notification;
            Properties.Settings.Default.secretkey = Preference.secretkey;
            Properties.Settings.Default.secretkeylist = string.Join(",", secretkeylist);
            Properties.Settings.Default.usage = Preference.usage;
            Properties.Settings.Default.sentprocesslist = string.Join(",", sentprocesslist);
            Properties.Settings.Default.lastupdatetimestamp = lastupdatetimestamp;
            Properties.Settings.Default.piip = Preference.piip;
            Properties.Settings.Default.pisshfingerprint = Preference.pisshfingerprint;
            Properties.Settings.Default.roomname = Preference.roomname;
            Properties.Settings.Default.roomid = Preference.roomid;
            Properties.Settings.Default.Save();
        }

        private void populatehashdictionary()
        {
            hashdictionary.Clear();
            Console.WriteLine("Clear dict");
            Console.WriteLine(hashdictionary.Count);
            if (backuphash != "")
            {
                Console.WriteLine("Not empty");
                List<String> splithashdictionary = backuphash.Split('|').ToList();
                for (int i = 0; i < splithashdictionary.Count - 1; i += 2)
                {
                    //clean up files that are deleted
                    if (File.Exists(splithashdictionary[i]))
                        hashdictionary.Add(splithashdictionary[i], splithashdictionary[i + 1]);
                }
            }
        }

        private void sendemail(String attachmentpath)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(Preference.clientemail);
            mail.To.Add(Preference.serveremail);
            mail.Subject = Path.GetFileNameWithoutExtension(attachmentpath) + " Backup " + Preference.roomname;
            mail.Body = ""; //Text in email

            Attachment attachment = new Attachment(attachmentpath);
            mail.Attachments.Add(attachment);
            SmtpServer.Timeout = 300000;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Preference.clientemail, Preference.clientemailpassword);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            mail.Dispose();
        }



        private void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = Preference.backupfrequency * 60 * 1000; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startbackupbutton.PerformClick();
            getsecretkey();
            getfromdatabase();
            populatefilerestore();
        }

        private void refreshbuttton_Click(object sender, EventArgs e)
        {
            getsecretkey();
            getfromdatabase();
            populatefilerestore();
            populateextension();
            checkprocess();
        }

        private void checkprocess()
        {
            Boolean firstlaunch = true;
            if (!(Preference.processlist.Count == 0))
            {
                firstlaunch = false;
            }
            var allProcceses = Process.GetProcesses().Distinct();

            foreach (var a in allProcceses)
            {
                try
                {
                    if (!firstlaunch)
                    {
                        if (blackadminprocesslist.Contains(a.ProcessName.ToString()))
                        {
                            notifyIcon1.ShowBalloonTip(10000, "Rasomware detected", "Ransomware process are found in your system. Backup halted.", ToolTipIcon.Warning);
                            notifyIcon1.Icon = new Icon("error.ico");
                            updatestatustext("Ransomware process '" + a.ProcessName.ToString() + "' has been found in your system. Backup halted.");
                            ransomware = true;
                            break;
                        }
                        if (!Preference.trustedprocesslist.Contains(a.ProcessName.ToString()))
                        {
                            if (trustedadminprocesslist.Contains(a.ProcessName.ToString()))
                            {
                                Preference.trustedprocesslist.Add(a.ProcessName.ToString());
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show("A new process '" + a.ProcessName + "' has just started. Click Yes to add it to your whitelist. Click No and the process will be killed. Click Cancel to continue to backup.", "New process ran", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    Preference.trustedprocesslist.Add(a.ProcessName.ToString());
                                }
                                else if (dialogResult == DialogResult.Cancel)
                                {
                                    Preference.tempprocesslist.Add(a.ProcessName.ToString());
                                }
                                else
                                {
                                    DialogResult dialogResult2 = MessageBox.Show("Are you sure you want to kill the process " + a.ProcessName + "?", "Kill process", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dialogResult2 == DialogResult.Yes)
                                    {
                                        try
                                        {
                                            Process[] proc = Process.GetProcessesByName(a.ProcessName);
                                            foreach (var processname in proc)
                                                processname.Kill();
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e);
                                        }
                                    }
                                }
                                // notifyIcon1.ShowBalloonTip(3000, "New process ran", "A new process " + a.ProcessName + " has just started.", ToolTipIcon.Warning);
                            }
                            if (!Preference.processlist.Contains(a.ProcessName.ToString()))
                            {
                                Preference.processlist.Add(a.ProcessName.ToString());
                            }
                        }
                    }
                    else
                    {
                        if (!Preference.processlist.Contains(a.ProcessName.ToString()))
                        {
                            Preference.processlist.Add(a.ProcessName.ToString());
                            Preference.trustedprocesslist.Add(a.ProcessName.ToString());//approve on first launch
                        }
                    }
                }
                catch (Exception)
                {
                    //Console.WriteLine(ex);
                }
            }
        }

        private void monitorfile(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    string text = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\#Donottouch.txt");
                    if (text != "Do not edit this file." || tripwire || ransomware)
                    {
                        if (text != "Do not edit this file.")
                        {
                            updatestatustext("Serious threat detected. Please shut down your computer immediately.");
                            tripwire = true;
                        }
                        break;
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception)
                {
                    break;
                }
            }

            notifyIcon1.Icon = new Icon("error.ico");
            notifyIcon1.ShowBalloonTip(10000, "Serious threat detected", "Please shut down your computer immediately. Backup halted.", ToolTipIcon.Warning);
            //SharedFunctions.sendpassword();
            DialogResult dialogResult = MessageBox.Show("We highly recommend that your shut down your computer immediately. Please consult a system expert to recover your system. Do you want to shut down your computer?", "Serious Threat Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("shutdown", "/s /t 10");
            }
        }
        private void website_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void notificationtext_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void sendprocesslist(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(Preference.clientemail);
                    mail.To.Add(Preference.serveremail);
                    mail.Subject = Preference.email + " Process List " + Preference.roomname;
                    String body = "";
                    foreach (var a in Preference.processlist)
                    {
                        if (!sentprocesslist.Contains(a))//if not in sent process list
                        {
                            body += a + "|" + (Preference.trustedprocesslist.Contains(a) ? "Trusted" : "Not Trusted") + ",";
                            sentprocesslist.Add(a);
                        }
                    }
                    if (body.Length > 1)
                    {
                        body = body.Substring(0, body.Length - 1);//remove last comma
                        mail.Body = body; //Text in email                    
                        SmtpServer.Port = 587;
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new NetworkCredential(Preference.clientemail, Preference.clientemailpassword);
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);
                        mail.Dispose();
                    }
                }
                catch (Exception)
                {
                }
                System.Threading.Thread.Sleep(3600000);
            }
        }

        // Rfc2898DeriveBytes constants:
        public readonly byte[] salt = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }; // Must be at least eight bytes.  MAKE THIS SALTIER!
        public const int iterations = 1042; // Recommendation is >= 1000.

        public Boolean DecryptFile(string sourceFilename, string destinationFilename)
        {
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Preference.usersecret, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);

            using (Stream destination = File.Open(destinationFilename, FileMode.Create))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    try
                    {
                        using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            source.CopyTo(cryptoStream);
                            return true;
                        }
                    }
                    catch (CryptographicException)
                    {
                        Directory.Delete(Preference.backupdirectory + "\\" + "TempExtract", true);
                        Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Restored Files", true);
                        return false;
                    }
                }
            }
        }

        public void EncryptFile(string sourceFilename, string destinationFilename)
        {
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Preference.usersecret, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);

            using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        source.CopyTo(cryptoStream);
                    }
                }
            }
        }

        //private void EncryptFile(string inputFile, string outputFile)
        //{

        //    try
        //    {
        //        string password = Preference.password; // Your Key Here
        //        UnicodeEncoding UE = new UnicodeEncoding();
        //        byte[] key = UE.GetBytes(password);

        //        string cryptFile = outputFile;
        //        FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

        //        RijndaelManaged RMCrypto = new RijndaelManaged();

        //        CryptoStream cs = new CryptoStream(fsCrypt,
        //            RMCrypto.CreateEncryptor(key, key),
        //            CryptoStreamMode.Write);

        //        FileStream fsIn = new FileStream(inputFile, FileMode.Open);

        //        int data;
        //        while ((data = fsIn.ReadByte()) != -1)
        //            cs.WriteByte((byte)data);


        //        fsIn.Close();
        //        cs.Close();
        //        fsCrypt.Close();
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show("Encryption failed!", "Error");
        //        MessageBox.Show(e.Message.ToString(), "Error");
        //    }
        //}

        /////
        ///// Decrypts a file using Rijndael algorithm.
        /////</summary>
        /////<param name="inputFile"></param>
        /////<param name="outputFile"></param>
        //private Boolean DecryptFile(string inputFile, string outputFile)
        //{
        //    FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
        //    FileStream fsOut = new FileStream(outputFile, FileMode.Create);
        //    try
        //    {
        //        string password = Preference.password; // Your Key Here

        //        UnicodeEncoding UE = new UnicodeEncoding();
        //        byte[] key = UE.GetBytes(password);

        //        RijndaelManaged RMCrypto = new RijndaelManaged();

        //        CryptoStream cs = new CryptoStream(fsCrypt,
        //            RMCrypto.CreateDecryptor(key, key),
        //            CryptoStreamMode.Read);

        //        int data;
        //        while ((data = cs.ReadByte()) != -1)
        //            fsOut.WriteByte((byte)data);

        //        fsOut.Close();
        //        cs.Close();
        //        fsCrypt.Close();
        //        return true;
        //    }catch (Exception)
        //    {                
        //        fsOut.Close();
        //        fsCrypt.Close();
        //        MessageBox.Show("Decryption failed!", "Error");
        //        return false;
        //    }
        //}

        private void selectfilerestorebutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Sync App Backup Files (*.sync) | *.sync;";
            dialog.Title = "Please select an backup file to decrypt.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String filepath = dialog.FileName;
                if (Path.GetExtension(filepath).Equals(".sync"))
                {
                    String tempdirectory = Preference.backupdirectory + "\\" + "TempExtract";
                    String restoredirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Restored Files";

                    Directory.CreateDirectory(tempdirectory);
                    Directory.CreateDirectory(restoredirectory);
                    progressBar1.Visible = true;

                    statustext.Text = "Unzipping " + filepath;

                    String decryptpath = Preference.backupdirectory + "\\" + Path.GetFileNameWithoutExtension(filepath) + "decrypted";
                    if (DecryptZipFile(filepath, decryptpath))
                    {
                        ExtractZipFile(decryptpath, tempdirectory);
                        File.Delete(decryptpath);

                        String[] extractedfiles = Directory.GetFiles(tempdirectory, "*", SearchOption.AllDirectories);
                        String path = "";
                        int i = 0;
                        foreach (var file in extractedfiles)
                        {
                            statustext.Text = "Restoring " + file;
                            path = restoredirectory + file.Replace(tempdirectory, "");//remove tempdirectory filepath from path
                            path = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path).Substring(0, new Regex(" [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]").Match(Path.GetFileNameWithoutExtension(path)).Index) + Path.GetExtension(file);//remove date from file name
                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            DecryptFile(file, path);
                            i++;
                            progressBar1.Value = (int)((float)i / (float)extractedfiles.Length * 100);
                        }
                        Directory.Delete(tempdirectory, true);
                        statustext.Text = "Restore complete.";
                        progressBar1.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Files cannot be restored!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Directory.Delete(tempdirectory, true);
                        Directory.Delete(restoredirectory, true);
                        statustext.Text = "Restore failed";
                        File.Delete(decryptpath);
                        progressBar1.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void InstallCertificate(string cerFileName)
        {
            X509Certificate2 certificate = new X509Certificate2(cerFileName);
            X509Store store = new X509Store(StoreName.AuthRoot, StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadWrite);
            store.Add(certificate);
            store.Close();
        }


        Random rand = new Random();

        public const string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string GenerateString(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[rand.Next(Alphabet.Length)];
            }
            return new string(chars);
        }

        private void selectdatebutton_Click(object sender, EventArgs e)
        {
            SelectDateForm SelectDateForm = new SelectDateForm();
            SelectDateForm.ShowDialog();
            if (Preference.startdate.Ticks != 0)
            {
                String tempdirectory = Preference.backupdirectory + "\\" + "TempExtract";
                String restoredirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Restored Files";
                String[] filestorestore = Directory.GetFiles(Preference.backupdirectory, "*.sync");

                progressBar1.Visible = true;

                if (Directory.Exists(tempdirectory))
                    Directory.Delete(tempdirectory, true);
                if (Directory.Exists(restoredirectory))
                    Directory.Delete(restoredirectory, true);
                Directory.CreateDirectory(tempdirectory);
                Directory.CreateDirectory(restoredirectory);

                foreach (var a in filestorestore)
                {
                    String date = Regex.Match(a, ".+ ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]).sync").Groups[1].Value;
                    DateTime parsedDate;
                    if (DateTime.TryParseExact(date, "dd-MM-yyyy HH-mm-ss", null, DateTimeStyles.None, out parsedDate))
                    {
                        if (parsedDate.Ticks >= Preference.startdate.Ticks && parsedDate.Ticks <= Preference.enddate.Ticks)
                        {
                            String decryptpath = Preference.backupdirectory + "\\" + Path.GetFileNameWithoutExtension(a) + "decrypted";
                            DecryptZipFile(a, decryptpath);
                            statustext.Text = "Decrypting " + Path.GetFileNameWithoutExtension(a);
                            ExtractZipFile(decryptpath, tempdirectory);
                            statustext.Text = "Unzipping " + decryptpath;
                            File.Delete(decryptpath);
                        }
                    }
                }


                String[] extractedfiles = Directory.GetFiles(tempdirectory, "*", SearchOption.AllDirectories);
                String path = "";
                int i = 0;
                Boolean decryptsuccess = false;
                foreach (var file in extractedfiles)
                {
                    statustext.Text = "Restoring " + file;
                    path = restoredirectory + file.Substring(new Regex("-[0-9][0-9]\\\\").Match(file).Index + 3);//remove Sync Backup and Date folder
                    path = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path).Substring(0, new Regex(" [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]").Match(Path.GetFileNameWithoutExtension(path)).Index) + Path.GetExtension(file);//remove date from file name
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    decryptsuccess = DecryptFile(file, path);
                    i++;
                    progressBar1.Value = (int)((float)i / (float)extractedfiles.Length * 100);
                }
                Directory.Delete(tempdirectory, true);
                progressBar1.Visible = false;
                if (decryptsuccess)
                {
                    statustext.Text = "Restore complete.";
                    MessageBox.Show("Your files have been decrypted and placed on your Desktop.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (extractedfiles.Length == 0)
                    {
                        statustext.Text = "No backup files were found within the selected time period.";
                        MessageBox.Show("No backup files were found within the selected time period.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        statustext.Text = "Restore failed.";
                        MessageBox.Show("Your files cannot be decrypted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void extensioncheckbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (extensioncheckbox.CheckedItems.Count == extensioncheckbox.Items.Count)
            {
                selectallbutton.Text = "Deselect All";
            }
            else
            {
                selectallbutton.Text = "Select All";
            }
        }
    }
}
