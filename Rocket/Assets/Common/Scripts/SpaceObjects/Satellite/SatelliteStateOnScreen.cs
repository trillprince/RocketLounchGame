using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteStateOnScreen: StateOnScreenPosition
    {
        private StateOnScreen _finalStateOnScreen;
        private StateOnScreen _currentStateOnScreen;
        public bool CargoDelivered { get; private set; } = false;
        private readonly SatelliteColor _satelliteColor;

        public SatelliteStateOnScreen(MeshCollider meshCollider,
            Transform transform,
            SatelliteColor satelliteColor,
            ISpaceObjectController spaceObjectController,
            GameLoopController gameLoopController,Dictionary<StateOnScreen,Action> actionsOnStates = null): base(meshCollider,transform,spaceObjectController,gameLoopController,actionsOnStates)
        {
            _satelliteColor = satelliteColor;
        }

        public override void StateCheck()
        {
            base.StateCheck();
        }
    }
}