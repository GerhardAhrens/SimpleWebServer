namespace SimpleWebServer.WebFunction
{
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
