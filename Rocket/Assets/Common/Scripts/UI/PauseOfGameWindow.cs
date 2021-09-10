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
        private Action _onUnpauseAction;

        public void Constructor(Action onUnpauseAction)
        {
            _onUnpauseAction = onUnpauseAction;
        }

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

        public void PauseTheGame()
        {
            Time.timeScale = 0;
        }

        public void UnpauseTheGame()
        {
            Time.timeScale = 1;
            _onUnpauseAction?.Invoke();
            Destroy(gameObject);
        }

        private void MainMenu()
        {
            Time.timeScale = 1;
            _gameStateMachine.Enter<MenuBootStrapState>();
        }

    }
}
