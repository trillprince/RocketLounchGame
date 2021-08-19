using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Planet;
using UnityEngine;

public class LandingController : MonoBehaviour
{
    private MovementStateController _movementStateMovementState;

    public static event Action Landing;

    private void OnEnable()
    {
        GameController.OnStateSwitch += OnChangeGameState;
    }

    private void OnDisable()
    {
        GameController.OnStateSwitch -= OnChangeGameState;
    }

    private void OnChangeGameState(GameState state)
    {
        if (state == GameState.Landing)
        {
            Landing?.Invoke();
        }
    }
}
