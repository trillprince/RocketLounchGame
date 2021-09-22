using System;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteDelivery
    {
        private DeliveryStatus _finalDeliveryStatus;
        private DeliveryStatus _currentDeliveryStatus;
        public bool CargoDelivered { get; private set; } = false;
        private Vector3 _screenBounds;
        private MeshCollider _meshCollider;
        private readonly Transform _transform;
        private readonly Action _onDispose;
        private readonly SatelliteColor _satelliteColor;
        private readonly Action _onLoose;
        private readonly Action _onCargoDelivery;
        private Action _onScopeCargoChange;

        public SatelliteDelivery(MeshCollider meshCollider,
            Transform transform, 
            Action onDispose,
            SatelliteColor satelliteColor,
            Action onLoose,
            Action onCargoDelivery,
            Action onScopeCargoChange)
        {
            _meshCollider = meshCollider;
            _transform = transform;
            _onDispose = onDispose;
            _satelliteColor = satelliteColor;
            _onLoose = onLoose;
            _onCargoDelivery = onCargoDelivery;
            _onScopeCargoChange = onScopeCargoChange;
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - _transform.position.z));
        }

        public void StateCheck()
        {
            if (_transform.position.y < -_screenBounds.y && 
                _transform.position.y >= -_screenBounds.y * 0.5f &&
                !_satelliteColor.IsCurrentColor(Color.red)
                )
            {
                _satelliteColor.SetColor(Color.red);
                _currentDeliveryStatus = DeliveryStatus.UpperRed;
            }
            else if (_transform.position.y < -_screenBounds.y * 0.5f &&
                     _transform.position.y >= 0 &&
                     !_satelliteColor.IsCurrentColor(Color.yellow)
                     )
            {
                _satelliteColor.SetColor(Color.yellow);
                _currentDeliveryStatus = DeliveryStatus.Yellow;
            }
            else if (_transform.position.y < 0 &&
                     _transform.position.y >= _screenBounds.y * 0.5f &&
                     !_satelliteColor.IsCurrentColor(Color.green)
            )
            {
                _satelliteColor.SetColor(Color.green);
                _currentDeliveryStatus = DeliveryStatus.Green;
            }
            else if (_transform.position.y < _screenBounds.y * 0.5f &&
                     _transform.position.y >= _screenBounds.y &&
                     !_satelliteColor.IsCurrentColor(Color.red) 
            )
            {
                _satelliteColor.SetColor(Color.red);
                _currentDeliveryStatus = DeliveryStatus.LowerRed;
                _onScopeCargoChange?.Invoke();
            }
            else if (_transform.position.y < _screenBounds.y - _meshCollider.bounds.size.y)
            {
                /*if (!CargoDelivered)
                {
                    _onLoose?.Invoke();
                }*/
                _onDispose?.Invoke();
            }
        }

        public void SetFinalDeliveryStatus()
        {
            CargoDelivered = true;
            _onCargoDelivery?.Invoke();
            _finalDeliveryStatus = _currentDeliveryStatus;
        }
    }
}