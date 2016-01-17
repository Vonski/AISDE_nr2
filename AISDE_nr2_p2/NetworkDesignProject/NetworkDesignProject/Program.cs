using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesignProject
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkDesign test = new NetworkDesign();

            test.show();
            test.firstSolution();
            test.show();
            test.showSolution();
            test.nextSolution();
            test.show();
            test.showSolution();

            Console.ReadLine();
        }
    }
}
