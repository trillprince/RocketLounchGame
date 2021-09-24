using System;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightSpaceObjectController: ISpaceObjectController
    {
        private readonly ISpaceObjectSpawner _rightSpaceObjectSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private GameStateController _gameStateController;
        private readonly GameLoopController _gameLoopController;
        private Queue<ISatellite> _rightMovableSatellites = new Queue<ISatellite>(10);
        public ISatellite RightScopedSatellite { get; private set; }


        public RightSpaceObjectController(
            ISpaceObjectSpawner rightSpaceObjectSpawner,
            RocketMovementController rocketMovementController,
            GameStateController gameStateController,
            GameLoopController gameLoopController)
        {
            _rightSpaceObjectSpawner = rightSpaceObjectSpawner;
            _rocketMovementController = rocketMovementController;
            _gameStateController = gameStateController;
            _gameLoopController = gameLoopController;
        }

        private void CreateSatellite()
        {
            GameObject gameObject;
            gameObject = _rightSpaceObjectSpawner.Spawn();
            ISatellite satellite = gameObject.GetComponent<ISatellite>();
            satellite.Constructor(_rocketMovementController,_gameStateController,this,_gameLoopController);
            if (!SatellitesExist())
            {
                ChangeScopedSatellite(satellite);
            }
            _rightMovableSatellites.Enqueue(satellite);
        }

        public void Spawn()
        {
            CreateSatellite();
        }

        public void DisposeLastSatellite()
        {
            GameObject gameObject = _rightMovableSatellites.Dequeue().GetGameObject();
            if (_rightMovableSatellites.Count > 0)
            {
                ChangeScopedSatellite(_rightMovableSatellites.Peek());
            }
            _rightSpaceObjectSpawner.Dispose(gameObject);
        }

        public void DisposeAll()
        {
            for (int i = 0; i < _rightMovableSatellites.Count; i++)
            {
                _rightSpaceObjectSpawner.Dispose(_rightMovableSatellites.Dequeue().GetGameObject());
            }
        }

        public void Execute()
        {
            if (_rightMovableSatellites.Count > 0)
            {
                foreach (var satellite in _rightMovableSatellites.ToArray())
                {
                    satellite.Execute();
                }
            }
        }

        private void ChangeScopedSatellite(ISatellite satellite)
        {
            if (satellite != null)
            {
                RightScopedSatellite = satellite;
            }
        }

        public bool SatellitesExist()
        {
            return _rightMovableSatellites.Count > 0;
        }

        public void ScopeToNextSatellite()
        {
            var array = _rightMovableSatellites.ToArray();
            ISatellite satellite = array[array.Length - 1];
            if ( satellite != null)
            {
                RightScopedSatellite = satellite;
            }
            else
            {
                ScopeToNextSatellite();
            }
        }

    }
}