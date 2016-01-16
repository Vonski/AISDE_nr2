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
            LabelCorrecting test = new LabelCorrecting();
            for (int i = 0; i < 1; i++)
            {
                test.redraw();
                test.graph.show();
                test.findAB(2, 3);
                Console.WriteLine(test.graph.paths[1][2].nodes_on_path);
            }

            Console.ReadLine();
        }
    }
}
