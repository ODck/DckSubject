using System;
using System.Collections.Generic;
using System.Reactive;

namespace Dck.Subject
{
    public class DckSubject<T> : IObservable<T>, IDckSubject<T>
    {
        private readonly List<IObserver<T>> _observers;

        public DckSubject()
        {
            _observers = new List<IObserver<T>>();
        }

        public DckConnection<T> Connect(IObserver<T> observer) => (Subscribe(observer) as DckConnection<T>)!;
        public DckConnection<T> Connect(Action<T> callback) => (Subscribe(callback) as DckConnection<T>)!;

        public DckConnection<T> Connect(Action<T> callback, Action<Exception> error) =>
            (Subscribe(callback, error) as DckConnection<T>)!;

        public DckConnection<T> Connect(Action<T> callback, Action<Exception> error, Action finish) =>
            (Subscribe(callback, error, finish) as DckConnection<T>)!;

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new DckConnection<T>(_observers, observer);
        }

        public IDisposable Subscribe(Action<T> callback)
        {
            var observer = new AnonymousObserver<T>(callback);
            return Subscribe(observer);
        }

        public IDisposable Subscribe(Action<T> callback, Action<Exception> error)
        {
            var observer = new AnonymousObserver<T>(callback, error);
            return Subscribe(observer);
        }

        public IDisposable Subscribe(Action<T> callback, Action<Exception> error, Action finish)
        {
            var observer = new AnonymousObserver<T>(callback, error, finish);
            return Subscribe(observer);
        }

        public void Trigger(T param)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(param);
            }
        }
    }
}