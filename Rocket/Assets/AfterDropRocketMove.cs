using System;
using System.Collections;
using Common.Scripts.UI.InGame;
using UnityEngine;
using Random = UnityEngine.Random;

public class AfterDropRocketMove : MonoBehaviour
{
    private float _scaleSmoothness = 2f;
    private float _rotateSpeed = 2f;
    private readonly Vector3 _minScale = new Vector3(0.6f,0.6f,0.6f);
    private readonly Vector3 _maxScale = new Vector3(2f,2f,2f);
    private float _minRot = -20;
    private float _maxRot = 20;
    private Vector3 _currentTargetRot;
    private bool _cargoDropped;
    private float _timeTillStopScaling = 3f;

    private void OnEnable()
    {
        CargoDropListener.CargoDropped += () =>
        {
            _cargoDropped = true;
            StartCoroutine(ResetCargoDroppedStatus());
        };
    }

    IEnumerator  ResetCargoDroppedStatus()
    {
        yield return new WaitForSeconds(_timeTillStopScaling);
        _cargoDropped = false;
        ResetTargetXRot();
    }

    private void Start()
    {
        ResetTargetXRot();
    }

    private void FixedUpdate()
    {
        if (_cargoDropped)
        {
            // ScaleDown();
            Rotate();
        }
    }
    
    private void ScaleDown()
    {
        transform.localScale = Vector3.Slerp(transform.localScale, _minScale, Time.deltaTime/_scaleSmoothness);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(_currentTargetRot.x,transform.eulerAngles.y, transform.eulerAngles.z), 
            _rotateSpeed * Time.deltaTime);
    }

    void ResetTargetXRot()
    {
        _currentTargetRot =  new Vector3 (Random.Range(_minRot, _maxRot),0,0);
    }
}
