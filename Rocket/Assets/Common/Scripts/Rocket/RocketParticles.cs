using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Scripts.Rocket
{
    public class RocketParticles : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _engineParticles;
        [SerializeField] private List<ParticleSystem> _sparksParticles;
        private RocketHealth _rocketHealth;
        private RocketSpeed _rocketSpeed;

        private void Awake()
        {
            _rocketHealth = GetComponentInParent<RocketController>().Health;
            _rocketHealth.OnDamage += PlaySparksParticles;
            _rocketSpeed = GetComponentInParent<RocketSpeed>();
        }
        
        private void OnEnable()
        {
            LaunchManager.OnRocketLaunch += EnableEngineParticles;
        }

        private void OnDisable()
        {
            LaunchManager.OnRocketLaunch += EnableEngineParticles;
            _rocketHealth.OnDamage -= PlaySparksParticles;
        }

        private void EnableEngineParticles()
        {
            foreach (ParticleSystem particle in _engineParticles)
            {
                particle.Play();
            }
        }

        private void PlaySparksParticles()
        {
            foreach (var particle in _sparksParticles)
            {
                var emission = particle.emission;
                emission.rateOverTime = _rocketSpeed.GetCurrentSpeed();
                particle.Play();
            }
        }
    }
}