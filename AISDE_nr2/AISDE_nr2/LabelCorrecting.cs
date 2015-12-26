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
        public List<String> paths;

        public LabelCorrecting()
        {
            network = new Network();
            for (int i = 0; i < network.number_of_nodes; i++)
            {
                network.nodes[i].label = double.PositiveInfinity;
            }
            paths = new List<string>();

            paths = new List<String>();
            for (int i = 0; i < network.number_of_nodes; i++)
            {
                string s = "";
                paths.Add(s);
            }

        }

        public void findAB(int A, int B)
        {

        }

        public void findOneToAll(int A)
        {
            network.nodes[A-1].label = 0;
            queue=new Queue<Node>();
            Heap<Link> heap = new Heap<Link>();
            queue.Enqueue(network.nodes[A-1]);
            network.nodes[A-1].enqueued=true;
            for (int i = 0; i < network.number_of_nodes; i++)
                paths[i] = A.ToString()+" ";
            Node node = new Node();
            while(queue.Count()!=0)
            {
                node = queue.Peek();
                heap = network.copyHeap(node.id-1);
                while(heap.counter!=0)
                {
                    if(network.nodes[heap.first().node_end-1].label > (node.label + heap.first().link_length))
                    {
                        network.nodes[heap.first().node_end - 1].label = node.label + heap.first().link_length;
                        
                        if (heap.first().node_start != A)
                            paths[heap.first().node_end - 1] = paths[heap.first().node_start-1] + network.nodes[heap.first().node_end - 1].id + " ";
                        else
                            paths[heap.first().node_end - 1] = paths[heap.first().node_end - 1] + network.nodes[heap.first().node_end - 1].id + " ";

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
        }
        public void findAll()
        {

        }
    }
}
