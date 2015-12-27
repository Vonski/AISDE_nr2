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
            LabelCorrecting lc = new LabelCorrecting();
            Random rnd = new Random();
            int r = 0;
            for (int i = 0; i < lc.network.number_of_links; i++)
            {
                r = rnd.Next(1, 10);
                lc.network.links[i].setLength(r);
                int start = lc.network.links[i].node_start;
                int end = lc.network.links[i].node_end;
                lc.network.connections[start - 1, end - 1] = r;
            }
            for (int i = 0; i < lc.network.number_of_nodes; i++)
            {
                lc.network.links_from_node[i].construct();
            }
            lc.network.show();
            Console.WriteLine("\r\n");
            lc.network.showLinksFromNodes();
            Path[][] p = lc.findAll();
            Console.WriteLine("\r\n");
            for (int i = 0; i < 6; i++)
                Console.WriteLine(lc.network.nodes[i].label);
            Console.WriteLine("\r\n");
            //Console.WriteLine(p.nodes_on_path+"    " + p.length);
            
            Console.Read();
            /*Network network = new Network();
            network.show();
            Random rnd = new Random();
            int r=0;
            for (int i = 0; i < network.number_of_links; i++)
            {
                r=rnd.Next(1,10);
                network.links[i].setLength(r);
                int start = network.links[i].node_start;
                int end = network.links[i].node_end;
                network.connections[start - 1, end - 1] = r;
            }
            for (int i=0; i<network.number_of_nodes; i++)
            {
                network.links_from_node[i].construct();
            }
            Console.WriteLine("");
            Console.WriteLine("");

            network.show();
            Console.WriteLine("");
            network.showLinksFromNodes();*/

            Console.Read();

            /*System.IO.StreamWriter filestream_heap = new System.IO.StreamWriter("timeheap.txt", false);
            Stopwatch stopwatch = new Stopwatch();
            Random rnd = new Random();
            Heap<int> heap = new Heap<int>();
            for (int i=10; i<=10000; i=i+10)
            {
                heap.table=new int[i];
                for(int j=0; j<i; j++)
                {
                    heap.table[j] = rnd.Next(1, 100);
                }
                heap.counter = i;
                stopwatch.Start();
                heap.construct();
                stopwatch.Stop();
                TimeSpan t = stopwatch.Elapsed;
                string time = t.TotalMilliseconds.ToString();
                filestream_heap.WriteLine(time);

                
            }
            filestream_heap.Close();*/

            /*Heap<int> heap = new Heap<int>();
            heap.table = new int[10];
            heap.table[0] = 8;
            heap.table[1] = 2;
            heap.table[2] = 4;
            heap.table[3] = 5;
            heap.table[4] = 1;
            heap.table[5] = 3;
            heap.table[6] = 4;
            heap.table[7] = 2;
            heap.table[8] = 7;
            heap.table[9] = 1;



            heap.counter = 10;
            heap.WriteOut();
            Console.WriteLine("\r\n");
            heap.construct2();
            heap.WriteOut();

            Console.Read();*/

            
            
           

        }
    }
}
