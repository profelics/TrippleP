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
    public partial class frmpress : Form
    {
        public frmpress()
        {
            InitializeComponent();
        }

        private void frmpress_Load(object sender, EventArgs e)
        {
            chart1.Titles[0].Text = "PORE PRESSURE PREDICTION PLOT";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.Series[0].Points.Clear(); chart1.Series[1].Points.Clear(); chart1.Series[2].Points.Clear();
            for (int j = 0; j < Form1.n; j++)
            {
                chart1.Series[0].Points.AddXY(Form1.obg[j], Form1.dt[j]);
                chart1.Series[1].Points.AddXY(Form1.mvgPlot[j], Form1.dt[j]);
                chart1.Series[2].Points.AddXY(Form1.pngPlot[j], Form1.dt[j]);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].AxisY.Minimum = Double.Parse(txty.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OOPs!! \n" + ex.Message, "Error encountered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
