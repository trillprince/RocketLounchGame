using System;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class DefaultRocketCollisionBehaviour : IRocketCollisionBehaviour
    {
        public void Collide(Collider collider, Action<Collider> applyCollision)
        {
            applyCollision?.Invoke(collider);
        }
    }
}