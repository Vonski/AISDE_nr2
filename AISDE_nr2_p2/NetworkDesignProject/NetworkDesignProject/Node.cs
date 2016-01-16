using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesignProject
{
    class Node
    {
        public int id;
        public double label;
        public bool enqueued;

        public Node()
        {
            this.id = 0;
        }

        public Node(int id)
        {
            this.id = id;
        }
    }
}
