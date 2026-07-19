namespace SimpleWebServer.WebFunction
{
    using SimpleWebServer.WebFunction.Services;

    public class SmartHomeAktorService : ObservableService<SmartHomeAktor>
    {
        public SmartHomeAktorService() : base(new SmartHomeAktor())
        {
        }

        public void Update(SmartHomeAktor value)
        {
            base.SetCurrent(value);
        }
    }
}
