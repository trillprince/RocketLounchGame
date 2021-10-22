using System;
using UnityEngine;

namespace Common.Scripts.Application
{
    public class ApplicationController : MonoBehaviour
    {
        private AccelerometerControl _accelerometerControl;
        private ScreenControl _screenControl;
        public event Action<bool> ApplicationFocused;
        private void Start()
        {
            _accelerometerControl = new AccelerometerControl(UnityEngine.Application.platform,this);
            _screenControl = new ScreenControl();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            ApplicationFocused?.Invoke(hasFocus);
        }
    
    
    }
}