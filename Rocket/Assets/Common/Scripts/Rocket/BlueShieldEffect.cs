using System;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.Boosters
{
    public class BlueShieldEffect : RocketEffect
    {
        private bool _boostActive;
        private Action _endOfEffectAction;

        public BlueShieldEffect(RocketHealth rocketHealth, GameObject effectGameObject, BlueShieldAudio blueShieldAudio)
            : base(rocketHealth, effectGameObject, blueShieldAudio)
        {
            Health.OnDamage += OnDamage;
        }

        private void OnDamage()
        {
            if (_boostActive)
            {
                ShieldAudio.ShieldDamageSound();
                DiscardEffect();
            }
        }

        public override void Boost(Action endOfEffectAction)
        {
            ShieldAudio.ShieldSoundActive(true);
            _endOfEffectAction = endOfEffectAction;
            _boostActive = true;
            Health.AddHealth(1);
        }

        public override void DiscardEffect()
        {
            ShieldAudio.ShieldSoundActive(false);
            Health.OnDamage -= OnDamage;
            _boostActive = false;
            _endOfEffectAction?.Invoke();
        }
    }
}