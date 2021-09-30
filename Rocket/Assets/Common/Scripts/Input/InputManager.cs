using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private TouchControls _touchControls;
        private UnityEngine.Camera _camera;
        private SwipeDetection _swipeDetection;
        public static event Action<Vector2> OnTouchStart;
        public static event Action <Vector2> OnTouchEnd;

        public event Action<Vector2, float> OnTouchStartEvent;
        public event Action<Vector2, float> OnTouchEndEvent;


        [Inject]
        public void Constructor(SwipeDetection swipeDetection)
        {
            _swipeDetection = swipeDetection;
        }

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _touchControls = new TouchControls();
            _touchControls.Touch.TouchHold.started += TouchStarted;
            _touchControls.Touch.TouchHold.canceled += TouchEnded;
        }

        private void Start()
        {
            _swipeDetection.Enable();
        }

        private void TouchStarted(InputAction.CallbackContext context)
        {
            Debug.Log("on touch start started");
            OnTouchStartEvent?.Invoke(GetPositionOfTouch(),(float)context.startTime);
        }
        private void TouchEnded(InputAction.CallbackContext context)
        {
            Debug.Log("on touch end event");
            OnTouchEndEvent?.Invoke(GetPositionOfTouch(),(float)context.time);
        }

   
        private void OnEnable()
        {
            _touchControls.Enable();
        }

        private void OnDisable()
        {
            _touchControls.Disable();
            _swipeDetection.Disable();
        }
        
        Vector2 GetPositionOfTouch()
        {
            return _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        }

        public Vector3 PrimaryPosition()
        {
            return Utils.ScreenToWorld(_camera, GetPositionOfTouch());
        }
        

    }
}
