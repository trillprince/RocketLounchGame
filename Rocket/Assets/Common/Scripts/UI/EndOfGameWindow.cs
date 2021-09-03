using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class EndOfGameWindow : MonoBehaviour,IExitWindow
{
    private GameStateMachine _gameStateMachine;
    [SerializeField] private Button _menuButton;

    private void Awake()
    {
        _gameStateMachine = FindObjectOfType<LevelAgregator>().GetStateMachine();
    }

    private void Start()
    {
        _menuButton.onClick.AddListener(Exit);
    }
   
    public void Exit()
    {
        _gameStateMachine.Enter<MenuBootStrapState>();
    }
}
