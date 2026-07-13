//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Lifeprojects.de">
//     Class: Program
//     Copyright © Lifeprojects.de 2026
// </copyright>
// <Template>
// 	Version 3.0.2026.2, 15.04.2026
// </Template>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>13.07.2026 15:20:09</date>
//
// <summary>
// Konsolen Applikation mit Menü
// </summary>
//-----------------------------------------------------------------------

namespace SimpleWebServer
{
    /* Imports from NET Framework */
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using SimpleWebServer.WebFunction;

    using System;

    public class Program
    {
        public Program()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
        }

        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<HelloWorldService>();

            builder.Services.AddSingleton<IRestApi, HelloApi>();
            builder.Services.AddSingleton<ApiRegistry>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            var registry = app.Services.GetRequiredService<ApiRegistry>();
            registry.Register(app);

            app.Run();
        }
    }
}
