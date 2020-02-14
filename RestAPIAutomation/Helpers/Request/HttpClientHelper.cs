using RestAPIAutomation.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIAutomation.Helpers.Request
{
    public class HttpClientHelper
    {
        private static HttpClient httpClient;
        private static HttpRequestMessage httpRequestMessage;
        private static RestResponse restResponse;

        private static HttpClient AddHeadersandCreateHttpClient(Dictionary <string,string> httpHeadesrs)
        {
            HttpClient httpClient = new HttpClient();
            if (null!=httpHeadesrs)
            {
                foreach (string key in httpHeadesrs.Keys)
                {
                    httpClient.DefaultRequestHeaders.Add(key, httpHeadesrs[key]);
                }
            }

            return httpClient;
        }

        private static HttpRequestMessage CreateHttpRequestMessage(string requestUrl, HttpMethod httpMethod, HttpContent httpContent)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, requestUrl);
            if (!(httpMethod == HttpMethod.Get))
                httpRequestMessage.Content = httpContent;
            return httpRequestMessage;
        }
        private static RestResponse SendRequest(string requestUrl, HttpMethod httpMethod, HttpContent httpContent, Dictionary<string, string> httpHeadesr)
        {
            httpClient = AddHeadersandCreateHttpClient(httpHeadesr);
            httpRequestMessage = CreateHttpRequestMessage(requestUrl, httpMethod, httpContent);

            try
            {
                Task<HttpResponseMessage> httpResponseMessage = httpClient.SendAsync(httpRequestMessage);
                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode,
                    httpResponseMessage.Result.Content.ReadAsStringAsync().Result);
            }
            catch (Exception err)
            {
                restResponse = new RestResponse(500, err.Message);
            }
            finally
            {
                httpRequestMessage?.Dispose();
                httpClient?.Dispose();
            }
            return restResponse;
        }

        public static RestResponse PerformGetRequest(string requestUrl, Dictionary<string, string> httpHeadesr)
        {
           return SendRequest(requestUrl, HttpMethod.Get, null, httpHeadesr);
        }

    }
}
