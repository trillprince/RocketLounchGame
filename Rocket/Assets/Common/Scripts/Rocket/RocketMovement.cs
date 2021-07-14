using Common.Scripts;
using Common.Scripts.Input;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    private float _rotateSpeed = 40f;
    private float _rotateMaxSpeed = 70f;
    private float _rotSpeedAcceleration = 400f;
    private float _rotSpeedDegradation = 150f;
    private bool _rocketLounched = false;
    [SerializeField] [Range(0,80f)] private float _rocketSpeed = 0;
    private float _rocketMaxSpeed = 80f;
    private float _rocketSpeedAcceleration = 10f;
    private float _rocketSpeedDegradation = 5f;
    [SerializeField] private bool _middleEngineEnabled = false;
    private bool _isTouching = false;
    private Vector2 _touchPos;

    private void TouchStart(Vector2 touchPos)
    {
        _isTouching = true;
        _touchPos = touchPos;
    }

    private void TouchEnd()
    {
        _isTouching = false;
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
        LounchManager.MiddleEngineEnable += MiddleEngine;
        LounchManager.MiddleEngineDisable += MiddleEngine;
        InputManager.TouchStart += TouchStart;
        InputManager.TouchEnd += TouchEnd;
        
    }

    private void OnDisable()
    {
        LounchManager.MiddleEngineEnable -= MiddleEngine;
        LounchManager.MiddleEngineDisable -= MiddleEngine;
        InputManager.TouchStart -= TouchStart;
        InputManager.TouchEnd -= TouchEnd;
    }


    public void Update()
    {
        if (!_rocketLounched)
        {
           return;
        }
        MiddleEngineSpeed();
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

    public Vector3 GetRocketDirection()
    {
        return transform.up;
    }
}