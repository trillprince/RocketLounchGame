using System;
using TMPro;
using UnityEngine;

namespace Common.Scripts.UI
{
    public class DropAccuratenessText : MonoBehaviour
    {
        private TextMeshProUGUI _textMesh;
        private CargoDropSlider _cargoDropSlider;
        private const string _notGoodDropText = "You can do better !";
        private const string _goodDropText = "Very nice !";
        private const string _perfectDropText = "Awesome !";
        private const string _noInteractionText = "Too slow !";
        private Color _redColor = Color.red;
        private Color _yellowColor = Color.yellow;
        private Color _greenColor = Color.green;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            _cargoDropSlider = GetComponentInParent<CargoDropSlider>();
        }


        private void OnEnable()
        {
            CargoDropSlider.DropAccuracy += SetTextValues;
        }

        private void OnDisable()
        {
            CargoDropSlider.DropAccuracy -= SetTextValues;
            _textMesh.text = String.Empty;
        }

        void SetTextValues(CargoDropSlider.DropAccurateness accuracy)
        {
            switch (accuracy)
            {
                case CargoDropSlider.DropAccurateness.NotGood:
                    _textMesh.text = _notGoodDropText;
                    SetTextColor(_redColor);
                    break;
                case CargoDropSlider.DropAccurateness.Nice:
                    _textMesh.text = _goodDropText;
                    SetTextColor(_yellowColor);
                    break;
                case CargoDropSlider.DropAccurateness.Perfect:
                    _textMesh.text = _perfectDropText;
                    SetTextColor(_greenColor);
                    break;
                case CargoDropSlider.DropAccurateness.NoInteraction:
                    _textMesh.text = _noInteractionText;
                    SetTextColor(_redColor);
                    break;
            }
        }

        void SetTextColor(Color color)
        {
            var tmpColor = _textMesh.color;
            tmpColor = color;
            _textMesh.color = color;
        }

        public void TextActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
