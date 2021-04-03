using System;
using System.Collections.Generic;

namespace Dck.Subject
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

    public static class DckConnectionExtension
    {
        public static DckConnection<T> AddTo<T>(this DckConnection<T> dckConnection, ICollection<IDisposable> disposables)
        {
            disposables.Add(dckConnection);
            return dckConnection;
        }
    }
}