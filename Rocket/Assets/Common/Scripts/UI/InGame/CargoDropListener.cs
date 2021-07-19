using Common.Scripts.Input;
using Common.Scripts.MissionSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI.InGame
{
    public class CargoDropListener : MonoBehaviour
    {
        public delegate void DropCargo();

        public static event DropCargo CargoDropped;
        private bool _dropReady;


        private void OnEnable()
        {
            MissionManager.TimeToDrop += DropReady;
            InputManager.OnTouchStartEvent += DropOnOnTouch;
        }

        private void OnDisable()
        {
            MissionManager.TimeToDrop -= DropReady;
            InputManager.OnTouchStartEvent -= DropOnOnTouch;
        }

        void DropReady(bool isReady)
        {
            _dropReady = isReady;
        }

        void DropOnOnTouch(Vector2 touchPos)
        {
            if (_dropReady)
            {
                CargoDropped?.Invoke();
            }
        }
    
    
    }
}
