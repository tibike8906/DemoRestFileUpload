﻿using FileDataCommunication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DemoRestFileUpload.Common
{
    public class FileControl
    {
        private string errorMessage;
        public string ErrorMessage { get { return errorMessage; } }

        private string _path;
        public FileControl()
        {
            _path = APISetting.FileReadPath;
        }

       
        private FileDescription GetFile(string fullPath)
        {                            
         
            FileDescription fileDescription = new FileDescription();
            fileDescription.FileName = Path.GetFileName(fullPath);
            fileDescription.FileData = EncodeToBase64(File.ReadAllText(fullPath));

            return fileDescription;
        }

        private List<FileDescription> GetFiles(string fileName = "")
        {
            List<FileDescription> list = new List<FileDescription>();
            string[] files = Directory.GetFiles(_path);
            if (files.Count() < 1)
            {
                errorMessage = "Nem található file a szerveren!";
                return null;
            }
            if (!string.IsNullOrWhiteSpace(fileName))
            {

                if (!File.Exists(_path + fileName))
                {
                    errorMessage = "A megadott file nem található a szerveren!";
                    return null;
                }
                else
                {
                    list.Add(GetFile(files.Where(p=> Path.GetFileName(p).ToLower() == fileName.ToLower()).FirstOrDefault()));
                    return list;
                }
            }
            foreach (var filePath in files)         
                list.Add(GetFile(filePath));

           
            return list;
        }

        public string GetXML(string fileName = "")
        {
            List<FileDescription> files = GetFiles(fileName);
            FileResponse response = new FileResponse();
            response.File = files;
            response.Message = errorMessage;
            XmlSerializer serializer = new XmlSerializer(typeof(FileResponse));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, response);
                return textWriter.ToString();
            }            
        }
      
        public void Save(string json)
        {
            
            FileDescription fileDescription = JsonConvert.DeserializeObject<FileDescription>(json);

            if (File.Exists(_path + fileDescription.FileName))
            {
                errorMessage = "A megadott fájlnév már létezik a szerveren!";
                return;
            }

            File.WriteAllText(_path + fileDescription.FileName, DecodeBase64(fileDescription.FileData));
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