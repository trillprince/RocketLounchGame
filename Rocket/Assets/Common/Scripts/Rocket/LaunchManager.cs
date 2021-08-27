using System.Collections;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class LaunchManager : MonoBehaviour
    {
        public delegate void Station();

        public static event Station OnRocketLounch;

        public static event Station Lounching;
        private float _timeTillLounch = 2f;


        public void Launch()
        {
            Lounching?.Invoke();
            StartCoroutine(WaitTillLounch());
        }

        IEnumerator WaitTillLounch()
        {
            yield return new WaitForSeconds(_timeTillLounch);
            OnRocketLounch?.Invoke();
        }
    }
}