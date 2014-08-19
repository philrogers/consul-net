using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Consul.Net
{
    public sealed class AgentConfig
    {
        [JsonProperty("Bootstrap")]
        public bool Bootstrap
        {
            get;
            set;
        }
         
        [JsonProperty("Server")]
        public bool Server
        {
            get;
            set;
        } 

        [JsonProperty("Datacenter")]
        public string Datacenter
        {
            get;
            set;
        } 

        [JsonProperty("DataDir")]
        public string DataDir
        {
            get;
            set;
        }

        [JsonProperty("DNSRecursor")]
        public string DNSRecursor
        {
            get;
            set;
        }

        [JsonProperty("Domain")]
        public string Domain
        {
            get;
            set;
        }

        [JsonProperty("LogLevel")]
        public string LogLevel
        {
            get;
            set;
        }

        [JsonProperty("NodeName")]
        public string NodeName
        {
            get;
            set;
        }

        [JsonProperty("ClientAddr")]
        public string ClientAddr
        {
            get;
            set;
        }

        [JsonProperty("BindAddr")]
        public string BindAddr
        {
            get;
            set;
        }

        [JsonProperty("AdvertiseAddr")]
        public string AdvertiseAddr
        {
            get;
            set;
        }

        [JsonProperty("Ports")]
        public AgentConfigPorts Ports
        {
            get;
            set;
        }

        [JsonProperty("LeaveOnTerm")]
        public bool LeaveOnTerm
        {
            get;
            set;
        }

        [JsonProperty("SkipLeaveOnInt")]
        public bool SkipLeaveOnInt
        {
            get;
            set;
        }

        [JsonProperty("StatsiteAddr")]
        public string StatsiteAddr
        {
            get;
            set;
        }

        [JsonProperty("Protocol")]
        public int Protocol
        {
            get;
            set;
        }

        [JsonProperty("EnableDebug")]
        public bool EnableDebug
        {
            get;
            set;
        }

        [JsonProperty("VerifyIncoming")]
        public bool VerifyIncoming
        {
            get;
            set;
        }

        [JsonProperty("VerifyOutgoing")]
        public bool VerifyOutgoing
        {
            get;
            set;
        }

        [JsonProperty("CAFile")]
        public string CAFile
        {
            get;
            set;
        }

        [JsonProperty("CertFile")]
        public string CertFile
        {
            get;
            set;
        }

        [JsonProperty("KeyFile")]
        public string KeyFile
        {
            get;
            set;
        }

        [JsonProperty("StartJoin")]
        public List<string> StartJoin
        {
            get;
            set;
        }

        [JsonProperty("UiDir")]
        public string UiDir
        {
            get;
            set;
        }

        [JsonProperty("PidFile")]
        public string PidFile
        {
            get;
            set;
        }

        [JsonProperty("EnableSyslog")]
        public bool EnableSyslog
        {
            get;
            set;
        }

        [JsonProperty("RejoinAfterLeave")]
        public bool RejoinAfterLeave
        {
            get;
            set;
        }

    }


    public sealed class AgentConfigPorts
    {
        [JsonProperty("DNS")]
        public int DNS
        {
            get;
            set;
        }

        [JsonProperty("HTTP")]
        public int HTTP
        {
            get;
            set;
        }

        [JsonProperty("RPC")]
        public int RPC
        {
            get;
            set;
        }

        [JsonProperty("SerfLan")]
        public int SerfLan
        {
            get;
            set;
        }

        [JsonProperty("SerfWan")]
        public int SerfWan
        {
            get;
            set;
        }

        [JsonProperty("Server")]
        public int Server
        {
            get;
            set;
        } 
    }
}
