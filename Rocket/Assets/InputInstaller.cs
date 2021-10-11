using System.Collections;
using System.Collections.Generic;
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
        Container.Bind<InputManager>().FromInstance(FindObjectOfType<InputManager>());
    }

    private void BuildInputOnPlatform()
    {
        _inputFactory = new InputFactory();
        Container.Bind<IInputPlatform>().FromInstance(_inputFactory.BuildPlatform(Application.platform));
    }
}