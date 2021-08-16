using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameState _currentGameState = GameState.CargoDrop;

    public static Action <GameState> OnChangeGameState;

    void OnEnable()
    {
        LounchManager.OnRocketLounch += engineEnabled =>
        {
            SetGameState(GameState.CargoDrop);
        };
        DropStatusController.OnOutOfCargo += () =>
        {
            SetGameState(GameState.Landing);
        };
    }

    void SetGameState(GameState state)
    {
        _currentGameState = state;
        OnChangeGameState?.Invoke(state);
    }
 
}

public enum GameState
{
    CargoDrop,
    Landing
}
