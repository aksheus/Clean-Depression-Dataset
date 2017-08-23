using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace CleanDepressionDataset
{
    class Program
    {
        private static readonly string TextTag = "TEXT";
        private static readonly string TitleTag = "TITLE";
        private static readonly string DateTag = "DATE";
        private static readonly string ParentTag = "WRITING";
        private static readonly string WriteDirectory = @"C:\Users\abkma\reddit-depression\cleaned_training\cleaned_negative";
        private static readonly string ReadDirectory = @"C:\Users\abkma\nlp\reddit-depression\training\positive_examples_anonymous_chunks";
        private static readonly int NumberOfFiles = 403;
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
           /* for (int OuterIndex = 0; OuterIndex < NumberOfFiles; OuterIndex++)
            {
               List<string> FilesToCombine = new List<string>();

                // add first chunk for each subject 
                FilesToCombine.Add(Directory.GetFiles(ReadDirectory + Chunks[0])[OuterIndex]);

                // iterate over chunks 
                for (int InnerIndex = 1; InnerIndex < Chunks.Length; InnerIndex++)
                {
                    // get the file we need 
                    foreach (string NextFile in Directory.GetFiles(ReadDirectory + Chunks[InnerIndex]))
                    {
                        if (IsTheNextChunk(FilesToCombine[0], NextFile))
                        {
                            FilesToCombine.Add(NextFile);
                        }
                    }
                }

                for (int InnerIndex = 0; InnerIndex < 9; InnerIndex++)
                {
                    List<string> TempList = new List<string>();
                    foreach (int Iter in Iterator[InnerIndex])
                    {
                        TempList.Add(FilesToCombine[Iter]);
                    }
                    FileCombiner Combiner = new FileCombiner(TempList, WriteDirectory + ChunkFolders[InnerIndex]);
                    Combiner.WriteCombinedFiles(GetNameFromPath(FilesToCombine[0]));
                }
            } */


            // For single chunks 
            
            string[] Directories = Directory.GetDirectories(ReadDirectory);

            Dictionary<string, List<int>> PostsPerDay = new Dictionary<string, List<int>>();
            foreach ( string Direc in Directories)
            {
                foreach(string XmlFile in Directory.GetFiles(Direc))
                {
                    // Console.WriteLine(XmlFile);
                    XmlReader Reader = new XmlReader(XmlFile);
                    string SubjectName = GetNameFromPath(XmlFile).Split('.')[0];
                    if (Reader.IsItAlright)
                    {
                    //  List<string> TextOutput = Reader.GetTagData(TextTag, ParentTag);
                        Dictionary<string, List<string>> DateToPost = Reader.GetTagData(DateTag, TextTag, TitleTag, ParentTag);
                        foreach (KeyValuePair<string,List<string>> kvp in DateToPost)
                        {
                            if (!PostsPerDay.ContainsKey(SubjectName))
                            {
                                PostsPerDay[SubjectName] = new List<int> { kvp.Value.Count };
                            }
                            else
                            {
                                PostsPerDay[SubjectName].Add(kvp.Value.Count);
                            }
                        }

                        // Writer Write = new Writer();
                        //Write.WriteToTxt(PreprocessPath(XmlFile), Output);
                    }
                }
            }

            foreach(KeyValuePair<string,List<int>> kvp in PostsPerDay)
            {
                Console.WriteLine(kvp.Key + ":" + kvp.Value.Count);
            }
            Console.WriteLine(PostsPerDay.Count);
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
