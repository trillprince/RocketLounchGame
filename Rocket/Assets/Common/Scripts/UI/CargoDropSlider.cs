using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI
{
    public class CargoDropSlider : MonoBehaviour
    {
        private Slider _cargoDropSlider;
        [SerializeField] private Image _fillImage;
        private float _sliderMaxValue = 1;
        private Color _redColor = Color.red;
        private Color _yellowColor = Color.yellow;
        private Color _greenColor = Color.green;
        private bool _filled = false;
        private float _fillSpeed = 1.5f;
        private float _valueForBadDrop = 0.3f;
        private float _valueForNormalDrop = 0.8f;


        private void Awake()
        {
            _cargoDropSlider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            FillMove();
            ChangeFillColor();
        }

        void ChangeFillColor()
        {
            if (_cargoDropSlider.value < _valueForBadDrop)
            {
                SetColor(_redColor);
            }
            else if (_cargoDropSlider.value > _valueForBadDrop && _cargoDropSlider.value < _valueForNormalDrop)
            {
                SetColor(_yellowColor);
            }
            else if (_cargoDropSlider.value > _valueForNormalDrop)
            {
                SetColor(_greenColor);
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
    }
}
