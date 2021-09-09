using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

public class BootstrapAgregator : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;
    public LoadingCurtain Curtain { get; private set; }

    [Inject]
    public void Constructor(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain)
    {
        _gameStateMachine = gameStateMachine;
        Curtain = loadingCurtain;
    }

    public GameStateMachine GetStateMachine()
    {
        return _gameStateMachine;
    }
    
}
