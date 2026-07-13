//-----------------------------------------------------------------------
// <copyright file="IRestApi.cs" company="Lifeprojects.de">
//     Class: IRestApi
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>13.07.2026</date>
//
// <summary>
// Template für eine neue Enum-Klasse
// </summary>
//-----------------------------------------------------------------------

namespace SimpleWebServer.WebFunction
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public interface IRestApi
    {
        void Register(IEndpointRouteBuilder endpoints);
    }
}
