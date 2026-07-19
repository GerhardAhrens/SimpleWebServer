namespace SimpleWebServer.WebFunction
{
    using System;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;

    public class SmartHomeNotificationService : IHostedService
    {
        private readonly SmartHomeAktorService _smartHomeService;
        private readonly IHubContext<WebServerHub> _hub;

        public SmartHomeNotificationService(SmartHomeAktorService smartHomeService, IHubContext<WebServerHub> hub)
        {
            this._smartHomeService = smartHomeService;
            this._hub = hub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._smartHomeService.Changed += OnChanged;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._smartHomeService.Changed -= OnChanged;

            return Task.CompletedTask;
        }

        private async void OnChanged(object sender, EventArgs e)
        {
            await _hub.Clients.All.SendAsync("SmartHomeChanged");
        }
    }
}
