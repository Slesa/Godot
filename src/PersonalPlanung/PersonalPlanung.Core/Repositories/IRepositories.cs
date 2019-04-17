using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Core.Repositories
{
    public interface IBerufRepository : IRepository<Beruf> {}
    public interface IRolleRepository : IRepository<Rolle> {}
    public interface IPersonRepository : IRepository<Person> {}
    public interface IVeranstaltungRepository : IRepository<Veranstaltung> {}
    public interface ISchichtRepository : IRepository<Schicht> {}
    public interface IZeitBuchungRepository : IRepository<ZeitBuchung> {}
}