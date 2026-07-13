//-----------------------------------------------------------------------
// <copyright file="ApiRegistry.cs" company="Lifeprojects.de">
//     Class: ApiRegistry
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>13.07.2026</date>
//
// <summary>
// API Manager
// </summary>
//-----------------------------------------------------------------------

namespace SimpleWebServer.WebFunction
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Routing;

    public class ApiRegistry
    {
        private readonly IEnumerable<IRestApi> _apis;

        public ApiRegistry(IEnumerable<IRestApi> apis)
        {
            _apis = apis;
        }

        public void Register(IEndpointRouteBuilder app)
        {
            foreach (var api in _apis)
            {
                api.Register(app);
            }
        }
    }
}
