namespace Common.Scripts.Rocket
{
    public interface IEventSubscriber
    {
        public void Subscribe();
        public void Unsubscribe();
    }
}