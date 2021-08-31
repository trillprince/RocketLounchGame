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

        public IWindow InstantiateUI()
        {
            _window = _createWindow?.Invoke();
            _window?.Hide();
            if (_window != null)
            {
                return _window;
            }
            return default;
        }
    }
}