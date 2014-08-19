using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Consul.Net
{
    public sealed class Service
    {
        [JsonProperty("ID")]
        public string ID
        {
            get;
            set;
        }
         
        [JsonProperty("Tags")]
        public List<string> Tags
        {
            get;
            set;
        }

        [JsonProperty("Port")]
        public int Port
        {
            get;
            set;
        }

        [JsonProperty("Check", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceCheck Check
        {
            get;
            set;
        }
    }

    public sealed class ServiceCheck
    {

        [JsonProperty("Script")]
        public string Script
        {
            get;
            set;
        }

        [JsonProperty("Interval")]
        public string Interval
        {
            get;
            set;
        }

        [JsonProperty("TTL")]
        public string TTL
        {
            get;
            set;
        }
    }
}
