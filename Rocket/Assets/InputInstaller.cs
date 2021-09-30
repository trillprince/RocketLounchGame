using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        var swipeDetection = new SwipeDetection(FindObjectOfType<InputManager>()); 
        Container.Bind<SwipeDetection>()
            .FromInstance(swipeDetection);

    }
}
