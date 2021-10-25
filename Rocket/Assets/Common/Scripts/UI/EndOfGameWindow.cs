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
    public Text _highScoreText;
    public Text _scoreText;
    private LoadingCurtain _loadingCurtain;
    private Animator _animator;
    private IGameTimeController _gameTimeController;
    private IGameLoopController _gameLoopController;
    private ILevelInfo _levelInfo;
    private RocketDistance _coveredDistance;
    private PlayerDataSaver _playerDataSaver;

    public void MainMenu()
    {
        _gameStateMachine.Enter<MenuBootStrapState>();
    }


    public void Constructor(Action onUnpauseAction, IGameTimeController gameTimeController,
        IGameLoopController gameLoopController,RocketController rocketController,PlayerDataSaver playerDataSaver)
    {
        _gameTimeController = gameTimeController;
        _gameLoopController = gameLoopController;
        _coveredDistance = rocketController.CoveredDistance;
        _playerDataSaver = playerDataSaver;
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
        var score = _coveredDistance.CoveredDistance;
        var highScore = _playerDataSaver.GetScore();
        
        SetTextColor(score, highScore);
        SetTextInfo(score, highScore);
    }

    private void SetTextInfo(float score,float highScore)
    {
        _scoreText.text = $"Score : {(int) score}";
        _highScoreText.text = $"HighScore : {highScore}";

    }

    private void SetTextColor(float score, int highScore)
    {
        if (score > highScore)
        {
            _scoreText.color = Color.green;
            _highScoreText.color = Color.green;
        }
        else
        {
            _highScoreText.color = Color.green;
        }
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