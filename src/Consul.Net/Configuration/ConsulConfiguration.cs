using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consul.Net
{
     public  partial class ConsulConfiguration
 //   public sealed partial class ConsulConfiguration
    {

        // dc
        // port
        // address
        // 
        private static  ConsulConfiguration instance;
        public  static ConsulConfiguration Instance
        {
            get { 
                // if we have come here with no arguments default the agent and datacentre back to what they were set to on startup
                // otherwise this will hold the last value set which may not be correct if you are flipping between data centres
                instance.SetBackToRuntimeDefaults(); 
                return instance;
            
                }
        }

        static ConsulConfiguration()
        {
            instance = new ConsulConfiguration();
            instance.GetFromFile();
        }
        public ConsulConfiguration ChangeDefaultConnection(string strhostandport, string strdatacentre)
        {
            if (strhostandport != "")
            {
               instance.Agent = strhostandport;
            }
            if (strdatacentre != "")
            {
                instance.DataCenter  = strdatacentre;
            }
            SetRuntimeDefaults(); // will only set if first time here
            return instance;
        }
        private void GetFromFile()
        {

           var oconfig = ConsulConfigurationHandler.Get();
            if (oconfig == null)
                throw new Exception("Consul section was not found in app/web.config");
            
            instance.DataCenter = oconfig.DataCenter;
            instance.Agent = oconfig.Agent;
            SetRuntimeDefaults();
        }

        // Allow Startup values to be remembered in case of later datacentre change
        // This feature allows the consul datacentre to change to a different datacentre for writing
        // if no datacentre is passed into the client will revert back to defaults.
        // instead of the last value set.
   
         private void SetRuntimeDefaults()  
         {
            if (instance.DefaultDataCenter == null)
            {
                instance.DefaultDataCenter = instance.DataCenter;
            }
            if (instance.DefaultAgent == null)
            {
                instance.DefaultAgent = instance.Agent;
            }
         }

         private void SetBackToRuntimeDefaults()
         {
             if (instance.DefaultDataCenter != null)
             {
                instance.DataCenter = instance.DefaultDataCenter;
             }
             if (instance.DefaultAgent != null)
             {
                 instance.Agent = instance.DefaultAgent;
             }
         }

        #region // Properties //
        public string DataCenter { get; private set; }
        public string Agent { get; private set; }
        public string DefaultDataCenter { get; private set; }
        public string DefaultAgent { get; private set; }

        #endregion

    }

    internal class ConsulConfigurationHandler : ConfigurationSection
    {
        public static ConsulConfigurationHandler Get()
        {
             ConfigurationManager.GetSection("consul/settings");
            
            return (ConsulConfigurationHandler)ConfigurationManager.GetSection("consul/settings");

        }

        [ConfigurationProperty("dataCenter", IsRequired = true)]
        public string DataCenter
        {
            get
            {
                return (string)this["dataCenter"];
            }
            set
            {
                this["dataCenter"] = value;
            }
        }

        [ConfigurationProperty("agent", IsRequired = false)]
        public string Agent
        {
            get
            {
              
                return (string)this["agent"];
            }
            set
            {
                this["agent"] = value;
            }
        }
         
    }
}
