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
        public Path[] path;
        public LabelCorrecting()
        {
            network = new Network();
            for (int i = 0; i < network.number_of_nodes; i++)
                network.nodes[i].label = double.PositiveInfinity;
            path = new Path[network.number_of_nodes];
            for (int i = 0; i < network.number_of_nodes; i++)
            {
                Path p = new Path();
                path[i] = p;
            }
            
        }

        public Path findAB(int A, int B)
        {
            Path[] p= findOneToAll(A);
            return p[B - 1];
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
                    path[i].nodes_on_path = A.ToString() + " ";
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
                                path[heap.first().node_end - 1].nodes_on_path = path[heap.first().node_start - 1].nodes_on_path + network.nodes[heap.first().node_end - 1].id + " ";
                            else
                                path[heap.first().node_end - 1].nodes_on_path = path[heap.first().node_end - 1].nodes_on_path + network.nodes[heap.first().node_end - 1].id + " ";

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
                        path[i].nodes_on_path = "Do tego wierzcholka nie da sie dojsc";
                }
                
                for (int i = 0; i < network.number_of_nodes; i++)
                    path[i].length = network.nodes[i].label;
               
            }

            for (int i = 0; i < network.number_of_nodes; i++)
            {
                network.nodes[i].label = double.PositiveInfinity;
                network.nodes[i].enqueued = false;
            }

                return path;
        }
        public Path[][] findAll()
        {
            Path[][] p = new Path[network.number_of_nodes][];
            for(int i=0; i<network.number_of_nodes; i++)
            {
                 p[i] = findOneToAll(i + 1);
            }
            return p;
        }
    }
}
