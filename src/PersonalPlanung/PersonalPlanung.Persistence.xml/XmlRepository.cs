using System.Collections.Generic;
using System.Linq;
using PersonalPlanung.Core;

namespace PersonalPlanung.Persistence.xml
{
    public class XmlRepository<T, TP> : IRepository<T> where TP : IPersist<T>
    {
        TP _persister;

        public XmlRepository(TP persister)
        {
            _persister = persister;
        }
        public bool Contains(T element)
        {
            return Elements.Contains(element);
        }

        public void Add(T element)
        {
            Elements.Add(element);
            _persister.Save(Elements);
        }

        public void Remove(T element)
        {
            Elements.Remove(element);
            _persister.Save(Elements);
        }

        public void Change(T element)
        {
            var item = _elements.FirstOrDefault(x => x.Equals(element));
            if (item != null)
                Remove(item);
            Add(element);
            _persister.Save(Elements);
        }

        public IEnumerable<T> GetAll()
        {
            return Elements;
        }

        List<T> _elements;
        List<T> Elements
        {
            get
            {
                if (_elements==null) _elements = _persister.Load().ToList();
                return _elements;
            }
    }
    }
}