using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TrippleP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int n;
        public static double[] dt;
        public static double[] r; public static double[] rn;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            computeRnTsm.Enabled = true;
            ofd.ShowDialog();
            try
            {
                string[] fields = new string[8];
                string[] dp = new string[15000];
                string[] res = new string[15000];
                var Filename = System.IO.Path.GetFileName(ofd.FileName);
                //FileStream infile = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(ofd.FileName);
                string recordIn = reader.ReadLine();
                const char DELIM = ',';
                int intsub = 0;
                while (recordIn != null)
                {
                    fields = recordIn.Split(DELIM);
                    dp[intsub] = fields[0];
                    res[intsub] = fields[1];
                    intsub++;
                    recordIn = reader.ReadLine();
                }
                //intsub is the variable that holds the total number of data from the esxcel raw file
                
             //Now we shall retrieve the data from the second cells in the excel file. the first cells are reserved for headers.
                for (int i = 0; i <= intsub - 2; i++)
                {   
                        dgv.Rows.Add();
                        dgv.Rows[i].Cells[0].Value = i + 1;
                        dgv.Rows[i].Cells[1].Value = dp[i + 1];   //depth (ft) from second cells in excel
                        dgv.Rows[i].Cells[2].Value = res[i + 1];// imort resistivity from second cells
                    
                }


                reader.Close();
            //infile.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Operation canceled! \n" + err.Message);
            }

            

        }

        public class SimpleMovingAverage
        {
            private readonly int _k;
            private readonly int[] _values;

            private int _index = 0;
            private int _sum = 0;

            public SimpleMovingAverage(int k)
            {
                if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k), "Must be greater than 0");

                _k = k;
                _values = new int[k];
            }

            public double Update(int nextInput)
            {
                // calculate the new sum
                _sum = _sum - _values[_index] + nextInput;

                // overwrite the old value with the new one
                _values[_index] = nextInput;

                // increment the index (wrapping back to 0)
                _index = (_index + 1) % _k;

                // calculate the average
                return ((double)_sum) / _k;
            }
        }

        private void computeRnTsm_Click(object sender, EventArgs e)
        {
            int dIndex;
            double A2, ro, D0, d2, r2, m;
            if (frmmatchdata.Dp>0)
            {
                //calculate for b
                //locate points x1,y1 i.e. R0,D0.
                d2 = frmmatchdata.Dp;
                D0 = Convert.ToDouble(dgv.Rows[1].Cells[1].Value);
                ro = Convert.ToDouble(dgv.Rows[1].Cells[2].Value);
                n = dgv.RowCount;
                rn = new double[n];
                dt = new double[n];
                r = new double[n];
                dIndex = -1;
                if (dgv.RowCount > -1)
                {
                    /*foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(d2.ToString()))
                        {
                            dIndex = row.Index;
                            break;
                        }
                    }*/

                    //r2 = Convert.ToDouble(dgv.Rows[dIndex].Cells[2].Value);
                    m = frmmatchdata.Dp;//(Math.Log10(r2 / ro)) / (d2 - D0);

                    /*for (int i = 0; i < dgv.RowCount; i++)
                    {
                        if (d2 == Convert.ToDouble(dgv.Rows[i].Cells[1].Value))
                        {
                            r2 = Convert.ToDouble(dgv.Rows[i].Cells[2].Value);
                            break;
                        }
                        if ((i == dgv.RowCount - 1) && d2 != Convert.ToDouble(dgv.Rows[i].Cells[1].Value))
                        {
                            MessageBox.Show("Value for undercompacted trend could not be found. please enter anaother depth value present in the importeddata", "Error message");
                        }
                    
                    
                    }*/

                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        dt[i] = Convert.ToDouble(dgv.Rows[i].Cells[1].Value);
                        r[i] = Convert.ToDouble(dgv.Rows[i].Cells[2].Value);
                        ro = Convert.ToDouble(dgv.Rows[0].Cells[2].Value);
                        A2 = Convert.ToDouble(dgv.Rows[i].Cells[1].Value);
                        dgv.Rows[i].Cells[3].Value = ro * Math.Exp(m * A2);
                        rn[i] = Convert.ToDouble(dgv.Rows[i].Cells[3].Value);
                    }


                }
                else { MessageBox.Show("Import data first!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }


            }
          
        }

        private void displayResistivityChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
            chart1.Series[0].Points.Clear();
            for (int j = 0; j < dgv.Rows.Count; j++)
            {
                chart1.Series[0].Points.AddXY(Convert.ToDouble(dgv.Rows[j].Cells[2].Value), Convert.ToDouble(dgv.Rows[j].Cells[1].Value));
                // chart1.Series[1].Points.AddXY(Form1.rn[j], Form1.dt[j]);
            }
            chart1.Visible = true;
            chart1.ChartAreas[0].AxisX.Minimum = 0.1;
            chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
        }

        private void rockAndFluidDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compPpg.Enabled = true;
            frmmatchdata fr = new frmmatchdata();
            fr.ShowDialog();
        }
        public static double[] obg, pp, pngPlot, mvgPlot, emvgPlot;
        private void compPpg_Click(object sender, EventArgs e)
        {
            ppg.Enabled = true;
            pp = new double[n];obg = new double[n]; pngPlot = new double[n];
            Model m = new Model();
            for(int i=0;i<dgv.RowCount;i++)
            {
              pp[i] = (m.Ppg(Convert.ToDouble(dgv.Rows[i].Cells[2].Value), frmmatchdata.obg,
                    frmmatchdata.png, frmmatchdata.n, Convert.ToDouble(dgv.Rows[i].Cells[3].Value))) *
                    Convert.ToDouble(dgv.Rows[i].Cells[1].Value);
                dgv.Rows[i].Cells[5].Value = pp[i];
                obg[i]= frmmatchdata.obg * Convert.ToDouble(dgv.Rows[i].Cells[1].Value);
                pngPlot[i] = frmmatchdata.png * Convert.ToDouble(dgv.Rows[i].Cells[1].Value);
                dgv.Rows[i].Cells[4].Value = obg[i];
                dgv.Rows[i].Cells[6].Value = pngPlot[i];
                
            }

            mvgPlot = new double[n];
            emvgPlot = new double[n];
            double current = 0;

            for (int i = 0; i < pp.Length; i++)
            {
                current = current + pp[i];
                mvgPlot[i] = current / (i + 1);
                dgv.Rows[i].Cells[7].Value = mvgPlot[i];
            }

            /*for (int i=0; i < pp.Length; i++)
            {
                current = current + pp[i];
                mvgPlot[i] = current / (i + 1);

                if (i < 10)
                {
                    emvgPlot[i] = mvgPlot[i];
                }
                else
                {
                    if(i == 10)
                    {
                        emvgPlot[10] = (pp[9] - mvgPlot[9]) * (2 / 11) + mvgPlot[9];
                    }
                    else
                    {
                        /*current = current + pp[i];
                        //mvgPlot[i] = current / i;
                        //emvgPlot[i] = (pp[i] - mvgPlot[i]) * (2 / (11) + mvgPlot[i]);
                        emvgPlot[i] = ((pp[i] * (2 / (i+1))) + (emvgPlot[i - 1] * (1 - (2/ (i + 1)))));
                    }
                }

                dgv.Rows[i].Cells[7].Value = emvgPlot[i];
            }*/

            /*var result = pp.MovingAvg(n);
            double[] mvgRes = new double[n] { };
            for(int i = 0; i < dgv.RowCount; i++)
            {
                mvgRes[i] = 
                mvgPlot[i] += Convert.ToDouble(result);
                dgv.Rows[i].Cells[7].Value = mvgPlot[i];
            }
            */

            //var input = new double[] { 1, 2, 2, 4, 5, 6, 6, 6, 9, 10, 11, 12, 13, 14, 15 };
            //Console.WriteLine(string.Join(", ", result));


            /*var calculator = new SimpleMovingAverage(k: n);

            string[] ppStr = new string[] {};
            int text = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                string textString = dgv.Rows[i].Cells[6].Value.ToString();
                text += Convert.ToInt32(textString);
            }

            var input = new[] { 1, 2, 3, 4 };

            foreach (var value in input)
            {
                var sma = calculator.Update(value);
                //Console.WriteLine($"The next value is {sma}");

            }*/
        }

        private void ppg_Click(object sender, EventArgs e)
        {
            frmpress fr = new frmpress();
            fr.ShowDialog();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void displayResistivityChartRnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmchart fr = new frmchart();
            fr.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(0);
        }
    }

    public static class Extensions
    {
        public static IEnumerable<double> MovingAvg(this IEnumerable<double> source, int period)
        {
            var buffer = new Queue<double>();

            foreach (var value in source)
            {
                buffer.Enqueue(value);

                // sume the buffer for the average at any given time
                yield return buffer.Sum() / buffer.Count;

                // Dequeue when needed 
                if (buffer.Count == period)
                    buffer.Dequeue();
            }
        }
    }
}
