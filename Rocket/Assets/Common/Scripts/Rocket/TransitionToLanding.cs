using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TransitionToLanding: IMovementTransition
{
    private MovementState _nextMovementState;
    private Quaternion _landingRot;
    private Vector3 _landingPos;
    private Vector3 _landingScale;
    private Transform _transform;

    public TransitionToLanding(Transform transform, Transform startTransform, MovementState nextMovementState,
        Action <Action <Transform,MovementState>> onMovementChangeSubscribe,Action <Action <Transform,MovementState>> onMovementChangeUnsubscribe)
    {
        _transform = transform;
        _nextMovementState = nextMovementState;
        onMovementChangeUnsubscribe?.Invoke(Transition);
        onMovementChangeSubscribe?.Invoke(Transition);
        SaveStartTransform(startTransform);
    }
    
    private void SaveStartTransform(Transform startTranform)
    {
        _landingRot = startTranform.rotation;
        _landingPos = startTranform.position;
        _landingScale = startTranform.localScale;
    }

    
    void SetLandingTransform(Transform transform, MovementState movementState)
    {
        if (_nextMovementState == movementState)
        {
            var tmp = _landingPos;
            tmp.y += 10;
            tmp.x = _landingPos.x + Random.Range(-15f, 15f);
            _landingPos = tmp;
            transform.position = _landingPos;
            transform.localScale = _landingScale;
            transform.rotation = _landingRot;
        }
    }

    public void Transition(Transform transform, MovementState movementState)
    {
        SetLandingTransform(transform, movementState);
    }
}