namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SmartHomeAktorService
    {
        public SmartHomeAktor Current { get; private set; } = new();

        public event EventHandler Changed;

        public void Update(SmartHomeAktor value)
        {
            if (Current.CurrentPower == value.CurrentPower &&
                Current.CurrentVolt == value.CurrentVolt &&
                Current.CurrentTemperature == value.CurrentTemperature)
            {
                return;
            }

            Current = value;

            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
