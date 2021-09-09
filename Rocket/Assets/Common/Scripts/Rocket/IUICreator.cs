using System;

namespace Common.Scripts.Rocket
{
    public interface IUICreator<T>
    {
        public IWindowModel GetWindowModel();
        public void OnWindowClose();
        public void OnCreatorUse();
    }
}