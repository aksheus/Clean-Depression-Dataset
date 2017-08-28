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


        public List<string> GetTagData(string requiredTag1,string requiredTag2, string parentTag)
        {
            List<string> TextData = new List<string>();

            foreach (XmlNode Node in Document.DocumentElement.ChildNodes)
            {
                if(Node.Name == parentTag && Node.HasChildNodes)
                {
                    foreach(XmlNode Child in Node.ChildNodes)
                    {
                        string ToBeAdded = "";
                        if (Child.Name == requiredTag1)
                        {
                            ToBeAdded += Child.InnerText;
                        }
                        if (Child.Name == requiredTag2)
                        {
                            ToBeAdded += Child.InnerText;
                        }

                        TextData.Add(ToBeAdded);

                    }
                }
               
            }

            return TextData;
        }

        public Dictionary<string,List<string>> GetTagData(string keyTag,string firstValueTag,string secondValueTag,string parentTag)
        {
            Dictionary<string, List<string>> TextData = new Dictionary<string, List<string>>();

            foreach (XmlNode Node in Document.DocumentElement.ChildNodes)
            {
                if (Node.Name == parentTag && Node.HasChildNodes)
                {
                    foreach (XmlNode Child in Node.ChildNodes)
                    {
                        string  FirstValueTagData="",SecondValueTagData="";
                        string KeyTagData = "";
                        if (Child.Name == keyTag)
                        {
                           KeyTagData = DateTime.Parse(Child.InnerText).Month.ToString();
                        }
                        if ( Child.Name == firstValueTag)
                        {
                            FirstValueTagData = Child.InnerText;
                        }
                        if ( Child.Name == secondValueTag)
                        {
                            SecondValueTagData = Child.InnerText;
                        }

                        if (KeyTagData !="") {
                            if (!TextData.ContainsKey(KeyTagData))
                            {
                                TextData.Add(KeyTagData, new List<string>() { FirstValueTagData + SecondValueTagData });
                            }
                            else 
                            {
                                TextData[KeyTagData].Add(FirstValueTagData + SecondValueTagData);
                            }
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
