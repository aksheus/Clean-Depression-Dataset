using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDepressionDataset
{
    class Program
    {
        private static readonly string RequiredTag = "TEXT";
        private static readonly string ParentTag = "WRITING";
        static void Main(string[] args)
        {
            
            XmlReader Reader = new XmlReader("C:\\Users\\abkma\\reddit-depression\\testing\\chunk_1\\test_subject25_1.xml");
            List<string> Output = Reader.GetTagData(RequiredTag,ParentTag);
            foreach(string s in Output)
            {
                Console.WriteLine(s);
            }

            Console.ReadKey();
        }
    }
}
