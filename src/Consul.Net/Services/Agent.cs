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
        public IEnumerable<Check> AgentChecks()
        {
            return AgentChecksAsync().ExecuteSync();
        }

        public async Task<IEnumerable<Check>> AgentChecksAsync()
        {
            var uri = GetRequestUri("agent/checks");            
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request).ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync();
            var o = JObject.Parse(json);
            return o.Children()
                    .Select(_ => _.Value<Check>());
        }

        public IEnumerable<Service> AgentServices()
        {
            return AgentServicesAsync().ExecuteSync();
        }

        public async Task<IEnumerable<Service>> AgentServicesAsync()
        {
            var uri = GetRequestUri("agent/services");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request).ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync();
            var o = JObject.Parse(json);
            return o.Children()
                    .Select(_ => _.Value<Service>());
        }

        public IEnumerable<Member> AgentMembers(bool? wan)
        {
            return AgentMembersAsync(wan).ExecuteSync();
        }

        public async Task<IEnumerable<Member>> AgentMembersAsync(bool? wan)
        {
            var uri = GetRequestUri("agent/members");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request).ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync();
            var o = JObject.Parse(json);
            return o.Children()
                    .Select(_ => _.Value<Member>());
        }

        public Tuple<AgentConfig, Member> AgentSelf()
        {
            return AgentSelfAsync().ExecuteSync();
        }

        public async Task<Tuple<AgentConfig, Member>> AgentSelfAsync()
        {
            var uri = GetRequestUri("agent/self");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request).ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync();
            var o = JToken.Parse(json);
            return new Tuple<AgentConfig,Member>(o.Value<AgentConfig>("Config"), 
                                                 o.Value<Member>("Member"));
        }

        public bool AgentJoin(string address, int? wan = null)
        {
            return AgentJoinAsync(address, wan).ExecuteSync();
        }

        public async Task<bool> AgentJoinAsync(string address, int? wan = null)
        {
            var uri = GetRequestUri("agent/join/" + address);
            if (wan.HasValue) uri.AddQuery("wan", wan.Value);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request).ConfigureAwait(false);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool AgentForceLeave(string node)
        {
            return AgentForceLeaveAsync(node).ExecuteSync();
        }

        public async Task<bool> AgentForceLeaveAsync(string node)
        {
            var uri = GetRequestUri("agent/force-leave/" + node);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request).ConfigureAwait(false);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
