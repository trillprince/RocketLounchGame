using System;
using Common.Scripts.UI;

namespace Common.Scripts.Rocket
{
    public interface IUICreator<T>
    {
        public IWindowModel GetWindowModel();
        public void OnCreatorDestroy();
        public void OnCreatorCreate();
    }
}