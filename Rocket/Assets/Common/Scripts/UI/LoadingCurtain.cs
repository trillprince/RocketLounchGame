using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Scripts.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        public CanvasGroup Curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show(Action action = null)
        {
            StartCoroutine(FadeOut(action));
        }

        public void Hide(Action action = null)
        {
            StartCoroutine(FadeIn(action));
        }

        private IEnumerator FadeIn(Action action = null)
        {
            while(Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            action?.Invoke();
        }
        private IEnumerator FadeOut(Action action = null)
        {
            while(Curtain.alpha < 1)
            {
                Curtain.alpha += 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            action?.Invoke();
        }

    }
}
