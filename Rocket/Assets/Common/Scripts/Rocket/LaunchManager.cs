using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.Rocket
{
    public class LaunchManager : MonoBehaviour,IControlledButton
    {
        public delegate void Station();

        public static event Station OnRocketLaunch;
        
        
        private float _timeTillLounch = 2f;
        [SerializeField] private Button _button;

        public void Launch()
        {
            SetInteractStatus(false);
            StartCoroutine(WaitTillLaunch());
        }

        IEnumerator WaitTillLaunch()
        {
            yield return new WaitForSeconds(_timeTillLounch);
            OnRocketLaunch?.Invoke();
        }

        public void SetInteractStatus(bool isActive)
        {
            _button.gameObject.SetActive(isActive);
        }
    }
}