using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpawnPosition
    {
        protected readonly Vector3 _screenBounds;
        protected readonly Vector3 _rocketPosition;

        protected SpawnPosition(RocketMovement rocketMovement)
        {
            _screenBounds = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - rocketMovement.Rigidbody.position.z));
            ;
            _rocketPosition = rocketMovement.GetTransform().position;
        }
    }
}