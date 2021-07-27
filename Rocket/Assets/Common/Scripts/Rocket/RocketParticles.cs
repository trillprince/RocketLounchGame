using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketParticles : MonoBehaviour
    {
        [SerializeField] private GameObject _smokeParticles_1;
        [SerializeField] private GameObject _smokeParticles_2;
        [SerializeField] private GameObject _smokeParticles_3;
        [SerializeField] private GameObject _smokeParticles_4;
        private LinkedList<string> str;
        

        private void OnEnable()
        {
            LounchManager.MiddleEngineEnable += engineEnabled =>
            {
                _smokeParticles_1.SetActive(engineEnabled);
                _smokeParticles_2.SetActive(engineEnabled);
                _smokeParticles_3.SetActive(engineEnabled);
                _smokeParticles_4.SetActive(engineEnabled);
            };
        }

    }
}
