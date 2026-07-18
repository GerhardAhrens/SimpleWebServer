namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class TimeService
    {
        public DateTime CurrentTime => DateTime.Now;

        public string CurrentTimeText => DateTime.Now.ToString("HH:mm:ss", CultureInfo.CurrentCulture);
    }
}
