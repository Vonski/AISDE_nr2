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
        public int number_of_demands;
        public List<Heap<Link>> links_from_node;
        public Link[] links;
        public Node[] nodes;
        public Path[][] paths;
        public Demand[] demands;
        public double price;

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

        public Graph copyGraph()
        {
            Graph graph = new Graph();
            graph.number_of_links = this.number_of_links;
            graph.number_of_nodes = this.number_of_nodes;
            graph.links_from_node = new List<Heap<Link>>();
            graph.nodes = new Node[number_of_nodes];
            graph.links = new Link[number_of_links];
            graph.paths = new Path[number_of_nodes][];
            graph.demands = new Demand[number_of_demands];


            for (int i = 0; i < number_of_nodes; i++)
            {
                Heap<Link> heap = new Heap<Link>();
                graph.links_from_node.Add(heap);
                graph.nodes[i] = new Node();
                graph.nodes[i].id = this.nodes[i].id;
                graph.nodes[i].label = this.nodes[i].label;
                graph.nodes[i].enqueued = this.nodes[i].enqueued;
            }

            for (int i = 0; i < number_of_links; i++)
            {
                Link link = new Link();
                graph.links[i] = link;
                graph.links_from_node[this.links[i].node_start-1].Add(link);
                graph.links[i].id = this.links[i].id;
                graph.links[i].capacity = this.links[i].capacity;
                graph.links[i].link_length = this.links[i].link_length;
                graph.links[i].modules_counter = this.links[i].modules_counter;
                graph.links[i].node_start = this.links[i].node_start;
                graph.links[i].node_end = this.links[i].node_end;
                graph.links[i].price = this.links[i].price;
            }

            for(int i=0; i<number_of_nodes; i++)
            {
                graph.paths[i] = new Path[number_of_nodes];
                for (int j = 0; j < number_of_nodes; j++)
                {
                    graph.paths[i][j] = new Path();
                    graph.paths[i][j] = this.paths[i][j];
                }
            }

            for (int i = 0; i < number_of_demands; i++)
            {
                graph.demands[i] = new Demand();
                graph.demands[i].id = this.demands[i].id;
                graph.demands[i].length = this.demands[i].length;
                graph.demands[i].node_end = this.demands[i].node_end;
                graph.demands[i].node_start = this.demands[i].node_start;
                graph.demands[i].nodes_on_path = this.demands[i].nodes_on_path;

            }


            return graph;
        }

        public Graph residualGraph()
        {
            Graph graph = new Graph();
            graph.number_of_links = this.number_of_links;
            graph.number_of_nodes = this.number_of_nodes;
            graph.links_from_node = new List<Heap<Link>>();
            graph.nodes = new Node[number_of_nodes];
            graph.links = new Link[number_of_links];
            graph.paths = new Path[number_of_nodes][];
            graph.demands = new Demand[number_of_demands];


            for (int i = 0; i < number_of_nodes; i++)
            {
                Heap<Link> heap = new Heap<Link>();
                graph.links_from_node.Add(heap);
                graph.links_from_node[i] = this.copyHeap(i);
                graph.nodes[i] = new Node();
                graph.nodes[i].id = this.nodes[i].id;
                graph.nodes[i].label = this.nodes[i].label;
                graph.nodes[i].enqueued = this.nodes[i].enqueued;
            }

            for (int i = 0; i < number_of_links; i++)
            {
                graph.links[i] = new Link();
                graph.links[i].id = this.links[i].id;
                graph.links[i].capacity = this.links[i].capacity;
                graph.links[i].link_length = this.links[i].link_length;
                graph.links[i].modules_counter = this.links[i].modules_counter;
                graph.links[i].node_start = this.links[i].node_start;
                graph.links[i].node_end = this.links[i].node_end;
                graph.links[i].price = this.links[i].price;
            }

            for (int i = 0; i < number_of_nodes; i++)
            {
                graph.paths[i] = new Path[number_of_nodes];
                for (int j = 0; j < number_of_nodes; j++)
                {
                    graph.paths[i][j] = new Path();
                    graph.paths[i][j] = this.paths[i][j];
                }
            }

            for (int i = 0; i < number_of_demands; i++)
            {
                graph.demands[i] = new Demand();
                graph.demands[i].id = this.demands[i].id;
                graph.demands[i].length = this.demands[i].length;
                graph.demands[i].node_end = this.demands[i].node_end;
                graph.demands[i].node_start = this.demands[i].node_start;
                graph.demands[i].nodes_on_path = this.demands[i].nodes_on_path;

            }


            return graph;
        }
    }
}
