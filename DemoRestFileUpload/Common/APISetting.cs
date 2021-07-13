using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DemoRestFileUpload.Common
{
    public static class APISetting
    {        
        public static string FileReadPath
        {
            get
            {
                XDocument xdoc = XDocument.Load(@"Settings.xml");                                             
                return xdoc.Element("Settings").Element("FileReadWritePath").Value;
            }          
        }

    }
}