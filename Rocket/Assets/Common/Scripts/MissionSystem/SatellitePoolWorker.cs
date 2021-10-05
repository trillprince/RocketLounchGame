using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatellitePoolWorker: SpaceObjectPoolWorker
    {
        public SatellitePoolWorker(RocketMovementController rocketMovementController, 
            ObjectPoolStorage objectPoolStorage,GameObject prefab) : base(rocketMovementController, objectPoolStorage,prefab)
        {
            
        }
 

    }
}