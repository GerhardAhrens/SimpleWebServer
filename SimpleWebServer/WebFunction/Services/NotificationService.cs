namespace SimpleWebServer.WebFunction
{
    using System;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;

    public class NotificationService : IHostedService
    {
        private readonly IHubContext<WebServerHub> _hub;
        private readonly HelloWorldService _helloService;

        public NotificationService(IHubContext<WebServerHub> hub, HelloWorldService helloService)
        {
            this._hub = hub;
            this._helloService = helloService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._helloService.TextChanged += this.OnTextChanged;

            Console.WriteLine("NotificationService gestartet");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _helloService.TextChanged -= this.OnTextChanged;

            Console.WriteLine("NotificationService beendet");

            return Task.CompletedTask;
        }

        private async void OnTextChanged(object sender, EventArgs e)
        {
            await this._hub.Clients.All.SendAsync("HelloChanged");
        }
    }
}
