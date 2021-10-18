using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.Planet;
using Common.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class EndOfGameUI: IUICreator<IPauseWindow>
    {
        private readonly IWindowModel _windowModel;
        private readonly Action<IUICreator<IPauseWindow>> _onEventAction;
        private readonly string _key = "EndOfGame";

        public EndOfGameUI(IWindowModel windowModel,
            Action<IUICreator<IPauseWindow>> onEventAction)
        {
            _windowModel = windowModel;
            _onEventAction = onEventAction;
            OnCreatorCreate();
        }

        private void OnGameOverUiCreate()
        {
            _onEventAction?.Invoke(this);
        }

        public IWindowModel GetWindowModel()
        {
            return _windowModel;
        }

        public void OnCreatorDestroy()
        {
            GameLoopController.OnGameOver -= OnGameOverUiCreate;
        }

        public void OnCreatorCreate()
        {
            GameLoopController.OnGameOver += OnGameOverUiCreate;
        }
        public string GetKey()
        {
            return _key;
        }
    }
}