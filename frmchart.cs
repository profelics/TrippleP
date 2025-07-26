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
    public partial class frmchart : Form
    {
        public frmchart()
        {
            InitializeComponent();
        }

        private void frmchart_Load(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear(); chart1.Series[1].Points.Clear();
            // chart1.ChartAreas[0].AxisX.Minimum =1;
            for (int j=0; j<Form1.n;j++)
            {
                chart1.Series[0].Points.AddXY(Form1.r[j],Form1.dt[j]);
               // chart1.Series[1].Points.AddXY(Form1.rn[j], Form1.dt[j]);

            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].AxisY.Minimum = Double.Parse(txty.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("OOPs!! \n"+ex.Message,"Error encountered", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
           
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            chart1.Series[1].Points.Clear();
            try
            {
                for (int j = 0; j < Form1.n; j++)
                {
                    // chart1.Series[0].Points.AddXY(Form1.r[j], Form1.dt[j]);
                    chart1.Series[1].Points.AddXY(Form1.rn[j], Form1.dt[j]);

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error encountered", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
           

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
