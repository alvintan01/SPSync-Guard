using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync
{
    public partial class WhichLab : Form
    {
        public WhichLab(Boolean reinstall)
        {
            InitializeComponent();
            if (SharedFunctions.getroomnames())
            {
                labdropdown.DataSource = Preference.pilist;
                labdropdown.DisplayMember = "RoomName";
                if (!reinstall)
                    selectalabprompt.Text = "Select a lab:";
                else
                    selectalabprompt.Text = "Select your existing lab:";
            }
            else
            {
                MessageBox.Show("Please connect to the internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Preference.closeform = true;
                Close();
            }
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            var b = (pi)labdropdown.SelectedItem;
            Preference.roomname = b.RoomName;

            foreach (var a in Preference.pilist)
            {
                if (a.RoomName == Preference.roomname)
                {
                    Preference.roomid = a.RoomID;
                    Preference.piip = a.IP;
                    Preference.pisshfingerprint = a.SSHfingerprint;
                    break;
                }
            }
            Close();
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            Preference.closeform = true;
            Close();
        }
    }
}
