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

    public class SchichtenXmlRepository : XmlRepository<Schicht, SchichtenXmlPersister>, ISchichtRepository
    {
        public SchichtenXmlRepository(SchichtenXmlPersister persister) : base(persister)
        {
        }
    }

    public class ZeitBuchungsXmlRepository : XmlRepository<ZeitBuchung, ZeitBuchungsXmlPersister>, IZeitBuchungRepository
    {
        public ZeitBuchungsXmlRepository(ZeitBuchungsXmlPersister persister) : base(persister)
        {
        }
    }
}