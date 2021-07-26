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
        }

        private void Start()
        {
            SetPosForPerfectDropImage();
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

        void SetPosForPerfectDropImage()
        {
            RectTransform rectTransform = _perfectDropImage.GetComponent<RectTransform>();
            Vector2 anchorMin = rectTransform.anchorMin;
            Vector2 anchorMax = rectTransform.anchorMax;
            anchorMin.y = (float)Math.Round(Random.Range(0f, 0.8f),1);
            anchorMax.y = anchorMin.y + 0.2f;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
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
            SetPosForPerfectDropImage();
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