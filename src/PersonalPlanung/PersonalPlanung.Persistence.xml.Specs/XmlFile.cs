using System.IO;
using System.Text;

namespace PersonalPlanung.Persistence.xml.Specs
{
    public class XmlFile
    {
        readonly StringBuilder _sb = new StringBuilder();

        public XmlFile WithLine(string line)
        {
            _sb.AppendLine(line);
            return this;
        }

        public XmlFile WithContent(string content)
        {
            _sb.Append(content);
            return this;
        }

        public void AsFile(string filename)
        {
            File.WriteAllText(filename, _sb.ToString());
        }
    }
}