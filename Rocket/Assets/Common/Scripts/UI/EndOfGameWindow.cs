using System;
using Common.Scripts.Infrastructure;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfGameWindow : MonoBehaviour, IPauseWindow
{
    private GameStateMachine _gameStateMachine;
    [SerializeField] private Button _playAgainButton;
    public Text _highScoreText;
    public Text _scoreText;
    public TextMeshProUGUI _rewardInfo;
    private LoadingCurtain _loadingCurtain;
    private Animator _animator;
    private IGameTimeController _gameTimeController;
    private IGameLoopController _gameLoopController;
    private ILevelInfo _levelInfo;
    private RocketDistance _coveredDistance;
    private PlayerDataSaver _playerDataSaver;
    private string[] _animationNames;
    private RocketController _rocketController;

    public void Constructor(Action onUnpauseAction, IGameTimeController gameTimeController,
        IGameLoopController gameLoopController, RocketController rocketController, PlayerDataSaver playerDataSaver,
        IGameStateController gameStateController)
    {
        _gameTimeController = gameTimeController;
        _gameLoopController = gameLoopController;
        _coveredDistance = rocketController.CoveredDistance;
        _rocketController = rocketController;
        _playerDataSaver = playerDataSaver;
    }

    private void Awake()
    {
        _gameStateMachine = FindObjectOfType<BootstrapAgregator>().GetStateMachine();
        _loadingCurtain = _gameStateMachine.Curtain;
        _playAgainButton.onClick.AddListener(PlayAgain);
        _animator = GetComponent<Animator>();
        _animationNames = new[] {"Pop_up", "Pop_down"};
    }

    private void FillInfo()
    {
        var score = _coveredDistance.CoveredDistance;
        var highScore = _playerDataSaver.GetScore();

        SetTextColor(score, highScore);
        SetTextInfo(score, highScore);
    }

    private void SetTextInfo(float score, float highScore)
    {
        _scoreText.text = $"Score : {(int) score}";
        _highScoreText.text = $"HighScore : {highScore}";
    }

    private void SetTextColor(float score, int highScore)
    {
        if (score > highScore)
        {
            _scoreText.color = Color.green;
            _highScoreText.color = Color.green;
        }
        else
        {
            _highScoreText.color = Color.green;
        }
    }

    private void SetRewardInfo()
    {
       _rewardInfo.text = $"{_rocketController.Inventory.GetCurrentCoinValue()} <sprite=0> " +
                          $"\n Current value : {_playerDataSaver.GetCoins()}";
    }

    private void Start()
    {
        _playerDataSaver.SaveCoins(_rocketController.Inventory.GetCurrentCoinValue());
        SetRewardInfo();
        FillInfo();
        _animator.Play(_animationNames[0]);
    }

    public void Pause()
    {
        _gameTimeController.Pause();
    }

    public void UnPause()
    {
        _gameTimeController.UnPause();
    }

    private void PlayAgain()
    {
        UnPause();
        _animator.SetBool("Pop_Down", true);
    }

    public void TransitionToLevelStart()
    {
        UnPause();
        _gameStateMachine.Enter<GameLoopState, string>("LaunchScene");
    }
}