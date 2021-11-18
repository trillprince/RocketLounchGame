using System;
using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;

public class HologramEffect : RocketEffect
{
    private RocketSpeed _rocketSpeed;
    private int _increasedSpeed;
    private readonly float _secondsToRotate = 5;
    private float _secondsToTurnOff = 10;
    private float _secondsSoFar;
    private bool _speedIncreaseReady;
    private Action _endOfEffectAction;
    private int _savedRocketSpeed;

    public HologramEffect(RocketController rocketController, IEffectAudio effectAudio)
        : base(rocketController, effectAudio)
    {
        _rocketSpeed = RocketController.Movement.RocketSpeed;
    }

    public override void Boost(Action endOfEffectAction)
    {
        _endOfEffectAction = endOfEffectAction;
        RocketController.CollisionController.RocketCollisionBehaviour = new HologramCollisionBehaviour();
        IncreaseSpeed();
    }

    private void IncreaseSpeed()
    {
        _increasedSpeed = _rocketSpeed.CurrentSpeed * 2;
        _savedRocketSpeed = _rocketSpeed.CurrentSpeed;
        _speedIncreaseReady = true;
    }

    public override void Execute()
    {
        if(!_speedIncreaseReady) return;
        _secondsSoFar += Time.deltaTime;
        float t = _secondsSoFar / _secondsToRotate;
        _rocketSpeed.CurrentSpeed = (int) Mathf.Lerp(_rocketSpeed.CurrentSpeed, _increasedSpeed, t);
        if (_secondsSoFar >= _secondsToTurnOff)
        {
            float f = _secondsSoFar / _secondsToRotate * 3;
            _rocketSpeed.CurrentSpeed = (int) Mathf.Lerp(_rocketSpeed.CurrentSpeed, _increasedSpeed - _savedRocketSpeed, f);
            RocketController.Graphics.SetShadersDefault();
            RocketController.CollisionController.SetCollisionBehaviorToDefault();
            _endOfEffectAction?.Invoke();
        } 
    }
}