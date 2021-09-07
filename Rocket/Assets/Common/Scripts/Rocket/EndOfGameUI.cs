using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.Planet;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class EndOfGameUI<T>: IUICreator<T> where T : IExitWindow
    {
        private Func<T> _createWindow;

        public EndOfGameUI(Func<T> createWindow)
        {
            _createWindow = createWindow;
        }

        public T InstantiateUI()
        {
            return _createWindow.Invoke();
        }
    }
}