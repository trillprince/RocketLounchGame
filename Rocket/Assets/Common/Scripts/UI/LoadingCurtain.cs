using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        public CanvasGroup Curtain;
        private GraphicRaycaster _graphicRaycaster;
        private Coroutine _currentCoroutine;

        private void Awake()
        {
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            DontDestroyOnLoad(this);
        }

        public void Show(Action OnFadeAction = null)
        {
            _currentCoroutine = StartCoroutine(FadeOut(OnFadeAction));
        }

        public void Hide(Action OnFadeAction = null)
        {
            _currentCoroutine = StartCoroutine(FadeIn(OnFadeAction));
        }

        private IEnumerator FadeIn(Action OnFadeAction = null)
        {
            while (_currentCoroutine != null)
            {
                yield return null;
            }
            while(Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.06f;
                yield return new WaitForSeconds(0.03f);
            }
            OnFadeAction?.Invoke();
            _currentCoroutine = null;
        }
        private IEnumerator FadeOut(Action OnFadeAction = null)
        {
            while (_currentCoroutine != null)
            {
                yield return null;
            }
            while(Curtain.alpha < 1)
            {
                Curtain.alpha += 0.06f;
                yield return new WaitForSeconds(0.03f);
            }
            OnFadeAction?.Invoke();
            _currentCoroutine = null;
        }

    }
}
