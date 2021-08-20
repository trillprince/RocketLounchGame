using UnityEngine;

public interface IMovementTransition
{
    void Transition(Transform transform, MovementState state);
}