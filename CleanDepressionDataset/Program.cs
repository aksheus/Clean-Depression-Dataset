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
        private static readonly string WriteDirectory = @"C:\Users\abkma\reddit-depression\cleaned_testing";
        private static readonly string ReadDirectory = @"C:\Users\abkma\reddit-depression\testing";

        static void Main(string[] args)
        {
            // add file exists check later 
            string hmm = @"C:\Users\abkma\reddit-depression\testing\chunk_1\test_subject25_1.xml";
            XmlReader Reader = new XmlReader(hmm);
            List<string> Output = Reader.GetTagData(RequiredTag,ParentTag);
            Writer Write = new Writer();
            Write.WriteToTxt(PreprocessPath(hmm), Output);

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
