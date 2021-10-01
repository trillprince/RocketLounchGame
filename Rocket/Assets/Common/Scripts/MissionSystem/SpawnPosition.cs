using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpawnPosition
    {
        protected readonly Vector3 _screenBounds;
        protected readonly Vector3 _rocketPosition;

        public SpawnPosition(RocketMovementController rocketMovementController)
        {
            _screenBounds = _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - rocketMovementController.Rigidbody.position.z));;
            _rocketPosition = rocketMovementController.transform.position;
        }

    }
}