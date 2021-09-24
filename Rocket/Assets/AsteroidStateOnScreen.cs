using Common.Scripts.MissionSystem;
using Common.Scripts.Satellite;
using UnityEngine;

public class AsteroidStateOnScreen : StateOnScreenPosition
{
    
    public AsteroidStateOnScreen(MeshCollider meshCollider,
        Transform transform,
        ISpaceObjectController spaceObjectController,
        GameLoopController gameLoopController) : base(meshCollider,transform,spaceObjectController,gameLoopController)
    {
        
    }
}