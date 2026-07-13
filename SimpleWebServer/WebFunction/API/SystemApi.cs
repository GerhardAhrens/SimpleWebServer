namespace SimpleWebServer.WebFunction
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    public class SystemApi : IRestApi
    {
        private readonly SystemService _service;

        public SystemApi(SystemService service)
        {
            _service = service;
        }

        public void Register(WebApplication app)
        {
            app.MapGet("/api/system/machinename", () =>
            {
                return Results.Ok(new
                {
                    MachineName = _service.MachineName
                });
            });
        }
    }
}
