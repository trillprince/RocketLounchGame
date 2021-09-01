using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfGameWindow : MonoBehaviour,IExitWindow
{
    private SceneLoader _sceneLoader;
    [SerializeField] private Button _menuButton;

    private void OnEnable()
    {
        _menuButton.onClick.AddListener(MainMenu);
    }
    private void OnDisable()
    {
        _menuButton.onClick.RemoveListener(MainMenu);
    }

    public void Exit()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        _sceneLoader.Load("Menu");
    }

}
