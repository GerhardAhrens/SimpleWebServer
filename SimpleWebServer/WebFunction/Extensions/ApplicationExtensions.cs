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

            ApiRegistry registry = app.Services.GetRequiredService<ApiRegistry>();

            registry.Register(app);

            WebServerConfiguration config = app.Services.GetRequiredService<WebServerConfiguration>();

            Console.WriteLine();
            Console.Line();
            Console.WriteText("Simple Web Server", ConsoleColor.Yellow);
            Console.Line();
            Console.WriteText($"Port      : {config.Port}", ConsoleColor.Green);
            Console.WriteText($"Localhost : {config.LocalhostOnly}", ConsoleColor.Green);
            Console.Line();
            Console.WriteLine();

            return app;
        }
    }
}