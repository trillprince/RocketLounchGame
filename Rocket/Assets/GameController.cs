using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    private GameState _currentGameState = GameState.CargoDrop;
    private LoadingCurtain _loadingCurtain;
    private float _timeBeforeStateSwitch = 3f;

    public delegate void StateSwitch(GameState state);

    public static event StateSwitch OnStateSwitch;

    private event Action StateSwitching; 
    
    private void Awake()
    {
        _loadingCurtain = FindObjectOfType<LoadingCurtain>();
    }

    void OnEnable()
    {
        LounchManager.OnRocketLounch += engineEnabled =>
        {
            SetGameState(GameState.CargoDrop);
        };
        DropStatusController.OnOutOfCargo += () =>
        {
            StartCoroutine(WaitTillStateSwitch());
        };
        StateSwitching += () =>
        {
            SetGameState(GameState.Landing);
        };
    }

    void SetGameState(GameState state)
    {
        _currentGameState = state;
        OnStateSwitch?.Invoke(state);
        _loadingCurtain.Hide();
    }

    IEnumerator WaitTillStateSwitch()
    {
        yield return new WaitForSeconds(_timeBeforeStateSwitch); 
        _loadingCurtain.Show(StateSwitching);
    }
}


public enum GameState
{
    CargoDrop,
    Landing
}
