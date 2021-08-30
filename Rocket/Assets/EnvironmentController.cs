using System;
using System.Collections.Generic;
using Common.Scripts.Background;
using Common.Scripts.Infrastructure;
using Common.Scripts.Planet;
using Common.Scripts.Rocket;
using UnityEngine;

public class EnvironmentController : MonoBehaviour, IGameStateDependable
{
    private List<IMovableEnvironment> _movableEnvironments;
    [SerializeField] private GameObject[] _movables;
    private GameState _currentGameState;

    private void OnEnable()
    {
        GameStateController.OnStateSwitch += OnGameStateSwitch;
    }

    private void OnDisable()
    {
        GameStateController.OnStateSwitch -= OnGameStateSwitch;
    }

    private void Awake()
    {
        _movableEnvironments = new List<IMovableEnvironment>
        {
            new BgScroll(_movables[0].GetComponent<Renderer>(), 3000),
            new BgScroll(_movables[1].GetComponent<Renderer>(), 5000),
            new PlanetMove(_movables[2].transform)
        };
    }

    private void Update()
    {
        if (_currentGameState == GameState.CargoDrop)
        {
            foreach (IMovableEnvironment environment in _movableEnvironments)
            {
                environment.Move();
            }
        }
    }

    public void OnGameStateSwitch(GameState gameState)
    {
        _currentGameState = gameState;
        foreach (IMovableEnvironment environment in _movableEnvironments)
        {
            if (environment is IMovableOnGameState iMovable)
            {
                iMovable.OnGameStateSwitch(_currentGameState);
            }
        }
    }
}