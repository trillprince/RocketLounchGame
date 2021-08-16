using System;
using System.Collections;
using Common.Scripts.Cargo;
using Common.Scripts.Input;
using Common.Scripts.MissionSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Common.Scripts.UI
{
    public class CargoDropSlider : MonoBehaviour
    {
        private DropAccuratenessText _dropAccuratenessText;
        [SerializeField] private Image _fillImage;
        private float _sliderMaxValue = 1;
        private float _sliderMinValue = 0;
        private bool _filled = false;
        private const float _fillSpeed = 1.5f;
        private DropAccuracy _currentDropAccuracy;
        private float _timeTillDisable = 1f;
        [SerializeField] private Image _perfectDropImage;
        [SerializeField] private Image _normalDropImage;
        private RectTransform _perfectDropRect;
        private RectTransform _normalDropRect;
        private Slider _cargoDropSlider;
        private int _fullFills = 0;
        private int _maxFilledCount = 2;


        public delegate void SliderStatus(DropAccuracy dropAccuracy);

        public static event SliderStatus OnGetDropAccuracy;
        
        private bool _handleActive;


        public DropAccuracy CurrentDropAccuracy
        {
            get => _currentDropAccuracy;
            set => _currentDropAccuracy = value;
        }

        private void OnEnable()
        {
            CargoDropController.CargoDropping += CargoDropping;
            CargoDropController.OnGetStartDropStatus += ActivateSlider;
        }

        private void OnDisable()
        {
            CargoDropController.CargoDropping -= CargoDropping;
            CargoDropController.OnGetStartDropStatus -= ActivateSlider;
        }
        

        private void Awake()
        {
            _cargoDropSlider = GetComponent<Slider>();
            _dropAccuratenessText = GetComponentInChildren<DropAccuratenessText>();
            _perfectDropRect = _perfectDropImage.GetComponent<RectTransform>();
            _normalDropRect = _normalDropImage.GetComponent<RectTransform>();
        }

        private void Start()
        {
            SetImagesRectTransform();
            StartCoroutine(ShowSlider(false, 0));
        }


        void SetImagesRectTransform()
        {
            SetNormalDropImageTransform();
            SetPerfectDropImageTransform();
        }

        void SetNormalDropImageTransform()
        {
            Vector2 normalDropAnchorMin = _normalDropRect.anchorMin;
            Vector2 normalDropAnchorMax = _normalDropRect.anchorMax;
            normalDropAnchorMin.y = (float) Math.Round(Random.Range(0f, 0.6f), 1);
            normalDropAnchorMax.y = normalDropAnchorMin.y + 0.4f;
            _normalDropRect.anchorMin = normalDropAnchorMin;
            _normalDropRect.anchorMax = normalDropAnchorMax;
        }

        void SetPerfectDropImageTransform()
        {
            Vector2 perfectDropAnchorMin = _perfectDropRect.anchorMin;
            Vector2 perfectDropAnchorMax = _perfectDropRect.anchorMax;
            perfectDropAnchorMin.y = _normalDropRect.anchorMin.y + 0.1f;
            perfectDropAnchorMax.y = _normalDropRect.anchorMax.y - 0.1f;
            _perfectDropRect.anchorMin = perfectDropAnchorMin;
            _perfectDropRect.anchorMax = perfectDropAnchorMax;
        }


        void CheckCurrentDropAccuracy()
        {
            if (_cargoDropSlider.value >= _perfectDropRect.anchorMin.y &&
                _cargoDropSlider.value <= _perfectDropRect.anchorMax.y)
            {
                OnGetDropAccuracy?.Invoke(DropAccuracy.Perfect);
            }
            else if (_cargoDropSlider.value >= _normalDropRect.anchorMin.y &&
                     _cargoDropSlider.value <= _normalDropRect.anchorMax.y)
            {
                OnGetDropAccuracy?.Invoke(DropAccuracy.Nice);
            }
            else
            {
                OnGetDropAccuracy?.Invoke(DropAccuracy.NotGood);
            }
        }

        private void Update()
        {
            if (_handleActive)
            {
                HandleMove();
            }
        }

        void HandleMove()
        {
            if (_cargoDropSlider.value >= _sliderMaxValue)
            {
                _filled = true;
                if (_fullFills > 0)
                {
                    _fullFills++;
                }
            }
            else if (_cargoDropSlider.value <= 0)
            {
                _filled = false;
                _fullFills++;
            }
            if (!_filled)
            {
                SetSliderValue(_cargoDropSlider.value + Mathf.Sin(Time.deltaTime * _fillSpeed));
            }
            else if (_filled)
            {
                SetSliderValue(_cargoDropSlider.value - Mathf.Sin(Time.deltaTime * _fillSpeed));
            }
            if (_fullFills == _maxFilledCount)
            {
                OnGetDropAccuracy?.Invoke(DropAccuracy.NoInteraction);
                SliderActive(false);
                _fullFills = 0;
            }
        }
        void CargoDropping()
        {
            _handleActive = false;
            CheckCurrentDropAccuracy();
            SliderActive(false);
        }

        void SliderActive(bool isActive)
        {
            _handleActive = isActive;
            if (!isActive)
            {
                StartCoroutine(ShowSlider(isActive, _timeTillDisable));
                return;
            }
            StartCoroutine(ShowSlider(isActive, 0));
        }

        IEnumerator ShowSlider(bool isActive, float timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            _dropAccuratenessText.TextActive(isActive);
            foreach (Image image in GetComponentsInChildren<Image>())
            {
                image.enabled = isActive;
            }
            SetImagesRectTransform();
            if (!isActive)
            {
                SetSliderValue(_sliderMaxValue);
                _fullFills = 0;
            }
        }

        void ActivateSlider()
        {
            SliderActive(true);                                                 
            _cargoDropSlider.value = _sliderMaxValue;
        }

        private void SetSliderValue(float value)
        {
            _cargoDropSlider.value = value;
        }
    }

}