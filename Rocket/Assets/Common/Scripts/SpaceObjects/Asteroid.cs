using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.Satellite;
using UnityEngine;

namespace Common.Scripts.SpaceObjects
{
    public class Asteroid : MonoBehaviour, ISpaceObject
    {
        private AsteroidStateOnScreen _asteroidStateOnScreen;
        private AsteroidMove _asteroidMove;
        private AsteroidDelivery _asteroidDelivery;
        private ISpawnPosition _spawnPosition;

        public Vector3 GetSpawnPosition()
        {
            return _spawnPosition.GetSpawnPosition();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public Transform GetTransform()
        {
            return transform;
        }
        
        public void Constructor(RocketMovementController rocketMovementController,
            ISpaceObjectController spaceObjectController,
            GameLoopController gameLoopController, ISpawnPosition spawnPosition)
        {
            
            _asteroidMove = new AsteroidMove(rocketMovementController, transform);
            _spawnPosition = spawnPosition;
            _asteroidDelivery = new AsteroidDelivery(spaceObjectController,gameLoopController);
            _asteroidStateOnScreen = new AsteroidStateOnScreen(GetComponent<MeshCollider>(), 
                transform,
                spaceObjectController,
                _asteroidDelivery
            );
        }

        public void Execute()
        {
            _asteroidMove.Move();
            _asteroidStateOnScreen.StateCheck();
        }
        
    }
}
