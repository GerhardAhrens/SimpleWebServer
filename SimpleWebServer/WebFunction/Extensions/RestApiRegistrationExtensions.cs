//-----------------------------------------------------------------------
// <copyright file="RestApiRegistrationExtensions.cs" company="Lifeprojects.de">
//     Class: RestApiRegistrationExtensions
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

namespace System.Windows
{
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;

    using SimpleWebServer.WebFunction;

    public static class RestApiRegistrationExtensions
    {
        public static IServiceCollection RegisterRestApis(
            this IServiceCollection services)
        {
            var apiTypes = typeof(IRestApi).Assembly
                .GetTypes()
                .Where(t =>
                    typeof(IRestApi).IsAssignableFrom(t) &&
                    !t.IsInterface &&
                    !t.IsAbstract);

            foreach (var apiType in apiTypes)
            {
                services.AddSingleton(typeof(IRestApi), apiType);
            }

            return services;
        }
    }
}