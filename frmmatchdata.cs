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
    public partial class 
        frmmatchdata : Form
    {
        public frmmatchdata()
        {
            InitializeComponent();
        }
        public static double n, obg, png,Dp;

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            n = double.Parse(txtn.Text); obg = double.Parse(txtobg.Text); png = double.Parse(txtpng.Text);
                 
            if(double.TryParse(txtH.Text,out _))
            {
                if (MessageBox.Show("Data Matched successfully!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Dp = double.Parse(txtH.Text);
                    this.Hide();
                }

            }
            else 
            {
                MessageBox.Show("Please input correct data");
            
            }

        }
    }
}
