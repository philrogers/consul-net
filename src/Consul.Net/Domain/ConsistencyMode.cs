using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consul.Net
{
    public enum ConsistencyMode
    {
        Default,
        Consistent,
        Stale
    }
}
