using TMPro;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class HeightText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        private Color _defaultColor = Color.white;
        private Color _dropReadyColor = Color.green;

        public delegate float HeightValue();

        public static event HeightValue Height;
        
        private void Update()
        {
            ShowRocketHeight();
        }

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            MissionManager.TimeToDrop += SetColorToDrop;
        }

        private void OnDisable()
        {
            MissionManager.TimeToDrop -= SetColorToDrop;
        }

        void SetColorToDrop(bool readyToDrop)
        {
            if (readyToDrop)
            {
                _textMesh.color = _dropReadyColor;
                return;
            }

            _textMesh.color = _defaultColor;

        }

        void ShowRocketHeight()
        {
            _textMesh.text = Height?.Invoke().ToString();
        }
    
    }
}
