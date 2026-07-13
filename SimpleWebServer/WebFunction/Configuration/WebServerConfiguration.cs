namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WebServerConfiguration
    {
        /// <summary>
        /// TCP-Port des Webservers
        /// </summary>
        public int Port { get; set; } = 8080;

        /// <summary>
        /// Nur lokale Zugriffe erlauben
        /// false = Netzwerkzugriff erlauben
        /// </summary>
        public bool LocalhostOnly { get; set; }
    }
}
