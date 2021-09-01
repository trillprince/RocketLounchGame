using System;
using Common.Scripts.Planet;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class EndOfGameUI: IUICreator<IExitWindow>
    {
        private Func<IExitWindow> _createWindow;
        private IExitWindow _window;
        
        public EndOfGameUI(Func<IExitWindow> createWindow)
        {
            _createWindow = createWindow;
        }

        public IExitWindow InstantiateUI()
        {
            _window = _createWindow?.Invoke();
            if (_window != null)
            {
                return _window;
            }
            return default;
        }
    }
}