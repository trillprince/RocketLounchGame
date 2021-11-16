using System;
using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;

public class HologramEffect : RocketEffect
{
    private RocketSpeed _rocketSpeed;
    private int _increasedSpeed;
    private readonly float secondsToRotate = 3;
    private float _secondsSoFar;
    private bool _speedIncreaseReady;

    public HologramEffect(RocketController rocketController, IEffectAudio effectAudio)
        : base(rocketController, effectAudio)
    {
        _rocketSpeed = RocketController.Movement.RocketSpeed;
    }

    public override void Boost(Action endOfEffectAction)
    {
        RocketController.CollisionController.RocketCollisionBehaviour = new HologramCollisionBehaviour();
        IncreaseSpeed();
    }

    private void IncreaseSpeed()
    {
        _increasedSpeed = _rocketSpeed.CurrentSpeed * 2;
        _speedIncreaseReady = true;
    }

    public override void Execute()
    {
        if(!_speedIncreaseReady) return;
        _secondsSoFar += Time.deltaTime;
        float t = _secondsSoFar / secondsToRotate;
        _rocketSpeed.CurrentSpeed = (int) Mathf.Lerp(_rocketSpeed.CurrentSpeed, _increasedSpeed, t);
    }
}