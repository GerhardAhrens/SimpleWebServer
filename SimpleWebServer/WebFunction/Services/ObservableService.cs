namespace SimpleWebServer.WebFunction
{
    using System;

    /// <summary>
    /// Basisklasse für Services, die einen aktuellen Zustand verwalten
    /// und Änderungen über ein Event bekannt geben.
    /// </summary>
    public abstract class ObservableService<T>
    {
        public event EventHandler Changed;

        public T Current { get; protected set; }

        protected ObservableService(T initialValue)
        {
            Current = initialValue;
        }

        protected bool SetCurrent(T value)
        {
            if (EqualityComparer<T>.Default.Equals(Current, value))
            {
                return false;
            }

            Current = value;

            Changed?.Invoke(this, EventArgs.Empty);

            return true;
        }
    }
}
