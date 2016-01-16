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
            int A = 100000;
            

            int choose_method;
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Wybierz algorytm:");
            Console.WriteLine("[0] Poprawianie etykiet");
            Console.WriteLine("[1] Floyd");
            choose_method = Convert.ToInt32(Console.ReadLine());
            if (choose_method == 0)
            {
                sw.Start();
                LabelCorrecting test = new LabelCorrecting();
                for (int i = 0; i < A; i++)
                {
                    test.redraw();
                    test.findAll();
                }
                sw.Stop();
                Console.WriteLine("Elapsed={0}", sw.Elapsed);
            }
            else if(choose_method == 1)
            {
                sw.Start();
                Floyd test = new Floyd();
                for (int i = 0; i < A; i++)
                {
                    test.redraw();
                    test.findAll();
                }
                sw.Stop();
                Console.WriteLine("Elapsed={0}", sw.Elapsed);
            }
            else
            {
                Console.WriteLine("Wybór spoza zakresu");
            }
            Console.ReadLine();
            

        }
    }
}
