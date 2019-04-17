using PersonalPlanung.Core;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;

namespace PersonalPlanung.Persistence.xml
{
    public class BerufeXmlRepository : XmlRepository<Beruf, BerufeXmlPersister>, IBerufRepository
    {
        public BerufeXmlRepository(BerufeXmlPersister persister) : base(persister)
        {
        }
    }

    public class RollenXmlRepository : XmlRepository<Rolle, RollenXmlPersister>, IRolleRepository
    {
        public RollenXmlRepository(RollenXmlPersister persister) : base(persister)
        {
        }
    }

    public class PersonenXmlRepository : XmlRepository<Person, PersonenXmlPersister>, IPersonRepository
    {
        public PersonenXmlRepository(PersonenXmlPersister persister) : base(persister)
        {
        }
    }

    public class VeranstaltungenXmlRepository : XmlRepository<Veranstaltung, VeranstaltungenXmlPersister>, IVeranstaltungRepository
    {
        public VeranstaltungenXmlRepository(VeranstaltungenXmlPersister persister) : base(persister)
        {
        }
    }

    //public class SchichtenRepository : Repository<Schicht>, ISchichtRepository { }
    //public class ZeitBuchungsRepository : Repository<ZeitBuchung>, IZeitBuchungRepository { }
}