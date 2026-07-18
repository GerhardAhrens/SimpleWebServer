namespace SimpleWebServer.WebFunction
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    using System;
    using System.Globalization;

    public class TimeApi : IRestApi
    {
        private readonly TimeService _service;

        public TimeApi(TimeService service)
        {
            this._service = service;
        }

        public void Register(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/time", () =>
            {
                return Results.Ok(new
                {
                    Time = this._service.CurrentTime.ToString("HH:mm:ss",CultureInfo.CurrentCulture)
                });
            });
        }
    }
}
