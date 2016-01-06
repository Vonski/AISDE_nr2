using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_nr2
{
    class LabelCorrecting :IFindPath
    {
        public Network network;
        Queue<Node> queue;
        private Random rand;
        public LabelCorrecting()
        {
            network = new Network();
            for (int i = 0; i < network.number_of_nodes; i++)
                network.nodes[i].label = double.PositiveInfinity;
            rand = new Random();
        }

        public void redraw()
        {
            for (int i = 0; i < network.links.Length; i++)
                network.links[i].link_length = rand.Next(0,1);
        }

        public Path findAB(int A, int B)
        {
            network.paths[A - 1] = findOneToAll(A);
            return network.paths[A - 1][B - 1];
        }

        public Path[] findOneToAll(int A)
        {
            if (network.links_from_node[A - 1].counter == 0)
                Console.WriteLine("Z wierzcholka " + A + " nie ma wyjscia");
            else
            {
                network.nodes[A - 1].label = 0;
                queue = new Queue<Node>();
                Heap<Link> heap = new Heap<Link>();
                queue.Enqueue(network.nodes[A - 1]);
                network.nodes[A - 1].enqueued = true;
                for (int i = 0; i < network.number_of_nodes; i++)
                    network.paths[A-1][i].nodes_on_path = A.ToString() + " ";
                Node node = new Node();
               
                while (queue.Count() != 0)
                {
                    node = queue.Peek();
                    heap = network.copyHeap(node.id - 1);

                    while (heap.counter != 0)
                    {
                        if (network.nodes[heap.first().node_end - 1].label > (node.label + heap.first().link_length))
                        {
                            network.nodes[heap.first().node_end - 1].label = node.label + heap.first().link_length;

                            if (heap.first().node_start != A)
                                network.paths[A-1][heap.first().node_end - 1].nodes_on_path = network.paths[A-1][heap.first().node_start - 1].nodes_on_path + network.nodes[heap.first().node_end - 1].id + " ";
                            else
                                network.paths[A-1][heap.first().node_end - 1].nodes_on_path = network.paths[A-1][heap.first().node_end - 1].nodes_on_path + network.nodes[heap.first().node_end - 1].id + " ";

                            if (!network.nodes[heap.first().node_end - 1].enqueued)
                            {
                                queue.Enqueue(network.nodes[heap.first().node_end - 1]);
                                network.nodes[heap.first().node_end - 1].enqueued = true;
                            }
                        }
                        heap.Delete();

                    }
                    queue.Dequeue();
                }

                for (int i = 0; i < network.number_of_nodes; i++)
                {
                    if (network.nodes[i].enqueued == false)
                        network.paths[A-1][i].nodes_on_path = "Do tego wierzcholka nie da sie dojsc";
                    network.paths[A-1][i].length = network.nodes[i].label;
                }
                        
            }

            for (int i = 0; i < network.number_of_nodes; i++)
            {
                network.nodes[i].label = double.PositiveInfinity;
                network.nodes[i].enqueued = false;
            }

            return network.paths[A-1];
        }
        public Path[][] findAll()
        {
            for(int i=0; i<network.number_of_nodes; i++)
            {
                 network.paths[i] = findOneToAll(i + 1);
            }
            return network.paths;
        }
    }
}
