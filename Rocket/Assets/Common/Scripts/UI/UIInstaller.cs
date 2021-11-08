using System.Collections;
using System.Collections.Generic;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindUiListener();
    }

    private void BindUiListener()
    {
        Container.Bind<IUIController>().FromInstance(new UIController());
    }

    private void BindWindows()
    {
        
    }
}