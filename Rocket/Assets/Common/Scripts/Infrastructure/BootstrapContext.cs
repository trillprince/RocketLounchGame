using System;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class BootstrapContext : MonoInstaller
    {
        [SerializeField] private GameObject _coroutineRunner;

        public override void InstallBindings()
        {
            BindGameStateMachine();
        }
        
        private void BindGameStateMachine()
        {
            CoroutineRunner co = Container.InstantiatePrefabForComponent<CoroutineRunner>(_coroutineRunner);
            SceneLoader sceneLoader = new SceneLoader(co);
            Container.Bind<SceneLoader>().FromInstance(sceneLoader);
            GameStateMachine gameBootstrapper = new GameStateMachine(sceneLoader);
            Container.Bind<GameStateMachine>().FromInstance(gameBootstrapper);

        }

    }
}


