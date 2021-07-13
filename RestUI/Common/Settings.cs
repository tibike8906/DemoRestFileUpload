using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RestUI.Common
{
    public static class Settings
    {        
        public static string ApiURL
        {
            get
            {
                XDocument xdoc = XDocument.Load(@"Settings.xml");                                             
                return xdoc.Element("Settings").Element("apiurl").Value;
            }          
        }

    }
}