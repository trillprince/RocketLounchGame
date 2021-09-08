using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayGameButton : MonoBehaviour, IGameStateChanger
{
    private Button _playGameButton;
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Constructor(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    private void OnEnable()
    {
        _playGameButton = GetComponent<Button>();
        _playGameButton.onClick.AddListener(ChangeGameState);
    }

    private void OnDisable()
    {
        _playGameButton.onClick.RemoveListener(ChangeGameState);
    }

    public void ChangeGameState()
    {
        _playGameButton.enabled = false;
        _gameStateMachine.Enter<LoadLevelState,string>(SceneInfo.SceneName.LaunchScene.ToString());
    }
}