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

        /// <summary>
        /// Basisverzeichnis der Bilder.
        /// </summary>
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>
        /// Aktualisierungsintervall für die Bildüberwachung.
        /// </summary>
        public int ImageRefreshSeconds { get; set; } = 2;

        /// <summary>
        /// Breite der erzeugten Vorschaubilder.
        /// </summary>
        public int ThumbnailWidth { get; set; } = 200;

        /// <summary>
        /// Höhe der erzeugten Vorschaubilder.
        /// </summary>
        public int ThumbnailHeight { get; set; } = 150;
    }
}
