using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayGameButton : MonoBehaviour
{
    private Button _playGameButton;
    private SceneLoader _sceneLoader;
    private GameStateMachine _gameStateMachine;
    
    [Inject]
    public void Constructor(SceneLoader sceneLoader, GameStateMachine gameStateMachine)
    {
        _sceneLoader = sceneLoader;
        _gameStateMachine = gameStateMachine;
        _playGameButton = GetComponent<Button>();
    }
    
    private void Start()
    {
        _playGameButton.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        _sceneLoader.Load(SceneInfo.SceneName.LaunchScene.ToString(),ChangeGameState);
    }

    private void ChangeGameState()
    {
        _gameStateMachine.Enter<GameLoopState>();
    }
    
}
