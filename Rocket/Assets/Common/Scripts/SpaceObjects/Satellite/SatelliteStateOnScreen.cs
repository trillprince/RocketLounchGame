using System;
using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteDelivery
    {
        private StateOnScreen _finalStateOnScreen;
        private StateOnScreen _currentStateOnScreen;
        public bool CargoDelivered { get; private set; } = false;
        private Vector3 _screenBounds;
        private MeshCollider _meshCollider;
        private readonly Transform _transform;
        private readonly SatelliteColor _satelliteColor;
        private readonly Action _onLoose;
        private readonly ISpaceObjectController _spaceObjectController;
        private readonly GameLoopController _gameLoopController;

        public SatelliteDelivery(MeshCollider meshCollider,
            Transform transform,
            SatelliteColor satelliteColor,
            ISpaceObjectController spaceObjectController,
            GameLoopController gameLoopController)
        {
            _meshCollider = meshCollider;
            _transform = transform;
            _satelliteColor = satelliteColor;
            _spaceObjectController = spaceObjectController;
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
                _currentStateOnScreen != StateOnScreen.UpperRed
                )
            {
                _satelliteColor.SetColor(Color.red);
                _currentStateOnScreen = StateOnScreen.UpperRed;
            }
            else if (_transform.position.y < -_screenBounds.y * 0.5f &&
                     _transform.position.y >= 0 &&
                     _currentStateOnScreen != StateOnScreen.Yellow
                     )
            {
                _satelliteColor.SetColor(Color.yellow);
                _currentStateOnScreen = StateOnScreen.Yellow;
            }
            else if (_transform.position.y < 0 &&
                     _transform.position.y >= _screenBounds.y * 0.5f &&
                     _currentStateOnScreen != StateOnScreen.Green
            )
            {
                _satelliteColor.SetColor(Color.green);
                _currentStateOnScreen = StateOnScreen.Green;
            }
            else if (_transform.position.y < _screenBounds.y * 0.5f &&
                     _transform.position.y >= _screenBounds.y &&
                     _currentStateOnScreen != StateOnScreen.LowerRed
            )
            {
                _satelliteColor.SetColor(Color.red);
                _currentStateOnScreen = StateOnScreen.LowerRed;
                _spaceObjectController.ScopeToNextObject();
            }
            else if (_transform.position.y < _screenBounds.y - _meshCollider.bounds.size.y)
            {
                /*if (!CargoDelivered)
                {
                    _gameLoopController.DisableSatelliteDrop();
                    return;
                }*/
                _spaceObjectController.DisposeLastObject();
            }
        }

        public void SetFinalDeliveryStatus()
        {
            Debug.Log("set final");
            CargoDelivered = true;
            _satelliteColor.SetFinalColor();
            _spaceObjectController.ScopeToNextObject();
            _finalStateOnScreen = _currentStateOnScreen;
        }
    }
}