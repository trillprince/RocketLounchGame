using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Common.Scripts.Infrastructure
{
    public class SceneLoader
    {
        public ICoroutineRunner Runner { get; }

        public SceneLoader(ICoroutineRunner coroutineRunner) => Runner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) => Runner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
