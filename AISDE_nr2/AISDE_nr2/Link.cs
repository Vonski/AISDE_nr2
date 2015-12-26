using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_nr2
{
    class Link :IComparable
    {
        public int id;
        public int node_start;
        public int node_end;
        public double link_length;

        public Link()
        {
            id = 0;
            node_start = 0;
            node_end = 0;
            link_length = 0;
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
