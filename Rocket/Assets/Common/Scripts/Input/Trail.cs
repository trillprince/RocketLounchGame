using Common.Scripts.Cargo;
using UnityEngine;

namespace Common.Scripts.Input
{
    public class Trail: IUpdatable
    {
        private readonly InputManager _inputManager;
        private readonly GameObject _trailGameObject;
        private bool _touchStart;
        private Vector3 _trailPos;

        public Trail(InputManager inputManager,GameObject trailGameObject)
        {
            _inputManager = inputManager;
            _trailGameObject = trailGameObject;
        }

        public void Enable(Vector3 touchOnWorldPoints)
        {
            _trailGameObject.SetActive(true);
            _touchStart = true;
            _trailGameObject.transform.position = touchOnWorldPoints;
        }

        public void Disable()
        {
            _trailGameObject.SetActive(false);
            _touchStart = false;
        }


        public void Execute()
        {
            if (_touchStart)
            {
                _trailGameObject.transform.position = _inputManager.PrimaryPosition();
            }
        }
    }
}