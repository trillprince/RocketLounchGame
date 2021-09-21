using System;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteDelivery
    {
        private DeliveryStatus _finalDeliveryStatus;
        private DeliveryStatus _currentDeliveryStatus;
        private Vector3 _screenBounds;
        private MeshCollider _meshCollider;
        private readonly Transform _transform;
        private readonly Action _onDispose;

        public SatelliteDelivery(MeshCollider meshCollider,Transform transform, Action onDispose)
        {
            _meshCollider = meshCollider;
            _transform = transform;
            _onDispose = onDispose;
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - _transform.position.z));
        }

        public void StateCheck()
        {
            if (_transform.position.y < -_screenBounds.y && _transform.position.y >= -_screenBounds.y * 0.5f)
            {
                
            }
            else if (_transform.position.y < -_screenBounds.y * 0.5f && _transform.position.y >= 0)
            {
                
            }
            else if (_transform.position.y < 0 && _transform.position.y >= _screenBounds.y * 0.5f)
            {
                
            }
            else if (_transform.position.y < _screenBounds.y * 0.5f && _transform.position.y >= _screenBounds.y)
            {
                
            }
            else if (_transform.position.y < _screenBounds.y - _meshCollider.bounds.size.y)
            {
                _onDispose?.Invoke();
            }
        }
        
        public void SetCurrentDeliveryStatus(DeliveryStatus deliveryStatus)
        {
            _currentDeliveryStatus = deliveryStatus;
        }

        public void SetFinalDeliveryStatus()
        {
            _finalDeliveryStatus = _currentDeliveryStatus;
        }
    }
}