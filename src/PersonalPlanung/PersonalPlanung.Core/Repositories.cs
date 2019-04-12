using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;

namespace PersonalPlanung.Core
{
    public class StatusRepository : Repository<Status>, IStatusRepository {}
    public class RolleRepository : Repository<Rolle>, IRolleRepository {}
    public class PersonRepository : Repository<Person>, IPersonRepository {}
    public class VeranstaltungRepository : Repository<Veranstaltung>, IVeranstaltungRepository {}
    public class SchichtRepository : Repository<Schicht>, ISchichtRepository {}
    public class ZeitBuchungRepository : Repository<ZeitBuchung>, IZeitBuchungRepository {}
}