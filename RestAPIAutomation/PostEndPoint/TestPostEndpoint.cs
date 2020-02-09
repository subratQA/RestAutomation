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
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestAPIAutomation.PostEndPoint
{
    [TestClass]
    public class TestPostEndpoint
    {
        private string POSTURL = "http://localhost:8080/laptop-bag/webapi/api/add";
        private string GETURL = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private RestResponse restResponse;
        private RestResponse restResponseForGet;
        private string JsonMediaType = "application/json";
        private string XmlMediaType = "application/xml";
        private Random random = new Random();


        [TestMethod]
        public void PostEndpoint()
        {

            int id = random.Next(1000);
            string jSonData = "{" +
                               "\"BrandName\": \"HP\"," +
                               "\"Features\": {" +
                               "\"Feature\": [" +
                               "\"4th Generation Intel® Core™ i2-8300H\"," +
                               "\"Windows 4 Home 64-bit English\"," +
                               "\"NVIDIA® GeForce® GTX 1260 Ti 1GB GDDR6\"," +
                               "\"1GB, 4GB, DDR4, 2000MHz\"" +
                               "]" +
                               "}," +
                               "\"Id\": " + id + "," +
                               "\"LaptopName\": \"HP M17\"" +
                               "}";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(jSonData, Encoding.UTF8, JsonMediaType);
                Task<HttpResponseMessage> postResponse = httpClient.PostAsync(POSTURL, httpContent);
                HttpStatusCode statusCode = postResponse.Result.StatusCode;
                HttpContent responseContent = postResponse.Result.Content;
                string responseData = responseContent.ReadAsStringAsync().Result;
                restResponse = new RestResponse((int)statusCode, responseData);
                Assert.AreEqual(200, restResponse.StatusCode);
                Assert.IsNotNull(restResponse.ResponseContent, "Response data is null/empty");
            }
        }

        [TestMethod]
        public void PostEndpointWithGetWithID()
        {
            int id = random.Next(1000);
            string jSonData = "{" +
                               "\"BrandName\": \"Lenovo\"," +
                               "\"Features\": {" +
                               "\"Feature\": [" +
                               "\"4th Generation Intel® Core™ i2-8300H\"," +
                               "\"Windows 4 Home 64-bit English\"," +
                               "\"NVIDIA® GeForce® GTX 1260 Ti 1GB GDDR6\"," +
                               "\"1GB, 4GB, DDR4, 2000MHz\"" +
                               "]" +
                               "}," +
                               "\"Id\": " + id + "," +
                               "\"LaptopName\": \"Len P07\"" +
                               "}";
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpContent httpContent = new StringContent(jSonData, Encoding.UTF8, JsonMediaType);
                Task<HttpResponseMessage> postResponse = httpClient.PostAsync(POSTURL, httpContent);
                HttpStatusCode statusCode = postResponse.Result.StatusCode;
                HttpContent responseContent = postResponse.Result.Content;
                string responseData = responseContent.ReadAsStringAsync().Result;
                restResponse = new RestResponse((int)statusCode, responseData);
                //Assert.AreEqual(200, restResponse.StatusCode);
                //Assert.IsNotNull(restResponse.ResponseContent, "Response data is null/empty");

                Task<HttpResponseMessage> getResponse = httpClient.GetAsync(GETURL+id);
                restResponseForGet = new RestResponse((int)getResponse.Result.StatusCode,
                    getResponse.Result.Content.ReadAsStringAsync().Result);
                JsonRootObject jsonRootObject = JsonConvert.DeserializeObject<JsonRootObject>(restResponseForGet.ResponseContent);
                Assert.AreEqual(id, jsonRootObject.Id);
                Assert.AreEqual("Lenovo", jsonRootObject.BrandName);
            }
        }

        [TestMethod]
        public void PostEndPointWithXmlData()
        {
            int id = random.Next(1000);
            string xmlData = "<Laptop>" +
                                "<BrandName>Lenovo</BrandName>"+
                                "<Features>" +
                                "<Feature>3th Generation Intel® Core™ i2 - 8300H </Feature>" +
                                "<Feature> Windows 4 Home 64 - bit English </Feature>" +
                                "<Feature> NVIDIA® GeForce® GTX 1260 Ti 1GB GDDR6</ Feature>" +
                                "< Feature  1GB, 4GB, DDR4, 2000MHz </Feature>" +
                                "</Features>" +
                                "< Id>" + id + "</Id>" +
                                "<LaptopName> Len P89</LaptopName>" +
                                "</Laptop> ";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", XmlMediaType);
                HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, XmlMediaType);
                Task<HttpResponseMessage> postResponse = httpClient.PostAsync(POSTURL, httpContent);
                //HttpStatusCode statusCode = postResponse.Result.StatusCode;
                //HttpContent responseContent = postResponse.Result.Content;
                //string responseData = responseContent.ReadAsStringAsync().Result;
                //restResponse = new RestResponse((int)statusCode, responseData);
                //Assert.AreEqual(200, restResponse.StatusCode);
               // Assert.IsNotNull(restResponse.ResponseContent, "Response data is null/empty");

                postResponse = httpClient.GetAsync(GETURL + id);
                if (!postResponse.Result.IsSuccessStatusCode)
                {
                    Assert.Fail();
                }             
                restResponseForGet = new RestResponse((int)postResponse.Result.StatusCode,
                    postResponse.Result.Content.ReadAsStringAsync().Result);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Laptop));
                TextReader textReader = new StringReader(restResponse.ResponseContent);
                Laptop xmlObject = (Laptop)xmlSerializer.Deserialize(textReader);
                Assert.IsTrue(xmlObject.Features.Feature.Contains("3th Generation Intel® Core™ i2 - 8300H"), "Item not found");
            }

        }
    }
}
