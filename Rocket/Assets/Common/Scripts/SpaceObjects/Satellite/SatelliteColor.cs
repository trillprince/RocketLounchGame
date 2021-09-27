using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteColor
    {
        private MeshRenderer _meshRenderer;
        private Color _currentColor;
        private bool _finalColorIsSet = false;

        public SatelliteColor(MeshRenderer meshCollider)
        {
            _meshRenderer = meshCollider;
        }

        public void SetColor(Color color)
        {
            if (_finalColorIsSet)
            {
                return;
            }
            _currentColor = color;
            _meshRenderer.material.color = color;
        }

        public void SetFinalColor()
        {
            _finalColorIsSet = true;
        }

        public bool IsCurrentColor(Color color)
        {
           return _currentColor == color;
        }
    }
}