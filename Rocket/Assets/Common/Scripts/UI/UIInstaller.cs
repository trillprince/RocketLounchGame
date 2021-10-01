using System.Collections;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindText();
    }

    private void BindText()
    {
        Container.Bind<MilesCount>().FromInstance(FindObjectOfType<MilesCount>());
    }
}