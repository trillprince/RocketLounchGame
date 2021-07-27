using Common.Scripts.Infrastructure;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour,ICoroutineRunner
{
    private Game _game;

    private void Awake()
    {
        _game = new Game();
        _game.StateMachine.Enter<BootStrapState>();
        DontDestroyOnLoad(this);
    }
}