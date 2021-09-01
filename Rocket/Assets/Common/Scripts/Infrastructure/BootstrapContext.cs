using System;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class BootstrapContext : MonoInstaller,ICoroutineRunner
    {
        private SceneLoader _sceneLoader;
        private GameObject _gameBootStrapper;

        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().FromNew().AsSingle().WithArguments(BindSceneLoader());
        }

        private SceneLoader BindSceneLoader()
        {
            _sceneLoader = new SceneLoader(this);
            Container.Bind<SceneLoader>().FromInstance(_sceneLoader);
            return _sceneLoader;
        }
    }
}


