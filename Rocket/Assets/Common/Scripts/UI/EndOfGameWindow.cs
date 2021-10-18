using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class EndOfGameWindow : MonoBehaviour,IPauseWindow
{
    private GameStateMachine _gameStateMachine;
    [SerializeField] private Button _menuButton;
    private LoadingCurtain _loadingCurtain;
    
    
    public void Constructor(Action onUnpauseAction)
    {
        
    }


    private void Awake()
    {
        _gameStateMachine = FindObjectOfType<BootstrapAgregator>().GetStateMachine();
        _loadingCurtain = _gameStateMachine.Curtain;
        _menuButton.onClick.AddListener(Exit);
    }

    private void Start()
    {
        PauseTheGame();
    }
   
    private void Exit()
    {
        UnpauseTheGame();
        _loadingCurtain.Show((() =>
        {
            _gameStateMachine.Enter<MenuBootStrapState>();
        }));
    }

    public void PauseTheGame()
    {
        // Time.timeScale = 0;
    }

    public void UnpauseTheGame()
    {
        // Time.timeScale = 1;
    }
}
