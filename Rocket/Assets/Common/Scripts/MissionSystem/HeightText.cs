using TMPro;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class HeightText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;

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
        

        void ShowRocketHeight()
        {
            _textMesh.text = Height?.Invoke().ToString();
        }
    
    }
}
