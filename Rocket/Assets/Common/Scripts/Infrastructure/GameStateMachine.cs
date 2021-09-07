using System;
using System.Collections.Generic;
using Common.Scripts.UI;

namespace Common.Scripts.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;
        private SceneLoader _loader;
        public LoadingCurtain Curtain { get; }

        public GameStateMachine(SceneLoader sceneLoader,LoadingCurtain loadingCurtain)
        {
            _loader = sceneLoader;
            Curtain = loadingCurtain;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootStrapState)] = new BootStrapState(this,loadingCurtain, sceneLoader),
                [typeof(MenuBootStrapState)] = new MenuBootStrapState(this,sceneLoader,loadingCurtain),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain),
                [typeof(GameLoopState)] = new GameLoopState(this,sceneLoader,loadingCurtain)
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
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}