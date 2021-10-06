using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallActionMap();
    }

    private void InstallActionMap()
    {
        Container.Bind<TouchControls>().FromInstance(new TouchControls());
        Container.Bind<InputManager>().FromInstance(FindObjectOfType<InputManager>());
    }
}
