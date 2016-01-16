using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetworkDesignProject
{
    class Graph
    {
        public int number_of_nodes;
        public int number_of_links;
        public List<Heap<Link>> links_from_node;
        public Link[] links;
        public Node[] nodes;
        public Path[][] paths;

        void readFile()
        {
            Console.WriteLine("Przeciagnij plik inicjalizacyjny i wcisnij enter:");
            string str = Console.ReadLine();
            if (str[0] == '"')
                str = str.Substring(1, str.Length - 2);
            FileStream fs = new FileStream(str, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            System.IO.StreamReader filestream = new System.IO.StreamReader(fs);

            string tmp = "";
            string[] words;
            int lid, node_start, node_end;

            tmp = filestream.ReadLine();
            tmp = filestream.ReadLine();
            words = tmp.Split(' ');
            number_of_nodes = int.Parse(words[2]);
            tmp = filestream.ReadLine();
            tmp = filestream.ReadLine();
            words = tmp.Split(' ');
            number_of_links = int.Parse(words[2]);

            links = new Link[number_of_links];
            nodes = new Node[number_of_nodes];
            paths = new Path[number_of_nodes][];

            links_from_node = new List<Heap<Link>>();
            for (int i = 0; i < number_of_nodes; i++)
            {
                Heap<Link> heap = new Heap<Link>();
                links_from_node.Add(heap);
            }

            tmp = filestream.ReadLine();
            for (int i = 0; i < number_of_links; i++)
            {
                tmp = filestream.ReadLine();
                words = tmp.Split(' ');
                lid = int.Parse(words[0]);
                node_start = int.Parse(words[1]);
                node_end = int.Parse(words[2]);

                Link link = new Link(lid, node_start, node_end, 1);
                links[i] = link;
                links_from_node[node_start - 1].Add(link);
            }
            for (int i = 0; i < number_of_nodes; i++)
            {
                Node node = new Node(i + 1);
                nodes[i] = node;
                Path[] p = new Path[number_of_nodes];
                paths[i] = p;
                for (int j = 0; j < number_of_nodes; j++)
                {
                    Path path = new Path();
                    paths[i][j] = path;
                }

            }

        }

        public Graph()
        {
            readFile();
        }

        public void show()
        {
            Console.WriteLine(number_of_links);
            Console.WriteLine();
            for (int i = 0; i < number_of_links; i++)
                Console.WriteLine("Polaczenie miedzy " + links[i].node_start + " a " + links[i].node_end + " o wadze: " + links[i].link_length);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(number_of_nodes);
            Console.WriteLine();
            for (int i = 0; i < number_of_nodes; i++)
                Console.WriteLine("Wezel: " + nodes[i].id);
        }

        public Heap<Link> copyHeap(int i)
        {
            Heap<Link> heap = new Heap<Link>();
            heap.table = new Link[links_from_node[i].counter];

            for (int n = 0; n < links_from_node[i].counter; n++)
            {
                Link link = new Link();
                heap.table[n] = link;
                heap.table[n].id = links_from_node[i].table[n].id;
                heap.table[n].node_start = links_from_node[i].table[n].node_start;
                heap.table[n].node_end = links_from_node[i].table[n].node_end;
                heap.table[n].link_length = links_from_node[i].table[n].link_length;

            }
            heap.counter = links_from_node[i].counter;
            return heap;
        }
    }
}
