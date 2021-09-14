using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteSpawner : MonoBehaviour
{
    private ISatelliteFactory _satelliteFactory;

    private void Awake()
    {
        _satelliteFactory = GetComponent<BasicSatelliteFactory>();
    }

    public void SpawnSatellite()
    {
        var satellite = _satelliteFactory.CreateSatellite();
    }
}