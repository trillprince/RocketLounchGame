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
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class EndOfGameWindow : MonoBehaviour,IPauseWindow
{
    private GameStateMachine _gameStateMachine;
    [SerializeField] private Button _menuButton;
    public Text _levelNumber;
    private LoadingCurtain _loadingCurtain;
    private Animator _animator;
    private IGameTimeController _gameTimeController;
    private IGameLoopController _gameLoopController;
    private ILevelInfo _levelInfo;

    public void MainMenu()
    {
        _gameStateMachine.Enter<MenuBootStrapState>();
    }


    public void Constructor(Action onUnpauseAction, IGameTimeController gameTimeController,
        IGameLoopController gameLoopController, ILevelInfo levelInfo)
    {
        _gameTimeController = gameTimeController;
        _gameLoopController = gameLoopController;
        _levelInfo = levelInfo;
    }
    
    private void Awake()
    {
        _gameStateMachine = FindObjectOfType<BootstrapAgregator>().GetStateMachine();
        _loadingCurtain = _gameStateMachine.Curtain;
        _menuButton.onClick.AddListener(Exit);
        _animator = GetComponent<Animator>();
    }

    private void FillInfo()
    {
        _levelNumber.text = $"Level : {_levelInfo.GetLevelNumber()}";
    }

    private void Start()
    {
        FillInfo();
        _gameLoopController.DisableGameLoop();
        _animator.Play("Pop_up");
    }

    public void Pause()
    {
        _gameTimeController.Pause();
    }

    public void UnPause()
    {
        _gameTimeController.UnPause();
    }
   
    private void Exit()
    {
        UnPause();
        _animator.SetBool("MainMenu",true);
    }

   
}
