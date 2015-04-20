using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;


namespace Consul.Net
{
    
    public sealed partial class ConsulClient
    {
        public IEnumerable<string> Dclist()
        {
            return DatacentreListAsync().ExecuteSync();
        }


        public async Task<IEnumerable<string>> DatacentreListAsync()
        // public async Task<IEnumerable<string>> KeyValueListAsync(string prefix, string separator)
        {
            var uri = GetRequestUri("catalog/datacenters");
         
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request).ConfigureAwait(false);
            //    var response = await Execute(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new List<string> { };

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
    }
}
