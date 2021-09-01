using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour,ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Constructor(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        private void Awake() 
        {
            _gameStateMachine.Enter<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}