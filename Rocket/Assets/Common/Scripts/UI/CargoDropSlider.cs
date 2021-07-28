using System;
using System.Collections;
using Common.Scripts.Input;
using Common.Scripts.MissionSystem;
using Common.Scripts.UI.InGame;
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
        private bool _filled = false;
        private const float _fillSpeed = 1.5f;
        private DropAccurateness _currentDropAccurateness;
        private bool _handleActive;
        private float _timeTillDisable = 2f;
        private bool _cargoDropped;
        [SerializeField] private Image _perfectDropImage;
        [SerializeField] private Image _normalDropImage;
        private RectTransform _perfectDropRect;
        private RectTransform _normalDropRect;
        private Slider _cargoDropSlider;


        public delegate void SliderStatus(DropAccurateness dropAccuracy);

        public static event SliderStatus DropAccuracy;
        


        public DropAccurateness CurrentDropAccurateness
        {
            get => _currentDropAccurateness;
            set => _currentDropAccurateness = value;
        }

        private void OnEnable()
        {
            MissionManager.TimeToDrop += DropTimeListener;
            CargoDropListener.CargoDropped += CargoDropped;
        }

        private void OnDisable()
        {
            MissionManager.TimeToDrop -= DropTimeListener;
            CargoDropListener.CargoDropped -= CargoDropped;
        }


        public enum DropAccurateness
        {
            NoInteraction,
            NotGood,
            Nice,
            Perfect
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
            StartCoroutine(SliderActive(false, 0));
        }

        void Update()
        {
            if (_handleActive)
            {
                HandleMove();
            }
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
                SetDropAccurateness(DropAccurateness.Perfect,DropAccuracy);
            }
            else if (_cargoDropSlider.value >= _normalDropRect.anchorMin.y &&
                     _cargoDropSlider.value <= _normalDropRect.anchorMax.y)
            {
                SetDropAccurateness(DropAccurateness.Nice,DropAccuracy);
            }
            else
            {
                SetDropAccurateness(DropAccurateness.NotGood,DropAccuracy);
            }
        }

        void HandleMove()
        {
            if (_cargoDropSlider.value >= _sliderMaxValue)
            {
                _filled = true;
            }
            else if (_cargoDropSlider.value <= 0)
            {
                _filled = false;
            }

            if (!_filled)
            {
                _cargoDropSlider.value += Mathf.Sin(Time.deltaTime * _fillSpeed);
            }
            else if (_filled)
            {
                _cargoDropSlider.value -= Mathf.Sin(Time.deltaTime * _fillSpeed);
                
            }
        }


        void SetDropAccurateness(DropAccurateness dropAccurateness,SliderStatus accuracyEvent)
        {
            CurrentDropAccurateness = dropAccurateness;
            accuracyEvent?.Invoke(CurrentDropAccurateness);
        }

        public DropAccurateness GetDropAccurateness()
        {
            return _currentDropAccurateness;
        }

        void CargoDropped()
        {
            _cargoDropped = true;
            CheckCurrentDropAccuracy();
        }

        void HandleActive(bool isActive)
        {
            _handleActive = isActive;
            if (!_handleActive)
            {
                if (!_cargoDropped)
                {
                    SetDropAccurateness(DropAccurateness.NoInteraction,DropAccuracy);
                    _cargoDropSlider.value = 0;
                }
                StartCoroutine(SliderActive(isActive, _timeTillDisable));
                _cargoDropped = false;
                return;
            }

            StartCoroutine(SliderActive(isActive, 0));
        }

        IEnumerator SliderActive(bool isActive, float timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            _dropAccuratenessText.TextActive(isActive);
            foreach (Image image in GetComponentsInChildren<Image>())
            {
                image.enabled = isActive;
            }
            SetImagesRectTransform();
        }

        void DropTimeListener(MissionManager.DropStatus dropStatus)
        {
            if (dropStatus == MissionManager.DropStatus.Start)
            {
                HandleActive(true);
            }
            else
            {
                HandleActive(false);
            }
        }
    }
}