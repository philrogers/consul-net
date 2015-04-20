using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Consul.Net;

namespace Consul.Net.TestSuite
{
    [TestClass]
    public class KeyValueTests
    {

        [TestInitialize]
        public void Setup()
        {
          //  ConsulClient client;
            client = new ConsulClient();
        }

        private ConsulClient client;

        [TestMethod]
        public void KeyValuePut()
        {
            var kv = new KeyValue();
            kv.Key = "test";
            kv.Value = "something";

            Assert.IsTrue(client.KeyValuePut(kv));
        }

        [TestMethod]
        public void KeyValueGet()
        {
            var test = client.KeyValueGet("test");
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void KeyValueGetRecursive()
        {
            var list = client.KeyValueGetRecursive("test?seperator=/");

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count() >= 1);
            Assert.IsTrue(list.Count() == list.Where(_ => _.Key.StartsWith("test")).Count());
        }

        [TestMethod]
        public void KeyValueList()
        {
            var list = client.KeyValueList("test?keys", "/");
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count() >= 0);
        }
    }
}
