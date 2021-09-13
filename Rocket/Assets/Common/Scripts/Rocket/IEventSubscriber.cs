using System;

namespace Common.Scripts.Rocket
{
    public interface IEventSubscriber<T>
    {
        public void Subscribe(Action<T> action = null);
        public void Unsubscribe(Action<T> action = null);
        
    }

    public interface IEventSubscriber
    {
        public void Subscribe(Action action = null);
        public void Unsubscribe(Action action = null);
    }
}