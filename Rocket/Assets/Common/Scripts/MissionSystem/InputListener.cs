using Common.Scripts.Input;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class InputListener : MonoBehaviour
    {
        public bool InputLeftSide { get; private set; }
        public bool InputRightSide { get; private set; }

        private void OnEnable()
        {
            InputManager.OnTouchStart += OnTouchStart;
            InputManager.OnTouchEnd += OnTouchEnd;
        }

        private void OnDisable()
        {
            InputManager.OnTouchStart -= OnTouchStart;
            InputManager.OnTouchEnd -= OnTouchEnd;
        }

        public void OnTouchEnd()
        {
            InputLeftSide = false;
            InputRightSide = false;
        }

        private void OnTouchStart(Vector2 touchPos)
        {
            if (touchPos.x < Screen.width / 2)
            {
                InputLeftSide = true;
            }
            else if (touchPos.x > Screen.width / 2)
            {
                InputRightSide = true;
            }
        }
    }
}