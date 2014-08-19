using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Consul.Net
{
    public sealed class Node
    {
        [JsonProperty("Node")]
        public string NodeName
        {
            get;
            set;
        }

        [JsonProperty("Address")]
        public string Address
        {
            get;
            set;
        }
         
    }
}
