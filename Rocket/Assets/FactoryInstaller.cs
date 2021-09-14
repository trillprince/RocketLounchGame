using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    [SerializeField] private BasicSatelliteFactory _basicSatelliteFactory;
    public override void InstallBindings()
    {
        BindSatelliteFactory();
    }

    private void BindSatelliteFactory()
    {
        Container.Bind<BasicSatelliteFactory>().FromInstance(_basicSatelliteFactory);
    }
}
