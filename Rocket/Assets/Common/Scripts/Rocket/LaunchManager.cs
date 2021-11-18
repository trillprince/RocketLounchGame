using System;
using System.Collections;
using Common.Scripts.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class LaunchManager : MonoBehaviour
    {
        public delegate void Station();

        public event Station OnRocketLaunch;
        public event Station RocketLaunching;


        private float _timeTillLounch = 3;
        private InputManager _inputManager;
        private bool _rocketLauched;

        [Inject]
        public void Constructor(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void OnEnable()
        {
            _inputManager.OnTouch += Launch;
        }

        private void OnDisable()
        {
            _inputManager.OnTouch -= Launch;
        }

        private void Launch()
        {
            if(_rocketLauched) return;
            StartCoroutine(WaitTillLaunch());
        }

        IEnumerator WaitTillLaunch()
        {
            _rocketLauched = true;
            RocketLaunching?.Invoke();
            yield return new WaitForSeconds(_timeTillLounch);
            OnRocketLaunch?.Invoke();
        }
    }
}