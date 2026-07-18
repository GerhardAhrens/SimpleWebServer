//-----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Lifeprojects.de">
//     Class: ServiceCollectionExtensions
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
    using System.Net;
    using System.Windows;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSimpleWebServer(this IServiceCollection services, WebApplicationBuilder builder)
        {
            //----------------------------------------------------------
            // Konfiguration
            //----------------------------------------------------------

            services.Configure<WebServerConfiguration>(builder.Configuration.GetSection(WebServerConfiguration.SectionName));

            //----------------------------------------------------------
            // SignalR
            //----------------------------------------------------------

            services.AddSignalR();

            //----------------------------------------------------------
            // Kestrel konfigurieren
            //----------------------------------------------------------

            builder.WebHost.ConfigureKestrel((context, options) =>
            {
                var config = context.Configuration.GetSection(WebServerConfiguration.SectionName).Get<WebServerConfiguration>() ?? new();

                if (config.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    options.ListenLocalhost(config.Port);
                }
                else if (config.Host.Equals("*"))
                {
                    options.ListenAnyIP(config.Port);
                }
                else if (config.Host.Equals("self"))
                {
                    var localIP = string.Join(":", NetworkHelper.GetLocalIPv4Addresses());
                    options.Listen(System.Net.IPAddress.Parse(localIP), config.Port);
                }
                else
                {
                    options.Listen(System.Net.IPAddress.Parse(config.Host), config.Port);
                }
            });

            //----------------------------------------------------------
            // Services registrieren
            //----------------------------------------------------------

            services.AddHostedService<ClockService>();
            services.AddHostedService<NotificationService>();

            services.AddSingleton<HelloWorldService>();
            services.AddSingleton<SystemService>();
            services.AddSingleton<TimeService>();

            //----------------------------------------------------------
            // Alle REST-APIs automatisch registrieren
            //----------------------------------------------------------

            services.RegisterRestApis();

            return services;
        }
    }
}