using DemoRestFileUpload.Common;
using System.Web.Http;

namespace DemoRestFileUpload.Controllers
{
    public class DokumentumokController : ApiController
    {        
        public string Get()
        {
            FileControl fileControl = new FileControl();          
            return fileControl.GetJson();
            
        }
        
        public string Get(string fileName)
        {
            FileControl fileControl = new FileControl();
            return fileControl.GetJson(fileName);
        }
        
        public string Post([FromBody]string value)
        {
            FileControl fileControl = new FileControl();
            fileControl.Save(value);
            return fileControl.ErrorMessage;

        }

    }
}
