using System;
using System.Collections.Generic;
using System.Xml;

namespace CleanDepressionDataset
{
    class XmlReader
    {
        private string XmlFile;
        private XmlDocument Document;

        public XmlReader(string xmlFile)
        {
            XmlFile = xmlFile;
            Document = new XmlDocument();
            Document.Load(xmlFile);
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
