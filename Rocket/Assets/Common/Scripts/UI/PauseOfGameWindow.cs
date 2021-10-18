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
        private IGameTimeController _gameTimeController;
        private RectTransform _rectTransform;
        private Animator _animator;


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
        }

        public void Constructor(Action onUnpauseAction,IGameTimeController gameTimeController)
        {
            _onUnpauseAction = onUnpauseAction;
            _gameTimeController = gameTimeController;
        }

        public void Pause()
        {
            _gameTimeController.Pause();
        }

        public void UnPause()
        {
            _onUnpauseAction?.Invoke();
            Destroy(transform.parent.gameObject);
        }


        private void Continue()
        {
            _gameTimeController.UnPause();
            _animator.Play("Pop_down");
        }

        private void MainMenu()
        {
            _gameStateMachine.Enter<MenuBootStrapState>();
        }

        private void SetStartPosition()
        {
            var yPos = Screen.height + _rectTransform.rect.height;
            Debug.Log(yPos);
            _rectTransform.position = new Vector3(_rectTransform.position.x, yPos, _rectTransform.position.z);
        }

    }
}
