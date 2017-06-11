using System;
using System.Collections.Generic;
using System.IO;
namespace CleanDepressionDataset
{
    class Program
    {
        private static readonly string RequiredTag = "TEXT";
        private static readonly string ParentTag = "WRITING";

        static void Main(string[] args)
        {
            // add file exists check later 
            XmlReader Reader = new XmlReader(@"C:\Users\abkma\reddit-depression\testing\chunk_1\test_subject25_1.xml");
            List<string> Output = Reader.GetTagData(RequiredTag,ParentTag);
            Writer Write = new Writer();
            Write.WriteToTxt(@"C:\Users\abkma\reddit-depression\cleaned_testing\chunk_1\test.txt", Output);

            Console.ReadKey();
        }
    }
}
