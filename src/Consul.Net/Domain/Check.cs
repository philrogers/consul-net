using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Consul.Net
{
    public sealed class Check
    {
        [JsonProperty("Node")]
        public string Node
        {
            get;
            set;
        }

        [JsonProperty("CheckID")]
        public string CheckID
        {
            get;
            set;
        }

        [JsonProperty("Name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("Status")]
        public string Status
        {
            get;
            set;
        }

        [JsonProperty("Notes")]
        public string Notes
        {
            get;
            set;
        }

        [JsonProperty("Output")]
        public string Output
        {
            get;
            set;
        }

        [JsonProperty("ServiceID")]
        public string ServiceID
        {
            get;
            set;
        }

        [JsonProperty("ServiceName")]
        public string ServiceName
        {
            get;
            set;
        } 
    }
}
