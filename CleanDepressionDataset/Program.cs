using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace CleanDepressionDataset
{
    class Program
    {
        //private static readonly string RequiredTag = "TEXT";
        //private static readonly string ParentTag = "WRITING";
        private static readonly string WriteDirectory = @"C:\Users\abkma\reddit-depression\cleaned_testing";
        private static readonly string ReadDirectory = @"C:\Users\abkma\reddit-depression\cleaned_testing";
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

        private static readonly Dictionary<int, string> ChunkFolders = new Dictionary<int, string>()
        {
            { 0 , "\\chunk1-2" },
            { 1 , "\\chunk1-2-3" },
            { 2 , "\\chunk1-2-3-4"},
            { 3 , "\\chunk1-2-3-4-5"},
            { 4 , "\\chunk1-2-3-4-5-6" },
            { 5 , "\\chunk1-2-3-4-5-6-7" },
            { 6 , "\\chunk1-2-3-4-5-6-7-8" },
            { 7 , "\\chunk1-2-3-4-5-6-7-8-9" },
            { 8 , "\\chunk1-2-3-4-5-6-7-8-9-10" }

        };
        private static readonly Dictionary<int, int[]> Iterator = new Dictionary<int, int[]>()
        {
            { 0 , new int [] { 0, 1 } },
            { 1 , new int [] { 0, 1 ,2 } },
            { 2 , new int [] { 0, 1 ,2 ,3 } },
            { 3 , new int [] { 0, 1 ,2 ,3 ,4 } },
            { 4 , new int [] { 0, 1 ,2 ,3 ,4 ,5 } },
            { 5 , new int [] { 0, 1 ,2 ,3 ,4 ,5,6 } },
            { 6 , new int [] { 0, 1 ,2 ,3 ,4 ,5,6,7 } },
            { 7 , new int [] { 0, 1 ,2 ,3 ,4 ,5,6,7,8 } },
            { 8 , new int [] { 0, 1 ,2 ,3 ,4 ,5,6,7,8 ,9} }
        };

        static void Main(string[] args)
        {
            List<string> FilesToCombine = new List<string> ();

            FilesToCombine.Add( Directory.GetFiles(ReadDirectory + Chunks[0])[0] );


            // iterate over chunks 
            for ( int Index = 1; Index < Chunks.Length; Index++)
            {
                // get the file we need 
                foreach (string NextFile in Directory.GetFiles(ReadDirectory + Chunks[Index]))
                {
                    if (IsTheNextChunk(FilesToCombine[0], NextFile))
                    {
                        FilesToCombine.Add(NextFile);
                    }
                }
            }
           // FileCombiner Combiner = new FileCombiner(FilesToCombine, WriteDirectory);
            //Combiner.WriteCombinedFiles(GetNameFromPath(FilesToCombine[0]));

            for(int Index = 0; Index < 9; Index ++)
            {
                List<string> TempList = new List<string>();
                foreach( int Iter in Iterator[Index])
                {
                    TempList.Add(FilesToCombine[Iter]);
                }
                FileCombiner Combiner = new FileCombiner(TempList, WriteDirectory+ChunkFolders[Index]);
                Combiner.WriteCombinedFiles(GetNameFromPath(FilesToCombine[0]));
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
            string [] Temp1 = first.Split('\\');
            string [] Temp2 = second.Split('\\');
            string First = Temp1[Temp1.Length - 1];
            string Second = Temp2[Temp2.Length - 1];
            return First.Split('_')[1].Equals(Second.Split('_')[1]);
        }

        public static string GetNameFromPath(string path)
        {
            string[] Temp = path.Split('\\');

            return Temp[Temp.Length - 1]
                   .Split('_')
                   [1]
                   + ".txt";
             
        }

    }
}
