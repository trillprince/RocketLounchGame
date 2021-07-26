using System;
using System.Collections;
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
        private const float _valueForBadDrop = 0.3f;
        private const float _valueForNormalDrop = 0.8f;
        private DropAccurateness _currentDropAccurateness;
        private bool _fillActive;
        private float _timeTillDisable = 2f;
        private bool _cargoDropped;
        [SerializeField] private Image _perfectDropImage;
        [SerializeField] private Image _normalDropImage;
        private RectTransform _perfectDropRect;
        private RectTransform _normalDropRect;
        private Slider _cargoDropSlider;



        public delegate void SliderStatus();

        public static  event SliderStatus NoPlayerInteraction;
        

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
            if (_fillActive)
            {
                FillMove();
                SetVisualValues();
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
            normalDropAnchorMin.y = (float)Math.Round(Random.Range(0f, 0.6f),1);
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
        

        void SetVisualValues()
        {
            if (_cargoDropSlider.value < _valueForBadDrop)
            {
                
                SetDropAccurateness(DropAccurateness.NotGood);
            }
            else if (_cargoDropSlider.value > _valueForBadDrop && _cargoDropSlider.value < _valueForNormalDrop)
            {
                
                SetDropAccurateness(DropAccurateness.Nice);
            }
            else if (_cargoDropSlider.value > _valueForNormalDrop)
            {
                
                SetDropAccurateness(DropAccurateness.Perfect);
            }
        }

        void FillMove()
        {
            if (_cargoDropSlider.value >= _sliderMaxValue)
            {
                _filled = true;
            }
            else if(_cargoDropSlider.value <= 0)
            {
                _filled = false;
            }
            if (!_filled)
            {
                _cargoDropSlider.value += Mathf.Sin(Time.deltaTime * _fillSpeed);
            }
            else if (_filled)
            {
                _cargoDropSlider.value -= Mathf.Sin(Time.deltaTime * _fillSpeed);;
            }
        }
        

        void SetDropAccurateness(DropAccurateness dropAccurateness)
        {
            CurrentDropAccurateness = dropAccurateness;
        }

        public DropAccurateness GetDropAccurateness()
        {
            return _currentDropAccurateness;
        }

        void CargoDropped()
        {
            _cargoDropped = true;
        }

        void FillActive(bool isActive)
        {
            _fillActive = isActive;
            if (!_fillActive)
            {
                if (!_cargoDropped)
                {
                    SetDropAccurateness(DropAccurateness.NoInteraction);
                    _cargoDropSlider.value = 0;
                    NoPlayerInteraction?.Invoke();
                }
                StartCoroutine(SliderActive(isActive, _timeTillDisable));
                _cargoDropped = false;
                return;
            }
            StartCoroutine(SliderActive(isActive, 0));
        }

        IEnumerator SliderActive(bool isActive,float timeToWait)
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
                FillActive(true);
            }
            else
            {
                FillActive(false);
            }
        }
        
        
    }
}