using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Consul.Net
{
    public sealed class Session
    {
        [JsonProperty("LockDelay")]
        public string LockDelay
        {
            get;
            set;
        }
         
        [JsonProperty("Checks")]
        public List<string> Checks
        {
            get;
            set;
        }

        [JsonProperty("Node")]
        public string Node
        {
            get;
            set;
        }

        [JsonProperty("ID")]
        public string ID
        {
            get;
            set;
        }

        [JsonProperty("CreateIndex")]
        public int CreateIndex
        {
            get;
            set;
        }
         
    }
     
}
