using System.Collections.Generic;
using System.IO;
using System.Xml;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    public class StammdatenXmlPersister
    {
        readonly string _listName;

        public StammdatenXmlPersister()
        {
            _listName = "Stammdaten";
        }

        void Save(IEnumerable<Rolle> rollen/*, IEnumerable<Status> status*/)
        {
            var fileName = FileNamer.GetFilenameFor(_listName);
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                var instance = serializer.Deserialize(XmlReader.Create(fs)) as T;
                yield return _dtoMapper.MapElement(instance);
            }
        }
    }
}