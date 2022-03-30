using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassLibrary
{
    public interface IInit
    {
        static Random random = new();
        static Random RandomObj => random;
        object Init();
    }
}
