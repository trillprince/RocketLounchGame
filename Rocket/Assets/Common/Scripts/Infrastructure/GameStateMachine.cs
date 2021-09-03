using System;
using System.Collections.Generic;
using Common.Scripts.UI;

namespace Common.Scripts.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        public IExitableState ActiveState { get; private set; }
        private SceneLoader _loader;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _loader = sceneLoader;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootStrapState)] = new BootStrapState(this, sceneLoader),
                [typeof(MenuBootStrapState)] = new MenuBootStrapState(this,sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this,sceneLoader)
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad payload) where TState : class, IPayloadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            ActiveState?.Exit();

            TState state = GetState<TState>();
            ActiveState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}