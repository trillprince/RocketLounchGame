using TMPro;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteCount: MonoBehaviour,IControlledText
    {
        private TextMeshProUGUI _textMesh;
        private int _satelliteCount = 0;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            UpdateView();
        }

        public void AddSatellite()
        {
            _satelliteCount++;
            UpdateView();
        }

        private void UpdateView()
        {
            _textMesh.text = _satelliteCount.ToString();
        }

        public void IsActive(bool isActive)
        {
            _textMesh.enabled = isActive;
        }
    }
}