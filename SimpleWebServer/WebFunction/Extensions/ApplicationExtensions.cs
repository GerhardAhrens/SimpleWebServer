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
    using System.Globalization;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class ApplicationExtensions
    {
        public static WebApplication UseSimpleWebServer(this WebApplication app)
        {
            var configuration = app.Services.GetRequiredService<IOptions<WebServerConfiguration>>().Value;

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (configuration.DisableBrowserCache == true)
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
                        ctx.Context.Response.Headers.Pragma = "no-cache";
                        ctx.Context.Response.Headers.Expires = "0";
                    }
                });
            }
            else
            {
                app.UseStaticFiles();
            }

            //----------------------------------------------------------
            // Alle REST-APIs registrieren
            //----------------------------------------------------------

            foreach (var api in app.Services.GetServices<IRestApi>())
            {
                api.Register(app);
            }

            app.MapHub<WebServerHub>("/hub/webserver");

            //----------------------------------------------------------
            // Konsolenausgabe
            //----------------------------------------------------------

            var system = app.Services.GetRequiredService<SystemService>();

            var info = system.GetSystemInfo();

            Console.WriteLine();
            Console.Line();
            Console.WriteText("Simple Web Server", ConsoleColor.Yellow);
            Console.Line();
            Console.WriteText($"Computer : {info.MachineName}");
            Console.WriteText($"Benutzer : {info.UserName}");
            Console.WriteText($"OS        : {info.OperatingSystem}");
            Console.WriteText($".NET      : {info.DotNetVersion}");
            Console.Line();
            Console.WriteText("Erreichbar unter:");

            if (configuration.Host.ToLower(CultureInfo.CurrentCulture) == "localhost".ToLower(CultureInfo.CurrentCulture))
            {
                Console.WriteText($"LocalHost : {configuration.Host}:{configuration.Port}", ConsoleColor.Green);
            }
            else
            {
                if (configuration.Host.Equals("self"))
                {
                    var localIP = string.Join(":", NetworkHelper.GetLocalIPv4Addresses());
                    Console.WriteText($"Localhost : {localIP}:{configuration.Port}", ConsoleColor.Green);
                }
                else
                {
                    Console.WriteText($"Host : {configuration.Host}:{configuration.Port}", ConsoleColor.Green);
                }
            }

            Console.Line();
            Console.WriteLine();

            return app;
        }
    }
}