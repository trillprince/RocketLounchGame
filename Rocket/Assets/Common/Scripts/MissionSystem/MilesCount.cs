using System.Globalization;
using TMPro;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class MilesCount: MonoBehaviour,IControlledText
    {
        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        public void IsActive(bool isActive)
        {
            _textMesh.enabled = isActive;
        }
    }
}