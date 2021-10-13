using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.SpaceObjects.Asteroid
{
    public class AsteroidMove: MoveComponent    {

        public AsteroidMove(RocketMovement rocketMovement, Transform transform):base(rocketMovement,transform)
        {
        }

    }
}