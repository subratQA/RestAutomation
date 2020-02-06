using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIAutomation.Modal
{
    public class RestResponse
    {
        private int statusCode;
        private string responseData;

        public RestResponse(int StatusCode, string ResposeData)
        {
            this.statusCode = StatusCode;
            this.responseData = ResposeData;
        }
        public int StatusCode
        {
            get { return statusCode; }
        }
        public string ResponseContent
        {
            get { return responseData; }
        }

        public override string ToString()
        {
            return String.Format("Status Code :{0} Response Data : {1}",statusCode,responseData);
        }
    }
}
