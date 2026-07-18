namespace SimpleWebServer.WebFunction
{
    using System;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;

    public class NotificationService : IHostedService
    {
        private readonly IHubContext<WebServerHub> _hub;
        private readonly HelloWorldService _helloService;
        private readonly SmartHomeAktorService _smartHomeService;

        public NotificationService(IHubContext<WebServerHub> hub, HelloWorldService helloWorldService, SmartHomeAktorService smartHomeService)
        {
            _hub = hub;
            _helloService = helloWorldService;
            _smartHomeService = smartHomeService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._helloService.TextChanged += this.OnTextChanged;
            this._smartHomeService.Changed += OnSmartHomeChanged;

            Console.WriteLine("NotificationService gestartet");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _helloService.TextChanged -= this.OnTextChanged;
            _smartHomeService.Changed -= OnSmartHomeChanged;

            Console.WriteLine("NotificationService beendet");

            return Task.CompletedTask;
        }

        private async void OnTextChanged(object sender, EventArgs e)
        {
            await this._hub.Clients.All.SendAsync("HelloChanged");
        }

        private async void OnSmartHomeChanged(object sender, EventArgs e)
        {
            await _hub.Clients.All.SendAsync("SmartHomeChanged");
        }
    }
}
