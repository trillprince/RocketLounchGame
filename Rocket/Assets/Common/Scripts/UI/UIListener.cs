using System;
using UnityEngine;

namespace Common.Scripts.UI
{
    public class UIController : IUIController
    {
        public event Action<bool> OnUIActive;

        public UIController()
        {
            
        }
        
        public void UIActive(bool isActive)
        {
            OnUIActive?.Invoke(isActive);
        }
        
    }
}