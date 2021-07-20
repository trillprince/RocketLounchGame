using UnityEngine;

namespace Common.Scripts.Planet
{
    public class PlanetRotatition : MonoBehaviour
    {
        private float _rotationSpeed = 5f;
        void Rotate()
        {
            transform.Rotate(transform.rotation.x, Time.deltaTime * _rotationSpeed,transform.rotation.z );
        }
    
        private void FixedUpdate()
        {
            Rotate();
        }
    }
}
