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
                var configuration = context.Configuration.GetSection(WebServerConfiguration.SectionName).Get<WebServerConfiguration>() ?? new();

                if (Environment.UserDomainName == "PTA")
                {
                    configuration.Host = "localhost";
                }

                if (configuration.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    options.ListenLocalhost(configuration.Port);
                }
                else if (configuration.Host.Equals("*", StringComparison.OrdinalIgnoreCase))
                {
                    options.ListenAnyIP(configuration.Port);
                }
                else if (configuration.Host.Equals("self",StringComparison.OrdinalIgnoreCase))
                {
                    var localIP = string.Join(":", NetworkHelper.GetLocalIPv4Addresses().Last());
                    options.Listen(System.Net.IPAddress.Parse(localIP), configuration.Port);
                }
                else
                {
                    options.Listen(System.Net.IPAddress.Parse(configuration.Host), configuration.Port);
                }
            });

            //----------------------------------------------------------
            // Services registrieren
            //----------------------------------------------------------

            WebServerConfiguration configuration = builder.Configuration.GetSection(WebServerConfiguration.SectionName).Get<WebServerConfiguration>() ?? new();
            services.AddSingleton(configuration);

            services.AddHostedService<ClockService>();
            services.AddHostedService<SmartHomeAktorFileService>();
            services.AddHostedService<HelloNotificationService>();
            services.AddHostedService<SmartHomeNotificationService>();
            services.AddHostedService<ImageNotificationService>();

            services.AddSingleton<HelloWorldService>();
            services.AddSingleton<SystemService>();
            services.AddSingleton<TimeService>();
            services.AddSingleton<SmartHomeAktorService>();
            services.AddSingleton<ImageService>();

            //----------------------------------------------------------
            // Alle REST-APIs automatisch registrieren
            //----------------------------------------------------------

            services.RegisterRestApis();

            return services;
        }
    }
}