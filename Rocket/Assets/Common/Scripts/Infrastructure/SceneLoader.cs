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

        public IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
