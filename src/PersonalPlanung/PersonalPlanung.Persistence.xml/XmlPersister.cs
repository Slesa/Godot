using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace PersonalPlanung.Persistence.xml
{
    public abstract class XmlPersister<T, TD> where TD: class
    {
        readonly string _listName;

        protected XmlPersister(string listName)
        {
            _listName = listName;
        }

        public void Save(IEnumerable<T> listData)
        {
            var fileName = FileNamer.GetFilenameFor(_listName);
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TD));
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                var writer = new XmlTextWriter(fs, Encoding.UTF8) { Formatting = Formatting.Indented };
                var instance = GetDto(listData);
                serializer.Serialize(writer, instance);
            }
        }

        public abstract object GetDto(IEnumerable<T> listData);
        public abstract IEnumerable<T> GetOrigin(TD dto);

        public IEnumerable<T> Load()
        {
            try
            {
                var fileName = FileNamer.GetFilenameFor(_listName);
                if (File.Exists(fileName))
                    using (var fs = new FileStream(fileName, FileMode.Open))
                    {
                        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TD));
                        var instance = serializer.Deserialize(XmlReader.Create(fs)) as TD;
                        return GetOrigin(instance);
                    }
            }
            catch
            {
                // ignored
            }
            return Enumerable.Empty<T>();
        }
    }
}