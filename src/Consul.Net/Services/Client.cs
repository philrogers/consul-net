using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Consul.Net
{
    public sealed partial class ConsulClient
    {
        // constructor

        #region // Constructor //
        public ConsulClient() : this(ConsulConfiguration.Instance)
        {            
        }

        public ConsulClient(ConsulConfiguration config)
        {
            _config = config;

            HttpClientHandler handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            handler.AutomaticDecompression = DecompressionMethods.GZip;
            _client = new HttpClient(handler);
        }

        
        #endregion

        #region // Properties //
        private readonly ConsulConfiguration _config;
        private readonly HttpClient _client;
        #endregion

        private Uri GetRequestUri(string suffix)
        {
            return new Uri("http://" + _config.Agent + "/v1/" + suffix);
        }

        private Task<HttpResponseMessage> Execute(HttpRequestMessage request)
        {            
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            return _client.SendAsync(request);                         
        }
         
    }
}
