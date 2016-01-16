using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesignProject
{
    class LabelCorrecting : IFindPath
    {
        public Graph graph;
        Queue<Node> queue;
        private Random rand;
        public LabelCorrecting()
        {
            graph = new Graph();
            for (int i = 0; i < graph.number_of_nodes; i++)
                graph.nodes[i].label = double.PositiveInfinity;
            rand = new Random();
        }

        public void redraw()
        {
            for (int i = 0; i < graph.links.Length; i++)
                graph.links[i].link_length = rand.NextDouble();
        }

        public Path findAB(int A, int B)
        {
            graph.paths[A - 1] = findOneToAll(A);
            return graph.paths[A - 1][B - 1];
        }

        public Path[] findOneToAll(int A)
        {
            if (graph.links_from_node[A - 1].counter == 0)
                Console.WriteLine("Z wierzcholka " + A + " nie ma wyjscia");
            else
            {
                graph.nodes[A - 1].label = 0;
                queue = new Queue<Node>();
                Heap<Link> heap = new Heap<Link>();
                queue.Enqueue(graph.nodes[A - 1]);
                graph.nodes[A - 1].enqueued = true;
                for (int i = 0; i < graph.number_of_nodes; i++)
                    graph.paths[A - 1][i].nodes_on_path = A.ToString() + " ";
                Node node = new Node();

                while (queue.Count() != 0)
                {
                    node = queue.Peek();
                    heap = graph.copyHeap(node.id - 1);

                    while (heap.counter != 0)
                    {
                        if (graph.nodes[heap.first().node_end - 1].label > (node.label + heap.first().link_length))
                        {
                            graph.nodes[heap.first().node_end - 1].label = node.label + heap.first().link_length;

                            if (heap.first().node_start != A)
                                graph.paths[A - 1][heap.first().node_end - 1].nodes_on_path = graph.paths[A - 1][heap.first().node_start - 1].nodes_on_path + graph.nodes[heap.first().node_end - 1].id + " ";
                            else
                                graph.paths[A - 1][heap.first().node_end - 1].nodes_on_path = graph.paths[A - 1][heap.first().node_end - 1].nodes_on_path + graph.nodes[heap.first().node_end - 1].id + " ";

                            if (!graph.nodes[heap.first().node_end - 1].enqueued)
                            {
                                queue.Enqueue(graph.nodes[heap.first().node_end - 1]);
                                graph.nodes[heap.first().node_end - 1].enqueued = true;
                            }
                        }
                        heap.Delete();

                    }
                    queue.Dequeue();
                }

                for (int i = 0; i < graph.number_of_nodes; i++)
                {
                    if (graph.nodes[i].enqueued == false)
                        graph.paths[A - 1][i].nodes_on_path = "Do tego wierzcholka nie da sie dojsc";
                    graph.paths[A - 1][i].length = graph.nodes[i].label;
                }

            }

            for (int i = 0; i < graph.number_of_nodes; i++)
            {
                graph.nodes[i].label = double.PositiveInfinity;
                graph.nodes[i].enqueued = false;
            }

            return graph.paths[A - 1];
        }
        public Path[][] findAll()
        {
            for (int i = 0; i < graph.number_of_nodes; i++)
            {
                graph.paths[i] = findOneToAll(i + 1);
            }
            return graph.paths;
        }
    }
}
