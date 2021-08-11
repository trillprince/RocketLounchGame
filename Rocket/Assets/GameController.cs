using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private MissionModelViewer _missionModelViewer;
    private GameState _currentGameState = GameState.CargoDrop;

    public static Action <GameState> OnChangeGameState;

    void OnEnable()
    {
        LounchManager.OnRocketLounch += engineEnabled =>
        {
            SetGameState(GameState.CargoDrop);
        };
        DropStatusController.Landing += () =>
        {
            SetGameState(GameState.Landing);
        };
    }

    void Awake()
    {
        _missionModelViewer = GetComponentInChildren<MissionModelViewer>();
    }

    void Start()
    {
        _missionModelViewer.InitMissionModel(3,5);
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
