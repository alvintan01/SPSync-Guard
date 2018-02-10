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
    public partial class SelectDateForm : Form
    {
        public SelectDateForm()
        {
            InitializeComponent();
            startdate.Value = DateTime.Now.AddDays(-1);
            enddate.Value = DateTime.Now;
        }

        private void confirmbutton_Click(object sender, EventArgs e)
        {
            if (startdate.Value.CompareTo(enddate.Value) > 0)
            {
                MessageBox.Show("Start date cannot be later than end date!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else if(enddate.Value.CompareTo(DateTime.Now) > 0)
            {
                MessageBox.Show("End date cannot be later than today!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Preference.startdate = startdate.Value;
                Preference.enddate = enddate.Value;
                Close();
            }
        }
    }
}
