namespace SimpleWebServer.WebFunction
{
    using System;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;

    public class ImageNotificationService : BackgroundService
    {
        private readonly ImageService _imageService;
        private readonly IHubContext<WebServerHub> _hub;
        private readonly WebServerConfiguration _configuration;

        private ImageDirectoryState _currentState = new(0, DateTime.MinValue);

        public ImageNotificationService(ImageService imageService, IHubContext<WebServerHub> hub, WebServerConfiguration configuration)
        {
            _imageService = imageService;
            _hub = hub;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _currentState = _imageService.GetState();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(_configuration.ImageRefreshSeconds));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    ImageDirectoryState newState =
                        _imageService.GetState();

                    if (newState == _currentState)
                        continue;

                    _currentState = newState;

                    await _hub.Clients.All.SendAsync("ImagesChanged", cancellationToken: stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}
