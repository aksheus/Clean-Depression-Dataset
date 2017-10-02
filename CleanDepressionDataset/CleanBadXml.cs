using System.Text;
using System.Xml;
using System.IO;
using System.Security;

namespace CleanDepressionDataset
{
    class CleanBadXml
    {
        
        private string BadXmlFile;


        public CleanBadXml( string badFile)
        {
            BadXmlFile = badFile;

        }

        public string ProcessBadXml()
        {
            string GoodXml = "";

            foreach (string Line in File.ReadLines(BadXmlFile))
            {
                GoodXml += RemoveInvalidXmlChars(Line);
            }

            return GoodXml;
        }

        public string RemoveInvalidXmlChars(string text)
        {
            if (text == null) return text;
            if (text.Length == 0) return text;

            text.Replace("&nbsp;", "&#160;");

            text = EscapeXMLValue(text);
            // a bit complicated, but avoids memory usage if not necessary
            StringBuilder result = null;
            for (int i = 0; i < text.Length; i++)
            {
                var ch = text[i];
                if (XmlConvert.IsXmlChar(ch))
                {
                    result?.Append(ch);
                }
                else
                {
                    if (result == null)
                    {
                        result = new StringBuilder();
                        result.Append(text.Substring(0, i));
                    }
                }
            }

            if (result == null)
                return text; // no invalid xml chars detected - return original text
            else
                return result.ToString();

        }

        public string UnescapeXMLValue(string xmlString)
        {
            return xmlString.Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&");
        }

        public string EscapeXMLValue(string xmlString)
        {
            return xmlString.Replace("'", "&apos;").Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;");
        }
    }
}
