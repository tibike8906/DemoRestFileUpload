using FileDataCommunication;
using Newtonsoft.Json;
using RestUI.Common;
using System.IO;
using System.Net;

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
                request = WebRequest.Create(apiURL + "/dokumentumok/"+ fileName);

            request.Proxy = proxy;
            Stream objStream;
            objStream = request.GetResponse().GetResponseStream();

            using (StreamReader reader = new StreamReader(objStream))            
                data = reader.ReadToEnd();          
            FileResponse response = JsonConvert.DeserializeObject<FileResponse>(data);
            return response;
        }

    }
}
