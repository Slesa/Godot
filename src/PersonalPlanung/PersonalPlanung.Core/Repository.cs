using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalPlanung.Core
{
    public class Repository<T> : IRepository<T> // where T: IEquatable<T>
    {
        readonly List<T> _elements = new List<T>();

        public bool Contains(T element)
        {
            return _elements.Contains(element);
        }

        public void Add(T element)
        {
            _elements.Add(element);
        }

        public void Remove(T element)
        {
            _elements.Remove(element);
        }

        public void Change(T element)
        {
            var item = _elements.FirstOrDefault(x => x.Equals(element));
            if (item != null)
                Remove(item);
            Add(element);
        }

        public IEnumerable<T> GetAll()
        {
            return _elements;
        }

    }
}