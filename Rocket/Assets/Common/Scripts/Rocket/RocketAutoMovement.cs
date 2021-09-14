using System;
using System.Collections;
using Common.Scripts.Cargo;
using Common.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class RocketAutoMovement : IRocketMoveComponent
    {
        private float _scaleSmoothness = 5f;
        private float _rotateSmoothness = 5f;
        private float _minScale;
        private float _maxScale;
        private readonly float _scaleDownValue = 0.4f;
        private readonly float _scaleUpValue = 0.4f;
        private readonly float _minXRot = -20;
        private readonly float _maxXRot = 20;
        private readonly float _minYRot = -90;
        private readonly float _maxYRot = 90;
        private Vector3 _currentTargetRot = new Vector3(0, -90, 0);
        private Vector3 _currentTargetScale = new Vector3(1, 1, 1);
        private float _timeTillLounch = 10f;
        private Quaternion _landingRot;
        private Vector3 _landingPos;
        private Vector3 _landingScale;
        private Transform _transform;
        private MovementState _nextMovementState;

        private void SetMinMaxScale(Transform transform)
        {
            _minScale = transform.localScale.x - _scaleDownValue;
            _maxScale = transform.localScale.x + _scaleUpValue;
        }

        public RocketAutoMovement(Transform transform)
        {
            _transform = transform;
            SetMinMaxScale(_transform);
        }

        private void ScaleDown()
        {
            _transform.localScale =
                Vector3.Lerp(_transform.localScale, _currentTargetScale, Time.deltaTime / _scaleSmoothness);
        }

        private void Rotation()
        {
            _transform.rotation = Quaternion.Lerp(
                _transform.rotation,
                Quaternion.Euler(_currentTargetRot.x, _currentTargetRot.y, _transform.eulerAngles.z),
                Time.deltaTime / _rotateSmoothness);
        }

        void ResetTargetRot()
        {
            _currentTargetRot = new Vector3(0, Random.Range(_minYRot, _maxYRot), 0);
        }

        void ResetTargetScale()
        {
            var scale = Random.Range(_minScale, _maxScale);
            _currentTargetScale = new Vector3(scale, scale, scale);
        }

        public void Move(Action<MovementState> changeState)
        {
            ScaleDown();
            Rotation();
        }

        public void Enable()
        {
            /*CargoDropController.CargoDropping += () =>
            {
                ResetTargetRot();
                ResetTargetScale();
            };*/
        }
    }
}