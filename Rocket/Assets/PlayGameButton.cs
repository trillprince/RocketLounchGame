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
    private GameBootstrapper _gameBootstrapper;

    private void Awake()
    {
        _gameBootstrapper = FindObjectOfType<GameBootstrapper>();
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

    private void PlayGame()
    {
        _gameBootstrapper.Loader.Load(SceneInfo.SceneName.LaunchScene.ToString(),ChangeGameState);
    }

    private void ChangeGameState()
    {
        _gameBootstrapper.StateMachine.Enter<GameLoopState>();
    }
    
}
