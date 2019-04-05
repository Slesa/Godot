using System.Collections.Generic;

namespace PersonalPlanung.Core
{
    public interface IRepository<T>
    {
        bool Contains(T element);
        void Add(T element);
        IEnumerable<T> GetAll();
    }
}