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

        public void Show(Action OnFadeAction = null)
        {
            StartCoroutine(FadeOut(OnFadeAction));
        }

        public void Hide(Action OnFadeAction = null)
        {
            StartCoroutine(FadeIn(OnFadeAction));
        }

        private IEnumerator FadeIn(Action OnFadeAction = null)
        {
            while(Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            OnFadeAction?.Invoke();
        }
        private IEnumerator FadeOut(Action OnFadeAction = null)
        {
            while(Curtain.alpha < 1)
            {
                Curtain.alpha += 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            OnFadeAction?.Invoke();
        }

    }
}
