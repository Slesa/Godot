using pos.domain.Tische;

namespace pos.domain
{
    public interface IPersistTischEvents
    {
        void Speichern(TischEventStore eventStore);
        TischEventStore Laden(TischNr tischnr);
    }
}