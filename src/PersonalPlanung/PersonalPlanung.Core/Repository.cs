using System.Collections.Generic;

namespace PersonalPlanung.Core
{
    public class Repository<T> : IRepository<T>
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

        public IEnumerable<T> GetAll()
        {
            return _elements;
        }

    }
}