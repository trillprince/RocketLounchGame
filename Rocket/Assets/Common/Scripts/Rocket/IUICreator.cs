namespace Common.Scripts.Rocket
{
    public interface IUICreator<T> where T : IExitWindow
    {
        public T InstantiateUI();
    }
}