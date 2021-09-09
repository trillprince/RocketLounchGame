using System;

namespace Common.Scripts.Rocket
{
    public interface IEventSubscriber<T>
    {
        public void Subscribe(Action<T> action);
        public void Unsubscribe(Action<T> action);
    }

    public interface IEventSubscriber
    {
        public void Subscribe(Action action);
        public void Unsubscribe(Action action);
    }
}