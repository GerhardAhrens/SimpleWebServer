namespace SimpleWebServer.WebFunction.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class SmartHomeAktorApi : IRestApi
    {
        public void Register(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/smarthomeaktor",
                (SmartHomeAktorService service) =>
                {
                    return Results.Ok(service.Current);
                });
        }
    }
}
