using System;
using Common.Scripts.Infrastructure;
using Common.Scripts.MissionSystem;
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
        private IGameTimeController _gameTimeController;
        private RectTransform _rectTransform;
        private Animator _animator;
        private IGameLoopController _gameLoopController;
        private ILevelInfo _levelInfo;


        private void Awake()
        {
            _gameStateMachine = FindObjectOfType<BootstrapAgregator>().GetStateMachine();
            _rectTransform = GetComponent<RectTransform>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _munuButton.onClick.AddListener(MainMenu);
            _continueButton.onClick.AddListener(Continue);
        }

        private void Start()
        {
            SetStartPosition();
            Pause();
        }

        public void Constructor(Action onUnpauseAction,IGameTimeController gameTimeController,IGameLoopController gameLoopController, ILevelInfo levelInfo)
        {
            _onUnpauseAction = onUnpauseAction;
            _gameTimeController = gameTimeController;
            _gameLoopController = gameLoopController;
            _levelInfo = levelInfo;
        }

        public void Pause()
        {
            _gameTimeController.Pause();
        }

        public void UnPause()
        {
            _gameTimeController.UnPause();
            _onUnpauseAction?.Invoke();
            Destroy(transform.parent.gameObject);
        }


        private void Continue()
        {
            _animator.Play("Pop_down");
        }

        private void MainMenu()
        {
            Continue();
            _gameLoopController.DisableGameLoop();
            _gameStateMachine.Enter<MenuBootStrapState>();
        }

        private void SetStartPosition()
        {
            var yPos = Screen.height + _rectTransform.rect.height;
            _rectTransform.position = new Vector3(_rectTransform.position.x, yPos, _rectTransform.position.z);
        }

    }
}
