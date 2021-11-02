using System;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.Boosters
{
    public class BlueShieldEffect : RocketEffect
    {
        private int _currentShieldPoints = 2;
        private bool _boostActive;
        private Action _endOfEffectAction;

        public BlueShieldEffect(RocketHealth rocketHealth, GameObject effectGameObject, BlueShieldAudio blueShieldAudio)
            : base(rocketHealth,effectGameObject,blueShieldAudio)
        {
            Health.OnDamage += OnDamage;
        }

        private void OnDamage()
        {
            if (_boostActive && _currentShieldPoints > 0)
            {
                ShieldAudio.ShieldDamageSound();
                _currentShieldPoints--;
                if (_currentShieldPoints < 1)
                {
                    DiscardEffect();
                }
            }
            
        }

        public override void Boost(Action endOfEffectAction)
        {
            ShieldAudio.ShieldSoundActive(true);
            _endOfEffectAction = endOfEffectAction;
            _boostActive = true;
            Health.AddHealth(2);
        }

        public override void DiscardEffect()
        {
            ShieldAudio.ShieldSoundActive(false);
            Health.OnDamage -= OnDamage;
            Health.DamageRocket(_currentShieldPoints);
            _boostActive = false;
            _endOfEffectAction?.Invoke();
        }
    }
}