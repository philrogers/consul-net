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

        public bool ServiceRegister(Service service)
        {
            return ServiceRegisterAsync(service).ExecuteSync();
        }

        public async Task<bool> ServiceRegisterAsync(Service service)
        {
            var uri = GetRequestUri("agent/service/register");            

            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonConvert.SerializeObject(service), Encoding.UTF8, "application/json");
            var response = await Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool ServiceDeregister(string id)
        {
            return ServiceDeregisterAsync(id).ExecuteSync();
        }

        public async Task<bool> ServiceDeregisterAsync(string id)
        {
            var uri = GetRequestUri("agent/service/deregister/" + id);
            var request = new HttpRequestMessage(HttpMethod.Put, uri); 
            var response = await Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
         
    }
}
