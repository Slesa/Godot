using System.Reactive;
using ReactiveUI;

namespace pos.avalonia.ViewModels
{
    public class OffenerTischViewModel : ViewModelBase
    {
        public OffenerTischViewModel()
        {
            Anzahl = "Anzahl";
            Plu = "PLU";

            SetzeAnzahlCommand = ReactiveCommand.Create(SetzeAnzahl);
            SetzePluCommand = ReactiveCommand.Create(SetzePlu);
            BestelleCommand = ReactiveCommand.Create(Bestelle);
            StorniereCommand = ReactiveCommand.Create(Storniere);
        }

        string _eingabe;
        public string Eingabe
        {
            get => _eingabe;
            set => this.RaiseAndSetIfChanged(ref _eingabe, value);
        }

        string _anzahl;
        public string Anzahl
        {
            get => _anzahl;
            set => this.RaiseAndSetIfChanged(ref _anzahl, value);
        }

        string _plu;
        public string Plu
        {
            get => _plu;
            set => this.RaiseAndSetIfChanged(ref _plu, value);
        }

        public ReactiveCommand<Unit, Unit> SetzeAnzahlCommand { get; }
        public ReactiveCommand<Unit, Unit> SetzePluCommand { get; }
        public ReactiveCommand<Unit, Unit> BestelleCommand { get; }
        public ReactiveCommand<Unit, Unit> StorniereCommand { get; }

        void SetzeAnzahl()
        {
            if (uint.TryParse(Eingabe, out var anzahl))
            {
                Anzahl = $"Anzahl: {anzahl}";
                Eingabe  = "";
            }
        }

        void SetzePlu()
        {
            if (uint.TryParse(Eingabe, out var plu))
            {
                Plu = $"PLU: {plu}";
                Eingabe = "";
            }
        }

        void Bestelle()
        {
            System.Diagnostics.Debug.WriteLine("Bestelle");
        }
        void Storniere()
        {
            System.Diagnostics.Debug.WriteLine("Storniere");
        }
    }
}