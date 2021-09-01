using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayGameButton : MonoBehaviour
{
    public Button _playGameButton;
    public SceneLoader _sceneLoader;
    public GameStateMachine _gameStateMachine;
    
    [Inject]
    public void Constructor(SceneLoader sceneLoader, GameStateMachine gameStateMachine)
    {
        _sceneLoader = sceneLoader;
        _gameStateMachine = gameStateMachine;
    }
    
    private void OnEnable()
    {
        _playGameButton = GetComponent<Button>();
        _playGameButton.onClick.AddListener(PlayGame);
    }

    private void OnDisable()
    {
        _playGameButton.onClick.AddListener(PlayGame);
    }

    public void PlayGame()
    {
        _sceneLoader.Load(SceneInfo.SceneName.LaunchScene.ToString(),ChangeGameState);
    }

    private void ChangeGameState()
    {
        _gameStateMachine.Enter<GameLoopState>();
    }
    
}
