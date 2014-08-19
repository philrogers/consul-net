using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Consul.Net
{ 

    public sealed partial class ConsulClient
    {
        public bool AgentCheckRegister(Check check)
        {
            return AgentCheckRegisterAsync(check).ExecuteSync();
        }

        public async Task<bool> AgentCheckRegisterAsync(Check check)
        {
            var uri = GetRequestUri("agent/check/register");

            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonConvert.SerializeObject(check), Encoding.UTF8, "application/json");
            var response = await Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool AgentCheckDeregister(string id)
        {
            return AgentCheckDeregisterAsync(id).ExecuteSync();
        }

        public async Task<bool> AgentCheckDeregisterAsync(string id)
        {
            var uri = GetRequestUri("agent/check/deregister/" + id);
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            var response = await Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool AgentCheckPass(string id, string note = null)
        {
            return AgentCheckPassAsync(id, note).ExecuteSync();
        }

        public async Task<bool> AgentCheckPassAsync(string id, string note = null)
        {
            var uri = GetRequestUri("agent/check/pass/" + id);
            if (!string.IsNullOrEmpty(note)) uri.AddQuery("note", note);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool AgentCheckWarn(string id, string note = null)
        {
            return AgentCheckWarnAsync(id, note).ExecuteSync();
        }

        public async Task<bool> AgentCheckWarnAsync(string id, string note = null)
        {
            var uri = GetRequestUri("agent/check/warn/" + id);
            if (!string.IsNullOrEmpty(note)) uri.AddQuery("note", note);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool AgentCheckFail(string id, string note = null)
        {
            return AgentCheckFailAsync(id, note).ExecuteSync();
        }

        public async Task<bool> AgentCheckFailAsync(string id, string note = null)
        {
            var uri = GetRequestUri("agent/check/fail/" + id);
            if (!string.IsNullOrEmpty(note)) uri.AddQuery("note", note);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
