using System;

namespace Common.Scripts.Rocket
{
    public interface IUICreator<T> 
    {
        public T InstantiateUI();
    }
}