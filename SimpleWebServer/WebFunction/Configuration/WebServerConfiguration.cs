namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WebServerConfiguration
    {
        public const string SectionName = "WebServer";

        public int Port { get; set; } = 8080;

        public string Host { get; set; } = "localhost";

        public bool DisableBrowserCache { get; set; } = true;
    }
}
