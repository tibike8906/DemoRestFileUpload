using System;
using System.IO;
using System.Xml.Serialization;
using DemoRestFileUpload.Common;
using FileDataCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoRestFileUpload.UnitTests
{
    [TestClass]
    public class FileControlTest
    {
        [TestMethod]
        public void GetXML()
        {
            FileControl fileControl = new FileControl();
            Assert.AreNotEqual(string.Empty, fileControl.GetXML());
        }
        [TestMethod]
        public void Save()
        {
            FileControl fileControl = new FileControl();
            FileDescription fileDescription = new FileDescription();
            fileDescription.FileName = "test.txt";
            fileDescription.FileData = fileControl.EncodeToBase64(DateTime.Now.ToString("yyyy-MM-dd hh:nn:ss"));
            string xml;

            XmlSerializer serializer = new XmlSerializer(typeof(FileDescription));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, fileDescription);
                xml = textWriter.ToString();
            }


            bool act = fileControl.Save(xml);
            Assert.AreNotEqual(false, act);
        }
    }
}
