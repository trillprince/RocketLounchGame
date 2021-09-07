using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class EndOfGameWindow : MonoBehaviour,IExitWindow
{
    private GameStateMachine _gameStateMachine;
    [SerializeField] private TextMeshProUGUI _cargoDropInfoText;
    [SerializeField] private TextMeshProUGUI _landingInfoText;
    [SerializeField] private Button _menuButton;
    [SerializeField] private MissionModel _missionModel;
    private string _currentText;

    private void Awake()
    {
        _gameStateMachine = FindObjectOfType<BootstrapAgregator>().GetStateMachine();
    }

    private void Start()
    {
        _menuButton.onClick.AddListener(Exit);
    }
   
    public void Exit()
    {
        _gameStateMachine.Enter<MenuBootStrapState>();
    }

    public void FillWithInfo()
    {
        foreach (DropAccuracy accuracy in _missionModel.Accuracies)
        {
            _cargoDropInfoText.text = $"{_currentText} \n - {accuracy.ToString()}";
            _currentText = _cargoDropInfoText.text;
        }
        _landingInfoText.text = _missionModel.LandingStatus.ToString();
    }
}
