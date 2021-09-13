using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.Rocket
{
    public class LaunchManager : MonoBehaviour,IControlledButton
    {
        public delegate void Station();

        public static event Station OnRocketLounch;

        public static event Station Lounching;
        
        private float _timeTillLounch = 2f;
        private Button _button;

        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
        }


        public void Launch()
        {
            SetInteractStatus(false);
            StartCoroutine(WaitTillLounch());
        }

        IEnumerator WaitTillLounch()
        {
            yield return new WaitForSeconds(_timeTillLounch);
            OnRocketLounch?.Invoke();
        }

        public void SetInteractStatus(bool isActive)
        {
            _button.gameObject.SetActive(isActive);
        }
    }
}