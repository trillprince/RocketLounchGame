using System;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;
using Object = System.Object;

namespace Common.Scripts.Infrastructure
{
    public class BootstrapContext : MonoInstaller
    {
        private BootStrapFactory _factory;
        

        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            _factory = new BootStrapFactory(new BootstrapProvider());
            
            var stateMachine = _factory.CreateStateMachine();
            Container.Bind<GameStateMachine>().FromInstance(stateMachine);
            Container.Bind<SceneLoader>().FromInstance(stateMachine.Loader);
        }
    }
}