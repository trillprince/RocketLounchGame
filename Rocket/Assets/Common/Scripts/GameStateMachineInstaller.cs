using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

public class GameStateMachineInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindGameStateMachine();
    }

    private void BindGameStateMachine()
    {
        var stateMachine = FindObjectOfType<GameBootstrapper>().StateMachine;
        Container.Bind<GameStateMachine>().FromInstance(stateMachine);
        Container.Bind<LoadingCurtain>().FromInstance(stateMachine.Curtain);
    }
}
