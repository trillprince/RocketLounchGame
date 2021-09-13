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
    [SerializeField] private TextMeshProUGUI _cargoDropInfoText;
    [SerializeField] private TextMeshProUGUI _landingInfoText;
    [SerializeField] private Button _menuButton;
    [SerializeField] private MissionModel _missionModel;
    private string _currentText;
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
        DisplayInfo();
    }
   
    private void Exit()
    {
        UnpauseTheGame();
        _loadingCurtain.Show((() =>
        {
            _gameStateMachine.Enter<MenuBootStrapState>();
        }));
    }

    private void ShowCargoDropInfo()
    {
        foreach (DropAccuracy accuracy in _missionModel.Accuracies)
        {
            _cargoDropInfoText.text = $"{_currentText} \n - {accuracy.ToString()}";
            _currentText = _cargoDropInfoText.text;
        }
    }

    private void ShowLandingInfo()
    {
        _landingInfoText.text = _missionModel.LandingStatus.ToString();
    }

    public void DisplayInfo()
    {
        ShowCargoDropInfo();
        ShowLandingInfo();
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseTheGame()
    {
        Time.timeScale = 1;
    }
}
