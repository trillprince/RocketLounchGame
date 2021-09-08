using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

public class GameWindowManager : MonoBehaviour
{
    private IWindow _currentWindow;
    private Dictionary<Type, IUICreator<IWindow>> _uiCreators;
    private IUICreator<IWindow> _uiCreator;
    [SerializeField] private WindowModels _windowModels;
    private IWindowModel _currentWindowModel;
    private Action <IUICreator<IWindow>> _action;

    private void Awake()
    {
        InitUiCreatorDictionary();
    }

    private void OnEnable()
    {
        _action += UpdateWindow;
    }

    private void OnDisable()
    {
        _action -= UpdateWindow;
    }

    private void InitUiCreatorDictionary()
    {
        _uiCreators = new Dictionary<Type, IUICreator<IWindow>>
        {
            [typeof(EndOfGameUI)] = new EndOfGameUI(_windowModels.GetSpecificModel(typeof(EndOfGameUI)),_action),
            [typeof(PauseOfGameUI)] = new PauseOfGameUI(_windowModels.GetSpecificModel(typeof(PauseOfGameUI)),_action)
        };
    }

    private void UpdateWindow(IUICreator<IWindow> uiCreator)
    {
        _currentWindow = CreateUI(uiCreator);
    }

    private IWindow CreateUI(IUICreator<IWindow> uiCreator)
    {
        return Instantiate(uiCreator.GetWindowModel().GetWindowObject()).GetComponentInChildren<IWindow>();
    }
    
}