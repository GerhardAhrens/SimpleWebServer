namespace SimpleWebServer.WebFunction
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class SystemApi : IRestApi
    {
        private readonly SystemService _service;

        public SystemApi(SystemService service)
        {
            this._service = service;
        }

        public void Register(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/system/info", () =>
            {
                return Results.Ok(this._service.GetSystemInfo());
            });

            endpoints.MapGet("/api/system/machinename", () =>
            {
                return Results.Ok(new
                {
                    MachineName = this._service.GetSystemInfo().MachineName
                });
            });
        }
    }
}
