using System;
using System.Collections;
using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    private Transform _camTransform;
    private Vector3 _originalPos;
    private ShakeStage _currentShakeStage;
    private float _lounchShakeAmount = 0.1f;
    private float _flightShakeAmount = 0.03f;
    private float _shakeDegradeteTime = 2f;
    private float _currentShakeAmount;

    enum ShakeStage
    {
        OnEarth,
        Lounch,
        Flight
    }

    private void OnEnable()
    {
        LounchManager.Lounching += engineEnabled =>
        {
            ChangeShakeStage(ShakeStage.Lounch);
        };
        LounchManager.MiddleEngineEnable += engineEnabled =>
        {
            ChangeShakeStage(ShakeStage.Flight);
        };

    }

    void Awake()
    {
        _camTransform = GetComponent<Transform>();
        _originalPos = _camTransform.localPosition;
    }

    private void Start()
    {
        ChangeShakeStage(ShakeStage.OnEarth);
    }


    void Update()
    {
        switch (_currentShakeStage)
        {
            case ShakeStage.OnEarth:
                break;
            case ShakeStage.Lounch:
                ShakeTheCamera(_lounchShakeAmount);
                break;
            case ShakeStage.Flight:
                ShakeTheCamera(_flightShakeAmount);
                break;
        }
    }

    void ChangeShakeStage(ShakeStage shakeStage)
    {
        _currentShakeStage = shakeStage;
    }

    void ShakeTheCamera(float shakeAmount)
    {
        if (_currentShakeStage == ShakeStage.Lounch)
        {
            _camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;
            _currentShakeAmount = shakeAmount;
        }
        else if(_currentShakeStage == ShakeStage.Flight)
        {
            _camTransform.localPosition = _originalPos + Random.insideUnitSphere * Mathf.Lerp(_currentShakeAmount,shakeAmount,_shakeDegradeteTime* Time.deltaTime );
            _currentShakeAmount = shakeAmount;
        }
        
    }

}
