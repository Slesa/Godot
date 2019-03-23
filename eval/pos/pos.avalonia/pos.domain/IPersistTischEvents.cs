namespace pos.domain
{
    public interface IPersistTischEvents
    {
        void Speichern(TischEventStore eventStore);
        TischEventStore Laden(uint tischnr, uint parteinr);
    }
}