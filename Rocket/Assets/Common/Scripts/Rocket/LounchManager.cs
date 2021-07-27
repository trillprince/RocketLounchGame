using System.Collections;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class LounchManager : MonoBehaviour
    {
        public delegate void Station(bool engineEnabled);
        public static event Station MiddleEngineEnable;
        public static event Station MiddleEngineDisable;

        public static event Station Lounching;
        private float _timeTillLounch = 2f;
        

        public void MiddleEngine(bool isEnabled)
        {
            if (isEnabled)
            {
                Lounching?.Invoke(true);
                StartCoroutine(WaitTillLounch(isEnabled));
                return;
            }

            MiddleEngineDisable?.Invoke(isEnabled);
        }

        IEnumerator WaitTillLounch(bool isEnabled)
        {
            yield return new WaitForSeconds(_timeTillLounch);
            MiddleEngineEnable?.Invoke(isEnabled);
        }
    }
}
