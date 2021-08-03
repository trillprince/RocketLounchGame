using System;
using UnityEngine;

namespace Common.Scripts.Camera
{
    public class FOVScaling : MonoBehaviour
    {
        private UnityEngine.Camera _mainCamera;
        private float _height;
        private float _cameraViewSize;
        private const float _hdWeidth = 1920;
        private const float _hdHeight = 1080;


        private void Awake()
        {
            _mainCamera = UnityEngine.Camera.main;
            _height = _mainCamera.transform.position.z;
            _cameraViewSize = _mainCamera.orthographicSize;
        }

        private void Start()
        {
            SetOrthographicCameraSize();
        }

        void SetOrthographicCameraSize()
        {
            _mainCamera.orthographicSize = _mainCamera.orthographicSize * _hdWeidth/_hdHeight / _mainCamera.aspect;
        }
    }
}