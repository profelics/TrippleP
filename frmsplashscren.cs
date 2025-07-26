using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrippleP
{
    public partial class frmsplashscren : Form
    {
        public frmsplashscren()
        {
            InitializeComponent();
        }

        private void frmsplashscren_Shown(object sender, EventArgs e)
        {
            tmrsp.Interval = 4000;
            tmrsp.Start();
        }

        private void frmsplashscren_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void tmrsp_Tick(object sender, EventArgs e)
        {
            tmrsp.Stop();
            this.Hide();
            Form1 fr = new Form1();
            fr.ShowDialog();
        }
    }
}
