using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.Rocket
{
    public class LaunchManager : MonoBehaviour,IControlledButton
    {
        private Button _button;
        public delegate void Station();
        public static event Station OnRocketLaunch;
        public static event Station RocketLaunching;

        
        private float _timeTillLounch = 1.7f;
        private bool _interactable = true;

        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
            _button.onClick.AddListener(Launch);
        }

        private void Launch()
        {
            StartCoroutine(WaitTillLaunch());
            SetInteractStatus(false);
            _interactable = false;
        }

        IEnumerator WaitTillLaunch()
        {
            RocketLaunching?.Invoke();
            yield return new WaitForSeconds(_timeTillLounch);
            OnRocketLaunch?.Invoke();
        }

        public void SetInteractStatus(bool isActive)
        {
            if (!_interactable && isActive)
            {
                return;
            }
            _button.gameObject.SetActive(isActive);
        }
    }
}