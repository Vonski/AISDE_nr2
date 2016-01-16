using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetworkDesignProject
{
    class NetworkDesign
    {
        Graph best_solution;
        Graph reference_solution;
        public LabelCorrecting working_copy;
        double temperature;
        Demand[] demands;
        int number_of_demands;

        public NetworkDesign()
        {
            working_copy = new LabelCorrecting();

            Console.WriteLine("Przeciagnij plik inicjalizacyjny i wcisnij enter:");
            string str = Console.ReadLine();
            if (str[0] == '"')
                str = str.Substring(1, str.Length - 2);
            FileStream fs = new FileStream(str, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            System.IO.StreamReader filestream = new System.IO.StreamReader(fs);

            string tmp = "";
            string[] words;
            int lid, node_start, node_end, did;

            bool logic = true;
            while (logic)
            {
                tmp = filestream.ReadLine();
                if (!string.IsNullOrEmpty(tmp))
                    if (tmp[0] != '#')
                    {
                        logic = false;
                    }
            }
            words = tmp.Split(' ');
            working_copy.graph.number_of_nodes = int.Parse(words[2]);
            logic = true;
            while (logic)
            {
                tmp = filestream.ReadLine();
                if (!string.IsNullOrEmpty(tmp))
                    if (tmp[0] != '#')
                    {
                        logic = false;
                    }
            }
            words = tmp.Split(' ');
            working_copy.graph.number_of_links = int.Parse(words[2]);

            working_copy.graph.links = new Link[working_copy.graph.number_of_links];
            working_copy.graph.nodes = new Node[working_copy.graph.number_of_nodes];
            working_copy.graph.paths = new Path[working_copy.graph.number_of_nodes][];

            working_copy.graph.links_from_node = new List<Heap<Link>>();
            for (int i = 0; i < working_copy.graph.number_of_nodes; i++)
            {
                Heap<Link> heap = new Heap<Link>();
                working_copy.graph.links_from_node.Add(heap);
            }
            logic = true;
            while (logic)
            {
                tmp = filestream.ReadLine();
                if (!string.IsNullOrEmpty(tmp))
                    if (tmp[0] != '#')
                    {
                        logic = false;
                    }
            }
            for (int i = 0; i < working_copy.graph.number_of_links; i++)
            {
                words = tmp.Split(' ');
                lid = int.Parse(words[0]);
                node_start = int.Parse(words[1]);
                node_end = int.Parse(words[2]);

                Link link = new Link(lid, node_start, node_end, 1);
                link.capacity = double.Parse(words[3], System.Globalization.CultureInfo.InvariantCulture);
                link.price = double.Parse(words[4], System.Globalization.CultureInfo.InvariantCulture);
                link.link_length = link.price / link.capacity;
                working_copy.graph.links[i] = link;
                working_copy.graph.links_from_node[node_start - 1].Add(link);

                tmp = filestream.ReadLine();
            }
            logic = true;
            while (logic)
            {
                tmp = filestream.ReadLine();
                if (!string.IsNullOrEmpty(tmp))
                    if (tmp[0] != '#')
                    {
                        logic = false;
                    }
            }
            words = tmp.Split(' ');
            number_of_demands = int.Parse(words[2]);
            demands = new Demand[number_of_demands];
            logic = true;
            while (logic)
            {
                tmp = filestream.ReadLine();
                if (!string.IsNullOrEmpty(tmp))
                    if (tmp[0] != '#')
                    {
                        logic = false;
                    }
            }
            for (int i = 0; i < number_of_demands; i++)
            {
                words = tmp.Split(' ');
                did = int.Parse(words[0]);
                node_start = int.Parse(words[1]);
                node_end = int.Parse(words[2]);

                Demand demand = new Demand(did, node_start, node_end);
                demand.length = double.Parse(words[3], System.Globalization.CultureInfo.InvariantCulture);
                demands[i] = demand;

                tmp = filestream.ReadLine();
            }
            for (int i = 0; i < working_copy.graph.number_of_nodes; i++)
            {
                Node node = new Node(i + 1);
                working_copy.graph.nodes[i] = node;
                Path[] p = new Path[working_copy.graph.number_of_nodes];
                working_copy.graph.paths[i] = p;
                for (int j = 0; j < working_copy.graph.number_of_nodes; j++)
                {
                    Path path = new Path();
                    working_copy.graph.paths[i][j] = path;
                }

            }
        }

        public void show()
        {
            working_copy.graph.show();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(number_of_demands);
            Console.WriteLine();
            for (int i = 0; i < number_of_demands; i++)
                Console.WriteLine("Zapotrzebowanie: " + demands[i].id + ", Miedzy " + demands[i].node_start + " a " + demands[i].node_end + " o rozmiarze: " + demands[i].length);
        }
    }
}
