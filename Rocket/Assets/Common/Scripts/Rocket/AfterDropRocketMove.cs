using System;
using System.Collections;
using Common.Scripts.Cargo;
using Common.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class AfterDropRocketMove : MonoBehaviour
    {
        private float _scaleSmoothness = 10f;
        private float _rotateSmoothness = 20f;
        private float _minScale;
        private float _maxScale;
        private readonly float _scaleDownValue = 0.7f;
        private readonly float _scaleUpValue = 0.3f;
        private readonly float _minXRot = -20;
        private readonly float _maxXRot = 20;
        private readonly float _minYRot = -90;
        private readonly float _maxYRot = 90;
        private Vector3 _currentTargetRot  = new Vector3(0,-90,0);
        private Vector3 _currentTargetScale = new Vector3(1, 1,1);
        private float _timeTillLounch = 10f;
        private bool _rocketLouched;

        private void Awake()
        {
            _minScale = transform.localScale.x - _scaleDownValue;
            _maxScale = transform.localScale.x + _scaleUpValue;
        }

        private void OnEnable()
        {
            CargoDropController.CargoDropping += () =>
            {
                ResetTargetRot();
                ResetTargetScale();
            };
            CargoDropSlider.OnGetDropAccuracy += ChangeRotSpeedOnAccuracy;

            LounchManager.OnRocketLounch += engineEnabled =>
            {
                _rocketLouched = engineEnabled;
            };
        }

        private void OnDisable()
        {
            CargoDropSlider.OnGetDropAccuracy -= ChangeRotSpeedOnAccuracy;
        }


        private void FixedUpdate()
        {
            if (_rocketLouched)
            {
                ScaleDown();
                Rotation();
            }
        }
    
        private void ScaleDown()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _currentTargetScale, Time.deltaTime/_scaleSmoothness);
        }

        private void Rotation()
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(_currentTargetRot.x,_currentTargetRot.y, transform.eulerAngles.z), 
                Time.deltaTime/_rotateSmoothness);
        }
        void ResetTargetRot()
        {
            _currentTargetRot =  new Vector3 (Random.Range(_minXRot, _maxXRot),Random.Range(_minYRot, _maxYRot),0);
        }

        void ResetTargetScale()
        {
            var scale = Random.Range(_minScale, _maxScale);
            _currentTargetScale = new Vector3(scale, scale, scale);
        }

        void ChangeRotSpeedOnAccuracy(DropAccuracy accuracy)
        {
            if (accuracy == DropAccuracy.Perfect)
            {
                _rotateSmoothness -= 3;
            }
            else if (accuracy == DropAccuracy.Nice)
            {
                _rotateSmoothness -= 2;
            }
            else if (accuracy == DropAccuracy.NotGood)
            {
                _rotateSmoothness -= 1;
            }
        }
    }
}
