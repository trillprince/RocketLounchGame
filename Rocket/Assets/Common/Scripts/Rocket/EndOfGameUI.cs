using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.Planet;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class EndOfGameUI: IUICreator<IPauseWindow>
    {
        private readonly IWindowModel _windowModel;
        private readonly Action<IUICreator<IPauseWindow>> _onEventAction;
        private readonly string _key = "EndOfGame";

        public EndOfGameUI(IWindowModel windowModel,Action<IUICreator<IPauseWindow>> onEventAction, IEventSubscriber<LandingStatus> iEventSubscriber)
        {
            _windowModel = windowModel;
            _onEventAction = onEventAction;
            iEventSubscriber.Subscribe(OnRocketLandingUiCreate);
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