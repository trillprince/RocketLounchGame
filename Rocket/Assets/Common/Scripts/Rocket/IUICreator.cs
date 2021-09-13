using System;

namespace Common.Scripts.Rocket
{
    public interface IUICreator<T>
    {
        public IWindowModel GetWindowModel();
        public void OnCreatorDestroy();
        public void OnCreatorUse();
        void ConstructorAction();
    }
}