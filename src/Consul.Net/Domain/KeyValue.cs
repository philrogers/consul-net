using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Consul.Net
{
    public sealed class KeyValue
    {
        [JsonProperty("CreateIndex")]
        public int CreateIndex
        {
            get;
            set;
        }

        [JsonProperty("ModifyIndex")]
        public int ModifyIndex
        {
            get;
            set;
        }

        [JsonProperty("LockIndex")]
        public int LockIndex
        {
            get;
            set;
        }

        [JsonProperty("Key")]
        public string Key
        {
            get;
            set;
        }

        [JsonProperty("Flags")]
        public uint Flags
        {
            get;
            set;
        }

        [JsonProperty("Value")]
        public string Value
        {
            get;
            set;
        }

        [JsonProperty("Session")]
        public string Session
        {
            get;
            set;
        }

    }
}
