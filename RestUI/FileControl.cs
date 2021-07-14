using FileDataCommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestUI
{
    public class FileControl
    {
        FileResponse _response;
        public FileControl(FileResponse response)
        {
            _response = response;
        }

        public void SaveFiles()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Mappa választás";
            fbd.ShowNewFolderButton = true;                        
            fbd.ShowDialog();
            string path = fbd.SelectedPath;

            if (string.IsNullOrWhiteSpace(path)) return;

            foreach (var file in _response.File)
            {
                using (FileStream fs = File.Create(path + "\\" +  file.FileName))
                {
                    byte[] data = new UTF8Encoding(true).GetBytes(DecodeBase64(file.FileData));
                    fs.Write(data, 0, data.Length);
                }
            }
           
        }

        private string EncodeToBase64(string json)
        {
            Byte[] bytes = Encoding.UTF8.GetBytes(json);
            return Convert.ToBase64String(bytes);
        }

        private string DecodeBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
