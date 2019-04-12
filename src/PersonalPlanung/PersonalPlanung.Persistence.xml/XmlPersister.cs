using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PersonalPlanung.Persistence.xml
{
    /*
    abstract class XmlPersister<T, D> where T: class
    {
        readonly string _listName;
        readonly IMapDtoToElement<T, D> _dtoMapper;

        protected XmlPersister(string listName, IMapDtoToElement<T, D> dtoMapper)
        {
            _listName = listName;
            _dtoMapper = dtoMapper;
        }

        public void Save(IEnumerable<T> listData)
        {

        }

        public IEnumerable<T> Load<I>() where I: IMapDtoToElement<T, D>
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
    */
}