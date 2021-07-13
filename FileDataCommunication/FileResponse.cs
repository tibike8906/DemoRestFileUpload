using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDataCommunication
{
    public class FileResponse
    {
        public string Message { get; set; }
        public List<FileDescription> File { get; set; }
    }
}
