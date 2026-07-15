//-----------------------------------------------------------------------
// <copyright file="HelloApi .cs" company="Lifeprojects.de">
//     Class: HelloApi 
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>13.07.2026</date>
//
// <summary>
// Rest API 'Hello'
// </summary>
//-----------------------------------------------------------------------

namespace SimpleWebServer.WebFunction
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    using System;

    public record HelloRequest(string Text);

    public class HelloApi : IRestApi
    {
        private readonly HelloWorldService _service;

        public HelloApi(HelloWorldService service)
        {
            _service = service;
        }

        public void Register(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/hello", () =>
            {
                return Results.Ok(new
                {
                    Text = _service.Text,
                    Time = DateTime.Now
                });
            });

            endpoints.MapPost("/api/hello", (HelloRequest request) =>
            {
                _service.Text = request.Text;

                return Results.Ok();
            });

            endpoints.MapGet("/api/hello/set", (string text, HelloWorldService service) =>
            {
                service.Text = text;

                return Results.Ok(new
                {
                    Success = true,
                    Text = service.Text
                });
            });
        }
    }
}
