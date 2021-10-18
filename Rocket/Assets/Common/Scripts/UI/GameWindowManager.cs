using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.UI
{
    public class GameWindowManager : MonoBehaviour
    {
        private IPauseWindow _currentWindow;
        private IUICreator<IPauseWindow> _currentUiCreator;
        private Dictionary<Type, IUICreator<IPauseWindow>> _uiCreators;
        private Type[] _dictionaryKeys;
        private IUICreator<IPauseWindow> _uiCreator;
        [SerializeField] private WindowModels _windowModels;
        private IButtonController _buttonController;
        private IGameTimeController _gameTimeController;

        
        [Inject]
        private void Constructor(IGameTimeController gameTimeController)
        {
            _gameTimeController = gameTimeController;
        }

        private void Awake()
        {
            _buttonController = FindObjectOfType<ButtonManager>();
            InitUiCreatorDictionary();
        }
        
        private void InitUiCreatorDictionary()
        {
            _dictionaryKeys = new[] {typeof(EndOfGameUI), typeof(PauseOfGameUI)};
        
            _uiCreators = new Dictionary<Type, IUICreator<IPauseWindow>>
            {
                [typeof(EndOfGameUI)] = 
                    new EndOfGameUI(_windowModels.GetSpecificModel("EndOfGame"),
                        CreateWindow),
                [typeof(PauseOfGameUI)] = 
                    new PauseOfGameUI(_windowModels.GetSpecificModel("PauseOfGame"),
                        CreateWindow)
            };
        }

        private void CreateWindow(IUICreator<IPauseWindow> uiCreator)
        {
            _buttonController.ButtonsActive(false);
            _currentUiCreator = uiCreator;
            _currentWindow = CreateUI(_currentUiCreator);
        }

        private IPauseWindow CreateUI(IUICreator<IPauseWindow> uiCreator)
        {
            var window = Instantiate(uiCreator.GetWindowModel().GetWindowObject(),transform).GetComponentInChildren<IPauseWindow>();
            window.Constructor((() =>
            {
                _buttonController.ButtonsActive(true);
            }),_gameTimeController);
            return window;
        }

        private void OnDisable()
        {
            foreach (Type type in _dictionaryKeys)
            {
                _uiCreators[type].OnCreatorDestroy();
            }
            _windowModels.SetInitStatusDefault();
        }
    }
}