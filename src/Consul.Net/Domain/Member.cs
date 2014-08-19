using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Consul.Net
{
    public sealed class Member
    {
        [JsonProperty("ID")]
        public string ID
        {
            get;
            set;
        }

        [JsonProperty("Service")]
        public string Service
        {
            get;
            set;
        }

        [JsonProperty("Tags")]
        public string Tags
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
    }
}
