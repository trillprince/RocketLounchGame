using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
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
        Container.Bind<GameStateMachine>().FromInstance(FindObjectOfType<GameBootstrapper>().StateMachine);
    }
}
