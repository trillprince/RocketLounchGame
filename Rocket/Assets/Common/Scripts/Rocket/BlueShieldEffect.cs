using System;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.Boosters
{
    public class BlueShieldEffect : RocketEffect
    {
        private bool _boostActive;
        private Action _endOfEffectAction;

        public BlueShieldEffect(RocketHealth rocketHealth, GameObject effectGameObject, IEffectAudio effectAudio)
            : base(rocketHealth, effectGameObject, effectAudio)
        {
            Health.OnDamage += OnDamage;
        }

        private void OnDamage()
        {
            if (_boostActive)
            {
                EffectAudio.PlayFxAudioClip("Energy Shield Damage");
                DiscardEffect();
            }
        }

        public override void Boost(Action endOfEffectAction)
        {
            EffectAudio.SoundActive(true);
            _endOfEffectAction = endOfEffectAction;
            _boostActive = true;
            Health.AddHealth(1);
        }

        public override void DiscardEffect()
        {
            EffectAudio.SoundActive(false);
            Health.OnDamage -= OnDamage;
            _boostActive = false;
            _endOfEffectAction?.Invoke();
        }
    }
}