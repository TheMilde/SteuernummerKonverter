using SteuernummerKonverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SteuernummerKonverter.DesktopClient.Services
{
    public class ClientService : IClientService
    {
        readonly static HttpClient client = new HttpClient();

        public ClientService()
        {
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<SteuernummerKonvertierungModel> SendKonvertierungsRequest(string steuernummer, string bundesland)
        {
            var t = new SteuernummerKonvertierungModel
            {
                InputSteuernummer = steuernummer,
                InputBundesland = bundesland,
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("Steuernummer/Konvertieren", t);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<SteuernummerKonvertierungModel>();
        }
    }
}
