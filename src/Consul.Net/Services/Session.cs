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
        // TimeSpan lockDelay, string name, string node, IEnumerable<string> checks

        public string SessionCreate(Session session)
        {
            return SessionCreateAsync(session).ExecuteSync();
        }

        public async Task<string> SessionCreateAsync(Session session)
        {
            var uri = GetRequestUri("session/create");

            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, "application/json");
            var response = await Execute(request);
            var json = await response.Content.ReadAsStringAsync();
            return JObject.Parse(json)["id"].Value<string>();
        }

        public bool SessionDestroy(string session, string dc = null)
        {
            return SessionDestroyAsync(session, dc).ExecuteSync();
        }

        public async Task<bool> SessionDestroyAsync(string session, string dc = null)
        {
            var uri = GetRequestUri("session/destroy/" + session);
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);

            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            var response = await Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public IEnumerable<Session> SessionInfo(string session, string dc = null)
        {
            return SessionInfoAsync(session, dc).ExecuteSync();
        }

        public async Task<IEnumerable<Session>> SessionInfoAsync(string session, string dc = null)
        {
            var uri = GetRequestUri("session/info/" + session);
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Session>>(json);  
        }

        public IEnumerable<Session> SessionNode(string session, string dc = null)
        {
            return SessionNodeAsync(session, dc).ExecuteSync();
        }

        public async Task<IEnumerable<Session>> SessionNodeAsync(string node, string dc = null)
        {
            var uri = GetRequestUri("session/node" + node);
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Session>>(json);  
        }

        public IEnumerable<Session> SessionList(string dc = null)
        {
            return SessionListAsync(dc).ExecuteSync();
        }

        public async Task<IEnumerable<Session>> SessionListAsync(string dc = null)
        {
            var uri = GetRequestUri("session/list");
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Session>>(json);  
        }

    }
}
