﻿using System;
using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Infrastructure
{
    public class MenuBootStrapState: IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private LoadingCurtain _loadingCurtain;


        public MenuBootStrapState(GameStateMachine gameStateMachine,SceneLoader sceneLoader,LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Show((() =>
            {
                _sceneLoader.Load(SceneInfo.SceneName.Menu.ToString(), InitServices);
            }));
        }
        
        private void InitServices()
        {
            _loadingCurtain.Hide();
        }

        public void Exit()
        {
            
        }
    }
}