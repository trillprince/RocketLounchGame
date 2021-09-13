﻿using System;
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
        private readonly IEventSubscriber<LandingStatus> _iEventSubscriber;
        private readonly Action _constructorAction;
        private readonly string _key = "EndOfGame";

        public EndOfGameUI(IWindowModel windowModel,
            Action<IUICreator<IPauseWindow>> onEventAction, 
            IEventSubscriber<LandingStatus> iEventSubscriber,
            Action constructorAction)
        {
            _windowModel = windowModel;
            _onEventAction = onEventAction;
            _iEventSubscriber = iEventSubscriber;
            _constructorAction = constructorAction;
            OnCreatorUse();
        }

        private void OnRocketLandingUiCreate(LandingStatus landingStatus)
        {
            _onEventAction?.Invoke(this);
        }

        public IWindowModel GetWindowModel()
        {
            return _windowModel;
        }

        public void OnCreatorDestroy()
        {
            _iEventSubscriber.Unsubscribe(OnRocketLandingUiCreate);
        }

        public void OnCreatorUse()
        {
            _iEventSubscriber.Subscribe(OnRocketLandingUiCreate);
        }

        public void ConstructorAction()
        {
            _constructorAction?.Invoke();
        }

        public string GetKey()
        {
            return _key;
        }
    }
}