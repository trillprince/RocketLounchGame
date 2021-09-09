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
            _munuButton.onClick.AddListener(MainMenu);
            _continueButton.onClick.AddListener((() =>
            {
                UnpauseTheGame();
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

        public void UnpauseTheGame(Action onUnpause = null)
        {
            Time.timeScale = 1;
            onUnpause?.Invoke();
            Destroy(gameObject);
        }

        private void MainMenu()
        {
            Time.timeScale = 1;
            _gameStateMachine.Enter<MenuBootStrapState>();
        }
    }
}
