using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using PersonalPlanung.Core.Model;
using PersonalPlanung.Core.Repositories;
using PersonalPlanung.Excel;
using Prism.Commands;
using Prism.Events;

namespace PersonalPlanung.Gui.ViewModels
{
    [DebuggerDisplay("{Fullname} ({Filename}) - {Changed}")]
    public class ImportFile
    {
        public ImportFile(string fullname, string filename, DateTime changed)
        {
            Fullname = fullname;
            Filename = filename;
            Changed = changed;
        }

        public string Fullname { get; }
        public string Filename { get; }
        public DateTime Changed { get; }
    }

    public class ImportViewModel
    {
        readonly IEventAggregator _eventAggregator;
        readonly IBerufRepository _berufRepository;
        readonly IPersonRepository _personRepository;
        readonly IRolleRepository _rolleRepository;
        readonly IVeranstaltungRepository _veranstaltungRepository;
        readonly ISchichtRepository _schichtRepository;
        const string ImportDir = "import";
        readonly FileSystemWatcher _watcher;
        readonly Dispatcher _dispatcher;

        public ImportViewModel(IEventAggregator eventAggregator, IBerufRepository berufRepository, IPersonRepository personRepository, IRolleRepository rolleRepository, IVeranstaltungRepository veranstaltungRepository, ISchichtRepository schichtRepository)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _eventAggregator = eventAggregator;
            _berufRepository = berufRepository;
            _personRepository = personRepository;
            _rolleRepository = rolleRepository;
            _veranstaltungRepository = veranstaltungRepository;
            _schichtRepository = schichtRepository;

            ImportFiles = new ObservableCollection<ImportFile>();
            ImportFiles.AddRange( GetImportFiles() );

            ImportCommand = new DelegateCommand<ImportFile>(DoImport, CanImport);

            _watcher = new FileSystemWatcher
            {
                Path = ImportDir,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                EnableRaisingEvents = true
            };
            _watcher.Created += OnFileCreated;
            _watcher.Renamed += OnFileRenamed;
            _watcher.Deleted += OnFileDeleted;
        }

        bool _currentlyImporting;
        public bool CurrentlyImporting
        {
            get => _currentlyImporting;
            set
            {
                _currentlyImporting = value;
                ImportCommand.RaiseCanExecuteChanged();
            }
        }

        bool CanImport(ImportFile arg)
        {
            return !CurrentlyImporting;
        }

        public ObservableCollection<ImportFile> ImportFiles { get; }

        public DelegateCommand<ImportFile> ImportCommand { get; }
        void DoImport(ImportFile file)
        {
            CurrentlyImporting = true;

            Task.Factory.StartNew(() =>
            {
                var importer = new ExcelImporter(file.Fullname);

                ImportBerufe();
                ImportPersonenUndRollen(importer);
                ImportVeranstaltungenUndAufgaben(importer);

                _eventAggregator.GetEvent<ReloadDataEvent>().Publish();
                CurrentlyImporting = false;
            });
        }

        void ImportVeranstaltungenUndAufgaben(ExcelImporter importer)
        {
            //var neueVeranstaltungen = new List<Veranstaltung>();
            var veranstaltungen = importer.ImportiereVeranstaltungen().ToList();
            foreach (var veranstaltung in veranstaltungen)
            {
                if (!_veranstaltungRepository.Contains(veranstaltung))
                {
                    //neueVeranstaltungen.Add(veranstaltung);
                    _veranstaltungRepository.Add(veranstaltung);
                }
            }

            foreach (var veranstaltung in veranstaltungen)
            {
                foreach (var aufgabe in veranstaltung.Aufgaben)
                {
                    var schicht = new Schicht {Aufgabe = aufgabe, Veranstaltung = veranstaltung};
                    _schichtRepository.Add(schicht);
                }
            }
        }

        void ImportPersonenUndRollen(ExcelImporter importer)
        {
            var rollen = new List<Rolle>();
            var personen = importer.ImportierePersonen();
            foreach (var person in personen.Distinct())
            {
                rollen.AddRange(person.EinsetzbarAls);
                if (!_personRepository.Contains(person))
                    _personRepository.Add(person);
            }

            foreach (var rolle in rollen.Distinct())
            {
                if (!_rolleRepository.Contains(rolle))
                    _rolleRepository.Add(rolle);
            }
        }

        void ImportBerufe()
        {
            foreach (var beruf in GetPossibleBeruf())
            {
                if (!_berufRepository.Contains(beruf))
                    _berufRepository.Add(beruf);
            }
        }


        void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            var fi = new FileInfo(e.FullPath);
            _dispatcher.Invoke( () => ImportFiles.Add(new ImportFile(fi.FullName, fi.Name, fi.LastWriteTime)) );
        }

        void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            var oldFi = new FileInfo(e.OldFullPath);
            var file = ImportFiles.FirstOrDefault(x => x.Fullname == oldFi.FullName);
            _dispatcher.Invoke( () => ImportFiles.Remove(file) );
            var fi = new FileInfo(e.FullPath);
            _dispatcher.Invoke( () => ImportFiles.Add(new ImportFile(fi.FullName, fi.Name, fi.LastWriteTime)) );
        }

        void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            var fi = new FileInfo(e.FullPath);
            _dispatcher.Invoke( () => ImportFiles.Remove(ImportFiles.FirstOrDefault(x => x.Fullname == fi.FullName)) );
        }


        IEnumerable<ImportFile> GetImportFiles()
        {
            var di = new DirectoryInfo(ImportDir);
            if( !di.Exists ) di.Create();
            return di.GetFiles().Select(fi => new ImportFile(fi.FullName, fi.Name, fi.LastWriteTime));
        }

        IEnumerable<Beruf> GetPossibleBeruf()
        {
            yield return Beruf.Rentner;
            yield return Beruf.Student;
            yield return Beruf.Kollege;
            yield return Beruf.Dienstleister;
        }

    }
}