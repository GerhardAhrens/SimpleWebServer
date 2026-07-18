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
            this._hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("ClockService gestartet");

            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await this._hub.Clients.All.SendAsync("TimeChanged", DateTime.Now.ToString("HH:mm:ss", CultureInfo.CurrentCulture), stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("ClockService beendet");
            }
        }
    }
}
