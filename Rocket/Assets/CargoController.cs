using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using UnityEngine;

public class CargoController : MonoBehaviour
{
    private ISatellite _satellite;
    private CargoMovement _cargoMovement;
    private CargoScaler _cargoScaler;

    public void Constructor(ISatellite satellite)
    {
        _satellite = satellite;
        _cargoMovement = new CargoMovement(transform,_satellite );
    }

    private void FixedUpdate()
    {
        _cargoMovement.Update();
    }
}
