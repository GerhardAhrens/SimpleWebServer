namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Member als statisch markieren", Justification = "<Ausstehend>")]
    public class SystemService
    {
        private readonly DateTime _startTime = DateTime.Now;

        public SystemInfo GetSystemInfo()
        {
            return new SystemInfo
            {
                MachineName = Environment.MachineName,

                UserName = Environment.UserName,

                OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription,

                DotNetVersion = Environment.Version.ToString(),

                ProcessorCount = Environment.ProcessorCount,

                StartTime = this._startTime,

                UpTime = DateTime.Now - this._startTime,

                IpAddresses = GetIPv4Addresses()
            };
        }

        private static List<string> GetIPv4Addresses()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(n =>
                    n.OperationalStatus == OperationalStatus.Up &&
                    n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties().UnicastAddresses)
                .Select(a => a.Address)
                .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                .Select(ip => ip.ToString())
                .ToList();
        }
    }
}
