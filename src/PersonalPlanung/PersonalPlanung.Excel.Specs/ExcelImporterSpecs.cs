using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using PersonalPlanung.Core.Model;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace PersonalPlanung.Excel.Specs
{
    [Subject(typeof(ExcelImporter))]
    internal class Wenn_Personen_aus_Excel_importiert_werden : ExcelImportSpecs
    {
        Establish context = () =>
        {
            _sut = new ExcelImporter(ExcelFile);
        };

        Because of = () => _personen = _sut.ImportierePersonen().Distinct().ToList();

        It sollte_alle_personen_lesen = () => _personen.Count.ShouldEqual(15);
        It sollte_name_der_personen_lesen = () => _personen.FirstOrDefault(x => x.Name=="Beck").ShouldNotBeNull();
        It sollte_vorname_der_personen_lesen = () => _personen.FirstOrDefault(x => x.Vorname=="K.").ShouldNotBeNull();
        It sollte_schlussdienst_rolle_der_personen_lesen= () => 
            _personen.FirstOrDefault(x => x.EinsetzbarAls.FirstOrDefault(r => r.Name== "Schlussdienst")!=null).ShouldNotBeNull();
        It sollte_rolle_der_personen_lesen_bei_nur_rollenname = () => 
            _personen.FirstOrDefault(x => x.EinsetzbarAls.FirstOrDefault(r => r.Name== "Ordnungsdienstleiter")!=null).ShouldNotBeNull();
        It sollte_rolle_der_personen_lesen_bei_nur_rolle_und_position = () =>
            _personen.FirstOrDefault(x => x.EinsetzbarAls.FirstOrDefault(r => r.Name == "Ordner") != null).ShouldNotBeNull();
        It sollte_rolle_der_personen_lesen_bei_mehr_rollen_und_position = () =>
            _personen.FirstOrDefault(x => x.EinsetzbarAls.FirstOrDefault(r => r.Name == "Platzanweiser/Saalkontrolle") != null).ShouldNotBeNull();
        It sollte_externe_dienstleister_markieren = () =>
            _personen.First(x => x.Name == "USEC").Beruf.Name.ShouldEqual("Dienstleister");

        static ExcelImporter _sut;
        static List<Person> _personen;
    }

    [Subject(typeof(ExcelImporter))]
    internal class Wenn_Veranstaltungen_aus_Excel_importiert_werden : ExcelImportSpecs
    {
        Establish context = () =>
        {
            _sut = new ExcelImporter(ExcelFile);
        };

        Because of = () => _veranstaltungen = _sut.ImportiereVeranstaltungen().ToList();
        It sollte_alle_veranstaltungen_lesen = () => _veranstaltungen.Count.ShouldEqual(30);
        It sollte_name_der_veranstaltung_lesen = () => _veranstaltungen.First(x => x.Name=="Cinderella").ShouldNotBeNull();
        It sollte_name_der_veranstaltung_in_nächster_zeile_lesen = () => _veranstaltungen.First(x => x.Name=="Gesundheitskongress ABBAU").ShouldNotBeNull();
        It sollte_beginn_der_veranstaltung_lesen = () => _veranstaltungen.First(x => x.BeginntAm==new DateTime(2019, 4, 26)).ShouldNotBeNull();
        It sollte_ende_der_veranstaltung_lesen = () => _veranstaltungen.First(x => x.BeginntAm==new DateTime(2019, 4, 26)).EndetAm.ShouldEqual(new DateTime(2019, 4, 26));
        It sollte_ende_der_veranstaltung_in_nächster_zeile_lesen = () => _veranstaltungen.First(x => x.Name=="La Noche").EndetAm.ShouldEqual(new DateTime(2019, 5, 1));

        It sollte_aufgaben_der_veranstaltung_lesen = () =>
            _veranstaltungen.First(x => x.Name == "Stadtratssitzung").Aufgaben.Count.ShouldEqual(1);
        It sollte_rolle_der_aufgaben_der_veranstaltung_lesen = () =>
            _veranstaltungen.First(x => x.Name == "Stadtratssitzung").Aufgaben[0].Rolle.Name.ShouldEqual("Ordner");
        It sollte_standort_der_aufgaben_der_veranstaltung_lesen = () =>
            _veranstaltungen.First(x => x.Name == "Stadtratssitzung").Aufgaben[0].Standort.Name.ShouldEqual("Westeingang");
        It sollte_längeren_standort_der_aufgaben_der_veranstaltung_lesen = () =>
            _veranstaltungen.First(x => x.Name == "Gesundheitskongress").Aufgaben.Where(p => p.Standort.Name=="Haupteingang Ost").ShouldNotBeNull();
        It sollte_beginn_der_aufgaben_der_veranstaltung_lesen = () =>
            _veranstaltungen.First(x => x.Name == "Stadtratssitzung").Aufgaben[0].Beginn.ShouldEqual(new DateTime(2019, 4, 16, 12, 30, 0));
        It sollte_ende_der_aufgaben_der_veranstaltung_lesen = () =>
            _veranstaltungen.First(x => x.Name == "Stadtratssitzung").Aufgaben[0].Ende.ShouldEqual(new DateTime(2019, 4, 16, 21, 0, 0));
        It sollte_ende_der_aufgaben_der_veranstaltung_anpassen_wenn_nach_mitternacht = () =>
            _veranstaltungen.First(x => x.Name == "La Noche").Aufgaben[0].Ende.ShouldEqual(new DateTime(2019, 5, 1, 0, 30, 0));
        It sollte_mehrere_aufgaben_der_veranstaltung_lesen = () =>
            _veranstaltungen.First(x => x.Name == "Film Tour").Aufgaben.Count.ShouldEqual(7);

        static ExcelImporter _sut;
        static List<Veranstaltung> _veranstaltungen;
    }

    internal class ExcelImportSpecs
    {
        protected const string ExcelFile = @"d:\work\github\Godot\src\PersonalPlanung\April.xlsx";
    }
}