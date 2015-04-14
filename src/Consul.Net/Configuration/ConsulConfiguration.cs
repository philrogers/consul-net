using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consul.Net
{
    public sealed partial class ConsulConfiguration
    {

        // dc
        // port
        // address
        // 

        private static ConsulConfiguration instance;
        public static ConsulConfiguration Instance
        {
            get { return instance; }
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
            return instance;
        }
        private void GetFromFile()
        {

           var oconfig = ConsulConfigurationHandler.Get();
            if (oconfig == null)
                throw new Exception("Consul section was not found in app/web.config");


            instance.DataCenter = oconfig.DataCenter;
            instance.Agent = oconfig.Agent;            
        }

        #region // Properties //
        public string DataCenter { get; private set; }
        public string Agent { get; private set; }
        #endregion

    }

    internal class ConsulConfigurationHandler : ConfigurationSection
    {
        public static ConsulConfigurationHandler Get()
        {
            //string phil;
            //  phil =  ConfigurationManager.GetSection() 
            try
            {
                ConfigurationManager.GetSection("consul/settings");
            }
            catch (Exception ex)
            {
            }

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
