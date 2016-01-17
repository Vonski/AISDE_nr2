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
            test.designing();
            test.showSolution();

            Console.ReadLine();
        }
    }
}
