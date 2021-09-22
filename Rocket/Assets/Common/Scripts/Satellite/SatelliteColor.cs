using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteColor
    {
        private MeshRenderer _meshRenderer;
        private Color _currentColor;

        public SatelliteColor(MeshRenderer meshCollider)
        {
            _meshRenderer = meshCollider;
        }

        public void SetColor(Color color)
        {
            if (color == _currentColor)
            {
                return;
            }
            _currentColor = color;
            _meshRenderer.material.color = color;
        }

        public bool IsCurrentColor(Color color)
        {
           return _currentColor == color;
        }
    }
}