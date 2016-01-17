using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesignProject
{
    class DistributionGenerator
    {
        Random rnd;
        public DistributionGenerator()
        {
            rnd = new Random();
        }
        public double generateRndStd(double mean, double stddev)
        {
            double a = rnd.NextDouble();
            double b = rnd.NextDouble();
            double rnd_std_normal = Math.Sqrt(-2.0 * Math.Log(a)) * Math.Sin(2.0 * Math.PI * b);
            rnd_std_normal = mean + stddev * rnd_std_normal;
            return rnd_std_normal;
        }

        public int generateRndInt(double rnd_std)
        {
            int rnd_int_normal = (int)Math.Floor(Math.Abs(rnd_std));
            return rnd_int_normal;
        }
        
    }
}
