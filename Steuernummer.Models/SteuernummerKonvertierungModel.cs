namespace SteuernummerKonverter.Models
{
    public class SteuernummerKonvertierungModel
    {
        public string InputSteuernummer { get; set; }
        public string InputBundesland { get; set; }
        public bool IsSuccessful { get; set; }
        public string OutputSteuernummer { get; set; }
    }
}
