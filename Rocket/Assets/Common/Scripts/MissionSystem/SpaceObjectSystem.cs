﻿using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectSystem : ISpaceObjectSystem
    {
        private readonly InputListener _inputListener;
        private readonly SatelliteStateChanger _satelliteStateChanger;
        private bool _satelliteSystemActive;
        private ISpaceObjectController[] _spaceObjectControllers;

        public SpaceObjectSystem
        (
            InputListener inputListener,
            SatelliteStateChanger satelliteStateChanger,
            params ISpaceObjectController [] spaceObjectControllers
        )
        {
            _inputListener = inputListener;
            _satelliteStateChanger = satelliteStateChanger;
            _spaceObjectControllers = spaceObjectControllers;
        }

        public void SpawnRandomSideSatellite()
        {
            var range = Random.Range(0, _spaceObjectControllers.Length + 1);
            var randomObjectIndex = Random.Range(0, range);
            
            for (int i = 0; i < range; i++)
            {
                if (!(i == randomObjectIndex && range > 2 ))
                {
                    _spaceObjectControllers[i].Spawn();
                }
            }
        }

        public void Execute()
        {
            if (_satelliteSystemActive)
            {
                foreach (var spaceObjectController in _spaceObjectControllers)
                {
                    spaceObjectController.Execute();
                }
                _satelliteStateChanger.Execute();
            }
        }

        public void Disable()
        {
            _satelliteSystemActive = false;
            foreach (var spaceObjectController in _spaceObjectControllers)
            {
                spaceObjectController.Disable();
            }
            _satelliteStateChanger.Disable();
        }

        public void Enable()
        {
            _satelliteSystemActive = true;
            foreach (var spaceObjectController in _spaceObjectControllers)
            {
                spaceObjectController.Enable();
            }
            _satelliteStateChanger.Enable();
        }
    }
}