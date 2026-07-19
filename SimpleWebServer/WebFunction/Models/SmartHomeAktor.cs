namespace SimpleWebServer.WebFunction
{
    public class SmartHomeAktor
    {
        public string CurrentPower { get; set; } = "";

        public string CurrentVolt { get; set; } = "";

        public string CurrentTemperature { get; set; } = "";

        public override bool Equals(object obj)
        {
            if (obj is not SmartHomeAktor other)
                return false;

            return CurrentPower == other.CurrentPower
                && CurrentVolt == other.CurrentVolt
                && CurrentTemperature == other.CurrentTemperature;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                CurrentPower,
                CurrentVolt,
                CurrentTemperature);
        }
    }
}
