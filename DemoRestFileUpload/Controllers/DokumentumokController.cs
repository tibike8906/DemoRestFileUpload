using DemoRestFileUpload.Common;
using System.Net.Http;
using System.Text;
using System.Web.Http;

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
        
        public string Post([FromBody]string value)
        {
            FileControl fileControl = new FileControl();
            fileControl.Save(value);
            return fileControl.ErrorMessage;

        }

    }
}
