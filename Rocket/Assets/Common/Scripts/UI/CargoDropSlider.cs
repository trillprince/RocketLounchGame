using System;
using System.Collections;
using Common.Scripts.MissionSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI
{
    public class CargoDropSlider : MonoBehaviour
    {
        private Slider _cargoDropSlider;
        private DropAccuratenessText _dropAccuratenessText;
        [SerializeField] private Image _fillImage;
        private float _sliderMaxValue = 1;
        private Color _redColor = Color.red;
        private Color _yellowColor = Color.yellow;
        private Color _greenColor = Color.green;
        private bool _filled = false;
        private const float _fillSpeed = 1.5f;
        private const float _valueForBadDrop = 0.3f;
        private const float _valueForNormalDrop = 0.8f;
        private DropAccurateness _currentDropAccurateness;
        private bool _fillActive;
        private float _timeTillDisable = 2f;

        public DropAccurateness CurrentDropAccurateness
        {
            get => _currentDropAccurateness;
            set => _currentDropAccurateness = value;
        }

        private void OnEnable()
        {
            MissionManager.TimeToDrop += FillActive;
        }

        private void OnDisable()
        {
            MissionManager.TimeToDrop -= FillActive;
        }


        public enum DropAccurateness
        {
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

        void SetVisualValues()
        {
            if (_cargoDropSlider.value < _valueForBadDrop)
            {
                SetColor(_redColor);
                SetDropAccurateness(DropAccurateness.NotGood);
            }
            else if (_cargoDropSlider.value > _valueForBadDrop && _cargoDropSlider.value < _valueForNormalDrop)
            {
                SetColor(_yellowColor);
                SetDropAccurateness(DropAccurateness.Nice);
            }
            else if (_cargoDropSlider.value > _valueForNormalDrop)
            {
                SetColor(_greenColor);
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
                _cargoDropSlider.value += Time.deltaTime * _fillSpeed;
            }
            else if (_filled)
            {
                _cargoDropSlider.value -= Time.deltaTime * _fillSpeed;
            }
        }

        void SetColor(Color color)
        {
            var tmpColor = _fillImage.color;
            tmpColor = color;
            _fillImage.color = color;
        }

        void SetDropAccurateness(DropAccurateness dropAccurateness)
        {
            CurrentDropAccurateness = dropAccurateness;
        }

        public DropAccurateness GetDropAccurateness()
        {
            return _currentDropAccurateness;
        }

        void FillActive(bool isActive)
        {
            _fillActive = isActive;
            if (!_fillActive)
            {
                StartCoroutine(SliderActive(isActive, _timeTillDisable));
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
            
        }
        
    }
}