using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CleanDepressionDataset
{
    class FileCombiner
    {
        private List<string> Files;
        private string WritePath;
        private bool Alright;
        private List<string> CombinedContent;

        public bool IsItAlright { get => Alright; set => Alright = value; }

        public FileCombiner (List<string> files,string writePath)
        {
            Files = files;
            WritePath = writePath;
            Alright = true;
            CombinedContent = new List<string>();
        }

        public void WriteCombinedFiles(string newFile)
        {
            CombineFiles();
            File.WriteAllLines(WritePath+"\\"+newFile, CombinedContent.ToArray());
        }

        public void CombineFiles()
        {
            try
            {
                foreach (string FileToRead in Files)
                {
                    if (File.Exists(FileToRead))
                    {
                        foreach (string line in File.ReadAllLines(FileToRead))
                        {
                            CombinedContent.Add(line);
                        }

                        CombinedContent.Add("\n");
                        CombinedContent.Add("\n");
                        CombinedContent.Add("\n");
                        CombinedContent.Add("\n");
                        CombinedContent.Add("\n");
                        CombinedContent.Add("\n");
                        CombinedContent.Add("\n");
                        CombinedContent.Add("\n");
                        CombinedContent.Add("\n");
                        CombinedContent.Add("\n");

                    }
                }
            }
            catch (Exception Excep)
            {
                IsItAlright = false;
            }
        }

    }
}
