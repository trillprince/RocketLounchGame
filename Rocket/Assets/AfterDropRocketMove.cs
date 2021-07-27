using System;
using System.Collections;
using Common.Scripts.UI.InGame;
using UnityEngine;
using Random = UnityEngine.Random;

public class AfterDropRocketMove : MonoBehaviour
{
    private float _scaleSmoothness = 2f;
    private float _rotateSpeed = 2f;
    private readonly float _minScale = 0.7f;
    private readonly float _maxScale = 2f;
    private readonly float _minXRot = -20;
    private readonly float _maxXRot = 20;
    private Vector3 _currentTargetRot;
    private Vector3 _currentTargetScale;
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
        ResetTargetScale();
    }

    private void Start()
    {
        ResetTargetXRot();
        ResetTargetScale();
    }

    private void FixedUpdate()
    {
        if (_cargoDropped)
        {
            ScaleDown();
            Rotate();
        }
    }
    
    private void ScaleDown()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _currentTargetScale, Time.deltaTime/_scaleSmoothness);
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
        _currentTargetRot =  new Vector3 (Random.Range(_minXRot, _maxXRot),0,0);
    }

    void ResetTargetScale()
    {
        var scale = Random.Range(_minScale, _maxScale);
        _currentTargetScale = new Vector3(scale, scale, scale);
    }
}
