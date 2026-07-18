namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Globalization;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;

    public class ClockService : BackgroundService
    {
        private readonly IHubContext<WebServerHub> _hub;

        public ClockService(IHubContext<WebServerHub> hub)
        {
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await _hub.Clients.All.SendAsync("TimeChanged", DateTime.Now.ToString("HH:mm:ss", CultureInfo.CurrentCulture), stoppingToken);
            }
        }
    }
}
