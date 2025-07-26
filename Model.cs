using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrippleP
{
    class Model
    {
        private double n;
        private double obg;
        private double png;
        public Model()
        {


        }
        public double N
        {
            get; set;
        }
        public double Obg
        {
            get; set;
        }
        public double Png
        {
            get; set;
        }

        public double Ppg(double R, double ob, double pn, double n, double Rn)
        {
            double val;
            val = ob - (ob - pn) * Math.Pow((R / Rn), n);
            if (val>0)
            {
                return val;
            }
            else
            {
                return 0;
            }
            
        }

    }
}
