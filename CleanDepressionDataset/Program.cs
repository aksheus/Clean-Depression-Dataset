using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDepressionDataset
{
    class Program
    {
        static void Main(string[] args)
        {
            TagData tg = new TagData("C:\\Users\\abkma\\reddit-depression\\testing\\chunk_1\\test_subject25_1.xml");
            tg.PrintContent();
            Console.ReadKey();
        }
    }
}
