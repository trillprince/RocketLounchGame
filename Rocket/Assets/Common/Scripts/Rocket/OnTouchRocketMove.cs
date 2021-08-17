using Common.Scripts.Input;
using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class OnTouchRocketMove : MonoBehaviour
    {
        private float _rotateSpeed = 40f;
        private float _rotateMaxSpeed = 90f;
        private float _rotSpeedAcceleration = 400f;
        private float _rotSpeedDegradation = 150f;
        private bool _rocketLounched = false;
        [SerializeField] private float _rocketSpeed;
        private float _rocketMaxSpeed = 100f;
        private float _rocketSpeedAcceleration = 15f;
        private float _rocketSpeedDegradation = 15f;
        [SerializeField] private bool _middleEngineEnabled = false;
        private bool _isTouching = false;
        private Vector2 _touchPos;
        private bool _controlActive;

        private void Start()
        {
            _controlActive = false;
        }

        private void OnTouchStartEvent(Vector2 touchPos)
        {
            if (_controlActive)
            {
                _isTouching = true;
                _touchPos = touchPos;
            }
        }

        private void OnTouchEndEvent()
        {
            if (_controlActive)
            {
                _isTouching = false; 
            }
        }
    
    
        private void Awake()
        {
            _middleEngineEnabled = false;
        }
    
        public float RocketSpeed
        {
            get => _rocketSpeed;
        }

        private void MiddleEngine(bool isEnabled)
        {
            if (!_rocketLounched)
            {
                _rocketLounched = true;
            }
            _middleEngineEnabled = isEnabled;
            Debug.Log(isEnabled);
        }
    
        private void MiddleEngineSpeed()
        {
            if (!_middleEngineEnabled && _rocketSpeed > 0)
            {
                _rocketSpeed -= Time.deltaTime * _rocketSpeedDegradation;
            }
            else if (!_middleEngineEnabled && _rocketSpeed < 0)
            {
                _rocketSpeed = 0;
            }
        
            if (_middleEngineEnabled && _rocketSpeed < _rocketMaxSpeed)
            {
                _rocketSpeed += Time.deltaTime * _rocketSpeedAcceleration;
            }
            else if (_rocketSpeed > _rocketMaxSpeed)
            {
                _rocketSpeed = _rocketMaxSpeed;
            }
        
        }
    

        private void OnEnable()
        {
            LaunchManager.OnRocketLounch += MiddleEngine;
            LaunchManager.MiddleEngineDisable += MiddleEngine;
            InputManager.OnTouchStartEvent += OnTouchStartEvent;
            InputManager.OnTouchEndEvent += OnTouchEndEvent;
        }


        private void OnDisable()
        {
            LaunchManager.OnRocketLounch -= MiddleEngine;
            LaunchManager.MiddleEngineDisable -= MiddleEngine;
            InputManager.OnTouchStartEvent -= OnTouchStartEvent;
            InputManager.OnTouchEndEvent -= OnTouchEndEvent;
        }


        public void Update()
        {
            if (!_rocketLounched)
            {
                return;
            }
            
            if (_controlActive)
            {
                Rotate();
                if (_isTouching)
                {
                    MoveOnTouchScreen(_touchPos);
                }
                else
                {
                    DegradateSpeed();
                }
            }
        }

        private void MoveOnTouchScreen(Vector2 touchPos)
        {
            if (touchPos.x < Screen.width / 2)
            {
                _rotateSpeed += Time.deltaTime * _rotSpeedAcceleration;
            }
            else if (touchPos.x > Screen.width / 2)
            {
                _rotateSpeed -= Time.deltaTime * _rotSpeedAcceleration;
            }

            if (_rotateSpeed >= _rotateMaxSpeed)
            {
                _rotateSpeed = _rotateMaxSpeed;
            }

            if (_rotateSpeed <= -_rotateMaxSpeed)
            {
                _rotateSpeed = -_rotateMaxSpeed;
            }
        }

        private void DegradateSpeed()
        {
            if (_rotateSpeed > 0)
            {
                if (_rotateSpeed - _rotSpeedDegradation * Time.deltaTime < 0)
                {
                    _rotateSpeed = 0;
                }
                else
                {
                    _rotateSpeed -= _rotSpeedDegradation * Time.deltaTime;
                }
            }
            else if (_rotateSpeed < 0)
            {
                if (_rotateSpeed + _rotSpeedDegradation * Time.deltaTime > 0)
                {
                    _rotateSpeed = 0;
                }
                else
                {
                    _rotateSpeed += _rotSpeedDegradation * Time.deltaTime;
                }
            }
        }
    
        private void Rotate()
        {
            transform.rotation *= Quaternion.Euler(0, 0, _rotateSpeed * Time.deltaTime);
        }
        
    }
}