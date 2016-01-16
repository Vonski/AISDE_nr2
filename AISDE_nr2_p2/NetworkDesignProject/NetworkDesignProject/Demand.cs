using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesignProject
{
    class Demand : Path
    {
        public int id;
        public int node_start;
        public int node_end;

        public Demand()
        {
            id = 0;
            node_start = 0;
            node_end = 0;
        }

        public Demand(int id, int start, int end)
        {
            this.id = id;
            this.node_start = start;
            this.node_end = end;
        }
    }
}
