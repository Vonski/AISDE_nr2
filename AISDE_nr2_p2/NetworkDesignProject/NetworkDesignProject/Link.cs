using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesignProject
{
    class Link : IComparable
    {
        public int id;
        public int node_start;
        public int node_end;
        public double link_length;      // Stosunek koszt/przepustowosc
        public double price;            // Cena modułu
        public double capacity;         // Przepustowosc modułu
        public int modules_counter;     // Aktualna liczba modułów
        public double capacity_in_use;     // Aktualnie używana przepustowość na danym łączu

        public Link()
        {
            id = 0;
            node_start = 0;
            node_end = 0;
            link_length = 0;
            price = 0;
            capacity = 0;
            capacity_in_use = 0;
        }

        public Link(int id, int start, int end, int length)
        {
            this.id = id;
            this.node_start = start;
            this.node_end = end;
            this.link_length = length;
        }

        public void setLength(int l)
        {
            this.link_length = l;
        }

        int IComparable.CompareTo(object obj)
        {
            Link l = (Link)obj;
            return this.link_length.CompareTo(l.link_length);
        }

    }
}
