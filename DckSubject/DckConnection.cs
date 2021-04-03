using System;
using System.Collections.Generic;

namespace DckSubject
{
    public class DckConnection<T> : IDisposable
    {
        private readonly List<IObserver<T>> _observers;
        private readonly IObserver<T> _observer;

        public DckConnection(List<IObserver<T>> observers, IObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Disconnect() => Dispose();

        public void Reconnect()
        {
            if (_observer != null)
                _observers.Add(_observer);
        }


        public void Dispose()
        {
            if (_observer != null) _observers.Remove(_observer);
        }
    }
}