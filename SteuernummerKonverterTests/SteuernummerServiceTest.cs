using SteuernummerKonverter.Models;
using SteuernummerKonverter.WebAPI.Services;
using System;
using Xunit;

namespace SteuernummerKonverterTests
{
    public class SteuernummerServiceTest
    {
        readonly ISteuernummerService sut = new SteuernummerService();

        [Theory]
        [InlineData("")]
        [InlineData("test")]
        [InlineData(null)]
        public void ShouldHandleInvalidBundesland(string bundesland)
        {
            var input = new SteuernummerKonvertierungModel
            {
                InputSteuernummer = "93815/08152",
                InputBundesland = bundesland,
            };

            var result = sut.ConvertSteuernummer(input);

            Assert.False(result.IsSuccessful);
        }

        [Theory]
        [InlineData("")]
        [InlineData("test")]
        [InlineData("123456789")]
        [InlineData("1234567890abcd")]
        [InlineData(null)]
        public void ShouldHandleInvalidSteuernummer(string steuernummer)
        {
            var input = new SteuernummerKonvertierungModel
            {
                InputSteuernummer = steuernummer,
                InputBundesland = "niedersachsen",
            };

            var result = sut.ConvertSteuernummer(input);

            Assert.False(result.IsSuccessful);
        }

        [Theory]
        [InlineData("93815/08152", "Baden Württemberg", "2893081508152")]
        [InlineData("181/815/08155", "Bayern", "9181081508155")]
        [InlineData("21/815/08150", "Berlin", "1121081508150")] 
        [InlineData("048/815/08155", "Brandenburg", "3048081508155")]
        [InlineData("7581508152", "Bremen", "2475081508152")]
        [InlineData("02/815/08156", "Hamburg", "2202081508156")]
        [InlineData("01381508153", "Hessen", "2613081508153")]
        [InlineData("079/815/08151", "Mecklenburg-Vorpommern", "4079081508151")]
        [InlineData("24/815/08151", "Niedersachsen", "2324081508151")]
        [InlineData("133/8150/8159", "Nordrhein-Westfalen", "5133081508159")]
        [InlineData("22/815/08154", "Rheinland-Pfalz", "2722081508154")]
        [InlineData("010/815/08182", "Saarland", "1010081508182")]
        [InlineData("201/123/12340", "Sachsen", "3201012312340")]
        [InlineData("101/815/08154", "Sachsen-Anhalt", "3101081508154")]
        [InlineData("29/815/08158", "Schleswig-Holstein", "2129081508158")]
        [InlineData("151/815/08156", "Thüringen", "4151081508156")]
        public void ShouldConvertSteuernummer(string inputSteuernummer, string bundesland, string resultSteuernummer)
        {
            var steuernummerModel = new SteuernummerKonvertierungModel
            {
                InputSteuernummer = inputSteuernummer,
                InputBundesland = bundesland,
            };

            var result = sut.ConvertSteuernummer(steuernummerModel);

            Assert.True(result.IsSuccessful);
            Assert.Equal(resultSteuernummer, result.OutputSteuernummer);
        }
    }
}
