namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Member als statisch markieren", Justification = "<Ausstehend>")]
    public class FritzSmartHomeService
    {
        public FritzSmartHome GetFritzSmartHome()
        {
            return new FritzSmartHome
            {
                CurrentPower = string.Empty,
                CurrentVolt = string.Empty,
                CurrentTemparatur = string.Empty,
            };
        }
    }
}
