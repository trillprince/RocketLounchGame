using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Camera
{
    public class CameraShake : MonoBehaviour
    {
        private Transform _camTransform;
        private Vector3 _originalPos;
        private ShakeStage _currentShakeStage;
        private readonly float _lounchShakeAmount = 0.1f;
        private readonly float _flightShakeAmount = 0.04f;
        private readonly float _shakeSmoothness = 5f;
        private float _currentShakeAmount;

        enum ShakeStage
        {
            OnEarth,
            Lounch,
            Flight
        }

        private void OnEnable()
        {
            LounchManager.Lounching += engineEnabled =>
            {
                ChangeShakeStage(ShakeStage.Lounch);
            };
            LounchManager.OnRocketLounch += engineEnabled =>
            {
                ChangeShakeStage(ShakeStage.Flight);
            };

        }

        void Awake()
        {
            _camTransform = GetComponent<Transform>();
            _originalPos = _camTransform.localPosition;
        }

        private void Start()
        {
            ChangeShakeStage(ShakeStage.OnEarth);
        }


        void Update()
        {
            switch (_currentShakeStage)
            {
                case ShakeStage.OnEarth:
                    break;
                case ShakeStage.Lounch:
                    ShakeTheCamera(_lounchShakeAmount);
                    break;
                case ShakeStage.Flight:
                    ShakeTheCamera(_flightShakeAmount);
                    break;
            }
        }

        void ChangeShakeStage(ShakeStage shakeStage)
        {
            _currentShakeStage = shakeStage;
        }

        void ShakeTheCamera(float shakeAmount)
        {
            if (_currentShakeStage == ShakeStage.Lounch)
            {
                _camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;
                _currentShakeAmount = shakeAmount;
            }
            else if(_currentShakeStage == ShakeStage.Flight)
            {
                _currentShakeAmount = Mathf.Lerp(_currentShakeAmount, shakeAmount, Time.deltaTime / _shakeSmoothness);
                _camTransform.localPosition = _originalPos + Random.insideUnitSphere * _currentShakeAmount;
            }
        
        }

    }
}
