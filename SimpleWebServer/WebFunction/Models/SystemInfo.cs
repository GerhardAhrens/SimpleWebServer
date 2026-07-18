namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SystemInfo
    {
        public string MachineName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string OperatingSystem { get; set; } = string.Empty;

        public string DotNetVersion { get; set; } = string.Empty;

        public int ProcessorCount { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan UpTime { get; set; }

        public List<string> IpAddresses { get; set; } = [];
    }
}
