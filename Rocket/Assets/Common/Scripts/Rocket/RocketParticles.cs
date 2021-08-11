using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketParticles : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _particles;
        

        private void OnEnable()
        {
            EnableParticles(false);
            LounchManager.OnRocketLounch += engineEnabled =>
            {
                EnableParticles(engineEnabled);
            };
        }

        void EnableParticles(bool isEnabled)
        {
            if (isEnabled)
            {
                foreach (ParticleSystem particle in _particles)
                {
                    particle.Play();
                }
                return;
            }
            foreach (ParticleSystem particle in _particles)
            {
                particle.Stop();
            }
        }

    }
}
