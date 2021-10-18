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
    private Animator _animator;
    private IGameTimeController _gameTimeController;

    public void MainMenu()
    {
        _loadingCurtain.Show((() =>
        {
            _gameStateMachine.Enter<MenuBootStrapState>();
        }));
    }


    public void Constructor(Action onUnpauseAction, IGameTimeController gameTimeController)
    {
        _gameTimeController = gameTimeController;
    }
    
    private void Awake()
    {
        _gameStateMachine = FindObjectOfType<BootstrapAgregator>().GetStateMachine();
        _loadingCurtain = _gameStateMachine.Curtain;
        _menuButton.onClick.AddListener(Exit);
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
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
