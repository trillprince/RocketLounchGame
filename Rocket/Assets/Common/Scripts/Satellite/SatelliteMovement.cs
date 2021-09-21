using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteMovement : MonoBehaviour,ISatellite
    {
        private RocketMovementController _rocketMoveController;
        private float _moveSmoothness = 10f;
        private MeshCollider _meshCollider;
        private Vector3 _screenBounds;
        private Vector3 _satellitePos;
        private Action  _onDispose;
        
    
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public Transform GetTransform()
        {
            return transform;
        }
    
        private void Awake()
        {
            _meshCollider = GetComponent<MeshCollider>();
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - transform.position.z));
            _satellitePos = transform.position;
        }

        public void Constructor(RocketMovementController rocketMovementController, Action  onDispose)
        {
            _rocketMoveController = rocketMovementController;
            _onDispose = onDispose;
        }

        public void Move()
        {
            transform.Translate((-_rocketMoveController.GetRocketDirection()*_rocketMoveController.GetRocketSpeed())/_moveSmoothness * Time.deltaTime);
        }

        public void StateCheck()
        {
            
            if (transform.position.y < -_screenBounds.y && transform.position.y >= -_screenBounds.y * 0.5f)
            {
                
            }
            else if (transform.position.y < -_screenBounds.y * 0.5f && transform.position.y >= 0)
            {
                
            }
            else if (transform.position.y < 0 && transform.position.y >= _screenBounds.y * 0.5f)
            {
                
            }
            else if (transform.position.y < _screenBounds.y * 0.5f && transform.position.y >= _screenBounds.y)
            {
                
            }
            else if (transform.position.y < _screenBounds.y - _meshCollider.bounds.size.y)
            {
                _onDispose?.Invoke();
            }
        }

    }
}
