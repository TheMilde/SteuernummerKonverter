using System.Threading.Tasks;
using SteuernummerKonverter.Models;

namespace SteuernummerKonverter.DesktopClient.Services
{
    public interface IClientService
    {
        Task<SteuernummerKonvertierungModel> SendKonvertierungsRequest(string steuernummer, string bundesland);
    }
}
