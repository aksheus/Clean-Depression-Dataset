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
        private static readonly string WriteDirectory = @"C:\Users\abkma\reddit-depression\training\cleaned_positive";
        private static readonly string ReadDirectory = @"C:\Users\abkma\reddit-depression\training\positive_examples_anonymous_chunks";
        private static readonly int NumberOfFiles = 401;
        private static readonly string[] Chunks = new string[]
        {
            "\\chunk_1",
            "\\chunk_2",
            "\\chunk_3",
            "\\chunk_4",
            "\\chunk_5",
            "\\chunk_6",
            "\\chunk_7",
            "\\chunk_8",
            "\\chunk_9",
            "\\chunk_10"
        };
        static void Main(string[] args)
        {

            string FirstChunk = Directory.GetFiles(ReadDirectory + Chunks[0])[0];

            for ( int Index = 1; Index < Chunks.Length; Index++)
            {
                List<string> FilesToCombine = new List<string>();
                string NextChunk;
                foreach (string s in Directory.GetFiles(ReadDirectory + Chunks[Index]))
                {
                    if (IsTheNextChunk(FirstChunk, s))
                    {
                        NextChunk = s;
                    }
                }
            }
                 

            // For single chunks 
            /*
                 string[] Directories = Directory.GetDirectories(ReadDirectory);
             * 
             *   foreach ( string Direc in Directories)
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

            */

            Console.ReadKey();
        }

        public static string PreprocessPath(string readFilePath)
        {
            string[] Temp = readFilePath.Split('\\');

            return WriteDirectory
                + "\\"
                + Temp[Temp.Length - 2]
                + "\\"
                + Temp[Temp.Length - 1].Split('.').First()
               + ".txt";
        }

        public static bool IsTheNextChunk(string first, string second)
        {
            return first.Substring(0, first.Length - 2)
                        .Equals(second.Substring(0, second.Length - 2));
        }
        
    }
}
