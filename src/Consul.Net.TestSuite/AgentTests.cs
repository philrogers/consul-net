using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Consul.Net;

namespace Consul.Net.TestSuite
{
    [TestClass]
    public class AgentTests
    {

        [TestInitialize]
        public void Setup()
        {
            client = new ConsulClient();
        }

        private ConsulClient client;

        [TestMethod]
        public void GetAgentChecks()
        {
            
        }

        [TestMethod]
        public void GetAgentSelf()
        {
            var self = client.AgentSelf();
            var config = self.Item1;
            var member = self.Item2;

            Assert.IsNotNull(config);
            Assert.IsNotNull(member);

        }
         
    }
}
