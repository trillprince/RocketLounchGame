using System;
using Common.Scripts.Infrastructure;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Common.Scripts.UI
{
    public class PauseOfGameWindow : MonoBehaviour, IPauseWindow
    {
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _continueButton;
        private GameStateMachine _gameStateMachine;
        private Action _onUnpauseAction;
        private IGameTimeController _gameTimeController;
        private RectTransform _rectTransform;
        private Animator _animator;
        private IGameLoopController _gameLoopController;
        private ILevelInfo _levelInfo;
        private IGameStateController _gameStateController;


        private void Awake()
        {
            _gameStateMachine = FindObjectOfType<BootstrapAgregator>().GetStateMachine();
            _rectTransform = GetComponent<RectTransform>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _tryAgainButton.onClick.AddListener(ReplayAnim);
            _continueButton.onClick.AddListener(Continue);
        }

        private void ReplayAnim()
        {
            _animator.Play("Pop_downToReplay");
        }

        private void Start()
        {
            Pause();
            SetStartPosition();
        }

        public void Constructor(Action onUnpauseAction,IGameTimeController gameTimeController,
            IGameLoopController gameLoopController,RocketController rocketController,PlayerDataSaver playerDataSaver,
            IGameStateController gameStateController)
        {
            _onUnpauseAction = onUnpauseAction;
            _gameTimeController = gameTimeController;
            _gameLoopController = gameLoopController;
            _gameStateController = gameStateController;
            
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

        private void Replay()
        {
            UnPause();
            _gameStateMachine.Curtain.Show((() =>
            {
                _gameLoopController.DisableGameLoop();
                _gameStateMachine.Enter<GameLoopState,string>("LaunchScene");
            }));
        }

        private void SetStartPosition()
        {
            var yPos = Screen.height + _rectTransform.rect.height;
            _rectTransform.position = new Vector3(_rectTransform.position.x, yPos, _rectTransform.position.z);
        }

    }
}
