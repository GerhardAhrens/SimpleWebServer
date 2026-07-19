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

    public record HelloResponse(string Text, DateTime Time);
    public record HelloRequest(string Text);

    public class HelloApi : IRestApi
    {
        public void Register(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/hello",
                (HelloWorldService service) =>
                {
                    return Results.Ok(
                        new HelloResponse(
                            service.Text,
                            DateTime.Now));
                });

            endpoints.MapPost("/api/hello",
                (HelloRequest request,
                 HelloWorldService service) =>
                {
                    service.SetText(request.Text);

                    return Results.Ok();
                });
        }
    }
}
