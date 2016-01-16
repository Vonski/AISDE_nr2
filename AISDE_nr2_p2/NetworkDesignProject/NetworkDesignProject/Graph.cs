using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        public Graph()
        {
        }

        public void show()
        {
            Console.WriteLine(number_of_links);
            Console.WriteLine();
            for (int i = 0; i < number_of_links; i++)
                Console.WriteLine("Polaczenie miedzy " + links[i].node_start + " a " + links[i].node_end + " o przepustowosci: " + links[i].capacity + ", cenie: " + links[i].price + ", stosunku: " + links[i].link_length);

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
