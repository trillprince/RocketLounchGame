using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using UnityEngine;
using Zenject;

public class MenuContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameStateMachine>().FromInstance(FindObjectOfType<GameBootstrapper>().StateMachine);
    }
}
