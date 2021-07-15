using FileDataCommunication;
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
            try
            {                
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
            catch (Exception e)
            {

                CommonVoids.ErrorMsg(e.Message);
                return null;
            }
           

        }

        public string SendFile(FileDescription fileDescription)
        {
            try
            {
                string xml;
                XmlSerializer serializer = new XmlSerializer(typeof(FileDescription));
                using (StringWriter textWriter = new StringWriter())
                {
                    serializer.Serialize(textWriter, fileDescription);
                    xml = "=" + textWriter.ToString();
                }
                byte[] bytes = Encoding.UTF8.GetBytes(xml);




                WebRequest request;
                request = WebRequest.Create((apiURL + "/dokumentumok"));

                request.ContentLength = bytes.Length;
                request.Method = "POST";
                request.Proxy = proxy;
                request.ContentType = "application/x-www-form-urlencoded";

                Stream objStream = request.GetRequestStream();
                objStream.Write(bytes, 0, bytes.Length);
                request.ContentType = "text/xml";
                objStream = request.GetResponse().GetResponseStream();
                XmlSerializer responseSerializer = new XmlSerializer(typeof(string));
                string response = (string)responseSerializer.Deserialize(new StreamReader(objStream, Encoding.UTF8));
                return response;
            }
            catch (Exception e)
            {
                CommonVoids.ErrorMsg(e.Message);
                return null;
            }
          
     
        }

    }
}
