using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        readonly IPersonRepository _personRepository;
        readonly IRolleRepository _rolleRepository;
        readonly IVeranstaltungRepository _veranstaltungRepository;
        readonly ISchichtRepository _schichtRepository;
        const string ImportDir = "import";
        readonly FileSystemWatcher _watcher;
        readonly Dispatcher _dispatcher;

        public ImportViewModel(IEventAggregator eventAggregator, IPersonRepository personRepository, IRolleRepository rolleRepository, IVeranstaltungRepository veranstaltungRepository, ISchichtRepository schichtRepository)
        {
            _eventAggregator = eventAggregator;
            _personRepository = personRepository;
            _rolleRepository = rolleRepository;
            _veranstaltungRepository = veranstaltungRepository;
            _schichtRepository = schichtRepository;

            _dispatcher = Dispatcher.CurrentDispatcher;
            ImportFiles = new ObservableCollection<ImportFile>();
            ImportFiles.AddRange( GetImportFiles() );

            ImportCommand = new DelegateCommand<ImportFile>(DoImport);

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

        public ObservableCollection<ImportFile> ImportFiles { get; }

        public DelegateCommand<ImportFile> ImportCommand { get; }
        void DoImport(ImportFile file)
        {
            var importer = new ExcelImporter(file.Fullname);
            var rollen = new List<Rolle>();

            var personen = importer.ImportierePersonen().Distinct();
            foreach (var person in personen)
            {
                rollen.AddRange(person.EinsetzbarAls);
                _personRepository.Add(person);
            }
            foreach (var rolle in rollen.Distinct())
                _rolleRepository.Add(rolle);

            var veranstaltungen = importer.ImportiereVeranstaltungen().ToList();
            foreach (var veranstaltung in veranstaltungen)
                _veranstaltungRepository.Add(veranstaltung);

            foreach (var veranstaltung in veranstaltungen)
            {
                foreach (var aufgabe in veranstaltung.Aufgaben)
                {
                    var schicht = new Schicht { Aufgabe = aufgabe, Veranstaltung = veranstaltung };
                    _schichtRepository.Add(schicht);
                }
            }
            _eventAggregator.GetEvent<ReloadDataEvent>().Publish();
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
    }
}