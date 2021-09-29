using System;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;
using Object = System.Object;

namespace Common.Scripts.Infrastructure
{
    public class BootstrapContext : MonoInstaller
    {
        [SerializeField] private GameObject _coroutineRunner;
        [SerializeField] private LoadingCurtain _loadingCurtain;
        private ITempFactory _factory;
        

        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            _factory = new BootStrapFactory(new BootstrapProvider());
            CoroutineRunner co = Container.InstantiatePrefabForComponent<CoroutineRunner>(_coroutineRunner);
            SceneLoader sceneLoader = new SceneLoader(co);
            Container.Bind<SceneLoader>().FromInstance(sceneLoader);
            GameStateMachine gameBootstrapper = new GameStateMachine(sceneLoader, _loadingCurtain);
            Container.Bind<GameStateMachine>().FromInstance(gameBootstrapper);
        }
    }
}