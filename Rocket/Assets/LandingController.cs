using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Planet;
using UnityEngine;

public class LandingController : MonoBehaviour
{
    private PlanetMove _planetMove;
    private RocketMovementController _rocketMovement;

    private void OnEnable()
    {
        GameController.OnChangeGameState += OnChangeGameState;
    }

    private void OnChangeGameState(GameState state)
    {
        if (state == GameState.Landing)
        {
            _planetMove.MoveToDefaultPos();
            _rocketMovement.ChangeMovementType();
        }
    }
}
