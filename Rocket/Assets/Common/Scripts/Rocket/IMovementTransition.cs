using UnityEngine;

namespace Common.Scripts.Rocket
{
    public interface IMovementTransition
    {
        void Transition(Transform transform, MovementState state);
    }
}