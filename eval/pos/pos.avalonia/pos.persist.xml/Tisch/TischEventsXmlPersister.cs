using System;
using System.IO;
using System.Linq;
using System.Xml;
using AutoMapper;
using pos.persist.xml.Tisch;

namespace pos.domain
{
#if not
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            Mapper.Instance.
            CreateMap<ArtikelBestelltEvent, ArtikelBestelltDto>().ReverseMap();
            CreateMap<ArtikelStorniertEvent, ArtikelStorniertEvent>().ReverseMap();

            /*Complex to Flattened
            CreateMap<Person, PersonDTO>().ForMember(dest => dest.City, opts => opts.MapFrom(src => src.Address.City)).ReverseMap(); */
        }
    }
#endif
    public class TischEventsXmlPersister : IPersistTischEvents
    {
        static TischEventsXmlPersister()
        {
            Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ArtikelBestelltEvent, ArtikelBestelltDto>().ReverseMap();
                    cfg.CreateMap<ArtikelStorniertEvent, ArtikelStorniertEvent>().ReverseMap();
                });
        }
        public TischEventStore Laden(uint tischnr, uint parteinr)
        {
            TischDto tischDto;
            using (var fs = new FileStream($"T{tischnr}_{parteinr}.0", FileMode.Open))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TischDto));
                tischDto = serializer.Deserialize(XmlReader.Create(fs)) as TischDto;
            }

            var es = new TischEventStore(tischnr, parteinr, tischDto.TischInhalt.Select(CreateEventFromDto).ToList());
            return es;
        }

        public void Speichern(TischEventStore eventStore)
        {
            var tischDto = new TischDto()
//            foreach(var evt in eventStore.Events)
            {
//                var dto = CreateDtoFromEvent(evt);
//                tischDto.TischInhalt.Add(dto);
                TischInhalt = eventStore.Events.Select(CreateDtoFromEvent).ToList()
            };

            var xmlWriterSettings = new XmlWriterSettings() { Indent = true };
            using (var fs = new FileStream($"T{eventStore.TischNr}_{eventStore.ParteiNr}.0", FileMode.OpenOrCreate))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TischDto));
                serializer.Serialize(XmlWriter.Create(fs, xmlWriterSettings), tischDto);
            }
        }

        TischEintragDto CreateDtoFromEvent(ITischEvent evt)
        {
            if (evt is ArtikelBestelltEvent bestellt) return Mapper.Map<ArtikelBestelltDto>(bestellt);
            if (evt is ArtikelStorniertEvent storniert) return Mapper.Map<ArtikelStorniertDto>(storniert);
            return null;
        }

        ITischEvent CreateEventFromDto(TischEintragDto evt)
        {
            if (evt is ArtikelBestelltDto bestellt)
                return new ArtikelBestelltEvent(bestellt.Anzahl, bestellt.Plu, bestellt.Preis)
                {
                    Id = bestellt.Id,
                    OccurredOn = bestellt.OccurredOn
                };
            if (evt is ArtikelStorniertDto storniert)
                return new ArtikelStorniertEvent(storniert.Anzahl, storniert.Bestellung, storniert.Betrag)
                {
                    Id = storniert.Id,
                    OccurredOn = storniert.OccurredOn
                };
            return null;
        }
    }
}