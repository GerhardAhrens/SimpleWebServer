namespace SimpleWebServer.WebFunction
{
    using System;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Member als statisch markieren", Justification = "<Ausstehend>")]
    public class SystemService
    {
        public string MachineName => Environment.MachineName;
    }
}
