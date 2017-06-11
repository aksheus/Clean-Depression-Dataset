using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace CleanDepressionDataset
{
    class Program
    {
        private static readonly string RequiredTag = "TEXT";
        private static readonly string ParentTag = "WRITING";
        private static readonly string WriteDirectory = @"C:\Users\abkma\reddit-depression\training\cleaned_negative";
        private static readonly string ReadDirectory = @"C:\Users\abkma\reddit-depression\training\negative_examples_anonymous_chunks";

        static void Main(string[] args)
        {
            string[] Directories = Directory.GetDirectories(ReadDirectory);

            foreach ( string Direc in Directories)
            {
                foreach(string XmlFile in Directory.GetFiles(Direc))
                {
                    XmlReader Reader = new XmlReader(XmlFile);
                    if (Reader.IsItAlright)
                    {
                        List<string> Output = Reader.GetTagData(RequiredTag, ParentTag);
                        Writer Write = new Writer();
                        Write.WriteToTxt(PreprocessPath(XmlFile), Output);
                    }
                }

            }

            Console.ReadKey();
        }

        public static string PreprocessPath(string readFilePath)
        {
            string  [] Temp = readFilePath.Split('\\');

            return WriteDirectory 
                + "\\" 
                + Temp[Temp.Length-2]
                + "\\"
                + Temp[Temp.Length-1].Split('.').First()
                + ".txt";
        }
    }
}
