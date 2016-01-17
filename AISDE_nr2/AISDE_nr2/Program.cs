using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AISDE_nr2
{
    class Program
    {
        static void Main(string[] args)
        {
            int A;
            double t1=0, t2=0;

            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Podaj liczbe iteracji: ");

            A = Convert.ToInt32(Console.ReadLine());
            
            LabelCorrecting test = new LabelCorrecting();
            for (int i = 0; i < A; i++)
            {
                sw.Restart();
                test.redraw();
                test.findAll();
                sw.Stop();
                t1 += sw.ElapsedMilliseconds;
            }
            t1 = t1 / A;
            Console.WriteLine("Poprawianie etykiet: {0}", t1);
            
            
            Floyd test2 = new Floyd();
            for (int i = 0; i < A; i++)
            {
                sw.Restart();
                test2.redraw();
                test2.findAll();
                sw.Stop();
                t2 += sw.ElapsedMilliseconds;
            }
            t2 = t2 / A;
            Console.WriteLine("Floyd: {0}", t2);
            
            
            Console.ReadLine();
            

        }
    }
}
