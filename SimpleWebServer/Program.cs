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
// Web Server initalisieren und starten
// </summary>
//-----------------------------------------------------------------------

namespace SimpleWebServer
{
    using System;
    /* Imports from NET Framework */
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using SimpleWebServer.WebFunction;

    public class Program
    {
        public Program()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
        }

        private static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddSimpleWebServer(builder);

                var app = builder.Build();

                /*
                var hostedServices = app.Services.GetServices<IHostedService>();
                foreach (var service in hostedServices)
                {
                    Console.WriteLine(service.GetType().FullName);
                }
                */

                app.UseSimpleWebServer();

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteError(ex.Message);
                Console.Wait();
            }
        }
    }
}
