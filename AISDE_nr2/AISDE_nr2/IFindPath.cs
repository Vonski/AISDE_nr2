using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_nr2
{
    interface IFindPath
    {
        void findAB(int A, int B);
        void findOneToAll(int A);
        void findAll();
    }
}
