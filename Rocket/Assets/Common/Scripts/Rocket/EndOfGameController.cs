using System;
using Common.Scripts.Infrastructure;
using Common.Scripts.Planet;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class EndOfGameController: MonoBehaviour
    {
        private EndOfGameUI _endOfGameUI;
        [SerializeField] private EndOfGameModel _endOfGameModel;

        private void OnEnable()
        {
            RocketStateController.OnLanding += OnLandingUICreate;
        }

        private void OnDisable()
        {
            RocketStateController.OnLanding += OnLandingUICreate;
        }


        private void OnLandingUICreate(LandingStatus landingStatus)
        {
            _endOfGameUI = new EndOfGameUI(CreateUI);
            _endOfGameUI.InstantiateUI();
        }
        
        private IWindow CreateUI()
        {
            IWindow window = Instantiate(_endOfGameModel.EndOfGameWindow).GetComponent<IWindow>();
            if (window != null)
            {
                return window;
            }
            return default;
        }
    }
}