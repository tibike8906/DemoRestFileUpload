using DemoRestFileUpload.Common;
using FileDataCommunication;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Serialization;

namespace DemoRestFileUpload.Controllers
{
    public class DokumentumokController : ApiController
    {
      

        public HttpResponseMessage Get()
        {
            FileControl fileControl = new FileControl();


            return new HttpResponseMessage()
            {
                Content = new StringContent(fileControl.GetXML(), Encoding.UTF8, "application/xml")
            };
        }

       
        public HttpResponseMessage Get(string fileName)
        {
            FileControl fileControl = new FileControl();
            
            
            return new HttpResponseMessage()
            {
                Content = new StringContent(fileControl.GetXML(fileName), Encoding.UTF8, "application/xml")
            };
        }

        public HttpResponseMessage Post([FromBody]string value)
        {            
            FileControl fileControl = new FileControl();
            bool result = fileControl.Save(value);
            string response;
            XmlSerializer serializer = new XmlSerializer(typeof(string));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, fileControl.ErrorMessage);
                response =  textWriter.ToString();
            }
            return new HttpResponseMessage()
            {
                Content = new StringContent(response, Encoding.UTF8, "application/xml")
            };

        }

    }
}
