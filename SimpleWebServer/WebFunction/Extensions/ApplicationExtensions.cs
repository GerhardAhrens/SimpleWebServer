//-----------------------------------------------------------------------
// <copyright file="ApplicationExtensions.cs" company="Lifeprojects.de">
//     Class: ApplicationExtensions
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>13.07.2026</date>
//
// <summary>
// Template für eine neue Extension Klasse 
// </summary>
//-----------------------------------------------------------------------

namespace SimpleWebServer.WebFunction
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationExtensions
    {
        public static WebApplication UseSimpleWebServer(this WebApplication app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //----------------------------------------------------------
            // Alle REST-APIs registrieren
            //----------------------------------------------------------

            foreach (var api in app.Services.GetServices<IRestApi>())
            {
                api.Register(app);
            }

            //----------------------------------------------------------
            // Konsolenausgabe
            //----------------------------------------------------------

            var config = app.Services.GetRequiredService<WebServerConfiguration>();

            Console.WriteLine();
            Console.Line();
            Console.WriteText("Simple Web Server", ConsoleColor.Yellow);
            Console.Line();
            Console.WriteText($"Port      : {config.Port}", ConsoleColor.Green);
            if (config.LocalhostOnly == true)
            {
                Console.WriteText($"Localhost : {config.LocalhostOnly}", ConsoleColor.Green);
            }
            else
            {
                var localIP = string.Join(":", NetworkHelper.GetLocalIPv4Addresses());
                if (config.IpAddress == "*")
                {
                    Console.WriteText($"Localhost : {localIP}", ConsoleColor.Green);
                }
                else
                {
                    Console.WriteText($"Localhost : {config.IpAddress}", ConsoleColor.Green);
                }
            }
            Console.Line();
            Console.WriteLine();

            return app;
        }
    }
}