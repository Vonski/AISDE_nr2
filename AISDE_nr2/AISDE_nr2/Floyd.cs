using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_nr2
{
    class Floyd : IFindPath
    {
        public Network network;
        private double[,] d;
        private Random rand;
        private bool not_drawed;
        
        public Floyd()
        {
            network = new Network();
            d = new double[network.number_of_nodes, network.number_of_nodes];
            rand = new Random();
            not_drawed = true;
        }

        public void redraw()
        {
            for (int i = 0; i < network.links.Length; i++)
                network.links[i].link_length = rand.Next(0,1);
            not_drawed = false;
        }

        public Path findAB(int A, int B)
        {
            network.paths = findAll();
            return network.paths[A - 1][B - 1];
        }

        public Path[] findOneToAll(int A)
        {
            network.paths = findAll();
            return network.paths[A - 1];
        }
        public Path[][] findAll()
        {
            for (int i = 0; i < network.number_of_nodes; i++)
            {
                for (int j = 0; j < network.number_of_nodes; j++)
                {
                    d[i, j] = double.PositiveInfinity;
                    network.paths[i][j].nodes_on_path = network.nodes[i].id + " ";
                }
                d[i, i] = 0;
            }
            double z;
            for (int i = 0; i < network.links.Length; i++)
            {
                z = rand.Next(0,1);
                d[network.links[i].node_start-1, network.links[i].node_end-1] = z;
                if(not_drawed)
                    network.links[i].link_length = z;
            }
            /*
            for (int i = 0; i < network.number_of_nodes; i++)
            {
                for (int j = 0; j < network.number_of_nodes; j++)
                {
                    if(d[i, j]== double.PositiveInfinity)
                        Console.Write("n ");
                    else
                        Console.Write(d[i, j]+" ");
                }
                Console.WriteLine();
            }
            */
            for (int k = 0; k < network.number_of_nodes; k++)
                for (int i = 0; i < network.number_of_nodes; i++)
                    for (int j = 0; j < network.number_of_nodes; j++)
                        if (i == j)
                            continue;
                        else if (d[i, j] > d[i, k] + d[k, j])
                        {
                            d[i, j] = d[i, k] + d[k, j];
                            string tmp = network.paths[k][j].nodes_on_path;
                            network.paths[i][j].nodes_on_path = network.paths[i][k].nodes_on_path + tmp.Remove(0,1);
                        }
                        else
                        {
                            if(!network.paths[i][j].nodes_on_path.Contains(network.nodes[j].id.ToString()))
                                network.paths[i][j].nodes_on_path = network.paths[i][j].nodes_on_path + network.nodes[j].id + " ";
                        }
            /*
            Console.WriteLine();
            for (int i = 0; i < network.number_of_nodes; i++)
            {
                for (int j = 0; j < network.number_of_nodes; j++)
                {
                    if (d[i, j] == double.PositiveInfinity)
                        Console.Write("n ");
                    else
                        Console.Write(d[i, j]+" ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < network.number_of_nodes; i++)
            {
                for (int j = 0; j < network.number_of_nodes; j++)
                {
                    Console.WriteLine(network.paths[i][j].nodes_on_path);
                }
                
            }
            */

            return network.paths;
        }
    }
}
