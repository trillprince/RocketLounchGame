using System;
using Common.Scripts.MissionSystem;
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
        private readonly SatelliteColor _satelliteColor;
        private readonly Action _onLoose;
        private readonly ISatelliteController _satelliteController;
        private readonly GameLoopController _gameLoopController;

        public SatelliteDelivery(MeshCollider meshCollider,
            Transform transform,
            SatelliteColor satelliteColor,
            ISatelliteController satelliteController,
            GameLoopController gameLoopController)
        {
            _meshCollider = meshCollider;
            _transform = transform;
            _satelliteColor = satelliteColor;
            _satelliteController = satelliteController;
            _gameLoopController = gameLoopController;
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
                     !_satelliteColor.IsCurrentColor(Color.black) 
            )
            {
                _satelliteColor.SetColor(Color.black);
                _currentDeliveryStatus = DeliveryStatus.Black;
                _satelliteController.ScopeToNextSatellite();
            }
            else if (_transform.position.y < _screenBounds.y - _meshCollider.bounds.size.y)
            {
                if (!CargoDelivered)
                {
                    _gameLoopController.DisableSatelliteDrop();
                    return;
                }
                _satelliteController.DisposeLastSatellite();
            }
        }

        public void SetFinalDeliveryStatus()
        {
            Debug.Log("set final");
            CargoDelivered = true;
            _satelliteColor.SetFinalColor();
            _satelliteController.ScopeToNextSatellite();
            _finalDeliveryStatus = _currentDeliveryStatus;
        }
    }
}