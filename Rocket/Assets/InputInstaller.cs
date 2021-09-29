using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private GameObject _trailGameObject;

    public override void InstallBindings()
    {
        var swipeDetection = new SwipeDetection(FindObjectOfType<InputManager>(), _trailGameObject); 
        Container.Bind<SwipeDetection>()
            .FromInstance(swipeDetection);

    }
}
