using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;

namespace PersonalPlanung.Core
{
    public class BerufRepository : Repository<Beruf>, IBerufRepository { }
    public class RolleRepository : Repository<Rolle>, IRolleRepository {}
    public class PersonRepository : Repository<Person>, IPersonRepository {}
    public class VeranstaltungRepository : Repository<Veranstaltung>, IVeranstaltungRepository {}

    public class SchichtRepository : Repository<Schicht>, ISchichtRepository
    {
        public void Save() {}
    }

    public class ZeitBuchungRepository : Repository<ZeitBuchung>, IZeitBuchungRepository
    {
        public void BeginChanges() { }
        public void Save() { }
    }
}