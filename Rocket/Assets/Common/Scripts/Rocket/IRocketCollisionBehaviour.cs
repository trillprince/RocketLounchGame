using System;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public interface IRocketCollisionBehaviour
    {
        public void Collide(Collider collider, Action<Collider> applyCollision);
    }
}