using SteuernummerKonverter.Models;
using SteuernummerKonverter.DesktopClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace SteuernummerKonverter.DesktopClient
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        readonly IClientService service;
        private bool loading = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<SteuernummerKonvertierungModel> Log { get; set; }
        public List<string> Bundesländer { get; set; }
        private string bundesland;
        public string Bundesland
        {
            get
            {
                return bundesland;
            }
            set
            {
                bundesland = value;
                OnPropertyChanged("Bundesland");
            }
        }
        private string steuernummer;
        public string Steuernummer
        {
            get
            {
                return steuernummer;
            }
            set
            {
                steuernummer = value;
                OnPropertyChanged("Steuernummer");
            }
        }
        private string statustext;
        public string Statustext
        {
            get
            {
                return statustext;
            }
            set
            {
                statustext = value;
                OnPropertyChanged("Statustext");
            }
        }

        public ICommand OnKonvertierenCommand { get; set; }

        public MainWindowVM()
        {
            service = new ClientService();
            Bundesländer = CreateBundeslandListe();
            Log = new ObservableCollection<SteuernummerKonvertierungModel>();
            OnKonvertierenCommand = new RelayCommand
                (
                    async () => await OnKonvertieren(),
                    () => !string.IsNullOrEmpty(Steuernummer) && !string.IsNullOrEmpty(Bundesland) && !loading
                );
        }

        void SetWaitIndicator(string text, bool isActive)
        {
            Statustext = text;
            loading = isActive;
        }

        async Task OnKonvertieren()
        {
            SetWaitIndicator("Lade...", true);
            var result = await service.SendKonvertierungsRequest(Steuernummer, Bundesland);
            SetWaitIndicator(string.Empty, false);

            Log.Add(result);
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        List<string> CreateBundeslandListe()
        {
            return new List<string>
            {
                "Baden Württemberg", "Bayern", "Berlin", "Brandenburg", "Bremen", "Hamburg",
                "Hessen", "Mecklenburg Vorpommern", "Niedersachsen", "Nordrhein Westfalen", "Rheinland Pfalz","Saarland",
                "Sachsen", "Sachsen Anhalt", "Schleswig Holstein", "Thüringen"
            };
        }
    }
}
