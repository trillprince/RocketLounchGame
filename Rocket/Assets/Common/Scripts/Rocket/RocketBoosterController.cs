﻿using System;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketBoosterController
    {
        private readonly RocketHealth _health;
        private readonly RocketMovement _movement;
        private Func<GameObject,Transform,GameObject> _instantiate;
        private readonly Action<GameObject> _destroyGo;
        private RocketEffect _rocketEffect;
        private GameObject _currentEffectObject;

        public RocketBoosterController(RocketHealth health, RocketMovement movement,
            Func<GameObject,Transform,GameObject> instantiate,Action<GameObject> destroyGo)
        {
            _health = health;
            _movement = movement;
            _instantiate = instantiate;
            _destroyGo = destroyGo;
        }

        public void ApplyHealthBooster(RocketEffect rocketEffect)
        {
            _rocketEffect?.DiscardEffect();
            _rocketEffect = rocketEffect;
            _rocketEffect.Boost(DiscardEffect);
            InstantiateEffect(_rocketEffect.GetEffectGameObject());
        }

        private void InstantiateEffect(GameObject gameObject)
        {
            _currentEffectObject = _instantiate?.Invoke(gameObject,_movement.GetTransform());
        }

        private void DiscardEffect()
        {
            _rocketEffect = null;
            _destroyGo?.Invoke(_currentEffectObject);
        }
    }
}