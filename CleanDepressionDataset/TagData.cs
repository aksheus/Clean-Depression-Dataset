using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CleanDepressionDataset
{
    class TagData
    {
        private string XmlFile;
        private XmlDocument Document;

        public TagData(string xmlFile)
        {
            XmlFile = xmlFile;
            Document = new XmlDocument();
            Document.Load(xmlFile);
        }



        public string [] GetTagData(string tag)
        {
            return new string[] { "Empty" };

        }

        public void PrintContent()
        {
            foreach(XmlNode node in Document.DocumentElement.ChildNodes)
            {
                Console.WriteLine(node.InnerText);
            }
        }
    }
}
