using Microsoft.AspNetCore.Mvc;
using SteuernummerKonverter.Models;
using SteuernummerKonverter.WebAPI.Services;

namespace SteuernummerKonverter.WebAPI.Controllers
{
    [ApiController]
    [Route("Steuernummer/")]
    public class SteuernummerController : ControllerBase
    {
        private readonly ISteuernummerService steuernummerService;

        public SteuernummerController(ISteuernummerService steuernummerService)
        {
            this.steuernummerService = steuernummerService;
        }

        [HttpPost]
        [Route("Konvertieren")]
        public ActionResult<SteuernummerKonvertierungModel> ConvertSteuernummer(SteuernummerKonvertierungModel steuernummerModel)
        {
            var convertedSteuernummer = steuernummerService?.ConvertSteuernummer(steuernummerModel);

            if (convertedSteuernummer == null)
            {
                return NotFound();
            }

            return convertedSteuernummer;
        }
    }
}
