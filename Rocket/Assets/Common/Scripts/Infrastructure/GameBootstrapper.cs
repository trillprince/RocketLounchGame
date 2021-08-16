using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour,ICoroutineRunner
    {
        private Game _game;
        [SerializeField] private LoadingCurtain _loadingCurtain;
        
        private void Awake() 
        {
            _game = new Game(this,_loadingCurtain);
            _game.StateMachine.Enter<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}