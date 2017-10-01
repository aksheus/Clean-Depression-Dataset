using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace CleanDepressionDataset
{
    class Program
    {
        private static readonly string TextTag = "post";//"TEXT";
        private static readonly string TitleTag = "TITLE";
        private static readonly string DateTag = "DATE";
        private static readonly string ParentTag = "Blog"; //"WRITING";
        private static readonly string WriteDirectory = @"C:\Users\abkma\anlp\assign1\cleaned_blogs_train";
        private static readonly string ReadDirectory = @"C:\Users\abkma\anlp\assign1\blogs_train";
        private static readonly int NumberOfFiles = 83;
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
          /*   for (int OuterIndex = 0; OuterIndex < NumberOfFiles; OuterIndex++)
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
             }  */

            // For single chunks 
             string[] Directories = Directory.GetDirectories(ReadDirectory);
             foreach (string Direc in Directories)
             {
                foreach (string XmlFile in Directory.GetFiles(Direc))
                {
                    XmlReader Reader = new XmlReader(XmlFile);
                    if (Reader.IsItAlright)
                    {
                        List<string> Output = Reader.GetTagData(TextTag, ParentTag);
                        Writer Write = new Writer();
                        Write.WriteToTxt(PreprocessPath2(XmlFile), Output);
                    }
                }
             } 

            /*
             *  statistics
             * string[] Directories = Directory.GetDirectories(ReadDirectory);

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
                              Console.WriteLine(kvp.Key);
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
              WriteStatistics(ref PostsPerDay);
              Console.ReadKey(); */
        }

        public static void WriteStatistics(ref Dictionary<string, List<int>> postsPerUnit)
        {
            List<double> Averages = new List<double>();

            using (StreamWriter sw = File.CreateText(@"C:\Users\abkma\nlp\reddit-depression\stats.csv"))
            {
                sw.WriteLine("SubjectName"+","+"Average"+","+"StandardDeviation");
                foreach (KeyValuePair<string, List<int>> kvp in postsPerUnit)
                {
                    double Average = kvp.Value.Average();
                    Averages.Add(Average);
                    double SumOfSquaredDiff = kvp.Value.Select(z => (z - Average) * (z - Average)).Sum();
                    double StandardDeviation = Math.Sqrt(SumOfSquaredDiff / kvp.Value.Count);
                    sw.WriteLine(kvp.Key+","+Average+","+StandardDeviation);

                }
            }

            double GrandMean = Averages.Average();
            double GrandSSDIff = Averages.Select(z => (z - GrandMean) * (z - GrandMean)).Sum();
            Console.WriteLine("Grand sd :" + Math.Sqrt(GrandSSDIff / Averages.Count));
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

        public static string PreprocessPath2(string readFilePath)
        {
            // C:\Users\abkma\anlp\assign1\blogs_train\female\11762.female.25.Student.Aries.xml
            string[] Temp = readFilePath.Split('\\');

            string ToReturn = WriteDirectory
                + "\\"
                + Temp[Temp.Length - 2]
                + "\\";

            string[] Temp2 = Temp[Temp.Length - 1].Split('.');

            string Fname = "";

            for(int Index = 0; Index < Temp2.Length - 1; Index++)
            {
                Fname += Temp2[Index] + ".";
            }

            return ToReturn + Fname + "txt";
               //+ ".txt";
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
