using System;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.Boosters
{
    public class BlueShieldEffect : RocketEffect
    {
        private bool _boostActive;
        private Action _endOfEffectAction;

        public BlueShieldEffect(RocketController rocketController, GameObject effectGameObject, IEffectAudio effectAudio)
            : base(rocketController, effectGameObject, effectAudio)
        {
            RocketController.Health.OnDamage += OnDamage;
        }

        private void OnDamage()
        {
            if (_boostActive)
            {
                EffectAudio.PlayFxAudioClip();
                DiscardEffect();
            }
        }

        public override void Boost(Action endOfEffectAction)
        {
            EffectAudio.SoundActive(true);
            _endOfEffectAction = endOfEffectAction;
            _boostActive = true;
            RocketController.Health.AddHealth(1);
        }

        public override void DiscardEffect()
        {
            EffectAudio.SoundActive(false);
            RocketController.Health.OnDamage -= OnDamage;
            _boostActive = false;
            _endOfEffectAction?.Invoke();
        }
    }
}