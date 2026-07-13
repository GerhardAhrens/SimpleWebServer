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
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSimpleWebServer(this IServiceCollection services, WebApplicationBuilder builder)
        {
            //
            // Konfiguration laden
            //
            builder.Services.Configure<WebServerConfiguration>(
                builder.Configuration.GetSection("WebServer"));

            var config =
                builder.Configuration.GetSection("WebServer").Get<WebServerConfiguration>() ?? new WebServerConfiguration();

            //
            // Kestrel konfigurieren
            //
            builder.WebHost.ConfigureKestrel(options =>
            {
                if (config.LocalhostOnly)
                {
                    options.ListenLocalhost(config.Port);
                }
                else
                {
                    options.ListenAnyIP(config.Port);
                }
            });

            services.AddSingleton(config);

            //
            // Services
            //
            services.AddSingleton<HelloWorldService>();
            services.AddSingleton<SystemService>();

            //
            // REST APIs
            //
            services.AddSingleton<IRestApi, HelloApi>();
            services.AddSingleton<IRestApi, SystemApi>();

            services.AddSingleton<ApiRegistry>();

            return services;
        }
    }
}