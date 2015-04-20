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

        public KeyValue KeyValueGet(string key, string dc = null)
        {
            return KeyValueGetAsync(key, dc).ExecuteSync();
        }

        public async Task<KeyValue> KeyValueGetAsync(string key, string dc = null)
        {
            var uri = GetRequestUri("kv/" + key);            
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);

            var request = new HttpRequestMessage(HttpMethod.Get  , uri);  
         
            var response = await Execute(request).ConfigureAwait(false)  ;
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
 
            var json = await  response.Content.ReadAsStringAsync();
            // return a list of keyvalue objects
            var kvx = JsonConvert.DeserializeObject<List<KeyValue>>(json);
            KeyValue kv = null;
           // kv = new KeyValue();
            int ct;
            ct= kvx.Count; 
            if (ct >= 0) 
            {
                 kv = kvx[0];
            }
  
           
            IEnumerable<string> values;
            if (response.Headers.TryGetValues("X-Consul-Index", out values))
            {
                kv.ModifyIndex = Convert.ToInt32(values.First());
            }

            string retval = string.Empty;
            if (!string.IsNullOrEmpty(kv.Value))
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(kv.Value);
                retval = Encoding.UTF8.GetString(base64EncodedBytes);
            }

            kv.Value = retval;
            return kv;
        }

        public IEnumerable<KeyValue> KeyValueGetRecursive(string key, string dc = null)
        {
            return KeyValueGetRecursiveAsync(key, dc).ExecuteSync();
        }

        public async Task<IEnumerable<KeyValue>> KeyValueGetRecursiveAsync(string key, string dc = null)
        {
            var uri = GetRequestUri("kv/" + key);
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);
            
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request).ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<KeyValue>>(json);  
        }

        public bool KeyValuePut(KeyValue kv, int? cas = null, string aquire = null, string release = null, string dc = null)
        {
            return KeyValuePutAsync(kv, cas, aquire, release, dc).ExecuteSync();
        }

        public async Task<bool> KeyValuePutAsync(KeyValue kv, int? cas = null, string aquire = null, string release = null, string dc = null)
        {
            var uri = GetRequestUri("kv/" + kv.Key);
            if (!string.IsNullOrEmpty(dc)) uri.AddQuery("dc", dc);
            if (cas.HasValue) uri.AddQuery("cas", cas.Value);   

            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(kv.Value);

            var response = await Execute(request).ConfigureAwait(false);
            return (response.StatusCode == HttpStatusCode.OK);            
        }

        public bool KeyValueDelete(string key, bool? recurse = null)
        {
            return KeyValueDeleteAsync(key, recurse).ExecuteSync();
        }

        public async Task<bool> KeyValueDeleteAsync(string key, bool? recurse = null)
        {
            var uri = GetRequestUri("kv/" + key);
            if (recurse.HasValue && recurse.Value) uri.AddQuery("recurse", "");

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            var response = await Execute(request).ConfigureAwait(false);
            return (response.StatusCode == HttpStatusCode.OK);
        }

        public IEnumerable<string> KeyValueList(string prefix, string separator)
        {
            return KeyValueListAsync(prefix, separator).ExecuteSync();
        }


        public async Task<IEnumerable<string>> KeyValueListAsync(string prefix, string separator)
        // public async Task<IEnumerable<string>> KeyValueListAsync(string prefix, string separator)
    
        {
            var uri = GetRequestUri("kv/" + prefix);
            uri.AddQuery("separator", separator);

         var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await Execute(request).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new List<string> {};

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
    }
}
