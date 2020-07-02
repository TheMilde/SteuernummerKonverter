using SteuernummerKonverter.Models;
using SteuernummerKonverter.DesktopClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteuernummerKonverter.DesktopClient
{
    public class MainWindowVM
    {
        readonly IClientService service;

        public ObservableCollection<SteuernummerKonvertierungModel> Log { get; set; }
        public List<string> Bundesländer { get; set; }
        public string Bundesland { get; set; }
        public string Steuernummer { get; set; }

        public ICommand OnKonvertierenCommand { get; set; } 

        public MainWindowVM()
        {
            service = new ClientService();
            Bundesländer = CreateBundeslandListe();
            Log = new ObservableCollection<SteuernummerKonvertierungModel>();
            OnKonvertierenCommand = new RelayCommand<string>(x => OnKonvertieren(), x => !string.IsNullOrEmpty(Steuernummer) && !string.IsNullOrEmpty(Bundesland));
        }

        async Task OnKonvertieren()
        {
            var result = await service.SendKonvertierungsRequest(Steuernummer, Bundesland);

            Log.Add(result);
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
