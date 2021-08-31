using System;
using Common.Scripts.Planet;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class EndOfGameUI: IUICreator
    {
        private Func<IWindow> _createWindow;
        private IWindow _window;
        
        public EndOfGameUI(Func<IWindow> createWindow)
        {
            _createWindow = createWindow;
        }

        public void InstantiateUI()
        {
            _window = _createWindow?.Invoke();
        }
    }
}