using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileDataCommunication
{
    [Serializable, XmlRoot("fileresponse"), XmlType("fileresponse")]
    public class FileResponse
    {
        [XmlElement("message")]
        public string Message { get; set; }
        [XmlElement("file")]
        public List<FileDescription> File { get; set; }
    }
}
