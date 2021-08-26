using UnityEngine;

namespace Common.Scripts.Camera
{
    public class BoundariesCheck
    {
        private Vector2 _screenBounds;
        private float _objectWidth;
        private float _objectHeight;
        private Rigidbody _rb;
        private Bounds _boundOfMesh;
        private MeshCollider _meshCollider;

        public delegate void Boundaries();
        public event Boundaries OnBoundariesCheck; 

        public BoundariesCheck(Rigidbody rigidbody, MeshCollider meshCollider, UnityEngine.Camera camera)
        {
            _rb = rigidbody;
            _boundOfMesh = meshCollider.bounds;
            _screenBounds =
                camera.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - _rb.position.z));
        }

        public void OnScreenBoundaries(float force,Boundaries afterBoundCheckAction)
        {
            Vector3 viewPos = _rb.position;
            if (viewPos.x <= _screenBounds.x + _boundOfMesh.size.x ||
                viewPos.x >= _screenBounds.x * -1 - _boundOfMesh.size.x ||
                viewPos.y <= _screenBounds.y + _boundOfMesh.size.y ||
                viewPos.y >= _screenBounds.y * -1 - _boundOfMesh.size.y)
            {
                viewPos.x = Mathf.Clamp(viewPos.x,
                    _screenBounds.x + _boundOfMesh.size.x,
                    _screenBounds.x * -1 - _boundOfMesh.size.x);
                viewPos.y = Mathf.Clamp(viewPos.y,
                    _screenBounds.y + _boundOfMesh.size.y,
                    _screenBounds.y * -1 - _boundOfMesh.size.y);
                _rb.position = viewPos;
                _rb.velocity = default;
                _rb.AddForce(-_rb.velocity * force,ForceMode.Impulse);
            }
            afterBoundCheckAction.Invoke();
        }
    }
}