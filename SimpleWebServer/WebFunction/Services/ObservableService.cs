namespace SimpleWebServer.WebFunction.Services
{
    using System;

    public class ObservableService<T> where T : class
    {
        public event EventHandler Changed;

        public T Current { get; private set; }

        public ObservableService(T initialValue)
        {
            this.Current = initialValue;
        }

        protected void RaiseChanged()
        {
            this.Changed?.Invoke(this, EventArgs.Empty);
        }

        protected void SetCurrent(T value)
        {
            this.Current = value;

            this.RaiseChanged();
        }
    }
}
