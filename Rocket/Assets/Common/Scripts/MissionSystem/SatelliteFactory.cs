using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteFactory: SpaceObjectFactory
    {
        public SatelliteFactory(RocketMovementController rocketMovementController, 
            ObjectPoolStorage objectPoolStorage,GameObject prefab) : base(rocketMovementController, objectPoolStorage,prefab)
        {
            
        }
 

    }
}