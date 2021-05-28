using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiddleEngine : MonoBehaviour
{
    [SerializeField] private RocketMovement _rocketMovement;

    [Inject]
    private void Costruct(RocketMovement rocketMovement)
    {
        _rocketMovement = rocketMovement;
    }

    public void EngineIsEnabled(bool isEnabled)
    {
        _rocketMovement.MiddleEngineEnabled = isEnabled;
    }
}
