namespace SimpleWebServer.WebFunction
{
    using System;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;

    public class HelloNotificationService : IHostedService
    {
        private readonly HelloWorldService _helloWorldService;
        private readonly IHubContext<WebServerHub> _hub;

        public HelloNotificationService(HelloWorldService helloWorldService, IHubContext<WebServerHub> hub)
        {
            _helloWorldService = helloWorldService;
            _hub = hub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _helloWorldService.Changed += OnChanged;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _helloWorldService.Changed -= OnChanged;

            return Task.CompletedTask;
        }

        private async void OnChanged(object sender, EventArgs e)
        {
            await _hub.Clients.All.SendAsync("HelloChanged");
        }
    }
}
