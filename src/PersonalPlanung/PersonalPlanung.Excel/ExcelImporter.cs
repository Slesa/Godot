using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Excel
{
    public class ExcelImporter
    {
        readonly string _fileName;
        static int colVeranstaltungsDatum = 1;
        static int colVeranstaltungsName = 2;
        static int colSchlussdienst = 3;
        static int colVeranstaltungsZeit = 4;
        static int colPersonenName = 5;
        static int colRollenName = 6;
        static int colStandortName = 6;

        //readonly List<Veranstaltung> _veranstaltungen = new List<Veranstaltung>();

        public ExcelImporter(string fileName)
        {
            _fileName = fileName;
        }

        XLWorkbook _workbook;
        XLWorkbook Workbook => _workbook ?? (_workbook = new XLWorkbook(_fileName));
        IXLWorksheet _workSheet;
        IXLWorksheet WorkSheet => _workSheet ?? (_workSheet = Workbook.Worksheet(1));

        //public IObservable<Veranstaltung> Veranstaltungen => _veranstaltungen.ToObservable();

        /*        public Task Import()
                {
                    System.Diagnostics.Debug.WriteLine("Import start");
                    var ws = Workbook.Worksheet(1);
                    return Task.Factory.StartNew(() =>
                    {
                        System.Diagnostics.Debug.WriteLine("Import run start");
                        if (IstVorabPlanung(ws))
                            ImportVorabplanung(ws);
                        System.Diagnostics.Debug.WriteLine("Import run end");
                    });
                    //var fileName = $"{Directory.GetCurrentDirectory()}\\fileNameHere";
                } */
        #region PersonenImport

        public IEnumerable<Person> ImportierePersonen()
        {
            foreach (var row in WorkSheet.Rows().Skip(2))
            {
                var name = row.Cell(colPersonenName).GetValue<string>();
                if (string.IsNullOrWhiteSpace(name)) continue;

                var person = new Person {Name = GetNachname(name), Vorname = GetVorname(name)};
                var schlussDienst = LeseSchlussdienst(row);
                if (schlussDienst != null)
                    person.EinsetzbarAls.Add(schlussDienst);
                if(person.Name=="USEC") person.Status = Status.Dienstleister;
                var rolle = LeseRolle(row);
                if( rolle!=null )
                    person.EinsetzbarAls.Add(rolle);
                yield return person;
                
            }
        }

        Rolle LeseSchlussdienst(IXLRow row)
        {
            var marker = row.Cell(colSchlussdienst).GetValue<string>().ToLower();
            return marker=="xxx" ? new Rolle("Schlussdienst") : null;
        }

        Rolle LeseRolle(IXLRow row)
        {
            var datum = row.Cell(colVeranstaltungsDatum).GetValue<string>();
            if (datum.ToLower() == "datum") return null;

            var name = row.Cell(colRollenName).GetValue<string>();
            if (string.IsNullOrWhiteSpace(name)) return null;
            var tmp = name.Split(' ');
            if (tmp.Length > 1)
            {
                if (tmp[1] == "/") return new Rolle($"{tmp[0]}/{tmp[2]}");
                return new Rolle(tmp[0]);
            }
            return new Rolle(name);
        }

        string GetNachname(string name)
        {
            var tmp = name.Split(' ');
            if (tmp.Length > 1) return tmp[0];
            return name;
        }

        string GetVorname(string name)
        {
            var tmp = name.Split(' ');
            if (tmp.Length > 1) return tmp[1];
            return "";
        }

        #endregion

        #region VeranstaltungsImport

        public IEnumerable<Veranstaltung> ImportiereVeranstaltungen()
        {
            Veranstaltung letzteVeranstaltung = null;
            foreach (var row in WorkSheet.Rows().Skip(2))
            {
                var datum = row.Cell(colVeranstaltungsDatum).GetValue<string>();
                var name = row.Cell(colVeranstaltungsName).GetValue<string>();
                if (string.IsNullOrWhiteSpace(datum))
                {
                    if (!string.IsNullOrWhiteSpace(name) && letzteVeranstaltung != null)
                        letzteVeranstaltung.Name += " " + name;
                    continue;
                }
                if (!datum.Contains(",")) continue;

                if (string.IsNullOrWhiteSpace(name))
                {
                    if (letzteVeranstaltung != null)
                        letzteVeranstaltung.EndetAm = LeseBeginn(datum);
                    continue;
                }

                var beginntAm = LeseBeginn(datum);
                var veranstaltung = new Veranstaltung(name, beginntAm, beginntAm);
                LesePosten(row, veranstaltung);
                letzteVeranstaltung = veranstaltung;
                yield return veranstaltung;
            }
        }

        void LesePosten(IXLRow startRow, Veranstaltung veranstaltung)
        {
            for (var row = startRow; row != null; row = row.RowBelow())
            {
                var vonbis = row.Cell(colVeranstaltungsZeit).GetValue<string>();
                if (string.IsNullOrWhiteSpace(vonbis)) return;

                var startZeit = LeseStartZeit(vonbis, veranstaltung.BeginntAm);
                var endeZeit = LeseEndeZeit(vonbis, veranstaltung.BeginntAm);
                if (endeZeit < startZeit) endeZeit = endeZeit.AddDays(1);
                var rolle = LeseRolle(row);
                var standort = LeseStandort(row);
                var posten = new Posten(startZeit, endeZeit, rolle, standort);
                veranstaltung.Posten.Add(posten);
            }
        }

        Standort LeseStandort(IXLRow row)
        {
            var result = "";
            var name = row.Cell(colStandortName).GetValue<string>();
            if (string.IsNullOrWhiteSpace(name)) return null;
            var tmp = name.Split(' ');
            var startPos = 1;
            if (tmp.Length > 1 && tmp[1] == "/") startPos = 3;
            while (startPos < tmp.Length)
                result += tmp[startPos++];
            return new Standort(result);
        }


        DateTime LeseStartZeit(string vonbis, DateTime datum)
        {
            var content = vonbis.Split('-');
            if( content.Length<2) return DateTime.MinValue;
            LeseUhrzeit(content[0], out var stunde, out var minute);
            return new DateTime(datum.Year, datum.Month, datum.Day, stunde, minute, 0);
        }

        DateTime LeseEndeZeit(string vonbis, DateTime datum)
        {
            var content = vonbis.Split('-');
            if( content.Length<2) return DateTime.MinValue;
            LeseUhrzeit(content[1], out var stunde, out var minute);
            return new DateTime(datum.Year, datum.Month, datum.Day, stunde, minute, 0);
        }

        void LeseUhrzeit(string buffer, out int stunde, out int minute)
        {
            stunde = minute = 0;
            var content = buffer.Split('.');
            if (content.Length > 1)
            {
                int.TryParse(content[0], out stunde);
                int.TryParse(content[1], out minute);
            }
        }
        #endregion

        DateTime LeseBeginn(string datum)
        {
            var content = datum.Split(',');
            if (content.Length < 2) return DateTime.MinValue;
            var digits = content[1].Trim().Split('.');
            if (digits.Length < 2) return DateTime.MinValue;
            if (!int.TryParse(digits[0], out int day)) return DateTime.MinValue;
            if (!int.TryParse(digits[1], out int month)) return DateTime.MinValue;
            return new DateTime(DateTime.Now.Year, month, day);
        }
        /*
        void ImportVorabplanung(IXLWorksheet ws)
        {
            System.Diagnostics.Debug.WriteLine("Import vorab start");
            int line = 1;
            foreach (var row in ws.Rows().Skip(2))
            {
                System.Diagnostics.Debug.WriteLine($"Row {line++}");
                var veranstaltungsName = row.Cell(colVeranstaltungsName).GetValue<string>();
                if (!string.IsNullOrWhiteSpace(veranstaltungsName))
                {
                    //System.Diagnostics.Debug.WriteLine("Import onnext start");
                    _veranstaltungen.Add(new Veranstaltung(veranstaltungsName));
                    //System.Diagnostics.Debug.WriteLine("Import onnext end");
                }
                //System.Diagnostics.Debug.WriteLine(cell);

            }
            System.Diagnostics.Debug.WriteLine("Import vorab complete");
            //_veranstaltungen.OnCompleted();
            //_personen.OnCompleted();
            System.Diagnostics.Debug.WriteLine("Import vorab end");
        }
        */
        bool IstVorabPlanung(IXLWorksheet ws)
        {
            var row = ws.Row(1);
            var cell = row.Cell(1);
            var value = cell.GetValue<string>().ToLowerInvariant();
            return value.StartsWith("ordnungsdienst");
        }
    }
}