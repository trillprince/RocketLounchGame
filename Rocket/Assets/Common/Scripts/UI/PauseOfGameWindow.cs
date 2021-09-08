using System;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI
{
    public class PauseOfGameWindow : MonoBehaviour, IPauseWindow
    {
        [SerializeField] private Button _munuButton;
        [SerializeField] private Button _continueButton;
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = FindObjectOfType<BootstrapAgregator>().GetStateMachine();
        }

        private void OnEnable()
        {
            _munuButton.onClick.AddListener((() =>
            {
                _gameStateMachine.Enter<MenuBootStrapState>();
            }));
            
            _continueButton.onClick.AddListener((() =>
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            }));
        }

        private void Start()
        {
            PauseTheGame();
        }

        public void PauseTheGame(Action onPause = null)
        {
            Time.timeScale = 0;
            onPause?.Invoke();
        }

    }
}
