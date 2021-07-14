using FileDataCommunication;
using Newtonsoft.Json;
using RestUI.Common;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace RestUI
{
    public class RestControl
    {
        private string apiURL;
        private WebProxy proxy;

        public RestControl()
        {
            apiURL = Settings.ApiURL;
            proxy = new WebProxy("own", 80);
            proxy.BypassProxyOnLocal = true;
        }

        public FileResponse GetData(string fileName = "")
        {
            string data;
            WebRequest request;
            if (string.IsNullOrWhiteSpace(fileName))
                request = WebRequest.Create(apiURL + "/dokumentumok");
            else
                request = WebRequest.Create(apiURL + "/dokumentumok/" + fileName);

            request.Proxy = proxy;
            request.ContentType = "text/xml";
            Stream objStream;

            objStream = request.GetResponse().GetResponseStream();
            XmlSerializer serializer = new XmlSerializer(typeof(FileResponse));            
            FileResponse response = (FileResponse)serializer.Deserialize(new StreamReader(objStream, Encoding.UTF8));
            return response;

        }

    }
}
