using System;
using System.Collections.Generic;
using Common.Scripts.Background;
using Common.Scripts.Infrastructure;
using Common.Scripts.Planet;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

public class EnvironmentController : MonoBehaviour, IGameStateDependable
{
    private List<IMovableEnvironment> _movableEnvironments;
    [SerializeField] private GameObject[] _movables;
    private GameState _currentGameState;
    [SerializeField] private float _movesmootheness = 500;
    private RocketController _rocketController;

    [Inject]
    private void Constructor(RocketController rocketController)
    {
        _rocketController = rocketController;
    }

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
        var rocketSpeed = _rocketController.GetComponent<RocketSpeed>();
        _movableEnvironments = new List<IMovableEnvironment>
        {
            new BgScroll(_movables[0].GetComponent<Renderer>(), _movesmootheness * 2,rocketSpeed),
            new BgScroll(_movables[1].GetComponent<Renderer>(), _movesmootheness * 3,rocketSpeed),
            new PlanetMove(_movables[2].transform,rocketSpeed)
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