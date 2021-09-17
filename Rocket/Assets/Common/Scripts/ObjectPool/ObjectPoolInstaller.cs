using System.Collections;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using UnityEngine;
using Zenject;
using Object = System.Object;

public class ObjectPoolInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindObjectPoolStorage();
    }

    private void BindObjectPoolStorage()
    {
        ObjectPoolStorage objectPoolStorage = new ObjectPoolStorage();
        Container.Bind<ObjectPoolStorage>().FromInstance(objectPoolStorage);
    }
}
