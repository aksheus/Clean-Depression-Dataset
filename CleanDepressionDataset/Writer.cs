using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CleanDepressionDataset
{
    class Writer
    {

        public Writer()
        {

        }

        public void WriteToTxt(string path,List<string> content)
        {
            using (StreamWriter Write = File.CreateText(path))
            {
                foreach(string Line in content)
                {
                    Write.WriteLine(Line);
                    Write.WriteLine("\n");
                    Write.WriteLine("\n");

                }
            }
        }

       
    }
}
