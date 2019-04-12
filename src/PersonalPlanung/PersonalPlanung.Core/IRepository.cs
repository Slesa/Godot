using System.Collections.Generic;

namespace PersonalPlanung.Core
{
    public interface IRepository<T>
    {
        bool Contains(T element);
        void Add(T element);
        void Change(T element);
        void Remove(T element);
        IEnumerable<T> GetAll();
    }
}