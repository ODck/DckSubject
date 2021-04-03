using System;

namespace Dck.Subject
{
    public interface IDckSubject<T> : IObservable<T>
    {
        DckConnection<T> Connect(IObserver<T> observer);
        DckConnection<T> Connect(Action<T> callback);
        DckConnection<T> Connect(Action<T> callback, Action<Exception> error);
        DckConnection<T> Connect(Action<T> callback, Action<Exception> error, Action finish);
        IDisposable Subscribe(Action<T> callback);
        IDisposable Subscribe(Action<T> callback, Action<Exception> error);
        IDisposable Subscribe(Action<T> callback, Action<Exception> error, Action finish);
        void Trigger(T param);
    }
}