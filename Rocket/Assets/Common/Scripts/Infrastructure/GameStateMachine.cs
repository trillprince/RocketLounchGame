using System;
using System.Collections.Generic;

namespace Common.Scripts.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootStrapState)] = new BootStrapState(this,sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this,sceneLoader)
                
            };
        }
        public void Enter<TState>() where TState : IState
        {  
            _activeState?.Exit();
            IState state = _states[typeof(TState)];
            _activeState = state;
            state.Enter();
        }
    }
}