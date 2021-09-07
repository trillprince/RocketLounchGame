using System;
using Common.Scripts.Infrastructure;
using Common.Scripts.MissionSystem;
using Common.Scripts.Planet;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class EndOfGameController: MonoBehaviour
    {
        private EndOfGameUI <IExitWindow> _endOfGameUI;
        [SerializeField] private EndOfGameModel _endOfGameModel;

        private void OnEnable()
        {
            RocketStateController.OnLanding += OnLandingUICreate;
        }

        private void OnDisable()
        {
            RocketStateController.OnLanding -= OnLandingUICreate;
        }


        private void OnLandingUICreate(LandingStatus landingStatus)
        {
            _endOfGameUI = new EndOfGameUI<IExitWindow>(CreateUI);
            _endOfGameUI.InstantiateUI().FillWithInfo();

        }

        private IExitWindow CreateUI()
        {
            return Instantiate(_endOfGameModel.EndOfGameWindow).GetComponentInChildren<IExitWindow>();
        }

    }
}