using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.Input;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    private InputFactory _inputFactory;
    public override void InstallBindings()
    {
        InstallActionMap();
    }

    private void InstallActionMap()
    {
        BuildInputOnPlatform();
        
    }

    private void BuildInputOnPlatform()
    {
        var coroutineRunner = FindObjectOfType<GameBootstrapper>().StateMachine.Loader.Runner;
        _inputFactory = new InputFactory(coroutineRunner);
        Container.Bind<InputManager>().FromInstance(new InputManager(_inputFactory.BuildPlatform(Application.platform)));
    }
}