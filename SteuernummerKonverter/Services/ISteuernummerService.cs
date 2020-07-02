using SteuernummerKonverter.Models;

namespace SteuernummerKonverter.WebAPI.Services
{
    public interface ISteuernummerService
    {
        SteuernummerKonvertierungModel ConvertSteuernummer(SteuernummerKonvertierungModel steuernummerModel);
    }
}
