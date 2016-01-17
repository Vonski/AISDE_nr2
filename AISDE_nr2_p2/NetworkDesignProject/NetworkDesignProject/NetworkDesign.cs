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
        public List<int> list;
        DistributionGenerator gen;
        double mean;
        double stddev;


        public NetworkDesign()
        {
            working_copy = new LabelCorrecting();
            list = new List<int>(20);
            gen = new DistributionGenerator();
            mean = 0;
            stddev = 1.25;


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
            working_copy.graph.number_of_demands = int.Parse(words[2]);
            working_copy.graph.demands = new Demand[working_copy.graph.number_of_demands];
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
            for (int i = 0; i < working_copy.graph.number_of_demands; i++)
            {
                words = tmp.Split(' ');
                did = int.Parse(words[0]);
                node_start = int.Parse(words[1]);
                node_end = int.Parse(words[2]);

                Demand demand = new Demand(did, node_start, node_end);
                demand.length = double.Parse(words[3], System.Globalization.CultureInfo.InvariantCulture);
                working_copy.graph.demands[i] = demand;

                list.Add(i+1);

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
            Console.WriteLine(working_copy.graph.number_of_demands);
            Console.WriteLine();
            for (int i = 0; i < working_copy.graph.number_of_demands; i++)
                Console.WriteLine("Zapotrzebowanie: " + working_copy.graph.demands[i].id + ", Miedzy " + working_copy.graph.demands[i].node_start + " a " + working_copy.graph.demands[i].node_end + " o rozmiarze: " + working_copy.graph.demands[i].length + " sciezka: " + working_copy.graph.demands[i].nodes_on_path);
            Console.WriteLine();
            
        }

        public void showSolution()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Koszt całkowity: " + working_copy.graph.price);
            for (int i = 0; i < working_copy.graph.number_of_demands; i++)
                Console.WriteLine("Zapotrzebowanie: " + working_copy.graph.demands[i].id + ", Miedzy " + working_copy.graph.demands[i].node_start + " a " + working_copy.graph.demands[i].node_end + " o rozmiarze: " + working_copy.graph.demands[i].length + " sciezka: " + working_copy.graph.demands[i].nodes_on_path);
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < working_copy.graph.number_of_links; i++)
                Console.WriteLine("Polaczenie miedzy " + working_copy.graph.links[i].node_start + " a " + working_copy.graph.links[i].node_end + " o przepustowosci: " + working_copy.graph.links[i].capacity + ", liczba modulow: " + working_copy.graph.links[i].modules_counter + ", uzywana przepustowosc: " + working_copy.graph.links[i].capacity_in_use);


        }

        public int choose()
        {
            double r = gen.generateRndStd(mean, stddev);
            int ri = gen.generateRndInt(r);
            while (ri < 0 || ri >= list.Count)
                ri = ri / list.Count;
            int id = list[ri];
            list.Add(id);
            list.RemoveAt(ri);
            return id;
        }

        public void firstSolution()
        {
            double sum = 0;
            int n=0;
            for (int i = 0; i < working_copy.graph.number_of_demands; i++)
            {
                sum += working_copy.graph.demands[i].length;
            }
            for (int i = 0; i < working_copy.graph.number_of_links; i++ )
            {
                n = (int)Math.Ceiling(sum / working_copy.graph.links[i].capacity);
                working_copy.graph.links[i].modules_counter = n;
            }
            Random rnd = new Random();

            Path path = new Path();
            int r = rnd.Next(0, working_copy.graph.number_of_demands);
            int rz = r;
            while(r>=0)
            {
                path = working_copy.findAB(working_copy.graph.demands[r].node_start, working_copy.graph.demands[r].node_end);
                working_copy.graph.demands[r].dataFromPath(path);
                r--;
            }
            while (rz < working_copy.graph.number_of_demands)
            {
                path = working_copy.findAB(working_copy.graph.demands[rz].node_start, working_copy.graph.demands[rz].node_end);
                working_copy.graph.demands[rz].dataFromPath(path);
                rz++;
            }

            graphUpdate();
            best_solution = working_copy.graph.copyGraph();
            reference_solution = working_copy.graph.copyGraph();

        }

        public void graphUpdate()
        {
            for (int i = 0; i < working_copy.graph.number_of_links; i++)
                working_copy.graph.links[i].modules_counter = 0;
            working_copy.graph.price = 0;

            for (int i = 0; i < working_copy.graph.number_of_demands; i++)
            {
                string[] words;
                words = working_copy.graph.demands[i].nodes_on_path.Split(' ');
                for (int k = 0; k < words.Length - 2; k++)
                {
                    for (int j = 0; j < working_copy.graph.links_from_node[int.Parse(words[k]) - 1].counter; j++)
                        if (working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].node_end == int.Parse(words[k+1]))
                        {
                            int n = (int)Math.Ceiling(working_copy.graph.demands[i].length / working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity);
                            working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].modules_counter += n;
                            working_copy.graph.price += n*working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].price;
                            working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity_in_use += working_copy.graph.demands[i].length;
                            break;
                        }
                }
            }

        }

        public bool nextSolution()
        {
            int did = choose();

            string[] words;
            words = working_copy.graph.demands[did-1].nodes_on_path.Split(' ');
            for (int k = 0; k < words.Length - 2; k++)
            {
                for (int j = 0; j < working_copy.graph.links_from_node[int.Parse(words[k]) - 1].counter; j++)
                    if (working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].node_end == int.Parse(words[k + 1]))
                    {
                        int n = (int)Math.Ceiling((working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity_in_use-working_copy.graph.demands[did-1].length) / working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity);
                        int tmp = working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].modules_counter;
                        working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].modules_counter = n;
                        working_copy.graph.price -= (tmp- n) * working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].price;
                        working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity_in_use -= working_copy.graph.demands[did-1].length;
                        break;
                    }
            }
            

            LabelCorrecting residual_graph = new LabelCorrecting();
            residual_graph.graph = working_copy.graph.residualGraph();

            for (int i = 0; i < residual_graph.graph.number_of_links; i++)
            {
                if (residual_graph.graph.links[i].capacity_in_use == 0)
                {
                    int n = (int)Math.Ceiling(working_copy.graph.demands[did - 1].length / residual_graph.graph.links[i].capacity);
                    residual_graph.graph.links[i].modules_counter += n;
                    residual_graph.graph.links[i].link_length = residual_graph.graph.links[i].price / residual_graph.graph.links[i].capacity;
                }
                else if (residual_graph.graph.links[i].capacity_in_use > 0)
                {
                    if (working_copy.graph.demands[did - 1].length > residual_graph.graph.links[i].capacity - residual_graph.graph.links[i].capacity_in_use)
                    {
                        int n = (int)Math.Ceiling((working_copy.graph.demands[did - 1].length - (residual_graph.graph.links[i].capacity - residual_graph.graph.links[i].capacity_in_use)) / residual_graph.graph.links[i].capacity);
                        if (n < 0)
                            n = 0;
                        residual_graph.graph.links[i].modules_counter += n;
                        residual_graph.graph.links[i].link_length = n * residual_graph.graph.links[i].price / (n * residual_graph.graph.links[i].capacity + residual_graph.graph.links[i].capacity_in_use);
                    }
                    else
                        residual_graph.graph.links[i].link_length = 0;
                }
            }
            
            //residual_graph.graph.show();

            Path path = new Path();
            path = working_copy.findAB(working_copy.graph.demands[did-1].node_start, working_copy.graph.demands[did-1].node_end);
            working_copy.graph.demands[did-1].dataFromPath(path);

            words = working_copy.graph.demands[did - 1].nodes_on_path.Split(' ');
            for (int k = 0; k < words.Length - 2; k++)
            {
                for (int j = 0; j < working_copy.graph.links_from_node[int.Parse(words[k]) - 1].counter; j++)
                    if (working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].node_end == int.Parse(words[k + 1]))
                    {
                        int n;
                        if (working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity_in_use==0)
                            n = (int)Math.Ceiling(working_copy.graph.demands[did - 1].length / working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity);
                        else
                            n = (int)Math.Ceiling((working_copy.graph.demands[did - 1].length - (residual_graph.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity - residual_graph.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity_in_use)) / working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity);
                        if (n < 0)
                            n = 0;
                        working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].modules_counter += n;
                        working_copy.graph.price += n * working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].price;
                        working_copy.graph.links_from_node[int.Parse(words[k]) - 1].table[j].capacity_in_use += working_copy.graph.demands[did-1].length;
                        break;
                    }
            }
            reference_solution = working_copy.graph.copyGraph();


            if (best_solution.price < working_copy.graph.price)
            {
                double x = -(working_copy.graph.price - best_solution.price) / temperature;
                double p = Math.Exp(x);
                Random rnd = new Random();
                double r = rnd.NextDouble();
                if (r <= p)
                {
                    best_solution = working_copy.graph.copyGraph();
                    return true;
                }
            }
            else if (best_solution.price > working_copy.graph.price)
            {
                best_solution = working_copy.graph.copyGraph();
                return true;
            }
            return false;
        }

        public Graph designing()
        {
            firstSolution();
            showSolution();
            bool changed;
            int counter = 0;
            temperature = 100;
            double alpha = 0.8;
            while (temperature > 1 && counter < 100)
            {
                changed = nextSolution();
                if (changed == true)
                {
                    temperature = temperature * alpha;
                    counter = 0;
                }
                else
                    counter++;
                Console.WriteLine(counter);
                showSolution();
            }

            showSolution();
            best_solution.show();
            return best_solution;
        }

    }
}
