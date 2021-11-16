using System;
using Common.Scripts.Cargo;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketEffect: IUpdatable    {
        protected RocketController RocketController { get; }
        public IEffectAudio EffectAudio { get; }
        protected GameObject EffectGameObject { get; }

        protected RocketEffect(RocketController rocketController, GameObject effectGameObject, IEffectAudio effectAudio)
        {
            RocketController = rocketController;
            EffectAudio = effectAudio;
            EffectGameObject = effectGameObject;
        }
        protected RocketEffect(RocketController rocketController,IEffectAudio effectAudio)
        {
            EffectAudio = effectAudio;
            RocketController = rocketController;
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

        public virtual void Execute()
        {
            
        }
    }
}