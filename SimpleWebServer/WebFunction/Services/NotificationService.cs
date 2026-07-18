namespace SimpleWebServer.WebFunction
{
    using System;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;

    public class NotificationService : IHostedService
    {
        private readonly IHubContext<WebServerHub> _hub;
        private readonly HelloWorldService _helloService;

        public NotificationService(
            IHubContext<WebServerHub> hub,
            HelloWorldService helloService)
        {
            _hub = hub;
            _helloService = helloService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _helloService.TextChanged += OnTextChanged;

            Console.WriteLine("NotificationService gestartet");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _helloService.TextChanged -= OnTextChanged;

            Console.WriteLine("NotificationService beendet");

            return Task.CompletedTask;
        }

        private async void OnTextChanged(object sender, EventArgs e)
        {
            await _hub.Clients.All.SendAsync("HelloChanged");
        }
    }
}
