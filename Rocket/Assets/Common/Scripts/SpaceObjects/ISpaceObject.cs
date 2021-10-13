using System;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.SpaceObjects
{
    public interface ISpaceObject: IUpdatable
    {
        public void Constructor(RocketController rocketController, ISpaceObjectController spaceObjectController,
            GameLoopController gameLoopController, GameStateController gameStateController, ISpawnPosition spawnPosition);

        public Vector3 GetSpawnPosition();
    
        public GameObject GetGameObject();

        public Transform GetTransform();
    }

    public enum StateOnScreen
    {
        UpperRed,
        Yellow,
        Green,
        LowerRed,
        DisposeZone
    }
}