using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
namespace CleanDepressionDataset
{
    class XmlReader
    {
        private string XmlFile;
        private XmlDocument Document;
        private bool Alright;

        public bool IsItAlright
        {
            get => Alright;
            set => Alright = value;
        }

        public XmlReader(string xmlFile)
        {
            XmlFile = xmlFile;
            IsItAlright = true;
            if (File.Exists(xmlFile))
            {
                try
                {
                    Document = new XmlDocument();
                    Document.Load(xmlFile);
                }
                catch (Exception Exp)
                {
                    IsItAlright = false;
                }
            }
        }


        public List<string> GetTagData(string requiredTag,string parentTag)
        {
            List<string> TextData = new List<string>();

            foreach (XmlNode Node in Document.DocumentElement.ChildNodes)
            {
                if(Node.Name == parentTag && Node.HasChildNodes)
                {
                    foreach(XmlNode Child in Node.ChildNodes)
                    {
                        if (Child.Name == requiredTag)
                        {
                            TextData.Add(Child.InnerText);
                        }
                    }
                }
               
            }

            return TextData;
        }

        public void PrintContent()
        {
            foreach(XmlNode Node in Document.DocumentElement.ChildNodes)
            {
                Console.WriteLine(Node.InnerText);
            }
        }
    }
}
