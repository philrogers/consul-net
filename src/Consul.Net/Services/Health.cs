using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Consul.Net
{ 

    public sealed partial class ConsulClient
    {

        public IEnumerable<Check> HealthNode(string node)
        {
            return HealthNodeAsync(node).ExecuteSync();
        }

        public async Task<IEnumerable<Check>> HealthNodeAsync(string node)
        {
            var uri = GetRequestUri("health/node/" + node);
            
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Check>>(json);  
        }

        public IEnumerable<Check> HealthChecksService(string service, string dc = null)
        {
            return HealthChecksServiceAsync(service, dc).ExecuteSync();
        }

        public async Task<IEnumerable<Check>> HealthChecksServiceAsync(string service, string dc = null)
        {
            var uri = GetRequestUri("health/checks/service/" + service);
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Check>>(json);  
        }

        public IEnumerable<Tuple<Node, Service, IEnumerable<Check>>> HealthService(string service, string tag = null, string dc = null, bool? passing = null)
        {
            return HealthServiceAsync(service, tag, dc, passing).ExecuteSync();
        }

        public async Task<IEnumerable<Tuple<Node,Service,IEnumerable<Check>>>> HealthServiceAsync(string service, string tag = null, string dc = null, bool? passing = null)
        {
            var uri = GetRequestUri("health/service/" + service);
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);
            if (!string.IsNullOrEmpty(tag)) uri.AddQuery("tag", tag);
            if (passing.HasValue) uri.AddQuery("tag", tag);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);

            var json = await response.Content.ReadAsStringAsync();
            var o = JObject.Parse(json);
            return o.Children()
                    .Select(_ => new Tuple<Node, Service, IEnumerable<Check>>(o.Value<Node>("Node"), 
                                                                              o.Value<Service>("Service"),
                                                                              o.Value<List<Check>>("Checks")));

        }

        public IEnumerable<Check> HealthState(string state, string dc = null)
        {
            return HealthStateAsync(state, dc).ExecuteSync();
        }

        public async Task<IEnumerable<Check>> HealthStateAsync(string state, string dc = null)
        {
            var uri = GetRequestUri("health/state/" + state);
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Check>>(json);  

        }
    }
}
