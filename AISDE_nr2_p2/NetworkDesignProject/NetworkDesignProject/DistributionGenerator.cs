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
        public List<int> list;
        public DistributionGenerator()
        {
            rnd = new Random();
            list = new List<int>(20);
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

        public int choose()
        {
            //NormalDistributionGenerator nd = new NormalDistributionGenerator();
            double r = generateRndStd(0, 1.25);
            int ri = generateRndInt(r);
            int ans = list[ri];
            list.Add(ans);
            list.RemoveAt(ri);
            return ans;
        }
    }
}
