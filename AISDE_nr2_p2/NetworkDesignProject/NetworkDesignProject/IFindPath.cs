using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesignProject
{
    interface IFindPath
    {
        Path findAB(int A, int B);
        Path[] findOneToAll(int A);
        Path[][] findAll();
    }
}
