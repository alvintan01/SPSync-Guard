using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync
{
    public partial class Preferences : Form
    {
        Boolean passwordsaved = true;

        public Preferences()
        {
            InitializeComponent();
            backupdirectory.Text = Properties.Settings.Default.backupdirectory;
            frequency.Text = Properties.Settings.Default.backupfrequency.ToString();
            directorysize.Text = Properties.Settings.Default.directorysize.ToString();
            email.Text = Properties.Settings.Default.email;
            noofmonths.Value = Properties.Settings.Default.noofmonths;
            populateprocesslist();
            directorysizebar.Maximum = 10 * 1024;//in MB
            months.Text = Properties.Settings.Default.noofmonths.ToString();
            if (Properties.Settings.Default.runatstartup == true)
            {
                runatstartupcheckbox.Checked = true;
            }
            else
            {
                runatstartupcheckbox.Checked = false;
            }


            if (Preference.usage / 1024.0 / 1024 / 1024 < 5)
            {
                directorysizebar.Value = Convert.ToInt32(Preference.usage / 1024 / 1024);
            }
            else if (Preference.usage / 1024.0 / 1024 / 1024 < 10)
            {
                directorysizebar.Value = Convert.ToInt32(Preference.usage / 1024 / 1024);
                ModifyProgressBarColor.SetState(directorysizebar, 3);//1 = normal (green); 2 = error (red); 3 = warning (yellow)
            }
            else
            {
                directorysizebar.Value = 10 * 1024;
                ModifyProgressBarColor.SetState(directorysizebar, 2);
            }

            directorysizetext.Text = "You have used " + Math.Round((Preference.usage / 1024.0 / 1024 / 1024), 3) + "GB out of 10GB";
        }

        private void currentdirectory_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string newfolderpath = folderBrowserDialog.SelectedPath;
                backupdirectory.Text = newfolderpath;
            }
        }


        private void frequency_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(frequency.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frequency.Text = new String(frequency.Text.Where(char.IsDigit).ToArray());
            }
            else if (frequency.Text == "0")
            {
                MessageBox.Show("Backup frequency must be at least 1 min.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frequency.Text = frequency.Text.Remove(frequency.Text.Length - 1);
            }
        }

        private void maxsize_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(directorysize.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                directorysize.Text = new String(directorysize.Text.Where(char.IsDigit).ToArray());
            }
            else if (directorysize.Text == "0")
            {
                MessageBox.Show("Backup directory size must be at least 1 MB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                directorysize.Text = directorysize.Text.Remove(directorysize.Text.Length - 1);
            }
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            if (frequency.Text != "")
            {
                Preference.backupfrequency = Int32.Parse(frequency.Text);
                Properties.Settings.Default.backupfrequency = Preference.backupfrequency;
            }

            if (backupdirectory.Text != "")
            {
                Preference.backupdirectory = backupdirectory.Text;
                Properties.Settings.Default.backupdirectory = Preference.backupdirectory;
            }

            if (directorysize.Text != "")
            {
                Preference.directorysize = Int32.Parse(directorysize.Text);
                Properties.Settings.Default.directorysize = Preference.directorysize;
            }

            //if (password.Text != "")
            //{
            //    passwordsaved = SharedFunctions.createuser();
            //    if (!passwordsaved)
            //    {
            //        MessageBox.Show("Please connect to the internet.");
            //    }
            //    else
            //    {
            //        Preference.password = password.Text;
            //        Properties.Settings.Default.password = Preference.password;
            //    }
            //}

            Properties.Settings.Default.processlist = string.Join(",", Preference.processlist);

            Preference.trustedprocesslist = trustedlist.CheckedItems.Cast<string>().ToList();
            Properties.Settings.Default.trustedprocesslist = string.Join(",", Preference.trustedprocesslist);
            Properties.Settings.Default.noofmonths = noofmonths.Value;
            Properties.Settings.Default.runatstartup = runatstartupcheckbox.Checked;
            Boolean updatesuccess = false;
            //wanted to provide support to change email but realised it is not possible as we need to update the directory name on pi and cloud
            if (Regex.IsMatch(email.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                if (Preference.noofmonths != Properties.Settings.Default.noofmonths || Preference.email != email.Text)
                {
                    if (Preference.noofmonths != Properties.Settings.Default.noofmonths)
                    {
                        if (SharedFunctions.updatenoofmonths(noofmonths.Value))
                        {
                            updatesuccess = true;
                            Preference.noofmonths = noofmonths.Value;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            MessageBox.Show("Please connect to the internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Preference.email != email.Text)
                    {
                        if (SharedFunctions.changeemail(Preference.email, email.Text))
                        {
                            updatesuccess = true;
                            Preference.email = email.Text;
                            Properties.Settings.Default.email = Preference.email;
                            Properties.Settings.Default.Save();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Please connect to the internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (updatesuccess)
                    {
                        Close();
                    }
                }
                else
                {
                    Properties.Settings.Default.Save();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid email.");
            }

        }

        //private void sendrecoverybutton_Click(object sender, EventArgs e)
        //{
        //    if (Regex.IsMatch(email.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
        //    {
        //        Preference.email = email.Text;
        //        Properties.Settings.Default.email = Preference.email;
        //        Properties.Settings.Default.Save();
        //        SharedFunctions.sendpassword();
        //        MessageBox.Show("Email has been sent.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please enter a valid email.");
        //    }
        //}

        private void populateprocesslist()
        {
            trustedlist.Items.Clear();
            //add all possible file extension

            foreach (var a in Preference.processlist.OrderBy(x => x))
            {
                trustedlist.Items.Add(a);
            }
            //check the processname
            Preference.trustedprocesslist.ForEach(item =>
            {
                var index = trustedlist.Items.IndexOf(item);
                if (index != -1)//if can find process, check it, else ignore.
                    trustedlist.SetItemChecked(index, true);
            });
        }

        private void selectallprocessbutton_Click(object sender, EventArgs e)
        {
            if (selectallprocessbutton.Text == "Select All")
            {
                for (int i = 0; i < trustedlist.Items.Count; i++)
                {
                    trustedlist.SetItemChecked(i, true);
                }
                selectallprocessbutton.Text = "Deselect All";
            }
            else
            {
                for (int i = 0; i < trustedlist.Items.Count; i++)
                {
                    trustedlist.SetItemChecked(i, false);
                }
                selectallprocessbutton.Text = "Select All";
            }
        }

        private void noofmonths_Scroll(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(noofmonths, noofmonths.Value.ToString());
            months.Text = noofmonths.Value.ToString();
        }

        private void deleteallversionbutton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete all versions?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(Preference.clientemail);
                    mail.To.Add(Preference.serveremail);
                    mail.Subject = Preference.email + " request to delete all versions "+Preference.roomname;
                    mail.Body = Preference.email + " request to delete all versions"; //Text in email
                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(Preference.clientemail, Preference.clientemailpassword);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    mail.Dispose();
                    MessageBox.Show("Please wait for 15 mins for your files to be deleted.", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Please connect to the internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void runatstartupcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (runatstartupcheckbox.Checked)
            {
                SharedFunctions.setstartup(true);
            }
            else
            {
                SharedFunctions.setstartup(false);
            }
        }

        private void deleteaccountbutton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete your account?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(Preference.clientemail);
                    mail.To.Add(Preference.serveremail);
                    mail.Subject = Preference.email + " request to delete account " +Preference.roomname;
                    mail.Body = Preference.email + " request to delete account"; //Text in email
                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(Preference.clientemail, Preference.clientemailpassword);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    mail.Dispose();
                    MessageBox.Show("Please wait for 15 mins for your account to be deleted.", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.email = "";//to reset sync app
                    Properties.Settings.Default.Save();
                    SharedFunctions.setstartup(false);
                    Preference.closeform = true;
                    Preference.usercloseform = true;
                    Close();
                }
                catch
                {
                    MessageBox.Show("Please connect to the internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void trustedlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trustedlist.CheckedItems.Count == trustedlist.Items.Count)
            {
                selectallprocessbutton.Text = "Deselect All";
            }
            else
            {
                selectallprocessbutton.Text = "Select All";
            }
        }
    }
}
