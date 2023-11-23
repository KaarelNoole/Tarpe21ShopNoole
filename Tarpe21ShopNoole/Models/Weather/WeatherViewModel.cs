namespace Tarpe21ShopNoole.Models.Weather
{
    public class WeatherViewModel
    {
        public DateTime Date { get; set; }
        public int EpochDate { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        //public Temperatures Temperature { get; set; }
        //public DayNightCycles DayNightCycle { get; set; }
        //public List<string> Sources { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }

        public double TempMinValue { get; set; }
        public string TempMinUnit { get; set; }
        public int TempMinUnitType { get; set; }

        public double TempMaxValue { get; set; }
        public string TempMaxUnit { get; set; }
        public int TempMaxUnitType { get; set; }

        public int DayIcon { get; set; }
        public bool DayHasPrecipitation { get; set; }
        public string DayIconPhrase { get; set; }
        public string DayPrecipitationType { get; set; }
        public string DayPrecipitationIntesity { get; set; }

        public int NightIcon { get; set; }
        public bool NightHasPrecipitation { get; set; }
        public string NightIconPhrase { get; set; }
        public string NightPrecipitationType { get; set; }
        public string NightPrecipitationIntesity { get; set; }
    }
    public class Temperatures
    {
        public Temperature Minimum { get; set; }
        public Temperature Maximum { get; set; }
    }
    public class DayNightCycles
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool hasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public string PrecipitationIntensity { get; set; }
    }
    public class Temperature
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

}
