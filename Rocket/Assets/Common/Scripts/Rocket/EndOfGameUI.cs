using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.Planet;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class EndOfGameUI: IEventSubscriber,IUICreator<IPauseWindow>
    {
        private IWindowModel _windowModel;
        private Action<IUICreator<IPauseWindow>> _onEventAction;
        private readonly string _key = "EndOfGame";

        public EndOfGameUI(IWindowModel windowModel,Action<IUICreator<IPauseWindow>> onEventAction)
        {
            _windowModel = windowModel;
            _onEventAction = onEventAction;
            Subscribe();
        }

        public void Subscribe()
        {
            RocketStateController.OnLanding += OnRocketLandingUiCreate;
        }
        public void Unsubscribe()
        {
            RocketStateController.OnLanding -= OnRocketLandingUiCreate;
        }
        
        private void OnRocketLandingUiCreate(LandingStatus landingStatus)
        {
            _onEventAction?.Invoke(this);
        }

        public IWindowModel GetWindowModel()
        {
            return _windowModel;
        }

        public string GetKey()
        {
            return _key;
        }
    }
}