using System;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketEffect
    {
        public BlueShieldAudio ShieldAudio { get; }
        protected GameObject EffectGameObject { get; }
        protected RocketHealth Health { get; }

        protected RocketEffect(RocketHealth rocketHealth, GameObject effectGameObject, BlueShieldAudio blueShieldAudio)
        {
            ShieldAudio = blueShieldAudio;
            EffectGameObject = effectGameObject;
            Health = rocketHealth;
        }

        public virtual void Boost(Action endOfEffectAction)
        {
            
        }

        public virtual void DiscardEffect()
        {
            
        }

        public GameObject GetEffectGameObject()
        {
            return EffectGameObject;
        }
    }
}