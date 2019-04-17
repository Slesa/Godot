using System.Collections.Generic;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Persistence.xml
{
    public interface IPersist<T>
    {
        void Save(IEnumerable<T> listData);
        IEnumerable<T> Load();
    }

    public interface IBerufPersister : IPersist<Beruf> { }
    public interface IRollePersister : IPersist<Rolle> { }
    public interface IPersonPersister : IPersist<Person> { }
    public interface IVeranstaltungPersister : IPersist<Veranstaltung> { }
    //public interface ISchichtRepository : IPersist<Schicht> { }
    //public interface IZeitBuchungRepository : IPersist<ZeitBuchung> { }

}