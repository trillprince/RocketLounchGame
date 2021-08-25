using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TransitionToLanding: IMovementTransition
{
    private MovementState _transisionMovementState;
    private Quaternion _landingRot;
    private Vector3 _landingPos;
    private Vector3 _landingScale;
    private Action<Action<Transform, MovementState>> _onMovementChangeSubscribe;
    private Action<Action<Transform, MovementState>> _onMovementChangeUnsubscribe;

    public TransitionToLanding(Transform startTransform, MovementState transitionMovementState,
        Action <Action <Transform,MovementState>> onMovementChangeSubscribe,Action <Action <Transform,MovementState>> onMovementChangeUnsubscribe)
    {
        _transisionMovementState = transitionMovementState;
        _onMovementChangeSubscribe = onMovementChangeSubscribe;
        _onMovementChangeUnsubscribe = onMovementChangeUnsubscribe;
        SaveStartTransform(startTransform);
        SubscribeToEvents();
    }
    
    ~TransitionToLanding()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        _onMovementChangeSubscribe?.Invoke(Transition);
    }
    
    private void UnsubscribeFromEvents()
    {
        _onMovementChangeUnsubscribe?.Invoke(Transition);
    }
    
    private void SaveStartTransform(Transform startTranform)
    {
        _landingRot = startTranform.rotation;
        _landingPos = startTranform.position;
        _landingScale = startTranform.localScale;
    }

    
    void SetLandingTransform(Transform transform, MovementState movementState)
    {
        if (_transisionMovementState == movementState)
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