using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCTLicenseManager
{
    public partial class Form1 : Form
    {
        private TCTLicenseCheck License = new TCTLicenseCheck();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(License.IsLegal) {
                btnOK.Text = "OK, it's legal!";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*txtTicks.Text = Convert.ToString(*/License.CreateLicenseFile(Convert.ToInt32(txtVer.Text), Convert.ToInt32(txtRev.Text), Convert.ToInt32(txtBuild.Text))/*)*/;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
