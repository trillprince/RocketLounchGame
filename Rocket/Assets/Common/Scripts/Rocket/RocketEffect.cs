using System;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketEffect
    {
        public IEffectAudio EffectAudio { get; }
        protected GameObject EffectGameObject { get; }
        protected RocketHealth Health { get; }

        protected RocketEffect(RocketHealth rocketHealth, GameObject effectGameObject, IEffectAudio effectAudio)
        {
            EffectAudio = effectAudio;
            EffectGameObject = effectGameObject;
            Health = rocketHealth;
        }
        protected RocketEffect(RocketHealth rocketHealth,IEffectAudio effectAudio)
        {
            EffectAudio = effectAudio;
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
            if (EffectGameObject != null)
            {
                return EffectGameObject;
            }

            return null;
        }
    }
}