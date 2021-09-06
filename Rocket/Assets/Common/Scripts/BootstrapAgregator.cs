using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using UnityEngine;
using Zenject;

public class BootstrapAgregator : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;
    
    [Inject]
    public void Constructor(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public GameStateMachine GetStateMachine()
    {
        return _gameStateMachine;
    }
    
}
