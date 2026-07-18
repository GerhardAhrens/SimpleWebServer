namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Member als statisch markieren", Justification = "<Ausstehend>")]
    public class TimeService
    {
        public DateTime CurrentTime => DateTime.Now;

        public string CurrentTimeText => DateTime.Now.ToString("HH:mm:ss", CultureInfo.CurrentCulture);
    }
}
