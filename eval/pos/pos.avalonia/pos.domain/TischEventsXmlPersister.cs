using System.IO;
using System.Xml;

namespace pos.domain
{

  public class TischEventsXmlPersister : IPersistTischEvents
  {
    public TischEventStore Laden(uint tischnr, uint parteinr)
    {
      var es = new TischEventStore(tischnr, parteinr);
      using (var fs = new FileStream($"T{tischnr}_{parteinr}.0", FileMode.Open))
      {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TischEventStore));
        serializer.Deserialize(XmlReader.Create(fs));
      }
      return es;
    }

    public void Speichern(TischEventStore eventStore)
    {
      using (var fs = new FileStream($"T{eventStore.TischNr}_{eventStore.ParteiNr}.0", FileMode.OpenOrCreate))
      {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TischEventStore));
        serializer.Serialize(XmlWriter.Create(fs), eventStore);
      }
    }
  }
}