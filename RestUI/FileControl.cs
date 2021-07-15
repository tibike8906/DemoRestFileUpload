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

        public FileDescription OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Válasszon file-t!";
            ofd.ShowDialog();
            string fileName = ofd.FileName;
            if (string.IsNullOrWhiteSpace(fileName)) return null;

            FileDescription fileDescription = new FileDescription();
            fileDescription.FileName = Path.GetFileName(fileName);
            fileDescription.FileData = EncodeToBase64(File.ReadAllText(fileName));

            return fileDescription;
        }
        public void SaveFiles(FileResponse response)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Mappa választás";
            fbd.ShowNewFolderButton = true;                        
            fbd.ShowDialog();
            string path = fbd.SelectedPath;

            if (string.IsNullOrWhiteSpace(path)) return;

            foreach (var file in response.File)
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
            Byte[] bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
