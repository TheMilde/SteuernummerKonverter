using SteuernummerKonverter.Models;

namespace SteuernummerKonverter.WebAPI.Services
{
    public class SteuernummerService : ISteuernummerService
    {
        enum Bundesland
        {
            Nicht_Gefunden,
            Baden_Württemberg,
            Bayern,
            Berlin,
            Brandenburg,
            Bremen,
            Hamburg,
            Hessen,
            Mecklenburg_Vorpommern,
            Niedersachsen,
            Nordrhein_Westfalen,
            Rheinland_Pfalz,
            Saarland,
            Sachsen,
            Sachsen_Anhalt,
            Schleswig_Holstein,
            Thüringen,
        }

        public SteuernummerKonvertierungModel ConvertSteuernummer(SteuernummerKonvertierungModel steuernummerModel)
        {
            var isValid = CheckValidität(steuernummerModel);
            if (isValid)
            {
                var bundesland = BestimmeBundesland(steuernummerModel.InputBundesland);
                var convertedSteuernummer = ConvertSteuernummer(bundesland, steuernummerModel.InputSteuernummer);

                return new SteuernummerKonvertierungModel
                {
                    OutputSteuernummer = convertedSteuernummer,
                    InputSteuernummer = steuernummerModel?.InputSteuernummer,
                    InputBundesland = steuernummerModel?.InputBundesland,
                    IsSuccessful = bundesland != Bundesland.Nicht_Gefunden && !string.IsNullOrWhiteSpace(convertedSteuernummer),
                };
            }
            else
            {
                return new SteuernummerKonvertierungModel
                {
                    InputSteuernummer = steuernummerModel?.InputSteuernummer,
                    InputBundesland = steuernummerModel?.InputBundesland,
                    IsSuccessful = false,
                };
            }
        }

        private bool CheckValidität(SteuernummerKonvertierungModel steuernummerModel)
        {
            var isNull = steuernummerModel == null;
            if (isNull)
            {
                return false;
            }

            var isSteuernummerEmpty = string.IsNullOrWhiteSpace(steuernummerModel.InputSteuernummer);
            var isBundeslandEmpty = string.IsNullOrWhiteSpace(steuernummerModel.InputBundesland);
            if (isSteuernummerEmpty || isBundeslandEmpty)
            {
                return false;
            }

            var isTooShort = steuernummerModel.InputSteuernummer.Length < 10;
            var isTooLong = steuernummerModel.InputSteuernummer.Length > 13;
            if (isTooShort || isTooLong)
            {
                return false;
            }

            return true;
        }

        private Bundesland BestimmeBundesland(string bundesland)
        {
            var cleanedBundesland = bundesland.Trim().ToLower();
            switch (cleanedBundesland)
            {
                case "badenwürttemberg":
                case "baden-württemberg":
                case "baden_württemberg":
                case "baden württemberg":
                case "badenwuerttemberg":
                case "baden-wuerttemberg":
                case "baden_wuerttemberg":
                case "baden wuerttemberg":
                    return Bundesland.Baden_Württemberg;
                case "bayern":
                    return Bundesland.Bayern;
                case "berlin":
                    return Bundesland.Berlin;
                case "brandenburg":
                    return Bundesland.Brandenburg;
                case "bremen":
                    return Bundesland.Bremen;
                case "hamburg":
                    return Bundesland.Hamburg;
                case "hessen":
                    return Bundesland.Hessen;
                case "mecklenburgvorpommern":
                case "mecklenburg-vorpommern":
                case "mecklenburg_vorpommern":
                case "mecklenburg vorpommern":
                    return Bundesland.Mecklenburg_Vorpommern;
                case "niedersachsen":
                    return Bundesland.Niedersachsen;
                case "nordrheinwestfalen":
                case "nordrhein-westfalen":
                case "nordrhein_westfalen":
                case "nordrhein westfalen":
                    return Bundesland.Nordrhein_Westfalen;
                case "rheinlandpfalz":
                case "rheinland-pfalz":
                case "rheinland_pfalz":
                case "rheinland pfalz":
                    return Bundesland.Rheinland_Pfalz;
                case "saarland":
                    return Bundesland.Saarland;
                case "sachsen":
                    return Bundesland.Sachsen;
                case "sachsenanhalt":
                case "sachsen-anhalt":
                case "sachsen_anhalt":
                case "sachsen anhalt":
                    return Bundesland.Sachsen_Anhalt;
                case "schleswigholstein":
                case "schleswig-holstein":
                case "schleswig_holstein":
                case "schleswig holstein":
                    return Bundesland.Schleswig_Holstein;
                case "thüringen":
                case "thueringen":
                    return Bundesland.Thüringen;
                default:
                    return Bundesland.Nicht_Gefunden;
            }
        }

        private string ConvertSteuernummer(Bundesland bundesland, string inputSteuernummer)
        {
            string bundesfinanzamtsnummer;
            string bezirksnummer;
            string unterscheidungsnummer;
            string prüfziffer;

            try
            {
                switch (bundesland)
                {
                    case Bundesland.Baden_Württemberg:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(0, 2);
                        bezirksnummer = inputSteuernummer.Substring(2, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(6, 4);
                        prüfziffer = inputSteuernummer.Substring(10, 1);
                        return $"28{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Bayern:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(0, 3);
                        bezirksnummer = inputSteuernummer.Substring(4, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(8, 4);
                        prüfziffer = inputSteuernummer.Substring(11, 1);
                        return $"9{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Berlin:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(0, 2);
                        bezirksnummer = inputSteuernummer.Substring(3, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(7, 4);
                        prüfziffer = inputSteuernummer.Substring(11, 1);
                        return $"11{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Brandenburg:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(1, 2);
                        bezirksnummer = inputSteuernummer.Substring(4, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(8, 4);
                        prüfziffer = inputSteuernummer.Substring(12, 1);
                        return $"30{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Bremen:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(0, 2);
                        bezirksnummer = inputSteuernummer.Substring(2, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(5, 4);
                        prüfziffer = inputSteuernummer.Substring(9, 1);
                        return $"24{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Hamburg:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(0, 2);
                        bezirksnummer = inputSteuernummer.Substring(3, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(7, 4);
                        prüfziffer = inputSteuernummer.Substring(11, 1);
                        return $"22{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Hessen:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(1, 2);
                        bezirksnummer = inputSteuernummer.Substring(3, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(6, 4);
                        prüfziffer = inputSteuernummer.Substring(10, 1);
                        return $"26{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Mecklenburg_Vorpommern:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(1, 2);
                        bezirksnummer = inputSteuernummer.Substring(4, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(8, 4);
                        prüfziffer = inputSteuernummer.Substring(12, 1);
                        return $"40{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Niedersachsen:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(0, 2);
                        bezirksnummer = inputSteuernummer.Substring(3, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(7, 4);
                        prüfziffer = inputSteuernummer.Substring(11, 1);
                        return $"23{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Nordrhein_Westfalen:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(0, 3);
                        bezirksnummer = inputSteuernummer.Substring(4, 4);
                        unterscheidungsnummer = inputSteuernummer.Substring(9, 3);
                        prüfziffer = inputSteuernummer.Substring(12, 1);
                        return $"5{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Rheinland_Pfalz:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(0, 2);
                        bezirksnummer = inputSteuernummer.Substring(3, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(7, 4);
                        prüfziffer = inputSteuernummer.Substring(11, 1);
                        return $"27{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Saarland:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(1, 2);
                        bezirksnummer = inputSteuernummer.Substring(4, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(8, 4);
                        prüfziffer = inputSteuernummer.Substring(12, 1);
                        return $"10{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Sachsen:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(1, 2);
                        bezirksnummer = inputSteuernummer.Substring(4, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(8, 4);
                        prüfziffer = inputSteuernummer.Substring(12, 1);
                        return $"32{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Sachsen_Anhalt:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(1, 2);
                        bezirksnummer = inputSteuernummer.Substring(4, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(8, 4);
                        prüfziffer = inputSteuernummer.Substring(12, 1);
                        return $"31{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Schleswig_Holstein:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(0, 2);
                        bezirksnummer = inputSteuernummer.Substring(3, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(7, 4);
                        prüfziffer = inputSteuernummer.Substring(11, 1);
                        return $"21{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Thüringen:
                        bundesfinanzamtsnummer = inputSteuernummer.Substring(1, 2);
                        bezirksnummer = inputSteuernummer.Substring(4, 3);
                        unterscheidungsnummer = inputSteuernummer.Substring(8, 4);
                        prüfziffer = inputSteuernummer.Substring(12, 1);
                        return $"41{bundesfinanzamtsnummer}0{bezirksnummer}{unterscheidungsnummer}{prüfziffer}";

                    case Bundesland.Nicht_Gefunden:
                    default:
                        return string.Empty;
                }
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                // Log Error
                return string.Empty;
            }
        }
    }
}
