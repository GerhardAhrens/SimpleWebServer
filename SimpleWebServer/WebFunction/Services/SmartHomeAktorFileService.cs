namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.Extensions.Hosting;

    public class SmartHomeAktorFileService : BackgroundService
    {
        private readonly SmartHomeAktorService _service;

        private readonly string _fileName = "SmartHomeAktor.txt";

        public SmartHomeAktorFileService(SmartHomeAktorService service)
        {
            this._service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer =
                new PeriodicTimer(TimeSpan.FromMilliseconds(500));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    ReadFile();
                }
            }
            catch (OperationCanceledException)
            {
                // Anwendung wird beendet
            }
        }

        private void ReadFile()
        {
            if (!File.Exists(this._fileName))
                return;

            string text;

            try
            {
                text = File.ReadAllText(this._fileName).Trim();
            }
            catch
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            string[] values = text.Split(';');

            if (values.Length != 3)
                return;

            var model = new SmartHomeAktor
            {
                CurrentPower = values[0],
                CurrentVolt = values[1],
                CurrentTemperature = values[2]
            };

            this._service.Update(model);
        }
    }
}
