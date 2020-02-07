using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestAPIAutomation.Modal;
using RestAPIAutomation.Modal.JsonModal;
using RestAPIAutomation.Model.XmlModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestAPIAutomation.GetEndPoint
{
    [TestClass]
    public class TestEndpoint
    {
        private string URL = "http://localhost:8080/laptop-bag/webapi/api/";
        [TestMethod]
        public void TestAllEndpoint()
        {
            HttpClient httpClient = new HttpClient();
            Uri GetUri = new Uri(URL);
            httpClient.GetAsync(GetUri);
            httpClient.Dispose();
        }
        [TestMethod]
        public void CaptureHttpResponse()
        {
            HttpClient httpClient = new HttpClient();
            Uri GetUri = new Uri(URL);
            Task<HttpResponseMessage> httpResponse =  httpClient.GetAsync(GetUri);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            //Console.WriteLine(httpResponseMessage.ToString());

            //Read Status Code
            HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status Code is=> " + httpStatusCode);
            Console.WriteLine("Status Code is=> " + (int)httpStatusCode);

            //Read Content
            HttpContent httpContent = httpResponseMessage.Content;
            Console.WriteLine("Total Content=> "+httpContent);
            Task<string> responseData = httpContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine("Total data=> "+data);
        }
        [TestMethod]
        public void TestEndPointInJson()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeader = httpClient.DefaultRequestHeaders;
            requestHeader.Add("Accept", "application/json");
            //Uri GetUri = new Uri(URL);
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(URL);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            //Read Status Code
            HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status Code is=> " + httpStatusCode);
            Console.WriteLine("Status Code is=> " + (int)httpStatusCode);

            //Read Content
            HttpContent httpContent = httpResponseMessage.Content;
            Console.WriteLine("Total Content=> " + httpContent);
            Task<string> responseData = httpContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine("Total data=> " + data);
        }
        [TestMethod]
        public void TestEndPointInJsonFormatUsingAcceptHeader()
        {
            MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue = new MediaTypeWithQualityHeaderValue("application/xml");
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeader = httpClient.DefaultRequestHeaders;
            //requestHeader.Add("Accept", "application/xml");
            requestHeader.Accept.Add(mediaTypeWithQualityHeaderValue);
            //Uri GetUri = new Uri(URL);
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(URL);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            //Read Status Code
            HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status Code is=> " + httpStatusCode);
            Console.WriteLine("Status Code is=> " + (int)httpStatusCode);

            //Read Content
            HttpContent httpContent = httpResponseMessage.Content;
            Console.WriteLine("Total Content=> " + httpContent);
            Task<string> responseData = httpContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine("Total data=> " + data);
        }
        [TestMethod]
        public void TestEndPointInXmlFormat()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeader = httpClient.DefaultRequestHeaders;
            requestHeader.Add("Accept", "application/xml");
            //Uri GetUri = new Uri(URL);
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(URL);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            //Read Status Code
            HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status Code is=> " + httpStatusCode);
            Console.WriteLine("Status Code is=> " + (int)httpStatusCode);

            //Read Content
            HttpContent httpContent = httpResponseMessage.Content;
            Console.WriteLine("Total Content=> " + httpContent);
            Task<string> responseData = httpContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine("Total data=> " + data);
        }
        [TestMethod]
        public void TestEndPointUsingSendAsync()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.RequestUri = new Uri(URL);
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.Headers.Add("Accept", "application/json");

            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            //Read Status Code
            HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status Code is=> " + httpStatusCode);
            Console.WriteLine("Status Code is=> " + (int)httpStatusCode);

            //Read Content
            HttpContent httpContent = httpResponseMessage.Content;
            Console.WriteLine("Total Content=> " + httpContent);
            Task<string> responseData = httpContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine("Total data=> " + data);
        }

        [TestMethod]
        public void TestDeselizationOfJsonObject()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.RequestUri = new Uri(URL);
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.Headers.Add("Accept", "application/json");

            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            //Read Status Code
            HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
            //Console.WriteLine("Status Code is=> " + httpStatusCode);
            //Console.WriteLine("Status Code is=> " + (int)httpStatusCode);

            //Read Content
            HttpContent httpContent = httpResponseMessage.Content;
            //Console.WriteLine("Total Content=> " + httpContent);
            Task<string> responseData = httpContent.ReadAsStringAsync();
            string data = responseData.Result;
            //Console.WriteLine("Total data=> " + data);

            RestResponse restResponse = new RestResponse((int)httpStatusCode, responseData.Result);
            List<JsonRootObject> jsonRootObjects = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
            Console.WriteLine(jsonRootObjects[0].ToString());
        }   
        [TestMethod]
        public void TestDesirlizationOfXmlData()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.RequestUri = new Uri(URL);
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.Headers.Add("Accept", "application/xml");

            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            //Read Status Code
            HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
            //Console.WriteLine("Status Code is=> " + httpStatusCode);
            //Console.WriteLine("Status Code is=> " + (int)httpStatusCode);

            //Read Content
            HttpContent httpContent = httpResponseMessage.Content;
            //Console.WriteLine("Total Content=> " + httpContent);
            Task<string> responseData = httpContent.ReadAsStringAsync();
            string data = responseData.Result;
            //Console.WriteLine("Total data=> " + data);

            RestResponse restResponse = new RestResponse((int)httpStatusCode, responseData.Result);
            //List<JsonRootObject> jsonRootObjects = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
            //Console.WriteLine(jsonRootObjects[0].ToString());

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LaptopDetails));
            TextReader textReader = new StringReader(restResponse.ResponseContent);
            LaptopDetails xmlData = (LaptopDetails)xmlSerializer.Deserialize(textReader);
            Console.WriteLine(xmlData.ToString());
         }
    }
}
